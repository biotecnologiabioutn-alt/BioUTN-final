using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BioUTN.ApiConsumer;
using BioUTN.Modelos;
using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BioUTN.MVC.Controllers
{
    [Authorize(Roles = "Coordinador, Tecnico, Docente, Tesista, Estudiante")]
    public class PlantaMadresController : Controller
    {
        // GET: PlantaMadres
        public IActionResult Index()
        {
            try
            {
                var list = Crud<PlantaMadre>.GetAll();
                
                var especies = Crud<Especie>.GetAll();
                var proyectos = Crud<Proyecto>.GetAll();
                var usuarios = Crud<Usuario>.GetAll();

                foreach(var item in list) {
                    item.Especie = Enumerable.FirstOrDefault(especies, e => e.Id == item.IdEspecie);
                    item.Proyecto = Enumerable.FirstOrDefault(proyectos, p => p.Id == item.IdProyecto);
                    item.Usuario = Enumerable.FirstOrDefault(usuarios, u => u.Id == item.IdUsuario);
                }
                
                return View(list);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar: " + ex.Message;
                return View(new List<PlantaMadre>());
            }
        }

        // GET: PlantaMadres/Details/5
        public IActionResult Details(int id)
        {
            try
            {
                var item = Crud<PlantaMadre>.GetById(id);
                if (item == null) return NotFound();

                if (item.IdEspecie > 0)
                    item.Especie = Crud<Especie>.GetById(item.IdEspecie);
                if (item.IdProyecto > 0)
                    item.Proyecto = Crud<Proyecto>.GetById(item.IdProyecto);
                if (item.IdUsuario > 0)
                    item.Usuario = Crud<Usuario>.GetById(item.IdUsuario);
                if (item.IdResponsableColecta != null)
                    item.ResponsableColecta = Crud<Usuario>.GetById(item.IdResponsableColecta.Value);
                if (item.IdDocumentoPermiso != null)
                    item.DocumentoPermiso = Crud<Documento>.GetById(item.IdDocumentoPermiso.Value);
                if (item.IdResponsableIntroduccion != null)
                    item.ResponsableIntroduccion = Crud<Usuario>.GetById(item.IdResponsableIntroduccion.Value);
                if (item.IdProtocolo != null)
                    item.Protocolo = Crud<Protocolo>.GetById(item.IdProtocolo.Value);

                return View(item);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar los detalles: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: PlantaMadres/Ficha/5
        public IActionResult Ficha(int id)
        {
            try
            {
                // El CRUD de PlantaMadre ya hace Include de Especie y Proyecto
                var item = Crud<PlantaMadre>.GetById(id);
                if (item == null) return NotFound();

                // Necesitamos el Tipo de Planta y la Taxonomía para el diseño de la Ficha
                if (item.Especie != null)
                {
                    item.Especie.Taxonomia = Crud<Taxonomia>.GetById(item.Especie.IdTaxonomia);
                    item.Especie.TipoPlanta = Crud<TipoPlanta>.GetById(item.Especie.IdTipoPlanta);
                }

                // Necesitamos los datos del Director y Tesista del Proyecto
                if (item.Proyecto != null)
                {
                    if (item.Proyecto.IdDirector.HasValue)
                        item.Proyecto.Director = Crud<Usuario>.GetById(item.Proyecto.IdDirector.Value);
                    
                    if (item.Proyecto.IdTesista.HasValue)
                        item.Proyecto.Tesista = Crud<Usuario>.GetById(item.Proyecto.IdTesista.Value);
                }

                return View(item);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar la ficha: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: PlantaMadres/Create
        public IActionResult Create()
        {
            var loggedUser = Crud<Usuario>.GetAll().FirstOrDefault(u => u.Email == User.Identity?.Name) 
                             ?? Crud<Usuario>.GetAll().FirstOrDefault();
            
            ViewBag.LoggedUserId = loggedUser?.Id;
            ViewBag.LoggedUserNombre = loggedUser?.Nombres + " " + loggedUser?.Apellidos;

            var todosUsuarios = Crud<Usuario>.GetAll();
            foreach(var u in todosUsuarios) {
                if(u.IdRol > 0 && u.Rol == null) u.Rol = Crud<Rol>.GetById(u.IdRol);
            }
            var usuariosSinCoordinador = todosUsuarios.Where(u => u.Rol == null || !u.Rol.NombreRol.Contains("Coordinador", StringComparison.OrdinalIgnoreCase)).ToList();

            ViewBag.Especies = new SelectList(Crud<Especie>.GetAll(), "Id", "NombreCientifico");
            ViewBag.Proyectos = new SelectList(Crud<Proyecto>.GetAll(), "Id", "NombreProyecto");
            ViewBag.Usuarios = new SelectList(todosUsuarios, "Id", "Nombres");
            ViewBag.UsuariosColecta = new SelectList(usuariosSinCoordinador, "Id", "NombreCompletoConRol");
            ViewBag.UsuariosIntroduccion = new SelectList(usuariosSinCoordinador, "Id", "NombreCompletoConRol");
            var todosDocumentos = Crud<Documento>.GetAll();
            foreach(var d in todosDocumentos) { if(d.CategoriaId > 0 && d.Categoria == null) d.Categoria = Crud<CategoriaDocumento>.GetById(d.CategoriaId); }
            var permisos = todosDocumentos.Where(d => d.Categoria != null && d.Categoria.Nombre.Contains("Permiso", StringComparison.OrdinalIgnoreCase)).ToList();

            ViewBag.Documentos = new SelectList(permisos, "Id", "Titulo");
            ViewBag.Protocolos = new SelectList(Crud<Protocolo>.GetAll(), "Id", "Titulo");
            return View();
        }

        // POST: PlantaMadres/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PlantaMadre item, IFormFile? fotoFisica)
        {
            ModelState.Remove("CodigoAsignado"); // Se generará automáticamente
            ModelState.Remove("Especie");
            ModelState.Remove("Proyecto");
            ModelState.Remove("Usuario");
            ModelState.Remove("ResponsableIntroduccion");
            ModelState.Remove("ResponsableColecta");
            ModelState.Remove("DocumentoPermiso");
            ModelState.Remove("Protocolo");
            
            if (ModelState.IsValid)
            {
                try
                {
                    item.FechaRecepcion = item.FechaRecepcion.ToUniversalTime();
                    item.FechaColecta = item.FechaColecta?.ToUniversalTime();
                    
                    var contadorEspecie = Crud<PlantaMadre>.GetAll().Count(p => p.IdEspecie == item.IdEspecie);
                    item.CodigoAsignado = $"PM-{contadorEspecie + 1}";
                    
                    if (fotoFisica != null && fotoFisica.Length > 0)
                    {
                        string folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "plantas");
                        if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(fotoFisica.FileName);
                        string filePath = Path.Combine(folder, fileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await fotoFisica.CopyToAsync(stream);
                        }
                        item.UrlFotografia = "/images/plantas/" + fileName;
                    }

                    bool isSuccess = Crud<PlantaMadre>.Create(item);
                    if (isSuccess)
                    {
                        TempData["Success"] = "Elemento creado correctamente.";
                        if (item.IdProyecto > 0)
                        {
                            return RedirectToAction("Details", "Proyectos", new { id = item.IdProyecto });
                        }
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["Error"] = "Hubo un problema al guardar la Planta Madre en la base de datos (Error 400/500). Verifica los datos.";
                    }
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Error al crear: " + ex.Message;
                }
            }
            var todosUsuarios = Crud<Usuario>.GetAll();
            foreach(var u in todosUsuarios) {
                if(u.IdRol > 0 && u.Rol == null) u.Rol = Crud<Rol>.GetById(u.IdRol);
            }
            var usuariosSinCoordinador = todosUsuarios.Where(u => u.Rol == null || !u.Rol.NombreRol.Contains("Coordinador", StringComparison.OrdinalIgnoreCase)).ToList();

            ViewBag.Especies = new SelectList(Crud<Especie>.GetAll(), "Id", "NombreCientifico", item.IdEspecie);
            ViewBag.Proyectos = new SelectList(Crud<Proyecto>.GetAll(), "Id", "NombreProyecto", item.IdProyecto);
            ViewBag.Usuarios = new SelectList(todosUsuarios, "Id", "Nombres", item.IdUsuario);
            ViewBag.UsuariosColecta = new SelectList(usuariosSinCoordinador, "Id", "NombreCompletoConRol", item.IdResponsableColecta);
            ViewBag.UsuariosIntroduccion = new SelectList(usuariosSinCoordinador, "Id", "NombreCompletoConRol", item.IdResponsableIntroduccion);
            var todosDocumentos = Crud<Documento>.GetAll();
            foreach(var d in todosDocumentos) { if(d.CategoriaId > 0 && d.Categoria == null) d.Categoria = Crud<CategoriaDocumento>.GetById(d.CategoriaId); }
            var permisos = todosDocumentos.Where(d => d.Categoria != null && d.Categoria.Nombre.Contains("Permiso", StringComparison.OrdinalIgnoreCase)).ToList();

            ViewBag.Documentos = new SelectList(permisos, "Id", "Titulo", item.IdDocumentoPermiso);
            ViewBag.Protocolos = new SelectList(Crud<Protocolo>.GetAll(), "Id", "Titulo", item.IdProtocolo);
            return View(item);
        }

        // GET: PlantaMadres/Edit/5
        public IActionResult Edit(int id)
        {
            try
            {
                var item = Crud<PlantaMadre>.GetById(id);
                if (item == null) return NotFound();

                if(item.IdUsuario > 0)
                {
                    var user = Crud<Usuario>.GetById(item.IdUsuario);
                    ViewBag.LoggedUserNombre = user?.Nombres + " " + user?.Apellidos;
                }

                var todosUsuarios = Crud<Usuario>.GetAll();
                foreach(var u in todosUsuarios) {
                    if(u.IdRol > 0 && u.Rol == null) u.Rol = Crud<Rol>.GetById(u.IdRol);
                }
                var usuariosSinCoordinador = todosUsuarios.Where(u => u.Rol == null || !u.Rol.NombreRol.Contains("Coordinador", StringComparison.OrdinalIgnoreCase)).ToList();

                ViewBag.Especies = new SelectList(Crud<Especie>.GetAll(), "Id", "NombreCientifico", item.IdEspecie);
                ViewBag.Proyectos = new SelectList(Crud<Proyecto>.GetAll(), "Id", "NombreProyecto", item.IdProyecto);
                ViewBag.Usuarios = new SelectList(todosUsuarios, "Id", "Nombres", item.IdUsuario);
                ViewBag.UsuariosColecta = new SelectList(usuariosSinCoordinador, "Id", "NombreCompletoConRol", item.IdResponsableColecta);
                ViewBag.UsuariosIntroduccion = new SelectList(usuariosSinCoordinador, "Id", "NombreCompletoConRol", item.IdResponsableIntroduccion);
                var todosDocumentos = Crud<Documento>.GetAll();
                foreach(var d in todosDocumentos) { if(d.CategoriaId > 0 && d.Categoria == null) d.Categoria = Crud<CategoriaDocumento>.GetById(d.CategoriaId); }
                var permisos = todosDocumentos.Where(d => d.Categoria != null && d.Categoria.Nombre.Contains("Permiso", StringComparison.OrdinalIgnoreCase)).ToList();

                ViewBag.Documentos = new SelectList(permisos, "Id", "Titulo", item.IdDocumentoPermiso);
                ViewBag.Protocolos = new SelectList(Crud<Protocolo>.GetAll(), "Id", "Titulo", item.IdProtocolo);
                return View(item);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: PlantaMadres/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PlantaMadre item, IFormFile? fotoFisica)
        {
            if (id != item.Id) return BadRequest();
            ModelState.Remove("Especie");
            ModelState.Remove("Proyecto");
            ModelState.Remove("Usuario");
            ModelState.Remove("ResponsableIntroduccion");
            ModelState.Remove("ResponsableColecta");
            ModelState.Remove("DocumentoPermiso");
            ModelState.Remove("Protocolo");

            if (ModelState.IsValid)
            {
                try
                {
                    item.FechaRecepcion = item.FechaRecepcion.ToUniversalTime();
                    item.FechaColecta = item.FechaColecta?.ToUniversalTime();

                    if (fotoFisica != null && fotoFisica.Length > 0)
                    {
                        string folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "plantas");
                        if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(fotoFisica.FileName);
                        string filePath = Path.Combine(folder, fileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await fotoFisica.CopyToAsync(stream);
                        }
                        item.UrlFotografia = "/images/plantas/" + fileName;
                    }

                    Crud<PlantaMadre>.Update(id, item);
                    TempData["Success"] = "Elemento actualizado correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                     TempData["Error"] = "Error al actualizar: " + ex.Message;
                }
            }
            var todosUsuarios = Crud<Usuario>.GetAll();
            foreach(var u in todosUsuarios) {
                if(u.IdRol > 0 && u.Rol == null) u.Rol = Crud<Rol>.GetById(u.IdRol);
            }
            var usuariosSinCoordinador = todosUsuarios.Where(u => u.Rol == null || !u.Rol.NombreRol.Contains("Coordinador", StringComparison.OrdinalIgnoreCase)).ToList();

            ViewBag.Especies = new SelectList(Crud<Especie>.GetAll(), "Id", "NombreCientifico", item.IdEspecie);
            ViewBag.Proyectos = new SelectList(Crud<Proyecto>.GetAll(), "Id", "NombreProyecto", item.IdProyecto);
            ViewBag.Usuarios = new SelectList(todosUsuarios, "Id", "Nombres", item.IdUsuario);
            ViewBag.UsuariosColecta = new SelectList(usuariosSinCoordinador, "Id", "NombreCompletoConRol", item.IdResponsableColecta);
            ViewBag.UsuariosIntroduccion = new SelectList(usuariosSinCoordinador, "Id", "NombreCompletoConRol", item.IdResponsableIntroduccion);
            var todosDocumentos = Crud<Documento>.GetAll();
            foreach(var d in todosDocumentos) { if(d.CategoriaId > 0 && d.Categoria == null) d.Categoria = Crud<CategoriaDocumento>.GetById(d.CategoriaId); }
            var permisos = todosDocumentos.Where(d => d.Categoria != null && d.Categoria.Nombre.Contains("Permiso", StringComparison.OrdinalIgnoreCase)).ToList();
            ViewBag.Documentos = new SelectList(permisos, "Id", "Titulo", item.IdDocumentoPermiso);
            ViewBag.Protocolos = new SelectList(Crud<Protocolo>.GetAll(), "Id", "Titulo", item.IdProtocolo);
            return View(item);
        }

        // GET: PlantaMadres/Delete/5
        public IActionResult Delete(int id)
        {
             try
            {
                var item = Crud<PlantaMadre>.GetById(id);
                if (item == null) return NotFound();

                return View(item);
            }
            catch(Exception ex)
            {
                TempData["Error"] = "Error al cargar: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: PlantaMadres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                Crud<PlantaMadre>.Delete(id);
                TempData["Success"] = "Elemento eliminado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "No se puede eliminar (posiblemente en uso): " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: PlantaMadres/TrazabilidadIndex
        public IActionResult TrazabilidadIndex()
        {
            try
            {
                var list = Crud<PlantaMadre>.GetAll();
                return View(list);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar: " + ex.Message;
                return View(new List<PlantaMadre>());
            }
        }

        // GET: PlantaMadres/TrazabilidadArbol/5
        public IActionResult TrazabilidadArbol(int id)
        {
            try
            {
                var planta = Crud<PlantaMadre>.GetById(id);
                if (planta == null) return NotFound();

                var lotes = Crud<LoteCultivo>.GetAll().Where(l => l.IdPlantaMadre == id).ToList();
                var frascos = Crud<UnidadFrasco>.GetAll();

                ViewBag.Lotes = lotes;
                ViewBag.Frascos = frascos;

                return View(planta);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar el árbol: " + ex.Message;
                return RedirectToAction(nameof(TrazabilidadIndex));
            }
        }

        // POST: PlantaMadres/CrearProyectoAjax
        [HttpPost]
        public IActionResult CrearProyectoAjax(string nombre, int idUsuario)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nombre)) return BadRequest("El nombre del proyecto es requerido.");
                if (idUsuario <= 0) return BadRequest("Debe seleccionar un técnico responsable en el formulario primero.");
                
                var nuevo = new Proyecto { 
                    NombreProyecto = nombre.Trim(),
                    Descripcion = "Proyecto creado automáticamente desde el registro de Planta Madre.",
                    TipoProyecto = "Investigación",
                    Estado = "Activo",
                    FechaInicio = DateTime.Now,
                    IdUsuarioResponsable = idUsuario
                };
                
                bool exito = Crud<Proyecto>.Create(nuevo);
                
                if (exito)
                {
                    var lista = Crud<Proyecto>.GetAll();
                    return Json(new { success = true, data = lista });
                }
                return BadRequest("No se pudo crear el proyecto en la base de datos.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}



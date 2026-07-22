using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BioUTN.ApiConsumer;
using BioUTN.Modelos;
using System;
using System.Collections.Generic;

namespace BioUTN.MVC.Controllers
{
    [Authorize(Roles = "Coordinador, Tecnico, Docente, Tesista, Estudiante")]
    public class ProyectosController : Controller
    {
        // GET: Proyectos
        public IActionResult Index()
        {
            try
            {
                var list = Crud<Proyecto>.GetAll();
                
                if (User.IsInRole("Docente") || User.IsInRole("Estudiante"))
                {
                    var userName = User.Identity?.Name ?? "";
                    var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                    int.TryParse(userIdClaim, out int userId);

                    list = list.Where(p => 
                        (p.DirectoresNombres != null && p.DirectoresNombres.Contains(userName)) ||
                        (p.EstudiantesNombres != null && p.EstudiantesNombres.Contains(userName)) ||
                        p.IdTesista == userId ||
                        p.IdUsuarioResponsable == userId
                    ).ToArray();
                }
                
                return View(list);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar: " + ex.Message;
                return View(new List<Proyecto>());
            }
        }

        // GET: Proyectos/Create
        public IActionResult Create(string tipo = "Investigacion")
        {
            try
            {
                var tipoSeguro = string.IsNullOrEmpty(tipo) ? "Investigacion" : tipo;
                
                var todosUsuarios = Crud<Usuario>.GetAll().Where(u => u.Activo).ToList();
                var usuarios = todosUsuarios.Where(u => u.Rol != null && (u.Rol.NombreRol == "Estudiante" || u.Rol.NombreRol == "Tecnico")).ToList();
                var directores = todosUsuarios.Where(u => u.Rol != null && u.Rol.NombreRol == "Docente").ToList();
                var tesistas = todosUsuarios.Where(u => u.Rol != null && u.Rol.NombreRol == "Estudiante").ToList();
                
                ViewBag.Usuarios = new SelectList(usuarios, "Id", "NombreCompletoConRol");
                ViewBag.Directores = new SelectList(directores, "Id", "NombreCompletoConRol");
                ViewBag.Tesistas = new SelectList(tesistas, "Id", "NombreCompletoConRol");
                ViewBag.Especies = new SelectList(Crud<Especie>.GetAll(), "Id", "NombreCientifico");
                
                var todosTipos = Crud<TipoProyecto>.GetAll() ?? Array.Empty<TipoProyecto>();
                ViewBag.TiposProyecto = new SelectList(todosTipos, "Id", "Nombre");
                
                var tipoId = todosTipos.FirstOrDefault(t => t.Nombre != null && t.Nombre.Contains(tipoSeguro, StringComparison.OrdinalIgnoreCase))?.Id;
                ViewBag.IdTipoProyectoSeleccionado = tipoId;
                
                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar el formulario de creación: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Proyectos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Proyecto item)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    item.FechaInicio = item.FechaInicio.ToUniversalTime();
                    if (item.FechaFin.HasValue) item.FechaFin = item.FechaFin.Value.ToUniversalTime();

                    Crud<Proyecto>.Create(item);
                    TempData["Success"] = "Elemento creado correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Error al crear: " + ex.Message;
                }
            }
            var todosUsuarios = Crud<Usuario>.GetAll().Where(u => u.Activo).ToList();
            var usuarios = todosUsuarios.Where(u => u.Rol?.NombreRol == "Estudiante" || u.Rol?.NombreRol == "Tecnico").ToList();
            var directores = todosUsuarios.Where(u => u.Rol?.NombreRol == "Docente").ToList();
            var tesistas = todosUsuarios.Where(u => u.Rol?.NombreRol == "Estudiante").ToList();

            ViewBag.Usuarios = new SelectList(usuarios, "Id", "NombreCompletoConRol", item.IdUsuarioResponsable);
            ViewBag.Directores = new SelectList(directores, "Id", "NombreCompletoConRol", item.IdDirector);
            ViewBag.Tesistas = new SelectList(tesistas, "Id", "NombreCompletoConRol", item.IdTesista);
            ViewBag.Especies = new SelectList(Crud<Especie>.GetAll(), "Id", "NombreCientifico", item.IdEspecie);
            ViewBag.TiposProyecto = new SelectList(Crud<TipoProyecto>.GetAll(), "Id", "Nombre", item.IdTipoProyecto);
            return View(item);
        }

        // GET: Proyectos/Edit/5
        public IActionResult Edit(int id)
        {
            try
            {
                var item = Crud<Proyecto>.GetById(id);
                if (item == null) return NotFound();

                var todosUsuarios = Crud<Usuario>.GetAll().Where(u => u.Activo).ToList();
                var usuarios = todosUsuarios.Where(u => u.Rol?.NombreRol == "Estudiante" || u.Rol?.NombreRol == "Tecnico").ToList();
                var directores = todosUsuarios.Where(u => u.Rol?.NombreRol == "Docente").ToList();
                var tesistas = todosUsuarios.Where(u => u.Rol?.NombreRol == "Estudiante").ToList();

                ViewBag.Usuarios = new SelectList(usuarios, "Id", "NombreCompletoConRol", item.IdUsuarioResponsable);
                ViewBag.Directores = new SelectList(directores, "Id", "NombreCompletoConRol", item.IdDirector);
                ViewBag.Tesistas = new SelectList(tesistas, "Id", "NombreCompletoConRol", item.IdTesista);
                ViewBag.Especies = new SelectList(Crud<Especie>.GetAll(), "Id", "NombreCientifico", item.IdEspecie);
            ViewBag.TiposProyecto = new SelectList(Crud<TipoProyecto>.GetAll(), "Id", "Nombre", item.IdTipoProyecto);
                ViewBag.TiposProyecto = new SelectList(Crud<TipoProyecto>.GetAll(), "Id", "Nombre", item.IdTipoProyecto);

                return View(item);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Proyectos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Proyecto item)
        {
            if (id != item.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    item.FechaInicio = item.FechaInicio.ToUniversalTime();
                    if (item.FechaFin.HasValue) item.FechaFin = item.FechaFin.Value.ToUniversalTime();

                    Crud<Proyecto>.Update(id, item);
                    TempData["Success"] = "Elemento actualizado correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                     TempData["Error"] = "Error al actualizar: " + ex.Message;
                }
            }
            var todosUsuarios = Crud<Usuario>.GetAll().Where(u => u.Activo).ToList();
            var usuarios = todosUsuarios.Where(u => u.Rol?.NombreRol == "Estudiante" || u.Rol?.NombreRol == "Tecnico").ToList();
            var directores = todosUsuarios.Where(u => u.Rol?.NombreRol == "Docente").ToList();
            var tesistas = todosUsuarios.Where(u => u.Rol?.NombreRol == "Estudiante").ToList();

            ViewBag.Usuarios = new SelectList(usuarios, "Id", "NombreCompletoConRol", item.IdUsuarioResponsable);
            ViewBag.Directores = new SelectList(directores, "Id", "NombreCompletoConRol", item.IdDirector);
            ViewBag.Tesistas = new SelectList(tesistas, "Id", "NombreCompletoConRol", item.IdTesista);
            ViewBag.Especies = new SelectList(Crud<Especie>.GetAll(), "Id", "NombreCientifico", item.IdEspecie);
            ViewBag.TiposProyecto = new SelectList(Crud<TipoProyecto>.GetAll(), "Id", "Nombre", item.IdTipoProyecto);
            return View(item);
        }

        // GET: Proyectos/Details/5
        public IActionResult Details(int id)
        {
            try
            {
                var item = Crud<Proyecto>.GetById(id);
                if (item == null) return NotFound();

                if (item.IdEspecie != null && item.IdEspecie > 0)
                {
                    item.Especie = Crud<Especie>.GetById(item.IdEspecie.Value);
                }

                // Cargar Planta Madres registradas para este proyecto
                var plantasProyecto = Crud<PlantaMadre>.GetAll().Where(p => p.IdProyecto == id).ToList();
                ViewBag.PlantasMadres = plantasProyecto;

                // ViewBags needed for the embedded Planta Madre Create form
                var loggedUser = Crud<Usuario>.GetAll().FirstOrDefault(u => u.Email == User.Identity?.Name) 
                                 ?? Crud<Usuario>.GetAll().FirstOrDefault();
                ViewBag.LoggedUserId = loggedUser?.Id;
                
                var usuariosRoles = Crud<Usuario>.GetAll().Where(u => u.Activo).ToList();
                foreach(var u in usuariosRoles) {
                    if (u.IdRol > 0) u.Rol = Crud<Rol>.GetById(u.IdRol);
                }

                ViewBag.Especies = new SelectList(Crud<Especie>.GetAll(), "Id", "NombreCientifico");
                var usuariosSinCoordinador = usuariosRoles.Where(u => u.Rol == null || !u.Rol.NombreRol.Contains("Coordinador", StringComparison.OrdinalIgnoreCase)).ToList();
                ViewBag.UsuariosColecta = new SelectList(usuariosSinCoordinador, "Id", "NombreCompletoConRol");
                ViewBag.UsuariosIntroduccion = new SelectList(usuariosSinCoordinador, "Id", "NombreCompletoConRol");
                var todosDocumentos = Crud<Documento>.GetAll();
                foreach(var d in todosDocumentos) { if(d.CategoriaId > 0 && d.Categoria == null) d.Categoria = Crud<CategoriaDocumento>.GetById(d.CategoriaId); }
                var permisos = todosDocumentos.Where(d => d.Categoria != null && d.Categoria.Nombre.Contains("Permiso", StringComparison.OrdinalIgnoreCase)).ToList();

                ViewBag.Documentos = new SelectList(permisos, "Id", "Titulo");
                ViewBag.Protocolos = new SelectList(Crud<Protocolo>.GetAll(), "Id", "Titulo");

                return View(item);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Proyectos/Delete/5
        public IActionResult Delete(int id)
        {
             try
            {
                var item = Crud<Proyecto>.GetById(id);
                if (item == null) return NotFound();

                return View(item);
            }
            catch(Exception ex)
            {
                TempData["Error"] = "Error al cargar: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Proyectos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                var item = Crud<Proyecto>.GetById(id);
                if (item != null)
                {
                    item.Estado = "Suspendido";
                    Crud<Proyecto>.Update(id, item);
                }
                TempData["Success"] = "Proyecto suspendido correctamente (Soft Delete).";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "No se puede eliminar (posiblemente en uso): " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }
        // POST: Proyectos/ConfirmarPlantasMadres/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmarPlantasMadres(int id)
        {
            try
            {
                var item = Crud<Proyecto>.GetById(id);
                if (item != null)
                {
                    item.PlantasMadresConfirmadas = true;
                    Crud<Proyecto>.Update(id, item);
                    TempData["Success"] = "Fase de Plantas Madres confirmada. Ya no se pueden añadir más.";
                }
                return RedirectToAction(nameof(Details), new { id });
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al confirmar: " + ex.Message;
                return RedirectToAction(nameof(Details), new { id });
            }
        }
    }
}




using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BioUTN.ApiConsumer;
using BioUTN.Modelos;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace BioUTN.MVC.Controllers
{
    [Authorize(Roles = "Coordinador, Tecnico, Docente, Tesista, Estudiante")]
    public class LoteCultivosController : Controller
    {
        // GET: LoteCultivos
        public IActionResult Index()
        {
            try
            {
                var list = Crud<LoteCultivo>.GetAll();
                return View(list);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar: " + ex.Message;
                return View(new List<LoteCultivo>());
            }
        }

        // GET: LoteCultivos/Details/5
        public IActionResult Details(int id)
        {
            try
            {
                var item = Crud<LoteCultivo>.GetById(id);
                if (item == null) return NotFound();

                if (item.IdPlantaMadre > 0)
                    item.PlantaMadre = Crud<PlantaMadre>.GetById(item.IdPlantaMadre);
                if (item.IdFaseCultivo > 0)
                    item.FaseCultivo = Crud<FaseCultivo>.GetById(item.IdFaseCultivo);
                if (item.IdMedioCultivo > 0)
                    item.MedioCultivo = Crud<MedioCultivo>.GetById(item.IdMedioCultivo);
                if (item.IdProyecto > 0)
                    item.Proyecto = Crud<Proyecto>.GetById(item.IdProyecto);
                if (item.IdUbicacionFisica > 0)
                    item.UbicacionFisica = Crud<UbicacionFisica>.GetById(item.IdUbicacionFisica);
                if (item.IdUsuario > 0)
                    item.Usuario = Crud<Usuario>.GetById(item.IdUsuario);
                if (item.IdDocumentoProtocolo != null)
                    item.DocumentoProtocolo = Crud<Documento>.GetById(item.IdDocumentoProtocolo.Value);
                if (item.IdUnidadFrascoOrigen != null)
                    item.UnidadFrascoOrigen = Crud<UnidadFrasco>.GetById(item.IdUnidadFrascoOrigen.Value);

                return View(item);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar los detalles: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: LoteCultivos/Create
        public IActionResult Create()
        {
            ViewBag.PlantaMadres = new SelectList(Crud<PlantaMadre>.GetAll(), "Id", "CodigoAsignado");
            ViewBag.FasesCultivo = new SelectList(Crud<FaseCultivo>.GetAll(), "Id", "NombreFase");
            ViewBag.MediosCultivo = new SelectList(Crud<MedioCultivo>.GetAll(), "Id", "Nombre");
            ViewBag.Proyectos = new SelectList(Crud<Proyecto>.GetAll(), "Id", "NombreProyecto");
            ViewBag.UbicacionesFisicas = new SelectList(Crud<UbicacionFisica>.GetAll(), "Id", "CodigoAnaquel");
            ViewBag.Usuarios = new SelectList(Crud<Usuario>.GetAll(), "Id", "Nombres");
            ViewBag.Documentos = new SelectList(Crud<Documento>.GetAll(), "Id", "Titulo");
            ViewBag.FrascosOrigen = new SelectList(new List<UnidadFrasco>(), "Id", "CodigoUnidad");
            return View();
        }

        // POST: LoteCultivos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(LoteCultivo item)
        {
            // Remover el requerimiento ya que se auto-generará
            ModelState.Remove("CodigoTrazabilidad");

            if (ModelState.IsValid)
            {
                try
                {
                    // 1. Obtener entidades relacionadas para el código
                    var planta = Crud<PlantaMadre>.GetById(item.IdPlantaMadre);
                    var especie = (planta != null) ? Crud<Especie>.GetById(planta.IdEspecie) : null;
                    var medio = Crud<MedioCultivo>.GetById(item.IdMedioCultivo);
                    var usuario = Crud<Usuario>.GetById(item.IdUsuario);

                    // 2. Determinar Número de Lote (Conteo de lotes de la planta madre)
                    int countLotes = Crud<LoteCultivo>.GetAll().Count(l => l.IdPlantaMadre == item.IdPlantaMadre);
                    int numLote = countLotes + 1;

                    // 3. Iniciales del Usuario
                    string usrIni = "XX";
                    if (usuario != null && !string.IsNullOrWhiteSpace(usuario.Nombres))
                    {
                        var nParts = usuario.Nombres.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                        var aParts = (usuario.Apellidos ?? "").Split(' ', StringSplitOptions.RemoveEmptyEntries);
                        string iN = nParts.Length > 0 ? nParts[0].Substring(0, 1) : "";
                        string iA = aParts.Length > 0 ? aParts[0].Substring(0, 1) : "";
                        usrIni = (iN + iA).ToUpper();
                        if (string.IsNullOrEmpty(usrIni)) usrIni = "XX";
                    }

                    // 4. Construir Código Base del Lote
                    string espCode = especie?.CodigoEstricto ?? "UNK";
                    string pmCode = planta?.CodigoAsignado ?? "PM0";
                    string medCode = medio?.Siglas ?? "MED";

                    item.CodigoTrazabilidad = $"{espCode}-{pmCode}-L{numLote}-R{item.NumeroRepique}-{medCode}";

                    Crud<LoteCultivo>.Create(item);
                    
                    // 5. Auto-generar frascos (unidades)
                    var loteDb = Crud<LoteCultivo>.GetAll().FirstOrDefault(l => l.CodigoTrazabilidad == item.CodigoTrazabilidad);
                    if (loteDb != null && item.TotalUnidades > 0)
                    {
                        string dateStr = item.FechaSiembra.ToString("ddMMyyyy");
                        for (int i = 1; i <= item.TotalUnidades; i++)
                        {
                            var frasco = new UnidadFrasco
                            {
                                IdLoteCultivo = loteDb.Id,
                                // [Especie]-[PlantaMadre]-F[NumFrasco]-R[Repique]-[Medio]-[DDMMAAAA]-[Responsable]
                                CodigoUnidad = $"{espCode}-{pmCode}-F{i}-R{item.NumeroRepique}-{medCode}-{dateStr}-{usrIni}",
                                NumeroResiembra = item.NumeroRepique,
                                Estado = "Saludable",
                                Activo = true
                            };
                            Crud<UnidadFrasco>.Create(frasco);
                        }
                    }

                    TempData["Success"] = "Elemento creado con código: " + item.CodigoTrazabilidad;
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Error al crear: " + ex.Message;
                }
            }
            ViewBag.PlantaMadres = new SelectList(Crud<PlantaMadre>.GetAll(), "Id", "CodigoAsignado", item.IdPlantaMadre);
            ViewBag.FasesCultivo = new SelectList(Crud<FaseCultivo>.GetAll(), "Id", "NombreFase", item.IdFaseCultivo);
            ViewBag.MediosCultivo = new SelectList(Crud<MedioCultivo>.GetAll(), "Id", "Nombre", item.IdMedioCultivo);
            ViewBag.Proyectos = new SelectList(Crud<Proyecto>.GetAll(), "Id", "NombreProyecto", item.IdProyecto);
            ViewBag.UbicacionesFisicas = new SelectList(Crud<UbicacionFisica>.GetAll(), "Id", "CodigoAnaquel", item.IdUbicacionFisica);
            ViewBag.Usuarios = new SelectList(Crud<Usuario>.GetAll(), "Id", "Nombres", item.IdUsuario);
            ViewBag.Documentos = new SelectList(Crud<Documento>.GetAll(), "Id", "Titulo", item.IdDocumentoProtocolo);
            var frascosLote = item.IdLotePadre.HasValue ? Crud<UnidadFrasco>.GetAll().Where(f => f.IdLoteCultivo == item.IdLotePadre).ToList() : new List<UnidadFrasco>();
            ViewBag.FrascosOrigen = new SelectList(frascosLote, "Id", "CodigoUnidad", item.IdUnidadFrascoOrigen);
            return View(item);
        }

        // GET: LoteCultivos/Resiembra/5
        public IActionResult Resiembra(int id)
        {
            return RedirectToAction("Create", "Resiembras", new { idLoteOrigen = id });
        }

        // GET: LoteCultivos/Enraizamiento/5
        public IActionResult Enraizamiento(int id)
        {
            return RedirectToAction("Create", "Enraizamientos", new { idLoteOrigen = id });
        }

        // GET: LoteCultivos/Aclimatacion/5
        public IActionResult Aclimatacion(int id)
        {
            return RedirectToAction("Create", "Aclimataciones", new { idLoteOrigen = id });
        }

        private IActionResult PrepararFaseSubsecuente(int lotePadreId, string faseNombre)
        {
            try
            {
                var padre = Crud<LoteCultivo>.GetById(lotePadreId);
                if (padre == null) return NotFound("El lote de origen no existe.");

                var fases = Crud<FaseCultivo>.GetAll();
                var idFase = fases.FirstOrDefault(f => f.NombreFase.Contains(faseNombre, StringComparison.OrdinalIgnoreCase))?.Id ?? 0;

                var nuevoLote = new LoteCultivo
                {
                    IdLotePadre = padre.Id,
                    IdPlantaMadre = padre.IdPlantaMadre,
                    IdProyecto = padre.IdProyecto,
                    IdFaseCultivo = idFase,
                    NumeroRepique = padre.NumeroRepique + 1,
                    FechaSiembra = DateTime.Now
                };

                ViewBag.PlantaMadres = new SelectList(Crud<PlantaMadre>.GetAll(), "Id", "CodigoAsignado", nuevoLote.IdPlantaMadre);
                ViewBag.FasesCultivo = new SelectList(fases, "Id", "NombreFase", nuevoLote.IdFaseCultivo);
                ViewBag.MediosCultivo = new SelectList(Crud<MedioCultivo>.GetAll(), "Id", "Nombre");
                ViewBag.Proyectos = new SelectList(Crud<Proyecto>.GetAll(), "Id", "NombreProyecto", nuevoLote.IdProyecto);
                ViewBag.UbicacionesFisicas = new SelectList(Crud<UbicacionFisica>.GetAll(), "Id", "CodigoAnaquel");
                ViewBag.Usuarios = new SelectList(Crud<Usuario>.GetAll(), "Id", "Nombres");
                ViewBag.Documentos = new SelectList(Crud<Documento>.GetAll(), "Id", "Titulo");
                
                var frascosLote = Crud<UnidadFrasco>.GetAll().Where(f => f.IdLoteCultivo == padre.Id && f.Activo).ToList();
                ViewBag.FrascosOrigen = new SelectList(frascosLote, "Id", "CodigoUnidad");
                
                ViewBag.LotePadreInfo = padre.CodigoTrazabilidad; // Para pasarlo a la vista
                ViewBag.FaseNombre = faseNombre;
                
                // Usamos la misma vista Create, pero le pasamos el modelo pre-cargado
                return View("Create", nuevoLote);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al preparar fase: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: LoteCultivos/Edit/5
        public IActionResult Edit(int id)
        {
            try
            {
                var item = Crud<LoteCultivo>.GetById(id);
                if (item == null) return NotFound();

                ViewBag.PlantaMadres = new SelectList(Crud<PlantaMadre>.GetAll(), "Id", "CodigoAsignado", item.IdPlantaMadre);
                ViewBag.FasesCultivo = new SelectList(Crud<FaseCultivo>.GetAll(), "Id", "NombreFase", item.IdFaseCultivo);
                ViewBag.MediosCultivo = new SelectList(Crud<MedioCultivo>.GetAll(), "Id", "Descripcion", item.IdMedioCultivo);
                ViewBag.Proyectos = new SelectList(Crud<Proyecto>.GetAll(), "Id", "NombreProyecto", item.IdProyecto);
                ViewBag.UbicacionesFisicas = new SelectList(Crud<UbicacionFisica>.GetAll(), "Id", "CodigoAnaquel", item.IdUbicacionFisica);
                ViewBag.Usuarios = new SelectList(Crud<Usuario>.GetAll(), "Id", "Nombres", item.IdUsuario);
                ViewBag.Documentos = new SelectList(Crud<Documento>.GetAll(), "Id", "Titulo", item.IdDocumentoProtocolo);
                
                var frascos = item.IdLotePadre.HasValue ? 
                    Crud<UnidadFrasco>.GetAll().Where(f => f.IdLoteCultivo == item.IdLotePadre.Value).ToList() : 
                    new List<UnidadFrasco>();
                ViewBag.FrascosOrigen = new SelectList(frascos, "Id", "CodigoUnidad", item.IdUnidadFrascoOrigen);

                return View(item);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: LoteCultivos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, LoteCultivo item)
        {
            if (id != item.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    item.FechaSiembra = item.FechaSiembra.ToUniversalTime();
                    Crud<LoteCultivo>.Update(id, item);
                    TempData["Success"] = "Elemento actualizado correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error: " + ex.Message);
                }
            }

            ViewBag.PlantaMadres = new SelectList(Crud<PlantaMadre>.GetAll(), "Id", "CodigoAsignado", item.IdPlantaMadre);
            ViewBag.FasesCultivo = new SelectList(Crud<FaseCultivo>.GetAll(), "Id", "NombreFase", item.IdFaseCultivo);
            ViewBag.MediosCultivo = new SelectList(Crud<MedioCultivo>.GetAll(), "Id", "Descripcion", item.IdMedioCultivo);
            ViewBag.Proyectos = new SelectList(Crud<Proyecto>.GetAll(), "Id", "NombreProyecto", item.IdProyecto);
            ViewBag.UbicacionesFisicas = new SelectList(Crud<UbicacionFisica>.GetAll(), "Id", "CodigoAnaquel", item.IdUbicacionFisica);
            ViewBag.Usuarios = new SelectList(Crud<Usuario>.GetAll(), "Id", "Nombres", item.IdUsuario);
            ViewBag.Documentos = new SelectList(Crud<Documento>.GetAll(), "Id", "Titulo", item.IdDocumentoProtocolo);
            
            var frascos = item.IdLotePadre.HasValue ? 
                Crud<UnidadFrasco>.GetAll().Where(f => f.IdLoteCultivo == item.IdLotePadre.Value).ToList() : 
                new List<UnidadFrasco>();
            ViewBag.FrascosOrigen = new SelectList(frascos, "Id", "CodigoUnidad", item.IdUnidadFrascoOrigen);
            
            return View(item);
        }

        // GET: LoteCultivos/Delete/5
        public IActionResult Delete(int id)
        {
             try
            {
                var item = Crud<LoteCultivo>.GetById(id);
                if (item == null) return NotFound();

                return View(item);
            }
            catch(Exception ex)
            {
                TempData["Error"] = "Error al cargar: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: LoteCultivos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                Crud<LoteCultivo>.Delete(id);
                TempData["Success"] = "Elemento eliminado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "No se puede eliminar (posiblemente en uso): " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public IActionResult CrearMedioAjax(string nombre)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nombre)) return Json(new { success = false, message = "El nombre es requerido." });

                var nuevoMedio = new MedioCultivo { Descripcion = nombre, Siglas = nombre.Length > 10 ? nombre.Substring(0, 10) : nombre, Componentes = "No definidos" };
                bool isSuccess = Crud<MedioCultivo>.Create(nuevoMedio);

                if (isSuccess)
                {
                    var todos = Crud<MedioCultivo>.GetAll().OrderBy(m => m.Id).ToList();
                    var ultimos = todos.Select(m => new { id = m.Id, nombre = m.Descripcion }).ToList();
                    return Json(new { success = true, data = ultimos });
                }
                return Json(new { success = false, message = "Error al guardar en la base de datos." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult CrearFaseAjax(string nombre)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nombre)) return Json(new { success = false, message = "El nombre es requerido." });

                var nuevaFase = new FaseCultivo { NombreFase = nombre };
                bool isSuccess = Crud<FaseCultivo>.Create(nuevaFase);

                if (isSuccess)
                {
                    var todos = Crud<FaseCultivo>.GetAll().OrderBy(f => f.Id).ToList();
                    var ultimos = todos.Select(f => new { id = f.Id, nombre = f.NombreFase }).ToList();
                    return Json(new { success = true, data = ultimos });
                }
                return Json(new { success = false, message = "Error al guardar en la base de datos." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}

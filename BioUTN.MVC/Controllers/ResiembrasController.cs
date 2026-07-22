using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BioUTN.ApiConsumer;
using BioUTN.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BioUTN.MVC.Controllers
{
    [Authorize(Roles = "Coordinador, Tecnico, Docente, Tesista, Estudiante")]
    public class ResiembrasController : Controller
    {
        // GET: Resiembras
        public IActionResult Index()
        {
            try
            {
                var todos = Crud<LoteCultivo>.GetAll();
                // Una resiembra es un lote que tiene un padre
                var list = todos.Where(l => l.IdLotePadre != null).ToList();
                return View(list);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar: " + ex.Message;
                return View(new List<LoteCultivo>());
            }
        }

        // GET: Resiembras/Details/5
        public IActionResult Details(int id)
        {
            try
            {
                var item = Crud<LoteCultivo>.GetById(id);
                if (item == null || item.IdLotePadre == null) return NotFound();

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

        // GET: Resiembras/Create
        public IActionResult Create(int? idLoteOrigen)
        {
            try
            {
                LoteCultivo? loteOrigen = null;
                if (idLoteOrigen.HasValue)
                {
                    loteOrigen = Crud<LoteCultivo>.GetById(idLoteOrigen.Value);
                }

                var resiembra = new LoteCultivo
                {
                    IdLotePadre = idLoteOrigen,
                    FechaSiembra = DateTime.Now,
                    NumeroRepique = (loteOrigen?.NumeroRepique ?? 0) + 1,
                    IdProyecto = loteOrigen?.IdProyecto ?? 0,
                    IdPlantaMadre = loteOrigen?.IdPlantaMadre ?? 0,
                    IdMedioCultivo = loteOrigen?.IdMedioCultivo ?? 0,
                    IdUbicacionFisica = loteOrigen?.IdUbicacionFisica ?? 0,
                    ExplantesPorUnidad = 1
                };

                // Asignar Fase automáticamente si existe una que se llame "Multiplicación" o "Resiembra"
                var fases = Crud<FaseCultivo>.GetAll();
                var faseResiembra = fases.FirstOrDefault(f => f.NombreFase.Contains("Multiplicación") || f.NombreFase.Contains("Resiembra"));
                if (faseResiembra != null) resiembra.IdFaseCultivo = faseResiembra.Id;

                PrepararViewBags(resiembra);
                return View(resiembra);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al preparar formulario: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Resiembras/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(LoteCultivo item)
        {
            ModelState.Remove("CodigoTrazabilidad");

            if (ModelState.IsValid)
            {
                try
                {
                    var lotePadre = Crud<LoteCultivo>.GetById(item.IdLotePadre ?? 0);
                    item.IdPlantaMadre = lotePadre?.IdPlantaMadre ?? item.IdPlantaMadre;
                    item.IdProyecto = lotePadre?.IdProyecto ?? item.IdProyecto;

                    // Lógica similar de auto-generación que en LoteCultivos
                    var planta = Crud<PlantaMadre>.GetById(item.IdPlantaMadre);
                    var especie = (planta != null) ? Crud<Especie>.GetById(planta.IdEspecie) : null;
                    var medio = Crud<MedioCultivo>.GetById(item.IdMedioCultivo);
                    var usuario = Crud<Usuario>.GetById(item.IdUsuario);

                    int countLotes = Crud<LoteCultivo>.GetAll().Count(l => l.IdPlantaMadre == item.IdPlantaMadre);
                    int numLote = countLotes + 1;

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

                    string espCode = especie?.CodigoEstricto ?? "UNK";
                    string pmCode = planta?.CodigoAsignado ?? "PM0";
                    string medCode = medio?.Siglas ?? "MED";

                    item.CodigoTrazabilidad = $"{espCode}-{pmCode}-L{numLote}-R{item.NumeroRepique}-{medCode}";

                    Crud<LoteCultivo>.Create(item);

                    // Auto-generar frascos de la resiembra
                    var loteDb = Crud<LoteCultivo>.GetAll().FirstOrDefault(l => l.CodigoTrazabilidad == item.CodigoTrazabilidad);
                    if (loteDb != null && item.TotalUnidades > 0)
                    {
                        string dateStr = item.FechaSiembra.ToString("ddMMyyyy");
                        for (int i = 1; i <= item.TotalUnidades; i++)
                        {
                            var frasco = new UnidadFrasco
                            {
                                IdLoteCultivo = loteDb.Id,
                                CodigoUnidad = $"{espCode}-{pmCode}-F{i}-R{item.NumeroRepique}-{medCode}-{dateStr}-{usrIni}",
                                NumeroResiembra = item.NumeroRepique,
                                Estado = "Saludable",
                                Activo = true
                            };
                            Crud<UnidadFrasco>.Create(frasco);
                        }
                    }

                    TempData["Success"] = "Resiembra registrada correctamente con código: " + item.CodigoTrazabilidad;
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Error al crear: " + ex.Message;
                }
            }

            PrepararViewBags(item);
            return View(item);
        }

        // GET: Resiembras/Edit/5
        public IActionResult Edit(int id)
        {
            try
            {
                var item = Crud<LoteCultivo>.GetById(id);
                if (item == null || item.IdLotePadre == null) return NotFound();

                PrepararViewBags(item);
                return View(item);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Resiembras/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, LoteCultivo item)
        {
            if (id != item.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    Crud<LoteCultivo>.Update(id, item);
                    TempData["Success"] = "Resiembra actualizada correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Error al actualizar: " + ex.Message;
                }
            }
            PrepararViewBags(item);
            return View(item);
        }

        // POST: Resiembras/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                Crud<LoteCultivo>.Delete(id);
                TempData["Success"] = "Resiembra eliminada correctamente.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "No se puede eliminar: " + ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }

        // Helper para llenar combos
        private void PrepararViewBags(LoteCultivo item)
        {
            ViewBag.LotesOrigen = new SelectList(Crud<LoteCultivo>.GetAll(), "Id", "CodigoTrazabilidad", item.IdLotePadre);
            
            var frascosLote = item.IdLotePadre.HasValue ? Crud<UnidadFrasco>.GetAll().Where(f => f.IdLoteCultivo == item.IdLotePadre).ToList() : new List<UnidadFrasco>();
            ViewBag.FrascosOrigen = new SelectList(frascosLote, "Id", "CodigoUnidad", item.IdUnidadFrascoOrigen);

            ViewBag.Usuarios = new SelectList(Crud<Usuario>.GetAll().Where(u => u.Activo), "Id", "Nombres", item.IdUsuario);
            ViewBag.FasesCultivo = new SelectList(Crud<FaseCultivo>.GetAll(), "Id", "NombreFase", item.IdFaseCultivo);
            ViewBag.MediosCultivo = new SelectList(Crud<MedioCultivo>.GetAll(), "Id", "Siglas", item.IdMedioCultivo);
            ViewBag.UbicacionesFisicas = new SelectList(Crud<UbicacionFisica>.GetAll(), "Id", "CodigoAnaquel", item.IdUbicacionFisica);

            var todosDocumentos = Crud<Documento>.GetAll();
            foreach(var d in todosDocumentos) { if(d.CategoriaId > 0 && d.Categoria == null) d.Categoria = Crud<CategoriaDocumento>.GetById(d.CategoriaId); }
            var protocolos = todosDocumentos.Where(d => d.Categoria != null && d.Categoria.Nombre.Contains("Protocolo", StringComparison.OrdinalIgnoreCase)).ToList();
            ViewBag.Protocolos = new SelectList(protocolos, "Id", "Titulo", item.IdDocumentoProtocolo);
        }
    }
}

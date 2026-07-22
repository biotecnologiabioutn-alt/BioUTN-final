using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BioUTN.ApiConsumer;
using BioUTN.Modelos;
using BioUTN.Servicios;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BioUTN.MVC.Controllers
{
    [Authorize]
    public class BibliotecaController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IBlobStorageService _blobStorageService;

        public BibliotecaController(IWebHostEnvironment webHostEnvironment, IBlobStorageService blobStorageService)
        {
            _webHostEnvironment = webHostEnvironment;
            _blobStorageService = blobStorageService;
        }

        // GET: Biblioteca
        public IActionResult Index(string categoria = null, string busqueda = null)
        {
            try
            {
                var list = Crud<Documento>.GetAll().ToList();
                var categoriasResult = Crud<CategoriaDocumento>.GetAll();
                var categorias = categoriasResult != null ? categoriasResult.ToList() : new List<CategoriaDocumento>();

                ViewBag.CategoriasDinámicas = categorias;

                if (!string.IsNullOrEmpty(categoria))
                {
                    // Comparar por nombre ya que el filtro llega como string
                    list = list.Where(d => d.Categoria != null && d.Categoria.Nombre == categoria).ToList();
                    ViewBag.CategoriaFiltro = categoria;
                }
                
                return View(list);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar la biblioteca: " + ex.Message;
                return View(new List<Documento>());
            }
        }

        // GET: Biblioteca/Create
        public IActionResult Create()
        {
            var categoriasResult = Crud<CategoriaDocumento>.GetAll();
            ViewBag.Categorias = categoriasResult != null ? categoriasResult.ToList() : new List<CategoriaDocumento>();
            return View();
        }

        // POST: Biblioteca/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Documento item, IFormFile ArchivoPdf)
        {
            if (ArchivoPdf == null || ArchivoPdf.Length == 0)
            {
                ModelState.AddModelError("UrlArchivo", "Por favor, seleccione un archivo PDF.");
            }
            else if (Path.GetExtension(ArchivoPdf.FileName).ToLower() != ".pdf")
            {
                ModelState.AddModelError("UrlArchivo", "El archivo debe ser un PDF.");
            }

            // Ignorar validación de campos que se llenan en el backend
            ModelState.Remove("UrlArchivo");
            ModelState.Remove("IdUsuario");
            ModelState.Remove("FechaSubida");
            ModelState.Remove("Usuario");
            ModelState.Remove("Categoria"); // Removemos el objeto virtual de la validación

            if (ModelState.IsValid)
            {
                try
                {
                    // 1. Guardar archivo en Azure Blob Storage
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(ArchivoPdf.FileName);
                    
                    // Subir al contenedor "biblioteca"
                    string fileUrl;
                    using (var stream = ArchivoPdf.OpenReadStream())
                    {
                        fileUrl = await _blobStorageService.UploadFileAsync(stream, ArchivoPdf.ContentType, "biblioteca", uniqueFileName);
                    }

                    // 2. Llenar modelo y guardar en DB
                    item.UrlArchivo = fileUrl;
                    item.FechaSubida = DateTime.UtcNow;

                    // Asignar IdUsuario del usuario logueado
                    var claimId = User.FindFirst(ClaimTypes.NameIdentifier);
                    if (claimId != null && int.TryParse(claimId.Value, out int idUsuario))
                    {
                        item.IdUsuario = idUsuario;
                    }
                    else
                    {
                        item.IdUsuario = 1; // Fallback
                    }

                    Crud<Documento>.Create(item);
                    TempData["Success"] = "Documento subido correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Error al subir documento: " + ex.Message;
                }
            }
            var categoriasResult = Crud<CategoriaDocumento>.GetAll();
            ViewBag.Categorias = categoriasResult != null ? categoriasResult.ToList() : new List<CategoriaDocumento>();
            
            return View(item);
        }
        // GET: Biblioteca/Edit/5
        [Authorize(Roles = "Coordinador, Tecnico, Administrador")]
        public IActionResult Edit(int id)
        {
            try
            {
                var item = Crud<Documento>.GetById(id);
                if (item == null) return NotFound();

                var categoriasResult = Crud<CategoriaDocumento>.GetAll();
                ViewBag.Categorias = categoriasResult != null ? categoriasResult.ToList() : new List<CategoriaDocumento>();

                return View(item);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar el documento: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Biblioteca/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Coordinador, Tecnico, Administrador")]
        public IActionResult Edit(int id, Documento model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Fetch existing to preserve non-editable fields (like UrlArchivo, FechaSubida, etc.)
                    var existingItem = Crud<Documento>.GetById(id);
                    if (existingItem == null) return NotFound();

                    existingItem.Titulo = model.Titulo;
                    existingItem.Descripcion = model.Descripcion;
                    existingItem.CategoriaId = model.CategoriaId;

                    Crud<Documento>.Update(id, existingItem);
                    TempData["Success"] = "Documento actualizado correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error al actualizar documento: " + ex.Message);
                }
            }

            var categoriasResult = Crud<CategoriaDocumento>.GetAll();
            ViewBag.Categorias = categoriasResult != null ? categoriasResult.ToList() : new List<CategoriaDocumento>();
            return View(model);
        }
        
        // GET: Biblioteca/Delete/5
        [Authorize(Roles = "Coordinador, Tecnico, Administrador")]
        public IActionResult Delete(int id)
        {
             try
            {
                var item = Crud<Documento>.GetById(id);
                if (item == null) return NotFound();

                return View(item);
            }
            catch(Exception ex)
            {
                TempData["Error"] = "Error al cargar: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Biblioteca/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Coordinador, Tecnico, Administrador")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var item = Crud<Documento>.GetById(id);
                if (item != null)
                {
                    // Eliminar de base de datos
                    Crud<Documento>.Delete(id);
                    
                    // Eliminar archivo de Azure
                    await _blobStorageService.DeleteFileAsync(item.UrlArchivo, "biblioteca");

                    TempData["Success"] = "Documento eliminado correctamente.";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al eliminar: " + ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

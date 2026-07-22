using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BioUTN.Modelos;
using BioUTN.Servicios;
using BioUTN.ApiConsumer;
using System.Threading.Tasks;

namespace BioUTN.MVC.Controllers
{
    [Authorize(Roles = "Administrador, Coordinador, Tecnico")]
    public class CategoriasDocumentoController : Controller
    {
        // GET: CategoriasDocumento
        public IActionResult Index()
        {
            var categorias = Crud<CategoriaDocumento>.GetAll();
            return View(categorias);
        }

        // GET: CategoriasDocumento/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CategoriasDocumento/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoriaDocumento categoria)
        {
            // Remove navigation properties from validation
            ModelState.Remove("Documentos");

            if (ModelState.IsValid)
            {
                try
                {
                    Crud<CategoriaDocumento>.Create(categoria);
                    TempData["Success"] = "Categoría creada exitosamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error al crear la categoría: " + ex.Message);
                }
            }
            return View(categoria);
        }

        // GET: CategoriasDocumento/Edit/5
        public IActionResult Edit(int id)
        {
            var categoria = Crud<CategoriaDocumento>.GetById(id);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        // POST: CategoriasDocumento/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, CategoriaDocumento categoria)
        {
            if (id != categoria.Id)
            {
                return NotFound();
            }

            ModelState.Remove("Documentos");

            if (ModelState.IsValid)
            {
                try
                {
                    Crud<CategoriaDocumento>.Update(id, categoria);
                    TempData["Success"] = "Categoría actualizada exitosamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error al actualizar la categoría: " + ex.Message);
                }
            }
            return View(categoria);
        }

        // GET: CategoriasDocumento/Delete/5
        public IActionResult Delete(int id)
        {
            var categoria = Crud<CategoriaDocumento>.GetById(id);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        // POST: CategoriasDocumento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                Crud<CategoriaDocumento>.Delete(id);
                TempData["Success"] = "Categoría eliminada exitosamente.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al eliminar la categoría: " + ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BioUTN.API.Data;
using BioUTN.Modelos;

namespace BioUTN.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasDocumentoController : ControllerBase
    {
        private readonly BioUTNContext _context;

        public CategoriasDocumentoController(BioUTNContext context)
        {
            _context = context;
        }

        // GET: api/CategoriasDocumento
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaDocumento>>> GetCategoriasDocumento()
        {
            return await _context.CategoriasDocumento.ToListAsync();
        }

        // GET: api/CategoriasDocumento/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaDocumento>> GetCategoriaDocumento(int id)
        {
            var categoria = await _context.CategoriasDocumento.FindAsync(id);

            if (categoria == null)
            {
                return NotFound();
            }

            return categoria;
        }

        // PUT: api/CategoriasDocumento/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoriaDocumento(int id, CategoriaDocumento categoria)
        {
            if (id != categoria.Id)
            {
                return BadRequest();
            }

            _context.Entry(categoria).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaDocumentoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CategoriasDocumento
        [HttpPost]
        public async Task<ActionResult<CategoriaDocumento>> PostCategoriaDocumento(CategoriaDocumento categoria)
        {
            _context.CategoriasDocumento.Add(categoria);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategoriaDocumento", new { id = categoria.Id }, categoria);
        }

        // DELETE: api/CategoriasDocumento/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoriaDocumento(int id)
        {
            var categoria = await _context.CategoriasDocumento.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }

            // Check if there are documents using this category
            bool hasDocuments = await _context.Documentos.AnyAsync(d => d.CategoriaId == id);
            if (hasDocuments)
            {
                return BadRequest("No se puede eliminar la categoría porque hay documentos asociados a ella.");
            }

            _context.CategoriasDocumento.Remove(categoria);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoriaDocumentoExists(int id)
        {
            return _context.CategoriasDocumento.Any(e => e.Id == id);
        }
    }
}

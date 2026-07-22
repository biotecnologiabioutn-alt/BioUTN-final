using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BioUTN.API.Data;
using BioUTN.Modelos;

namespace BioUTN.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnidadesMedidaController : ControllerBase
    {
        private readonly BioUTNContext _context;

        public UnidadesMedidaController(BioUTNContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UnidadMedida>>> GetUnidadesMedida()
        {
            return await _context.UnidadesMedida.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UnidadMedida>> GetUnidadMedida(int id)
        {
            var unidadMedida = await _context.UnidadesMedida.FindAsync(id);
            if (unidadMedida == null) return NotFound();
            return unidadMedida;
        }

        [HttpPost]
        public async Task<ActionResult<UnidadMedida>> PostUnidadMedida(UnidadMedida unidadMedida)
        {
            _context.UnidadesMedida.Add(unidadMedida);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetUnidadMedida", new { id = unidadMedida.Id }, unidadMedida);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUnidadMedida(int id, UnidadMedida unidadMedida)
        {
            if (id != unidadMedida.Id) return BadRequest();
            _context.Entry(unidadMedida).State = EntityState.Modified;
            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException)
            {
                if (!UnidadMedidaExists(id)) return NotFound();
                else throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUnidadMedida(int id)
        {
            var unidadMedida = await _context.UnidadesMedida.FindAsync(id);
            if (unidadMedida == null) return NotFound();
            _context.UnidadesMedida.Remove(unidadMedida);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool UnidadMedidaExists(int id) => _context.UnidadesMedida.Any(e => e.Id == id);
    }
}

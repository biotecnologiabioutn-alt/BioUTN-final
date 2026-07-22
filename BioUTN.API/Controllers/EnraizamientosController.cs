using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BioUTN.API.Data;
using BioUTN.Modelos;

namespace BioUTN.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnraizamientosController : ControllerBase
    {
        private readonly BioUTNContext _context;

        public EnraizamientosController(BioUTNContext context)
        {
            _context = context;
        }

        // GET: api/Enraizamientos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Enraizamiento>>> GetEnraizamientos()
        {
            return await _context.Enraizamientos
                .Include(e => e.LoteOrigen)
                .Include(e => e.UsuarioResponsable)
                .Include(e => e.Proyecto)
                .Include(e => e.Protocolo)
                .ToListAsync();
        }

        // GET: api/Enraizamientos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Enraizamiento>> GetEnraizamiento(int id)
        {
            var enraizamiento = await _context.Enraizamientos
                .Include(e => e.LoteOrigen)
                .Include(e => e.UsuarioResponsable)
                .Include(e => e.Proyecto)
                .Include(e => e.Protocolo)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (enraizamiento == null)
            {
                return NotFound();
            }

            return enraizamiento;
        }

        // PUT: api/Enraizamientos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEnraizamiento(int id, Enraizamiento enraizamiento)
        {
            if (id != enraizamiento.Id)
            {
                return BadRequest();
            }

            _context.Entry(enraizamiento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnraizamientoExists(id))
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

        // POST: api/Enraizamientos
        [HttpPost]
        public async Task<ActionResult<Enraizamiento>> PostEnraizamiento(Enraizamiento enraizamiento)
        {
            _context.Enraizamientos.Add(enraizamiento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEnraizamiento", new { id = enraizamiento.Id }, enraizamiento);
        }

        // DELETE: api/Enraizamientos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnraizamiento(int id)
        {
            var enraizamiento = await _context.Enraizamientos.FindAsync(id);
            if (enraizamiento == null)
            {
                return NotFound();
            }

            _context.Enraizamientos.Remove(enraizamiento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EnraizamientoExists(int id)
        {
            return _context.Enraizamientos.Any(e => e.Id == id);
        }
    }
}

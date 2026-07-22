using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BioUTN.API.Data;
using BioUTN.Modelos;

namespace BioUTN.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AclimatacionesController : ControllerBase
    {
        private readonly BioUTNContext _context;

        public AclimatacionesController(BioUTNContext context)
        {
            _context = context;
        }

        // GET: api/Aclimataciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aclimatacion>>> GetAclimataciones()
        {
            return await _context.Aclimataciones
                .Include(a => a.LoteEnraizado)
                .Include(a => a.UsuarioResponsable)
                .Include(a => a.UbicacionFisica)
                .Include(a => a.Protocolo)
                .ToListAsync();
        }

        // GET: api/Aclimataciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Aclimatacion>> GetAclimatacion(int id)
        {
            var aclimatacion = await _context.Aclimataciones
                .Include(a => a.LoteEnraizado)
                .Include(a => a.UsuarioResponsable)
                .Include(a => a.UbicacionFisica)
                .Include(a => a.Protocolo)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (aclimatacion == null)
            {
                return NotFound();
            }

            return aclimatacion;
        }

        // PUT: api/Aclimataciones/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAclimatacion(int id, Aclimatacion aclimatacion)
        {
            if (id != aclimatacion.Id)
            {
                return BadRequest();
            }

            _context.Entry(aclimatacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AclimatacionExists(id))
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

        // POST: api/Aclimataciones
        [HttpPost]
        public async Task<ActionResult<Aclimatacion>> PostAclimatacion(Aclimatacion aclimatacion)
        {
            _context.Aclimataciones.Add(aclimatacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAclimatacion", new { id = aclimatacion.Id }, aclimatacion);
        }

        // DELETE: api/Aclimataciones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAclimatacion(int id)
        {
            var aclimatacion = await _context.Aclimataciones.FindAsync(id);
            if (aclimatacion == null)
            {
                return NotFound();
            }

            _context.Aclimataciones.Remove(aclimatacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AclimatacionExists(int id)
        {
            return _context.Aclimataciones.Any(e => e.Id == id);
        }
    }
}

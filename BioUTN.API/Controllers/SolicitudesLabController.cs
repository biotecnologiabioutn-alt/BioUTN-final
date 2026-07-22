using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BioUTN.API.Data;
using BioUTN.Modelos;

namespace BioUTN.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudesLabController : ControllerBase
    {
        private readonly BioUTNContext _context;

        public SolicitudesLabController(BioUTNContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SolicitudLab>>> GetSolicitudesLab()
        {
            return await _context.SolicitudesLab.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SolicitudLab>> GetSolicitudLab(int id)
        {
            var solicitudLab = await _context.SolicitudesLab.FindAsync(id);
            if (solicitudLab == null) return NotFound();
            return solicitudLab;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSolicitudLab(int id, SolicitudLab solicitudLab)
        {
            if (id != solicitudLab.Id) return BadRequest();
            _context.Entry(solicitudLab).State = EntityState.Modified;
            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException)
            {
                if (!SolicitudLabExists(id)) return NotFound();
                else throw;
            }
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<SolicitudLab>> PostSolicitudLab(SolicitudLab solicitudLab)
        {
            _context.SolicitudesLab.Add(solicitudLab);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetSolicitudLab", new { id = solicitudLab.Id }, solicitudLab);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSolicitudLab(int id)
        {
            var solicitudLab = await _context.SolicitudesLab.FindAsync(id);
            if (solicitudLab == null) return NotFound();
            _context.SolicitudesLab.Remove(solicitudLab);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool SolicitudLabExists(int id) => _context.SolicitudesLab.Any(e => e.Id == id);
    }
}

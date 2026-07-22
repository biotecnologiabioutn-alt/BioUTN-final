using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BioUTN.API.Data;
using BioUTN.Modelos;

namespace BioUTN.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KardexMovimientosController : ControllerBase
    {
        private readonly BioUTNContext _context;

        public KardexMovimientosController(BioUTNContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<KardexMovimiento>>> GetKardexMovimientos()
        {
            return await _context.KardexMovimientos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<KardexMovimiento>> GetKardexMovimiento(int id)
        {
            var kardex = await _context.KardexMovimientos.FindAsync(id);
            if (kardex == null) return NotFound();
            return kardex;
        }

        [HttpPost]
        public async Task<ActionResult<KardexMovimiento>> PostKardexMovimiento(KardexMovimiento kardex)
        {
            _context.KardexMovimientos.Add(kardex);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetKardexMovimiento", new { id = kardex.Id }, kardex);
        }
    }
}

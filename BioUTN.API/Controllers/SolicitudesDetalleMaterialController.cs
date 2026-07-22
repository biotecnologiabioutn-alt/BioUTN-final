using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BioUTN.API.Data;
using BioUTN.Modelos;

namespace BioUTN.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudesDetalleMaterialController : ControllerBase
    {
        private readonly BioUTNContext _context;

        public SolicitudesDetalleMaterialController(BioUTNContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SolicitudDetalleMaterial>>> GetSolicitudesDetalleMaterial()
        {
            return await _context.SolicitudesDetalleMaterial.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SolicitudDetalleMaterial>> GetSolicitudDetalleMaterial(int id)
        {
            var detalle = await _context.SolicitudesDetalleMaterial.FindAsync(id);
            if (detalle == null) return NotFound();
            return detalle;
        }

        [HttpPost]
        public async Task<ActionResult<SolicitudDetalleMaterial>> PostSolicitudDetalleMaterial(SolicitudDetalleMaterial detalle)
        {
            _context.SolicitudesDetalleMaterial.Add(detalle);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetSolicitudDetalleMaterial", new { id = detalle.Id }, detalle);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using BioUTN.Modelos;
using BioUTN.API.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BioUTN.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiposProyectoController : ControllerBase
    {
        private readonly BioUTNContext _context;

        public TiposProyectoController(BioUTNContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoProyecto>>> Get()
        {
            var data = await _context.TiposProyecto.ToListAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TipoProyecto>> Get(int id)
        {
            var data = await _context.TiposProyecto.FindAsync(id);
            if (data == null) return NotFound();
            return Ok(data);
        }

        [HttpPost]
        public async Task<ActionResult<TipoProyecto>> Post(TipoProyecto entity)
        {
            _context.TiposProyecto.Add(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TipoProyecto>> Put(int id, TipoProyecto entity)
        {
            if (id != entity.Id) return BadRequest();
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TipoProyecto>> Delete(int id)
        {
            var entity = await _context.TiposProyecto.FindAsync(id);
            if (entity == null) return NotFound();
            _context.TiposProyecto.Remove(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }
    }
}

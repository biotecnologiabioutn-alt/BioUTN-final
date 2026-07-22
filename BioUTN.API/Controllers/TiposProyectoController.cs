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
        public async Task<ActionResult<ApiResult<List<TipoProyecto>>>> Get()
        {
            var data = await _context.TiposProyecto.ToListAsync();
            var res = new ApiResult<List<TipoProyecto>> { Success = true, Data = data, Message = "Éxito" };
            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<TipoProyecto>>> Get(int id)
        {
            var data = await _context.TiposProyecto.FindAsync(id);
            if (data == null) return NotFound(new ApiResult<TipoProyecto> { Success = false, Message = "No encontrado" });
            var res = new ApiResult<TipoProyecto> { Success = true, Data = data, Message = "Éxito" };
            return Ok(res);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResult<TipoProyecto>>> Post(TipoProyecto entity)
        {
            _context.TiposProyecto.Add(entity);
            await _context.SaveChangesAsync();
            var res = new ApiResult<TipoProyecto> { Success = true, Data = entity, Message = "Creado exitosamente" };
            return Ok(res);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResult<TipoProyecto>>> Put(int id, TipoProyecto entity)
        {
            if (id != entity.Id) return BadRequest(new ApiResult<TipoProyecto> { Success = false, Message = "ID no coincide" });
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            var res = new ApiResult<TipoProyecto> { Success = true, Data = entity, Message = "Actualizado exitosamente" };
            return Ok(res);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResult<TipoProyecto>>> Delete(int id)
        {
            var entity = await _context.TiposProyecto.FindAsync(id);
            if (entity == null) return NotFound(new ApiResult<TipoProyecto> { Success = false, Message = "No encontrado" });
            _context.TiposProyecto.Remove(entity);
            await _context.SaveChangesAsync();
            var res = new ApiResult<TipoProyecto> { Success = true, Data = entity, Message = "Eliminado exitosamente" };
            return Ok(res);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BioUTN.Modelos;
using BioUTN.API.Data;

namespace BioUTN.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermisoAmbientalsController : ControllerBase
    {
        private readonly BioUTNContext _context;

        public PermisoAmbientalsController(BioUTNContext context)
        {
            _context = context;
        }

        // GET: api/PermisoAmbientals
        [HttpGet]
        public async Task<ActionResult<List<PermisoAmbiental>>> GetPermisoAmbientals()
        {
            try
            {
                var list = await _context.PermisosAmbientales.ToListAsync();
                return Ok(list); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno: " + ex.Message);
            }
        }

        // GET: api/PermisoAmbientals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PermisoAmbiental>> GetPermisoAmbiental(int id)
        {
            try
            {
                var item = await _context.PermisosAmbientales.FindAsync(id);

                if (item == null)
                {
                    return NotFound($"No se encontrÃ³ con ID {id}.");
                }

                return Ok(item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
        }

        // PUT: api/PermisoAmbientals/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPermisoAmbiental(int id, PermisoAmbiental item)
        {
            if (id != item.Id)
            {
                return BadRequest("El ID de la URL no coincide con el ID del objeto.");
            }

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return NoContent(); 
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PermisoAmbientalExists(id))
                {
                    return NotFound("Elemento no encontrado.");
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al actualizar: " + ex.Message);
            }
        }

        // POST: api/PermisoAmbientals
        [HttpPost]
        public async Task<ActionResult<PermisoAmbiental>> PostPermisoAmbiental(PermisoAmbiental item)
        {
            try
            {
                _context.PermisosAmbientales.Add(item);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetPermisoAmbiental), new { id = item.Id }, item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al guardar: " + ex.Message);
            }
        }

        // DELETE: api/PermisoAmbientals/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PermisoAmbiental>> DeletePermisoAmbiental(int id)
        {
            try
            {
                var item = await _context.PermisosAmbientales.FindAsync(id);
                if (item == null)
                {
                    return NotFound("Elemento no encontrado.");
                }

                _context.PermisosAmbientales.Remove(item);
                await _context.SaveChangesAsync();

                return Ok(item); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al eliminar: " + ex.Message);
            }
        }

        private bool PermisoAmbientalExists(int id)
        {
            return _context.PermisosAmbientales.Any(e => e.Id == id);
        }
    }
}

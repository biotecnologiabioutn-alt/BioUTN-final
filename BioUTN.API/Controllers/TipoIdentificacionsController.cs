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
    public class TipoIdentificacionsController : ControllerBase
    {
        private readonly BioUTNContext _context;

        public TipoIdentificacionsController(BioUTNContext context)
        {
            _context = context;
        }

        // GET: api/TipoIdentificacions
        [HttpGet]
        public async Task<ActionResult<List<TipoIdentificacion>>> GetTipoIdentificacions()
        {
            try
            {
                var list = await _context.TiposIdentificacion.ToListAsync();
                return Ok(list); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno: " + ex.Message);
            }
        }

        // GET: api/TipoIdentificacions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoIdentificacion>> GetTipoIdentificacion(int id)
        {
            try
            {
                var item = await _context.TiposIdentificacion.FindAsync(id);

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

        // PUT: api/TipoIdentificacions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoIdentificacion(int id, TipoIdentificacion item)
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
                if (!TipoIdentificacionExists(id))
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

        // POST: api/TipoIdentificacions
        [HttpPost]
        public async Task<ActionResult<TipoIdentificacion>> PostTipoIdentificacion(TipoIdentificacion item)
        {
            try
            {
                _context.TiposIdentificacion.Add(item);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetTipoIdentificacion), new { id = item.Id }, item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al guardar: " + ex.Message);
            }
        }

        // DELETE: api/TipoIdentificacions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TipoIdentificacion>> DeleteTipoIdentificacion(int id)
        {
            try
            {
                var item = await _context.TiposIdentificacion.FindAsync(id);
                if (item == null)
                {
                    return NotFound("Elemento no encontrado.");
                }

                _context.TiposIdentificacion.Remove(item);
                await _context.SaveChangesAsync();

                return Ok(item); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al eliminar: " + ex.Message);
            }
        }

        private bool TipoIdentificacionExists(int id)
        {
            return _context.TiposIdentificacion.Any(e => e.Id == id);
        }
    }
}

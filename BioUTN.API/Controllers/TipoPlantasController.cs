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
    public class TipoPlantasController : ControllerBase
    {
        private readonly BioUTNContext _context;

        public TipoPlantasController(BioUTNContext context)
        {
            _context = context;
        }

        // GET: api/TipoPlantas
        [HttpGet]
        public async Task<ActionResult<List<TipoPlanta>>> GetTipoPlantas()
        {
            try
            {
                var list = await _context.TiposPlanta.ToListAsync();
                return Ok(list); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno: " + ex.Message);
            }
        }

        // GET: api/TipoPlantas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoPlanta>> GetTipoPlanta(int id)
        {
            try
            {
                var item = await _context.TiposPlanta.FindAsync(id);

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

        // PUT: api/TipoPlantas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoPlanta(int id, TipoPlanta item)
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
                if (!TipoPlantaExists(id))
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

        // POST: api/TipoPlantas
        [HttpPost]
        public async Task<ActionResult<TipoPlanta>> PostTipoPlanta(TipoPlanta item)
        {
            try
            {
                _context.TiposPlanta.Add(item);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetTipoPlanta), new { id = item.Id }, item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al guardar: " + ex.Message);
            }
        }

        // DELETE: api/TipoPlantas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TipoPlanta>> DeleteTipoPlanta(int id)
        {
            try
            {
                var item = await _context.TiposPlanta.FindAsync(id);
                if (item == null)
                {
                    return NotFound("Elemento no encontrado.");
                }

                _context.TiposPlanta.Remove(item);
                await _context.SaveChangesAsync();

                return Ok(item); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al eliminar: " + ex.Message);
            }
        }

        private bool TipoPlantaExists(int id)
        {
            return _context.TiposPlanta.Any(e => e.Id == id);
        }
    }
}

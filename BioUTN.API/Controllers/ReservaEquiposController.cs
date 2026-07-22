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
    public class ReservaEquiposController : ControllerBase
    {
        private readonly BioUTNContext _context;

        public ReservaEquiposController(BioUTNContext context)
        {
            _context = context;
        }

        // GET: api/ReservaEquipos
        [HttpGet]
        public async Task<ActionResult<List<ReservaEquipo>>> GetReservaEquipos()
        {
            try
            {
                var list = await _context.ReservasEquipo.ToListAsync();
                return Ok(list); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno: " + ex.Message);
            }
        }

        // GET: api/ReservaEquipos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReservaEquipo>> GetReservaEquipo(int id)
        {
            try
            {
                var item = await _context.ReservasEquipo.FindAsync(id);

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

        // PUT: api/ReservaEquipos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservaEquipo(int id, ReservaEquipo item)
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
                if (!ReservaEquipoExists(id))
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

        // POST: api/ReservaEquipos
        [HttpPost]
        public async Task<ActionResult<ReservaEquipo>> PostReservaEquipo(ReservaEquipo item)
        {
            try
            {
                _context.ReservasEquipo.Add(item);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetReservaEquipo), new { id = item.Id }, item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al guardar: " + ex.Message);
            }
        }

        // DELETE: api/ReservaEquipos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ReservaEquipo>> DeleteReservaEquipo(int id)
        {
            try
            {
                var item = await _context.ReservasEquipo.FindAsync(id);
                if (item == null)
                {
                    return NotFound("Elemento no encontrado.");
                }

                _context.ReservasEquipo.Remove(item);
                await _context.SaveChangesAsync();

                return Ok(item); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al eliminar: " + ex.Message);
            }
        }

        private bool ReservaEquipoExists(int id)
        {
            return _context.ReservasEquipo.Any(e => e.Id == id);
        }
    }
}

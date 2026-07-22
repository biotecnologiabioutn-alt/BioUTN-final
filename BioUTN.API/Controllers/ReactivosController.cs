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
    public class ReactivosController : ControllerBase
    {
        private readonly BioUTNContext _context;

        public ReactivosController(BioUTNContext context)
        {
            _context = context;
        }

        // GET: api/Reactivos
        [HttpGet]
        public async Task<ActionResult<List<Reactivo>>> GetReactivos()
        {
            try
            {
                var list = await _context.Reactivos.ToListAsync();
                return Ok(list); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno: " + ex.Message);
            }
        }

        // GET: api/Reactivos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reactivo>> GetReactivo(int id)
        {
            try
            {
                var item = await _context.Reactivos.FindAsync(id);

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

        // PUT: api/Reactivos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReactivo(int id, Reactivo item)
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
                if (!ReactivoExists(id))
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

        // POST: api/Reactivos
        [HttpPost]
        public async Task<ActionResult<Reactivo>> PostReactivo(Reactivo item)
        {
            try
            {
                _context.Reactivos.Add(item);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetReactivo), new { id = item.Id }, item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al guardar: " + ex.Message);
            }
        }

        // DELETE: api/Reactivos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Reactivo>> DeleteReactivo(int id)
        {
            try
            {
                var item = await _context.Reactivos.FindAsync(id);
                if (item == null)
                {
                    return NotFound("Elemento no encontrado.");
                }

                _context.Reactivos.Remove(item);
                await _context.SaveChangesAsync();

                return Ok(item); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al eliminar: " + ex.Message);
            }
        }

        private bool ReactivoExists(int id)
        {
            return _context.Reactivos.Any(e => e.Id == id);
        }
    }
}

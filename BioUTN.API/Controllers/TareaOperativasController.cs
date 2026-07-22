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
    public class TareaOperativasController : ControllerBase
    {
        private readonly BioUTNContext _context;

        public TareaOperativasController(BioUTNContext context)
        {
            _context = context;
        }

        // GET: api/TareaOperativas
        [HttpGet]
        public async Task<ActionResult<List<TareaOperativa>>> GetTareaOperativas()
        {
            try
            {
                var list = await _context.TareasOperativas.ToListAsync();
                return Ok(list); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno: " + ex.Message);
            }
        }

        // GET: api/TareaOperativas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TareaOperativa>> GetTareaOperativa(int id)
        {
            try
            {
                var item = await _context.TareasOperativas.FindAsync(id);

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

        // PUT: api/TareaOperativas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTareaOperativa(int id, TareaOperativa item)
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
                if (!TareaOperativaExists(id))
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

        // POST: api/TareaOperativas
        [HttpPost]
        public async Task<ActionResult<TareaOperativa>> PostTareaOperativa(TareaOperativa item)
        {
            try
            {
                _context.TareasOperativas.Add(item);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetTareaOperativa), new { id = item.Id }, item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al guardar: " + ex.Message);
            }
        }

        // DELETE: api/TareaOperativas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TareaOperativa>> DeleteTareaOperativa(int id)
        {
            try
            {
                var item = await _context.TareasOperativas.FindAsync(id);
                if (item == null)
                {
                    return NotFound("Elemento no encontrado.");
                }

                _context.TareasOperativas.Remove(item);
                await _context.SaveChangesAsync();

                return Ok(item); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al eliminar: " + ex.Message);
            }
        }

        private bool TareaOperativaExists(int id)
        {
            return _context.TareasOperativas.Any(e => e.Id == id);
        }
    }
}

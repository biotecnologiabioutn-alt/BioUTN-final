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
    public class UbicacionFisicasController : ControllerBase
    {
        private readonly BioUTNContext _context;

        public UbicacionFisicasController(BioUTNContext context)
        {
            _context = context;
        }

        // GET: api/UbicacionFisicas
        [HttpGet]
        public async Task<ActionResult<List<UbicacionFisica>>> GetUbicacionFisicas()
        {
            try
            {
                var list = await _context.UbicacionesFisicas.ToListAsync();
                return Ok(list); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno: " + ex.Message);
            }
        }

        // GET: api/UbicacionFisicas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UbicacionFisica>> GetUbicacionFisica(int id)
        {
            try
            {
                var item = await _context.UbicacionesFisicas.FindAsync(id);

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

        // PUT: api/UbicacionFisicas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUbicacionFisica(int id, UbicacionFisica item)
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
                if (!UbicacionFisicaExists(id))
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

        // POST: api/UbicacionFisicas
        [HttpPost]
        public async Task<ActionResult<UbicacionFisica>> PostUbicacionFisica(UbicacionFisica item)
        {
            try
            {
                _context.UbicacionesFisicas.Add(item);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetUbicacionFisica), new { id = item.Id }, item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al guardar: " + ex.Message);
            }
        }

        // DELETE: api/UbicacionFisicas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UbicacionFisica>> DeleteUbicacionFisica(int id)
        {
            try
            {
                var item = await _context.UbicacionesFisicas.FindAsync(id);
                if (item == null)
                {
                    return NotFound("Elemento no encontrado.");
                }

                _context.UbicacionesFisicas.Remove(item);
                await _context.SaveChangesAsync();

                return Ok(item); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al eliminar: " + ex.Message);
            }
        }

        private bool UbicacionFisicaExists(int id)
        {
            return _context.UbicacionesFisicas.Any(e => e.Id == id);
        }
    }
}

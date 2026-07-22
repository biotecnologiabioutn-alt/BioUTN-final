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
    public class RolsController : ControllerBase
    {
        private readonly BioUTNContext _context;

        public RolsController(BioUTNContext context)
        {
            _context = context;
        }

        // GET: api/Rols
        [HttpGet]
        public async Task<ActionResult<List<Rol>>> GetRols()
        {
            try
            {
                var list = await _context.Roles.ToListAsync();
                return Ok(list); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno: " + ex.Message);
            }
        }

        // GET: api/Rols/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rol>> GetRol(int id)
        {
            try
            {
                var item = await _context.Roles.FindAsync(id);

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

        // PUT: api/Rols/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRol(int id, Rol item)
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
                if (!RolExists(id))
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

        // POST: api/Rols
        [HttpPost]
        public async Task<ActionResult<Rol>> PostRol(Rol item)
        {
            try
            {
                _context.Roles.Add(item);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetRol), new { id = item.Id }, item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al guardar: " + ex.Message);
            }
        }

        // DELETE: api/Rols/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Rol>> DeleteRol(int id)
        {
            try
            {
                var item = await _context.Roles.FindAsync(id);
                if (item == null)
                {
                    return NotFound("Elemento no encontrado.");
                }

                _context.Roles.Remove(item);
                await _context.SaveChangesAsync();

                return Ok(item); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al eliminar: " + ex.Message);
            }
        }

        private bool RolExists(int id)
        {
            return _context.Roles.Any(e => e.Id == id);
        }
    }
}

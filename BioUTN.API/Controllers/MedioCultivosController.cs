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
    public class MedioCultivosController : ControllerBase
    {
        private readonly BioUTNContext _context;

        public MedioCultivosController(BioUTNContext context)
        {
            _context = context;
        }

        // GET: api/MedioCultivos
        [HttpGet]
        public async Task<ActionResult<List<MedioCultivo>>> GetMedioCultivos()
        {
            try
            {
                var list = await _context.MediosCultivo.ToListAsync();
                return Ok(list); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno: " + ex.Message);
            }
        }

        // GET: api/MedioCultivos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MedioCultivo>> GetMedioCultivo(int id)
        {
            try
            {
                var item = await _context.MediosCultivo.FindAsync(id);

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

        // PUT: api/MedioCultivos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedioCultivo(int id, MedioCultivo item)
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
                if (!MedioCultivoExists(id))
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

        // POST: api/MedioCultivos
        [HttpPost]
        public async Task<ActionResult<MedioCultivo>> PostMedioCultivo(MedioCultivo item)
        {
            try
            {
                _context.MediosCultivo.Add(item);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetMedioCultivo), new { id = item.Id }, item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al guardar: " + ex.Message);
            }
        }

        // DELETE: api/MedioCultivos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MedioCultivo>> DeleteMedioCultivo(int id)
        {
            try
            {
                var item = await _context.MediosCultivo.FindAsync(id);
                if (item == null)
                {
                    return NotFound("Elemento no encontrado.");
                }

                _context.MediosCultivo.Remove(item);
                await _context.SaveChangesAsync();

                return Ok(item); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al eliminar: " + ex.Message);
            }
        }

        private bool MedioCultivoExists(int id)
        {
            return _context.MediosCultivo.Any(e => e.Id == id);
        }
    }
}

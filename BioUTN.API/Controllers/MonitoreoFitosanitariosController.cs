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
    public class MonitoreoFitosanitariosController : ControllerBase
    {
        private readonly BioUTNContext _context;

        public MonitoreoFitosanitariosController(BioUTNContext context)
        {
            _context = context;
        }

        // GET: api/MonitoreoFitosanitarios
        [HttpGet]
        public async Task<ActionResult<List<MonitoreoFitosanitario>>> GetMonitoreoFitosanitarios()
        {
            try
            {
                var list = await _context.MonitoreosFitosanitarios.ToListAsync();
                return Ok(list); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno: " + ex.Message);
            }
        }

        // GET: api/MonitoreoFitosanitarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MonitoreoFitosanitario>> GetMonitoreoFitosanitario(int id)
        {
            try
            {
                var item = await _context.MonitoreosFitosanitarios.FindAsync(id);

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

        // PUT: api/MonitoreoFitosanitarios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMonitoreoFitosanitario(int id, MonitoreoFitosanitario item)
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
                if (!MonitoreoFitosanitarioExists(id))
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

        // POST: api/MonitoreoFitosanitarios
        [HttpPost]
        public async Task<ActionResult<MonitoreoFitosanitario>> PostMonitoreoFitosanitario(MonitoreoFitosanitario item)
        {
            try
            {
                _context.MonitoreosFitosanitarios.Add(item);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetMonitoreoFitosanitario), new { id = item.Id }, item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al guardar: " + ex.Message);
            }
        }

        // POST: api/MonitoreoFitosanitarios/batch
        [HttpPost("batch")]
        public async Task<IActionResult> PostMonitoreoFitosanitarioBatch(List<MonitoreoFitosanitario> items)
        {
            try
            {
                if (items == null || !items.Any())
                {
                    return BadRequest("La lista de monitoreos está vacía.");
                }

                _context.MonitoreosFitosanitarios.AddRange(items);
                await _context.SaveChangesAsync();

                return Ok(new { message = $"{items.Count} monitoreos guardados correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al guardar en lote: " + ex.Message);
            }
        }

        // DELETE: api/MonitoreoFitosanitarios/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MonitoreoFitosanitario>> DeleteMonitoreoFitosanitario(int id)
        {
            try
            {
                var item = await _context.MonitoreosFitosanitarios.FindAsync(id);
                if (item == null)
                {
                    return NotFound("Elemento no encontrado.");
                }

                _context.MonitoreosFitosanitarios.Remove(item);
                await _context.SaveChangesAsync();

                return Ok(item); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al eliminar: " + ex.Message);
            }
        }

        private bool MonitoreoFitosanitarioExists(int id)
        {
            return _context.MonitoreosFitosanitarios.Any(e => e.Id == id);
        }
    }
}

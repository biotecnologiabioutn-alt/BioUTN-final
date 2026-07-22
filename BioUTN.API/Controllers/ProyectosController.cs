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
    public class ProyectosController : ControllerBase
    {
        private readonly BioUTNContext _context;

        public ProyectosController(BioUTNContext context)
        {
            _context = context;
        }

        // GET: api/Proyectos
        [HttpGet]
        public async Task<ActionResult<List<Proyecto>>> GetProyectos()
        {
            try
            {
                var list = await _context.Proyectos
                    .Include(p => p.TipoProyectoRef)
                    .ToListAsync();
                return Ok(list); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno: " + ex.Message);
            }
        }

        // GET: api/Proyectos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Proyecto>> GetProyecto(int id)
        {
            try
            {
                var item = await _context.Proyectos
                    .Include(p => p.Tesista)
                    .Include(p => p.TipoProyectoRef)
                    .FirstOrDefaultAsync(p => p.Id == id);

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

        // PUT: api/Proyectos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProyecto(int id, Proyecto item)
        {
            if (id != item.Id)
            {
                return BadRequest("El ID de la URL no coincide con el ID del objeto.");
            }

            item.FechaInicio = item.FechaInicio.ToUniversalTime();
            if (item.FechaFin.HasValue) item.FechaFin = item.FechaFin.Value.ToUniversalTime();

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return NoContent(); 
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProyectoExists(id))
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

        // POST: api/Proyectos
        [HttpPost]
        public async Task<ActionResult<Proyecto>> PostProyecto(Proyecto item)
        {
            try
            {
                item.FechaInicio = item.FechaInicio.ToUniversalTime();
                if (item.FechaFin.HasValue) item.FechaFin = item.FechaFin.Value.ToUniversalTime();

                _context.Proyectos.Add(item);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetProyecto), new { id = item.Id }, item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al guardar: " + ex.Message);
            }
        }

        // DELETE: api/Proyectos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Proyecto>> DeleteProyecto(int id)
        {
            try
            {
                var item = await _context.Proyectos.FindAsync(id);
                if (item == null)
                {
                    return NotFound("Elemento no encontrado.");
                }

                item.Activo = false;
                await _context.SaveChangesAsync();

                return Ok(item); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al eliminar: " + ex.Message);
            }
        }

        private bool ProyectoExists(int id)
        {
            return _context.Proyectos.Any(e => e.Id == id);
        }
    }
}

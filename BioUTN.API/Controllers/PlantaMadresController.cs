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
    public class PlantaMadresController : ControllerBase
    {
        private readonly BioUTNContext _context;

        public PlantaMadresController(BioUTNContext context)
        {
            _context = context;
        }

        // GET: api/PlantaMadres
        [HttpGet]
        public async Task<ActionResult<List<PlantaMadre>>> GetPlantaMadres()
        {
            try
            {
                var list = await _context.PlantasMadre
                    .Include(pm => pm.Especie)
                    .Include(pm => pm.Proyecto)
                    .ToListAsync();
                return Ok(list); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno: " + ex.Message);
            }
        }

        // GET: api/PlantaMadres/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlantaMadre>> GetPlantaMadre(int id)
        {
            try
            {
                var item = await _context.PlantasMadre
                    .Include(pm => pm.Especie)
                    .Include(pm => pm.Proyecto)
                    .FirstOrDefaultAsync(pm => pm.Id == id);

                if (item == null)
                {
                    return NotFound($"No se encontró con ID {id}.");
                }

                return Ok(item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
        }

        // PUT: api/PlantaMadres/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlantaMadre(int id, PlantaMadre item)
        {
            if (id != item.Id)
            {
                return BadRequest("El ID de la URL no coincide con el ID del objeto.");
            }
            item.FechaRecepcion = item.FechaRecepcion.ToUniversalTime();

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return NoContent(); 
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlantaMadreExists(id))
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

        // POST: api/PlantaMadres
        [HttpPost]
        public async Task<ActionResult<PlantaMadre>> PostPlantaMadre(PlantaMadre item)
        {
            try
            {
                item.FechaRecepcion = item.FechaRecepcion.ToUniversalTime();

                // Autogenerar CodigoAsignado: PM-{IdEspecie}-{Año}-{Secuencial}
                int year = DateTime.UtcNow.Year;
                int count = await _context.PlantasMadre.CountAsync(pm => pm.IdEspecie == item.IdEspecie && pm.FechaRecepcion.Year == year);
                string secuencial = (count + 1).ToString("D3");
                item.CodigoAsignado = $"PM-{item.IdEspecie}-{year}-{secuencial}";

                item.Activo = true;

                _context.PlantasMadre.Add(item);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetPlantaMadre), new { id = item.Id }, item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al guardar: " + ex.Message);
            }
        }

        // DELETE: api/PlantaMadres/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PlantaMadre>> DeletePlantaMadre(int id)
        {
            try
            {
                var item = await _context.PlantasMadre.FindAsync(id);
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

        private bool PlantaMadreExists(int id)
        {
            return _context.PlantasMadre.Any(e => e.Id == id);
        }
    }
}

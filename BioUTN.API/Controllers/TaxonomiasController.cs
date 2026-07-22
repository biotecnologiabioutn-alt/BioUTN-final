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
    public class TaxonomiasController : ControllerBase
    {
        private readonly BioUTNContext _context;

        public TaxonomiasController(BioUTNContext context)
        {
            _context = context;
        }

        // GET: api/Taxonomias
        [HttpGet]
        public async Task<ActionResult<List<Taxonomia>>> GetTaxonomias()
        {
            try
            {
                var list = await _context.Taxonomia.ToListAsync();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno: " + ex.Message);
            }
        }

        // GET: api/Taxonomias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Taxonomia>> GetTaxonomia(int id)
        {
            try
            {
                var item = await _context.Taxonomia.FindAsync(id);

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

        // PUT: api/Taxonomias/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaxonomia(int id, Taxonomia item)
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
                if (!TaxonomiaExists(id))
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

        // POST: api/Taxonomias
        [HttpPost]
        public async Task<ActionResult<Taxonomia>> PostTaxonomia(Taxonomia item)
        {
            try
            {
                _context.Taxonomia.Add(item);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetTaxonomia), new { id = item.Id }, item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al guardar: " + ex.Message);
            }
        }

        // DELETE: api/Taxonomias/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Taxonomia>> DeleteTaxonomia(int id)
        {
            try
            {
                var item = await _context.Taxonomia.FindAsync(id);
                if (item == null)
                {
                    return NotFound("Elemento no encontrado.");
                }

                _context.Taxonomia.Remove(item);
                await _context.SaveChangesAsync();

                return Ok(item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al eliminar: " + ex.Message);
            }
        }

        private bool TaxonomiaExists(int id)
        {
            return _context.Taxonomia.Any(e => e.Id == id);
        }
    }
}

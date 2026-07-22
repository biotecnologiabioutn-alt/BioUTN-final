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
    public class FaseCultivosController : ControllerBase
    {
        private readonly BioUTNContext _context;

        public FaseCultivosController(BioUTNContext context)
        {
            _context = context;
        }

        // GET: api/FaseCultivos
        [HttpGet]
        public async Task<ActionResult<List<FaseCultivo>>> GetFaseCultivos()
        {
            try
            {
                var list = await _context.FasesCultivo.ToListAsync();
                return Ok(list); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno: " + ex.Message);
            }
        }

        // GET: api/FaseCultivos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FaseCultivo>> GetFaseCultivo(int id)
        {
            try
            {
                var item = await _context.FasesCultivo.FindAsync(id);

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

        // PUT: api/FaseCultivos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFaseCultivo(int id, FaseCultivo item)
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
                if (!FaseCultivoExists(id))
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

        // POST: api/FaseCultivos
        [HttpPost]
        public async Task<ActionResult<FaseCultivo>> PostFaseCultivo(FaseCultivo item)
        {
            try
            {
                _context.FasesCultivo.Add(item);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetFaseCultivo), new { id = item.Id }, item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al guardar: " + ex.Message);
            }
        }

        // DELETE: api/FaseCultivos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<FaseCultivo>> DeleteFaseCultivo(int id)
        {
            try
            {
                var item = await _context.FasesCultivo.FindAsync(id);
                if (item == null)
                {
                    return NotFound("Elemento no encontrado.");
                }

                _context.FasesCultivo.Remove(item);
                await _context.SaveChangesAsync();

                return Ok(item); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al eliminar: " + ex.Message);
            }
        }

        private bool FaseCultivoExists(int id)
        {
            return _context.FasesCultivo.Any(e => e.Id == id);
        }
    }
}

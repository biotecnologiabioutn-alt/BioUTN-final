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
    public class DocumentosController : ControllerBase
    {
        private readonly BioUTNContext _context;

        public DocumentosController(BioUTNContext context)
        {
            _context = context;
        }

        // GET: api/Documentos
        [HttpGet]
        public async Task<ActionResult<List<Documento>>> GetDocumentos()
        {
            try
            {
                var list = await _context.Documentos
                    .Include(d => d.Categoria)
                    .Include(d => d.Usuario)
                    .Include(d => d.Proyecto)
                    .Include(d => d.LoteCultivo)
                    .ToListAsync();
                return Ok(list); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno: " + ex.Message);
            }
        }

        // GET: api/Documentos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Documento>> GetDocumento(int id)
        {
            try
            {
                var item = await _context.Documentos
                    .Include(d => d.Categoria)
                    .Include(d => d.Usuario)
                    .Include(d => d.Proyecto)
                    .Include(d => d.LoteCultivo)
                    .FirstOrDefaultAsync(d => d.Id == id);

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

        // PUT: api/Documentos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocumento(int id, Documento item)
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
                if (!DocumentoExists(id))
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

        // POST: api/Documentos
        [HttpPost]
        public async Task<ActionResult<Documento>> PostDocumento(Documento item)
        {
            try
            {
                _context.Documentos.Add(item);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetDocumento), new { id = item.Id }, item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al guardar: " + ex.Message);
            }
        }

        // DELETE: api/Documentos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Documento>> DeleteDocumento(int id)
        {
            try
            {
                var item = await _context.Documentos.FindAsync(id);
                if (item == null)
                {
                    return NotFound("Elemento no encontrado.");
                }

                _context.Documentos.Remove(item);
                await _context.SaveChangesAsync();

                return Ok(item); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al eliminar: " + ex.Message);
            }
        }

        private bool DocumentoExists(int id)
        {
            return _context.Documentos.Any(e => e.Id == id);
        }
    }
}

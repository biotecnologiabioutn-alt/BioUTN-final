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
    public class LoteCultivosController : ControllerBase
    {
        private readonly BioUTNContext _context;

        public LoteCultivosController(BioUTNContext context)
        {
            _context = context;
        }

        // GET: api/LoteCultivos
        [HttpGet]
        public async Task<ActionResult<List<LoteCultivo>>> GetLoteCultivos()
        {
            try
            {
                var list = await _context.LotesCultivo
                    .Include(lc => lc.PlantaMadre)
                        .ThenInclude(pm => pm.Especie)
                    .Include(lc => lc.MedioCultivo)
                    .Include(lc => lc.Proyecto)
                    .Include(lc => lc.UbicacionFisica)
                    .Include(lc => lc.FaseCultivo)
                    .ToListAsync();
                return Ok(list); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno: " + ex.Message);
            }
        }

        // GET: api/LoteCultivos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LoteCultivo>> GetLoteCultivo(int id)
        {
            try
            {
                var item = await _context.LotesCultivo
                    .Include(lc => lc.PlantaMadre)
                        .ThenInclude(pm => pm.Especie)
                    .Include(lc => lc.MedioCultivo)
                    .Include(lc => lc.Proyecto)
                    .Include(lc => lc.UbicacionFisica)
                    .Include(lc => lc.FaseCultivo)
                    .FirstOrDefaultAsync(lc => lc.Id == id);

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

        // PUT: api/LoteCultivos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLoteCultivo(int id, LoteCultivo item)
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
                if (!LoteCultivoExists(id))
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

        // POST: api/LoteCultivos
        [HttpPost]
        public async Task<ActionResult<LoteCultivo>> PostLoteCultivo(LoteCultivo item)
        {
            try
            {
                item.FechaSiembra = item.FechaSiembra.ToUniversalTime();

                // Autogenerar CodigoTrazabilidad: LT-{CodigoPlantaMadre}-{Mes}{Año}-{Secuencial}
                var plantaMadre = await _context.PlantasMadre.FindAsync(item.IdPlantaMadre);
                string baseCode = plantaMadre != null ? plantaMadre.CodigoAsignado : item.IdPlantaMadre.ToString();
                int year = item.FechaSiembra.Year;
                int month = item.FechaSiembra.Month;
                int count = await _context.LotesCultivo.CountAsync(l => l.IdPlantaMadre == item.IdPlantaMadre && l.FechaSiembra.Year == year && l.FechaSiembra.Month == month);
                item.CodigoTrazabilidad = $"LT-{baseCode}-{month:D2}{year}-{(count + 1):D2}";

                item.Activo = true;

                _context.LotesCultivo.Add(item);
                await _context.SaveChangesAsync();

                // Creación de Frascos Hijos
                if (item.TotalUnidades > 0)
                {
                    for (int i = 1; i <= item.TotalUnidades; i++)
                    {
                        var unidad = new UnidadFrasco
                        {
                            IdLoteCultivo = item.Id,
                            CodigoUnidad = $"{item.CodigoTrazabilidad}-{i:D3}",
                            NumeroResiembra = item.NumeroRepique,
                            Estado = "Saludable",
                            Activo = true
                        };
                        _context.UnidadesFrasco.Add(unidad);
                    }
                    await _context.SaveChangesAsync();
                }

                return CreatedAtAction(nameof(GetLoteCultivo), new { id = item.Id }, item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al guardar: " + ex.Message);
            }
        }

        // DELETE: api/LoteCultivos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<LoteCultivo>> DeleteLoteCultivo(int id)
        {
            try
            {
                var item = await _context.LotesCultivo.FindAsync(id);
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

        private bool LoteCultivoExists(int id)
        {
            return _context.LotesCultivo.Any(e => e.Id == id);
        }
    }
}

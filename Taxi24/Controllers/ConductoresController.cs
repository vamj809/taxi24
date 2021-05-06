using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taxi24.Models;
using GeoCoordinatePortable;

namespace Taxi24.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConductoresController : ControllerBase
    {
        private readonly Taxi24DBContext _context;

        public ConductoresController(Taxi24DBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Devuelve una lista de todos los conductores
        /// </summary>
        /// <returns></returns>
        // GET: api/Conductores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Conductor>>> GetConductores()
        {
            return await _context.Conductores.ToListAsync();
        }

        /// <summary>
        /// Devuelve una lista de todos los conductores disponibles
        /// </summary>
        /// <returns></returns>
        // GET: api/Conductores/available
        [HttpGet("available")]
        public async Task<ActionResult<IEnumerable<Conductor>>> GetConductoresDisponibles() {
            return await _context.Conductores.Where(conductor => conductor.Disponible == true).ToListAsync();
        }

        /// <summary>
        /// Devuelve una lista de todos los conductores disponibles en un radio de 3km para una ubicación específica.
        /// </summary>
        /// <returns></returns>
        // GET: api/Conductores/available-radio
        [HttpGet("available-radio")]
        public async Task<ActionResult<IEnumerable<Conductor>>> GetConductoresDisponibles(double Latitud, double Longitud) {
            var Ubicacion = new GeoCoordinate(Latitud, Longitud);

            var conductores = await _context.Conductores.ToListAsync();

            return await _context.Conductores.Where(conductor => Ubicacion.GetDistanceTo(new GeoCoordinate(conductor.Latitud, conductor.Longitud)) <= 3000).ToListAsync();
        }

        /// <summary>
        /// Devuelve la información de un conductor especifico por su ID
        /// </summary>
        /// <returns></returns>
        // GET: api/Conductores/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Conductor>> GetConductor(int id)
        {
            var conductor = await _context.Conductores.FindAsync(id);

            if (conductor == null)
            {
                return NotFound();
            }

            return conductor;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // --------------------------------------------------------------------------------------------------------------------- //
        // ----------------------------------------- END OF CUSTOM APPLICATION ACTIONS ----------------------------------------- //
        // --------------------------------------------------------------------------------------------------------------------- //
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // PUT: api/Conductores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConductor(int id, Conductor conductor)
        {
            if (id != conductor.Id)
            {
                return BadRequest();
            }

            _context.Entry(conductor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConductorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Conductores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Conductor>> PostConductor(Conductor conductor)
        {
            _context.Conductores.Add(conductor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConductor", new { id = conductor.Id }, conductor);
        }

        // DELETE: api/Conductores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConductor(int id)
        {
            var conductor = await _context.Conductores.FindAsync(id);
            if (conductor == null)
            {
                return NotFound();
            }

            _context.Conductores.Remove(conductor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ConductorExists(int id)
        {
            return _context.Conductores.Any(e => e.Id == id);
        }
    }
}

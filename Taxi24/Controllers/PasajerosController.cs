using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeoCoordinatePortable;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taxi24.Models;

namespace Taxi24.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasajerosController : ControllerBase
    {
        private readonly Taxi24DBContext _context;

        public PasajerosController(Taxi24DBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Devuelve todos los pasajeros
        /// </summary>
        /// <returns></returns>
        // GET: api/Pasajeros
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pasajero>>> GetAllPasajeros()
        {
            return await _context.Pasajeros.ToListAsync();
        }

        /// <summary>
        /// Devuelve la información del pasajero por su ID
        /// </summary>
        /// <returns></returns>
        // GET: api/Pasajeros/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Pasajero>> GetPasajero(int id)
        {
            var pasajero = await _context.Pasajeros.FindAsync(id);

            if (pasajero == null)
            {
                return NotFound($"Pasajero de id {id} no encontrado");
            }

            return pasajero;
        }

        /// <summary>
        /// Devuelve la información de los tres conductores más cercanos al pasajero con el id respectivo.
        /// </summary>
        /// <returns></returns>
        // GET: api/Pasajeros/ride
        [HttpGet("ride/{id}")]
        public async Task<ActionResult<IEnumerable<Conductor>>> GetConductoresAvailable(int id)
        {
            var pasajero = await _context.Pasajeros.FindAsync(id);

            if (pasajero == null) {
                return NotFound($"Pasajero de id {id} no encontrado");
            }

            var conductores = await _context.Conductores.ToListAsync();

            if(conductores == null || conductores.Count == 0){
                return NotFound($"No hay conductores disponibles");
            }

            var Ubicacion = new GeoCoordinate(pasajero.Latitud, pasajero.Longitud);

            return conductores.OrderBy(conductor => {
                return Ubicacion.GetDistanceTo(new GeoCoordinate(conductor.Latitud, conductor.Longitud));
            }).Take(3).ToList();
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // --------------------------------------------------------------------------------------------------------------------- //
        // ----------------------------------------- END OF CUSTOM APPLICATION ACTIONS ----------------------------------------- //
        // --------------------------------------------------------------------------------------------------------------------- //
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // PUT: api/Pasajeros/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPasajero(int id, Pasajero pasajero)
        {
            if (id != pasajero.Id)
            {
                return BadRequest();
            }

            _context.Entry(pasajero).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PasajeroExists(id))
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

        // POST: api/Pasajeros
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Pasajero>> PostPasajero(Pasajero pasajero)
        {
            _context.Pasajeros.Add(pasajero);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPasajero", new { id = pasajero.Id }, pasajero);
        }

        // DELETE: api/Pasajeros/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePasajero(int id)
        {
            var pasajero = await _context.Pasajeros.FindAsync(id);
            if (pasajero == null)
            {
                return NotFound();
            }

            _context.Pasajeros.Remove(pasajero);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PasajeroExists(int id)
        {
            return _context.Pasajeros.Any(e => e.Id == id);
        }
    }
}

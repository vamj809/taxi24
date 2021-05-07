using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taxi24.Models;

namespace Taxi24.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViajesController : ControllerBase
    {
        private readonly Taxi24DBContext _context;

        public ViajesController(Taxi24DBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Crea una nueva solicitud de viaje sin completar.
        /// </summary>
        /// <returns></returns>
        // POST: api/Viajes
        [HttpPost]
        public async Task<ActionResult<Viaje>> CreateViaje(Viaje viaje)
        {
            //Asegura que el viaje no se envie como completado.
            viaje.Completado = false;

            //Busca al conductor y valida si está disponible
            var conductor = await _context.Conductores.FindAsync(viaje.ConductorId);
            if(conductor == null) {
                return NotFound($"Conductor de id {viaje.ConductorId} no existe.");
            } else if(conductor.Disponible == false) {
                return BadRequest($"Conductor de id {viaje.ConductorId} no está disponible.");
            }

            //Prepara las entidades
            conductor.Disponible = false;
            _context.Entry(conductor).State = EntityState.Modified;

            _context.Viajes.Add(viaje);

            //Guarda los cambios en la DB
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetViaje", new { id = viaje.Id }, viaje);
        }

        /// <summary>
        /// Modifica un viaje de forma que se marque como completado.
        /// </summary>
        /// <returns></returns>
        // PUT: api/Viajes/complete/1
        [HttpPut("complete/{id}")]
        public async Task<IActionResult> CompleteViaje(int id)
        {
            var viaje = await _context.Viajes.FindAsync(id);
            var conductor = await _context.Conductores.FindAsync(viaje.ConductorId);

            viaje.Completado = true;
            conductor.Disponible = true;

            _context.Entry(viaje).State = EntityState.Modified;
            _context.Entry(conductor).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                if (!ViajeExists(id)) {
                    return NotFound();
                }
                else {
                    throw;
                }
            }

            return Ok("Viaje completado.");
        }

        /// <summary>
        /// Devuelve todos los viajes en curso (no-completados)
        /// </summary>
        /// <returns></returns>
        // GET: api/Viajes/active
        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<Viaje>>> GetViajesActivos()
        {
            return await _context.Viajes.Where(v => v.Completado == false).ToListAsync();
        }

        private bool ViajeExists(int id)
        {
            return _context.Viajes.Any(e => e.Id == id);
        }
    }
}

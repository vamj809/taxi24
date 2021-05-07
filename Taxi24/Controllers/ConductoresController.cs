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
        public async Task<ActionResult<IEnumerable<Conductor>>> GetAllConductores()
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

            var resultado = new List<Conductor>();
            var conductores = await _context.Conductores.ToListAsync();

            foreach (var conductor in conductores) {
                if(Ubicacion.GetDistanceTo(new GeoCoordinate(conductor.Latitud, conductor.Longitud)) <= 3000) {
                    resultado.Add(conductor);
                }
            }

            return resultado;
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
    }
}

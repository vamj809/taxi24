using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taxi24.Models;

namespace Taxi24.Controllers
{
    [Route("/")]
    [ApiController]
    public class IndexController : ControllerBase
    {
        private List<string> Controladores;
        public IndexController() {}

        /// <summary>
        /// Devuelve un listado de todos los controladores disponibles
        /// </summary>
        /// <returns></returns>
        // GET: api/Index
        [HttpGet]
        public IEnumerable<string> GetControladores() {

            var type = typeof(ControllerBase);
            var ControllerAssemblies = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(ReflectionAssembly => ReflectionAssembly.GetTypes())
                .Where(Types => type.IsAssignableFrom(Types))
                .ToList();

            Controladores = new List<string>();
            foreach (var controlador in ControllerAssemblies) {
                Controladores.Add(controlador.Name);
            }
            return Controladores;
        }
    }
}

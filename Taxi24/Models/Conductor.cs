using System;
using System.Collections.Generic;

#nullable disable

namespace Taxi24.Models
{
    public partial class Conductor
    {
        public Conductor()
        {
            Viajes = new HashSet<Viaje>();
        }

        public bool? Disponible { get; set; }
        public int Id { get; set; }
        public double? Latitud { get; set; }
        public double? Longitud { get; set; }

        public virtual ICollection<Viaje> Viajes { get; set; }
    }
}

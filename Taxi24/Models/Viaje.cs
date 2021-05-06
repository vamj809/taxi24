using System;
using System.Collections.Generic;

#nullable disable

namespace Taxi24.Models
{
    public partial class Viaje
    {
        public int Id { get; set; }
        public int PasajeroId { get; set; }
        public int? ConductorId { get; set; }
        public bool Completado { get; set; }

        public virtual Conductor Conductor { get; set; }
        public virtual Pasajero Pasajero { get; set; }
    }
}

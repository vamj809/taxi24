﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Taxi24.Models
{
    public partial class Conductor
    {
        public bool? Disponible { get; set; }
        public int Id { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
    }
}

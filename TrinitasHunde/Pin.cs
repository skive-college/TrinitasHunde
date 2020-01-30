using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrinitasHunde
{
    public class Pin
    {
        public string Name { get; set; }
        public double GPSLatitude { get; set; }
        public double GPSLongitude { get; set; }
        public string PinColor { get; set; }
        public string PinType { get; set; }
        public string LocationType { get; set; }
    }
}

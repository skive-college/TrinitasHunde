using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrinitasHunde.Models
{
    public class Pin
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double GPSLatitude { get; set; }
        public double GPSLongitude { get; set; }
        public string PinColor { get; set; }
        public PinType TypePin { get; set; }
        public LocationType TypeLocation { get; set; }
    }
}

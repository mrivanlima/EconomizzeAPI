using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Economizze.Library
{
    public class Address
    {
        public int AddressId { get; set; }
        public int? StreetId { get; set; }
        public string Complement { get; set; } = string.Empty;
        public string ComplementAscii { get; set; } = string.Empty;
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}

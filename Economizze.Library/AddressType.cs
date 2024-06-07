using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Economizze.Library
{
    public class AddressType
    {
        public short AddressTypeId { get; set; }
        public string AddressTypeName { get; set; } = string.Empty;
        public string AddressTypeNameAscii { get; set; } = string.Empty;
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}

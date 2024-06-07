using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Economizze.Library
{
    public class ProductVersion
    {
        public short ProductVersionId { get; set; }
        public string ProductVersionName { get; set; } = string.Empty;
        public string ProductVersionAscii { get; set; } = string.Empty;
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}

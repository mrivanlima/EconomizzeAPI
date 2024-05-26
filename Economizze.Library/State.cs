using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Economizze.Library
{
	public class State
	{
		public short StateId { get; set; }
		public string StateName { get; set; } = string.Empty;
		public string StateNameAscii { get; set; } = string.Empty;
		public double? Longitude { get; set; }
		public double? Latitude { get; set; }
		public int CreatedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public int ModifiedBy { get; set; }
		public DateTime ModifiedOn { get; set; }

	}
}

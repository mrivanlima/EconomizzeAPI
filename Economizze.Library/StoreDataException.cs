using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Economizze.Library
{
    public class StoreDataException
    {
        public int ExceptionID { get; set; }
        public int StoreID { get; set; }
        public string Reason { get; set; } = string.Empty;
        public DateTime ExceptionDate { get; set; }
        public TimeSpan? OpenTime { get; set; } // Nullable to allow for closed stores
        public TimeSpan? CloseTime { get; set; }
        public bool IsHoliday { get; set; }
    }
}

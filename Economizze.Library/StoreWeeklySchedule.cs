using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Economizze.Library
{
    public class StoreWeeklySchedule
    {
        public int ScheduleID { get; set; }
        public int StoreID { get; set; }
        public int DayOfWeek { get; set; } // 1 for Monday, 7 for Sunday
        public TimeSpan OpenTime { get; set; }
        public TimeSpan CloseTime { get; set; }
        public bool Open24 {  get; set; }   
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace SNL_LogicLayer.Models
{
    public class Schedule
    {
        public int ScheduleID { get; set; }
        public string ScheduleName { get; set; }
        public int DivisionID { get; set; }
        public IEnumerable<ScheduleDetail> scheduleDetailsList { get; set; }
    }
}

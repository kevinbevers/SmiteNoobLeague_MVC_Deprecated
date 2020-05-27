using System;
using System.Collections.Generic;
using System.Text;

namespace SNL_LogicLayer.Models
{
    public class ScheduleDetail
    {
        public int ScheduleDetailsID { get; set; }
        public int ScheduleID { get; set; }
        public int HomeTeamID { get; set; }
        public int AwayTeamID { get; set; }
        public int WeekNumber { get; set; }
        public int MatchNumber { get; set; }
    }
}

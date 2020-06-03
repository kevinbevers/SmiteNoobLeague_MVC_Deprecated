using System;
using System.Collections.Generic;
using System.Text;

namespace SNL_InterfaceLayer.DateTransferObjects
{
    public class ScheduleDetailDTO
    {
        public int? ScheduleDetailsID { get; set; }
        public int? ScheduleID {get; set;}
        public int? HomeTeamID { get; set; }
        public int? AwayTeamID { get; set; }
        public int? WeekNumber { get; set; }
        public int? MatchNumber { get; set; }
    }
}

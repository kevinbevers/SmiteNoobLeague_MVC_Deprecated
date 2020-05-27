using System;
using System.Collections.Generic;
using System.Text;

namespace SNL_InterfaceLayer.DateTransferObjects
{
    public class ScheduleDTO
    {
        public int ScheduleID { get; set; }
        public string ScheduleName { get; set; }
        public int DivisionID { get; set; }
        public IEnumerable<ScheduleDetailDTO> scheduleDetailsList { get; set; }
    }
}

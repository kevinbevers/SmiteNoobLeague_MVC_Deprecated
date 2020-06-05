using System;
using System.Collections.Generic;
using System.Text;

namespace SNL_InterfaceLayer.DateTransferObjects
{
    public class StandingDTO
    {
        public int? StandingID { get; set; }
        public int? DivisionID { get; set; }
        public int? TeamID { get; set; }
        public int? Score { get; set; }
    }
}

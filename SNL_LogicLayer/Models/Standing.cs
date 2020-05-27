using System;
using System.Collections.Generic;
using System.Text;

namespace SNL_LogicLayer.Models
{
    public class Standing
    {
        public int StandingID { get; set; }
        public int DivisionID { get; set; }
        public Team Team { get; set; }
        public int Score { get; set; }
    }
}

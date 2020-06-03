using System;
using System.Collections.Generic;
using System.Text;

namespace SNL_LogicLayer.Models
{
    public class Division
    {
        public int? DivisionID { get; set; }
        public string DivisionName { get; set; }
        public string DivisionDescription { get; set; }
        public List<Team> DivisionTeams { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace SNL_LogicLayer.Models
{
    public class Team
    {
        public int? TeamID { get; set; }
        public string TeamName { get; set; }
        public byte[] TeamLogo { get; set; }
        public Division TeamDivision { get; set; }
        public Player TeamCaptain { get; set; }
        public List<Player> TeamMembers { get; set; }
    }
}

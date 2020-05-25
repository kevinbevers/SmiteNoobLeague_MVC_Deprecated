using System;
using System.Collections.Generic;
using System.Text;
using SNL_PersistenceLayer;

namespace SNL_LogicLayer.Models
{
    public class Team
    {
        public int TeamID { get; set; }
        public string TeamName { get; set; }
        public byte[] TeamLogo { get; set; } 
        public int TeamDivisionID { get; set; }
        public int TeamCaptainID { get; set; }
        public int TeamMember2ID { get; set; }
        public int TeamMember3ID { get; set; }
        public int TeamMember4ID { get; set; }
        public int TeamMember5ID { get; set; }
    }
}

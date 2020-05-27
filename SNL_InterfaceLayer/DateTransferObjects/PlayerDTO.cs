using System;
using System.Collections.Generic;
using System.Text;

namespace SNL_InterfaceLayer.DateTransferObjects
{
    public class PlayerDTO
    {
        public int PlayerID { get; set; }
        public string PlayerName { get; set; }
        public int PlayerTeamID { get; set; }
        public int PlayerRoleID { get; set; }
        public int PlayerPlatformID { get; set; }
    }
}

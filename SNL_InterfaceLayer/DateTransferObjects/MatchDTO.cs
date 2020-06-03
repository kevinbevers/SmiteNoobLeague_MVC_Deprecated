using System;
using System.Collections.Generic;
using System.Text;

namespace SNL_InterfaceLayer.DateTransferObjects
{
    public class MatchDTO
    {
        public int? MatchID { get; set; }
        public int? ApiMatchID { get; set; }
        public int? HomeTeamID { get; set; }
        public int? AwayTeamID { get; set; }
        public int? WinningTeamID { get; set; }
        public double MatchLength { get; set; }
        public DateTime MatchDate { get; set; }
        public string PatchNumber { get; set; }
        public IEnumerable<GodDTO> GodBanList { get; set; }
    }
}

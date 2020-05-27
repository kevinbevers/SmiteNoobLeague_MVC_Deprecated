using System;
using System.Collections.Generic;
using System.Text;

namespace SNL_InterfaceLayer.DateTransferObjects
{
    public class DivisionDTO
    {
        public int DivisionID { get; set; }
        public string DivisionName { get; set; }
        public string DivisionDescription { get; set; }
        public IEnumerable<TeamDTO> DivisionTeams { get; set; }
    }
}

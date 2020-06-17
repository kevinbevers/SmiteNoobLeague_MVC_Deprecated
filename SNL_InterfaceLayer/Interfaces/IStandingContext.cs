using System;
using System.Collections.Generic;
using System.Text;
using SNL_InterfaceLayer.DateTransferObjects;

namespace SNL_InterfaceLayer.Interfaces
{
    public interface IStandingContext : IContext<StandingDTO>
    {
        IEnumerable<StandingDTO> GetByDivisionID(int? id);
        IEnumerable<StandingDTO> GetByTeamID(int? id);
    }
}

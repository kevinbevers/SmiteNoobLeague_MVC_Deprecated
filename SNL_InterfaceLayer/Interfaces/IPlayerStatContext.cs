using System;
using System.Collections.Generic;
using System.Text;
using SNL_InterfaceLayer.DateTransferObjects;

namespace SNL_InterfaceLayer.Interfaces
{
    public interface IPlayerStatContext : IContext<PlayerStatDTO>
    {
        int? AddMultiple(IEnumerable<PlayerStatDTO> entityList);
        IEnumerable<PlayerStatDTO> GetByPlayerID(int? id);
        IEnumerable<PlayerStatDTO> GetByGodID(int? id);
        IEnumerable<PlayerStatDTO> GetByTeamID(int? id);
        IEnumerable<PlayerStatDTO> GetByMatchID(int? id);
    }
}

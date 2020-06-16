using System;
using System.Collections.Generic;
using System.Text;
using SNL_InterfaceLayer.DateTransferObjects;

namespace SNL_InterfaceLayer.Interfaces
{
    public interface ITeamContext
    {
        void Add(TeamDTO entity);
        TeamDTO GetByID(int? id);
        IEnumerable<TeamDTO> GetAll();
        void Update(TeamDTO entity);
        void Remove(TeamDTO entity);
    }
}

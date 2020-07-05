using System;
using System.Collections.Generic;
using System.Text;
using SNL_InterfaceLayer.DateTransferObjects;

namespace SNL_InterfaceLayer.Interfaces
{
    public interface ITeamContext : IContext<TeamDTO>
    {
        //extra functions for team context
        bool NameAvailable(string name);
        bool CaptainAvailable(int captainid);
        bool PlayerAvailable(int playerid);
    }
}

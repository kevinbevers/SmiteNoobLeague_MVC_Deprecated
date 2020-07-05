using System;
using System.Collections.Generic;
using System.Text;
using SNL_LogicLayer.Models;

namespace SNL_LogicLayer.ServiceInterfaces
{
    public interface ITeamService : IService<Team>
    {
       bool TeamNameAvailable(string teamname);
       bool CaptainAvailable(int captainid);
       bool PlayerAvailable(int playerid);
       bool IsPlayerPickable(Player player, int? teamID);
    }
}

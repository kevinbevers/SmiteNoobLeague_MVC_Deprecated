using System;
using System.Collections.Generic;
using System.Text;
using SNL_LogicLayer.Models;

namespace SNL_LogicLayer.ServiceInterfaces
{
    public interface ITeamService : IService<Team>
    {
       void GetTeamRecentMatches();
    }
}

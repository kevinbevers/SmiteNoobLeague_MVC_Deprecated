using System;
using System.Collections.Generic;
using System.Text;
using SNL_PersistenceLayer;
using SNL_LogicLayer.Services;
using SNL_PersistenceLayer.Contexts;

namespace SNL_LogicLayer
{
    public class LogicFactory
    {
        private readonly ConnectionContext _conn;
        public LogicFactory(ConnectionContext context)
        {
            _conn = context;
        }

        public TeamService GetTeamService()
        {
            return new TeamService(new TeamContext(_conn), new PlayerContext(_conn), new Rolecontext(_conn), new DivisionContext(_conn));
        }
    }
}

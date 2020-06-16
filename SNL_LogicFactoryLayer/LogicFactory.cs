using System;
using System.Collections.Generic;
using System.Text;
using SNL_PersistenceLayer;
using SNL_LogicLayer.Services;
using SNL_PersistenceLayer.Contexts;
using SNL_LogicLayer.ServiceInterfaces;

namespace SNL_FactoryLayer
{
    public class LogicFactory
    {
        private readonly ConnectionContext _conn;
        public LogicFactory(ConnectionContext context)
        {
            _conn = context;
        }
        
        public ITeamService GetTeamService()
        {
            //factory layer get TeamService
            return new TeamService(new TeamContext(_conn), new PlayerContext(_conn), new Rolecontext(_conn), new DivisionContext(_conn));
        }
    }
}

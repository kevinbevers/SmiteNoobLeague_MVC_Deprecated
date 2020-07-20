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
        private readonly HirezApiContext _api;
        public LogicFactory(ConnectionContext context, HirezApiContext api)
        {
            _conn = context;
            _api = api; 
        }
        
        public ITeamService GetTeamService()
        {
            //factory layer get TeamService
            return new TeamService(new TeamContext(_conn), new PlayerContext(_conn), new Rolecontext(_conn), new DivisionContext(_conn));
        }
        public IAccountService GetAccountService()
        {
            return new AccountService(new PlayerContext(_conn),new AccountContext(_conn));
        }

        public IHirezApiService GetHirezApiService()
        {
            return new HirezApiService(_api, new GodContext(_conn), new ItemContext(_conn));
        }
    }
}

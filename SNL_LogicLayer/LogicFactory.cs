using System;
using System.Collections.Generic;
using System.Text;
using SNL_PersistenceLayer;
using SNL_LogicLayer.Collection;
using SNL_PersistenceLayer.Contexts;

namespace SNL_LogicLayer
{
    public class LogicFactory
    {
        private readonly ConnectionContext _context;
        public LogicFactory(ConnectionContext context)
        {
            _context = context;
        }

        public TeamCollection GetTeamCollection()
        {
            return new TeamCollection(new TeamContext(_context), new PlayerContext(_context));
        }
    }
}

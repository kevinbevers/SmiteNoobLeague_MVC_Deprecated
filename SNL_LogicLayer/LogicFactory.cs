using System;
using System.Collections.Generic;
using System.Text;
using SNL_PersistenceLayer;
using SNL_LogicLayer.Models;
using SNL_LogicLayer.Collection;
using SNL_PersistenceLayer.Repo;

namespace SNL_LogicLayer
{
    public class LogicFactory
    {
        private readonly SmiteNoobLeagueContext _context;
        public LogicFactory(SmiteNoobLeagueContext context)
        {
            _context = context;
            Team = new TeamCollection(new TeamRepo(_context));
        }

        public TeamCollection Team { get; private set; }
    }
}

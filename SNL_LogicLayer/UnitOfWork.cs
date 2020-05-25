using System;
using System.Collections.Generic;
using System.Text;
using SNL_PersistenceLayer;
using SNL_LogicLayer.Models;
using SNL_LogicLayer.WorkClasses;
using SNL_PersistenceLayer.Repo;

namespace SNL_LogicLayer
{
    public class UnitOfWork
    {
        private readonly SmiteNoobLeagueContext _context;
        public UnitOfWork(SmiteNoobLeagueContext context)
        {
            _context = context;
            Team = new Team(new TeamRepo(_context));
        }

        public Team Team { get; private set; }
    }
}

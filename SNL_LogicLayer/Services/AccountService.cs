using SNL_InterfaceLayer.Interfaces;
using SNL_LogicLayer.Models;
using SNL_LogicLayer.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SNL_LogicLayer.Services
{
    public class AccountService : IAccountService
    {
        //contexts
        private readonly IPlayerContext _playerContext;
        private readonly IAccountContext _accountContext;

        public AccountService(IPlayerContext playerContext, IAccountContext accountContext)
        {
            _playerContext = playerContext;
            _accountContext = accountContext;
        }
        public void Add(Account entity)
        {
            throw new NotImplementedException();
        }

        public bool EmailTaken(string email)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Account> GetAll()
        {
            throw new NotImplementedException();
        }

        public Account GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Account entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Account entity)
        {
            throw new NotImplementedException();
        }

        public bool UserNameTaken(string username)
        {
            throw new NotImplementedException();
        }
    }
}

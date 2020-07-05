using SNL_InterfaceLayer.DateTransferObjects;
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
            AccountDTO aDTO = new AccountDTO
            {
                AccountName = entity.AccountName,
                AccountEmail = entity.AccountEmail,
                AccountPassword = entity.AccountPassword,
                PlayerID = entity.AccountPlayer?.PlayerID,
            };
            //add the account to DB
            _accountContext.Add(aDTO);
            //do something with the account player    
            if(entity.AccountPlayer != null)
            {
                var player = entity.AccountPlayer;

                if (_playerContext.GetByID(player.PlayerID).PlayerID == null)
                {
                    _playerContext.Add(new PlayerDTO { PlayerID = player.PlayerID, PlayerName = player.PlayerName, PlayerPlatformID = player.PlayerPlatformID, });
                }
            }
        }
        public IEnumerable<Account> GetAll()
        {
            IEnumerable<AccountDTO> aDTOList = _accountContext.GetAll();
            List<Account> accountList = new List<Account>();

            foreach (var aDTO in aDTOList)
            {
                Account a = BuildAccountModel(aDTO);
                accountList.Add(a);
            }
            return accountList;
        }

        public Account GetByID(int id)
        {
            //team
            AccountDTO aDTO = _accountContext.GetByID(id);

            Account a = BuildAccountModel(aDTO);

            return a;
        }

        public void Remove(Account entity)
        {
            AccountDTO aDTO = new AccountDTO
            {
                AccountID = entity.AccountID
            };
            //remove the account from the DB
            _accountContext.Remove(aDTO);
        }

        public void Update(Account entity)
        {
            AccountDTO aDTO = new AccountDTO
            {
                AccountID = entity.AccountID,
                AccountName = entity.AccountName,
                AccountEmail = entity.AccountEmail,
                AccountPassword = entity.AccountPassword,
                PlayerID = entity.AccountPlayer?.PlayerID,
            };
            //update the account to DB
            _accountContext.Update(aDTO);
            //do something with the account player    
            if (entity.AccountPlayer != null)
            {
                var player = entity.AccountPlayer;

                if (_playerContext.GetByID(player.PlayerID).PlayerID == null)
                {
                    _playerContext.Add(new PlayerDTO { PlayerID = player.PlayerID, PlayerName = player.PlayerName, PlayerPlatformID = player.PlayerPlatformID, });
                }
            }
        }

        public bool UserNameTaken(string username)
        {
            return _accountContext.AccountNameAvailable(username);
        }
        public bool EmailTaken(string email)
        {
            return _accountContext.EmailAvailable(email);
        }

        private Account BuildAccountModel(AccountDTO aDTO)
        {
            PlayerDTO accountPlayer = _playerContext.GetByID(aDTO.PlayerID) ?? new PlayerDTO();

            Account a = new Account
            {
                AccountID = aDTO.AccountID,
                AccountEmail = aDTO.AccountEmail,
                AccountName = aDTO.AccountName,
                AccountPassword = aDTO.AccountPassword,
                AccountPlayer = new Player {PlayerID = accountPlayer.PlayerID, PlayerName = accountPlayer.PlayerName, PlayerPlatformID = accountPlayer.PlayerPlatformID },
            };

            return a;
        }
    }
}

using SNL_LogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SNL_LogicLayer.ServiceInterfaces
{
    interface IAccountService : IService<Account>
    {
        bool UserNameTaken(string username);
        bool EmailTaken(string email);
    }
}

﻿using System;
using System.Collections.Generic;
using System.Text;
using SNL_InterfaceLayer.DateTransferObjects;

namespace SNL_InterfaceLayer.Interfaces
{
    public interface IAccountContext : IContext<AccountDTO>
    {
        bool AccountNameAvailable(string name);
        bool EmailAvailable(string name);
        bool PlayerAvailable(int playerid);
    }
}

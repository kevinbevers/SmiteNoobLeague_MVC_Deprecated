using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace SNL_InterfaceLayer.DateTransferObjects
{
    public class AccountDTO
    {
        public int AccountID { get; set; }
        public string AccountName { get; set; }
        public string AccountEmail { get; set; }
        public string AccountPassword { get; set; }
        public int PlayerID { get; set; }
    }
}

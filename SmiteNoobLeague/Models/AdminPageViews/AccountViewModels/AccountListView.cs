using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmiteNoobLeague.Models.AdminPageViews.AccountViewModels
{
    public class AccountListView
    {
        public string AccountName { get; set; }
        public int? AccountID { get; set; }
        public string AccountEmail { get; set; }
        public string AccountPlayerName { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmiteNoobLeague.Models.AdminPageViews
{
    public class AdminPageViewCollection
    {
        //empty models for create
        public AdminTeamCreateView TeamCreateModel { get; set; }
        public AdminAccountCreateView AccountCreateModel { get; set; }
        //list with models for edit and other shannanigans
        public IEnumerable<AdminTeamEditView> TeamEditList { get; set; }
        public IEnumerable<AdminAccountCreateView> AccountEditList { get; set; }
    }
}

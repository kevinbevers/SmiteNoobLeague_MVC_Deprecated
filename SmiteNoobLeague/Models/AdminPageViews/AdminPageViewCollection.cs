using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmiteNoobLeague.Models.AdminPageViews.TeamViewModels;

namespace SmiteNoobLeague.Models.AdminPageViews
{
    //THIS BADBOY IS NOT NEEDED, i use ajax to get my viewmodels with partial views. so it is loaded dynamically on button click
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

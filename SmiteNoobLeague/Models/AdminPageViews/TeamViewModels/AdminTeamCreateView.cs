using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmiteNoobLeague.Models.AdminPageViews.TeamViewModels
{
    public class AdminTeamCreateView
    {
        public AdminTeamBasicCreateView basic { get; set; }
        public AdminTeamMembersCreateView members { get; set; }
    }
}

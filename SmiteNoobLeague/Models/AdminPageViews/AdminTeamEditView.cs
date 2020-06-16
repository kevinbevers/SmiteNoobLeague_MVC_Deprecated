using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SmiteNoobLeague.HelperClasses;

namespace SmiteNoobLeague.Models.AdminPageViews
{
    public class AdminTeamEditView
    {
        [Required]
        public int TeamID { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a teamname")]
        public string TeamName { get; set; }
        [Required(ErrorMessage = "Please select a teampcaptain")]
        public int TeamCaptainID { get; set; }
        [Required(ErrorMessage = "Please add a team member")]
        public int TeamMember2ID { get; set; }
        [Required(ErrorMessage = "Please add a team member")]
        public int TeamMember3ID { get; set; }
        [Required(ErrorMessage = "Please add a team member")]
        public int TeamMember4ID { get; set; }
        [Required(ErrorMessage = "Please add a team member")]
        public int TeamMember5ID { get; set; }
        public byte[] TeamLogo { get; set; }
    }
}

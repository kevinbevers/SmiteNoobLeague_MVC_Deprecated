using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SmiteNoobLeague.HelperClasses;

namespace SmiteNoobLeague.Models.AdminPageViews
{
    public class AdminTeamEditView
    {
        public int TeamID { get; set; }
        //Team needs a name
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a teamname")]
        public string TeamName { get; set; }
        [Required(ErrorMessage = "Please select a teampcaptain")]
        public int TeamCaptainID { get; set; }
        //Hidden ID inputs, this message will never show in the client
        [Required(ErrorMessage = "Please add a team member")]
        public int TeamMember2ID { get; set; }
        [Required(ErrorMessage = "Please add a team member")]
        public int TeamMember3ID { get; set; }
        [Required(ErrorMessage = "Please add a team member")]
        public int TeamMember4ID { get; set; }
        [Required(ErrorMessage = "Please add a team member")]
        public int TeamMember5ID { get; set; }
        //Team member names for validation
        [Required(ErrorMessage = "Please add a team member")]
        public string TeamMember2Name { get; set; }
        [Required(ErrorMessage = "Please add a team member")]
        public string TeamMember3Name { get; set; }
        [Required(ErrorMessage = "Please add a team member")]
        public string TeamMember4Name { get; set; }
        [Required(ErrorMessage = "Please add a team member")]
        public string TeamMember5Name { get; set; }
        //Team logo not required, it will be replaced with a local image if empty
        public string TeamLogoString64 { get; set; }
        public IFormFile TeamLogoFile { get; set; }
    }
}

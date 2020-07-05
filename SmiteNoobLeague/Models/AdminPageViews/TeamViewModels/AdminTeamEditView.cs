using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmiteNoobLeague.HelperClasses;

namespace SmiteNoobLeague.Models.AdminPageViews.TeamViewModels
{
    public class AdminTeamEditView : AdminBasicTeamEditView
    {
        //Hidden ID inputs, show this error, the only way an ID is filled in is by using the search player button
        [Required(ErrorMessage = "Please search the player with the Smite API and select a player")]
        public int? TeamMember2ID { get; set; }
        [Required(ErrorMessage = "Please search the player with the Smite API and select a player")]
        public int? TeamMember3ID { get; set; }
        [Required(ErrorMessage = "Please search the player with the Smite API and select a player")]
        public int? TeamMember4ID { get; set; }
        [Required(ErrorMessage = "Please search the player with the Smite API and select a player")]
        public int? TeamMember5ID { get; set; }
        //Team member names for validation error message is empty because the ID is auto filled together with the name
        [Required(ErrorMessage = " ")]
        public string TeamMember2Name { get; set; }
        [Required(ErrorMessage = " ")]
        public string TeamMember3Name { get; set; }
        [Required(ErrorMessage = " ")]
        public string TeamMember4Name { get; set; }
        [Required(ErrorMessage = " ")]
        public string TeamMember5Name { get; set; }
    }
}

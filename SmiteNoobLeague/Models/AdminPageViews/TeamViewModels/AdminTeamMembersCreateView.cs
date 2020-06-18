using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmiteNoobLeague.Models.AdminPageViews.TeamViewModels
{
    public class AdminTeamMembersCreateView
    {
        [Required(ErrorMessage = "Please search the player with the Smite API and select a player")]
        public int TeamMember2ID { get; set; }
        [Required(ErrorMessage = "Please search the player with the Smite API and select a player")]
        public int TeamMember3ID { get; set; }
        [Required(ErrorMessage = "Please search the player with the Smite API and select a player")]
        public int TeamMember4ID { get; set; }
        [Required(ErrorMessage = "Please search the player with the Smite API and select a player")]
        public int TeamMember5ID { get; set; }
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

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmiteNoobLeague.Models.AdminPageViews.TeamViewModels
{
    public class AdminTeamCreateView : AdminTeamBasicCreateView
    {
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
        [Required(ErrorMessage = " ")]
        public int? TeamMember2PlatformID { get; set; }
        [Required(ErrorMessage = " ")]
        public int? TeamMember3PlatformID { get; set; }
        [Required(ErrorMessage = " ")]
        public int? TeamMember4PlatformID { get; set; }
        [Required(ErrorMessage = " ")]
        public int? TeamMember5PlatformID { get; set; }
        public bool tm2Empty
        {
            get
            {
                return (
                        string.IsNullOrWhiteSpace(TeamMember2Name) &&
                        TeamMember2PlatformID == null &&
                        TeamMember2ID == null);
            }
        }
        public bool tm3Empty
        {
            get
            {
                return (
                        string.IsNullOrWhiteSpace(TeamMember3Name) &&
                        TeamMember3PlatformID == null &&
                        TeamMember3ID == null);
            }
        }
        public bool tm4Empty
        {
            get
            {
                return (
                        string.IsNullOrWhiteSpace(TeamMember4Name) &&
                        TeamMember4PlatformID == null &&
                        TeamMember4ID == null);
            }
        }
        public bool tm5Empty
        {
            get
            {
                return (
                        string.IsNullOrWhiteSpace(TeamMember5Name) &&
                        TeamMember5PlatformID == null &&
                        TeamMember5ID == null);
            }
        }
    }
}

using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using SmiteNoobLeague.HelperClasses;

namespace SmiteNoobLeague.Models.AdminPageViews
{
    public class AdminTeamCreateView
    {
        public int TeamID { get; set; }
        //Team needs a name
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a teamname")]
        public string TeamName { get; set; }
        [Required(ErrorMessage = "Please select a teampcaptain")]
        public int TeamCaptainID { get; set; }
        //Hidden ID inputs, this message will never show in the client
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
        //Team logo not required, it will be replaced with a local image if empty
        [MaxFileSize(29 * 1024 * 1024)]
        [AllowedExtensions(new[] { ".jpg", ".png", ".jpeg,", ".bmp" })]
        public IFormFile TeamLogoFile { get; set; }  
    }
}

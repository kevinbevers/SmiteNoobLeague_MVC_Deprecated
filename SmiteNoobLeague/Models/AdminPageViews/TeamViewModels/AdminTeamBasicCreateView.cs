using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using SmiteNoobLeague.HelperClasses;

namespace SmiteNoobLeague.Models.AdminPageViews.TeamViewModels
{
    public class AdminTeamBasicCreateView
    {
        public int TeamID { get; set; }
        //Team needs a name
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a teamname")]
        public string TeamName { get; set; }
        [Required(ErrorMessage = "Please select a teampcaptain")]
        public int TeamCaptainID { get; set; }
        //Team logo not required, it will be replaced with a local image if empty
        [MaxFileSize(29 * 1024 * 1024)]
        [AllowedExtensions(new[] { ".jpg", ".png", ".jpeg,", ".bmp" })]
        public IFormFile TeamLogoFile { get; set; }  
    }
}

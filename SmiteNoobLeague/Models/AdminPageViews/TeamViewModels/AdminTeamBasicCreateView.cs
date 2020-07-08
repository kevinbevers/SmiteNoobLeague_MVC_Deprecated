using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using SmiteNoobLeague.HelperClasses;
using SNL_FactoryLayer;
using SNL_LogicLayer.ServiceInterfaces;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SmiteNoobLeague.Models.AdminPageViews.TeamViewModels
{
    public class AdminTeamBasicCreateView
    {      
        [Key]
        public int TeamID { get; set; }
        //Team needs a name
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a teamname")]
        [MinLength(3, ErrorMessage = "Teamname needs to be atleast 3 characters long")]
        [Remote(action: "TeamNameTaken", controller: "Admin")]
        public string TeamName { get; set; }
        [Required(ErrorMessage = "Please select a teamcaptain")]
        [Remote(action: "CaptainTaken", controller: "Admin")]
        public int TeamCaptainID { get; set; }
        //Team logo not required, it will be replaced with a local image if empty
        [MaxFileSize(29 * 1024 * 1024)]
        [AllowedExtensions(new[] { ".jpg", ".png", ".jpeg,", ".bmp" })]
        public IFormFile TeamLogoFile { get; set; }
        //list to select captains
        public List<SelectListItem> CaptainsList { get; set; }
    }
}

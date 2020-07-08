using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmiteNoobLeague.HelperClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmiteNoobLeague.Models.AdminPageViews.TeamViewModels
{
    public class AdminBasicTeamEditView
    {
        [Required]
        [Key]
        public int? TeamID { get; set; }
        //Team needs a name
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a teamname")]
        [MinLength(3, ErrorMessage = "Teamname needs to be atleast 3 characters long")]
        [Remote(action: "TeamNameTakenEdit", controller: "Admin", AdditionalFields = nameof(TeamID))]
        public string TeamName { get; set; }
        [Required(ErrorMessage = "Please select a teamcaptain")]
        [Remote(action: "CaptainTakenEdit", controller: "Admin", AdditionalFields = nameof(TeamID))]
        public int? TeamCaptainID { get; set; }
        //Team logo not required, it will be replaced with a local image if empty
        [MaxFileSize(29 * 1024 * 1024)]
        [AllowedExtensions(new[] { ".jpg", ".png", ".jpeg,", ".bmp" })]
        public IFormFile TeamLogoFile { get; set; }
        public string TeamLogoString64 { get; set; }
        public List<SelectListItem> CaptainsList { get; set; }
    }
}

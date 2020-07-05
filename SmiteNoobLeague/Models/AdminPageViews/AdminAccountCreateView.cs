using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmiteNoobLeague.Models.AdminPageViews
{
    public class AdminAccountCreateView
    {
        [Key]
        public int AccountID { get; set; }
        [Required(ErrorMessage = "Please enter an accountname")]
        [MinLength(8, ErrorMessage = "Accountname needs to be atleast 8 characters long")]
        [Remote(action: "UserNameTaken", controller: "Admin")]
        public string AccountName { get; set; }
        [Required(ErrorMessage = "Please enter an e-mailaddress")]
        [Remote(action: "EmailTaken", controller: "Admin")]
        public string AccountEmail { get; set; }
        [Required(ErrorMessage = "Please enter a password or generate one"), MinLength(8), MaxLength(30)]
        public string AccountPassword { get; set; } //only for input not output
        [Required(ErrorMessage = "Please search the player with the Smite API and select a player")]
        public int PlayerID { get; set; }
        [Required(ErrorMessage = " ")] //empty, when the ID is filled in the name and platform ID are also filled in
        public string PlayerName { get; set; }
        [Required(ErrorMessage = " ")]
        public int PlayerPlatformID { get; set; }
    }
}

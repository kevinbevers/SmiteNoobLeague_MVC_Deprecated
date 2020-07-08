using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmiteNoobLeague.Models.AdminPageViews.AccountViewModels
{
    public class AdminAccountEditView
    {
        [Key]
        [Required]
        public int? AccountID { get; set; }
        [Required(ErrorMessage = "Please enter an accountname")]
        [MinLength(8, ErrorMessage = "Accountname needs to be atleast 8 characters long")]
        [Remote(action: "UserNameTakenEdit", controller: "Admin", AdditionalFields = nameof(AccountID))]
        public string AccountName { get; set; }
        [Required(ErrorMessage = "Please enter an e-mailaddress")]
        [Remote(action: "EmailTakenEdit", controller: "Admin", AdditionalFields = nameof(AccountID))]
        [DataType(DataType.EmailAddress)]
        public string AccountEmail { get; set; }
        [MinLength(8, ErrorMessage = "Password needs to be atleast 8 long"), MaxLength(30, ErrorMessage = "Password can't be longer then 30")]
        [DataType(DataType.Password)]
        public string AccountPassword { get; set; } //only for input not output
        [Required(ErrorMessage = "Please search the player with the Smite API and select a player")]
        public int? PlayerID { get; set; }
        [Required(ErrorMessage = " ")] //empty, when the ID is filled in the name and platform ID are also filled in
        [Remote(action: "PlayerTakenEdit", controller: "Admin", AdditionalFields = nameof(AccountID))]
        public string PlayerName { get; set; }
        [Required(ErrorMessage = " ")]
        public int? PlayerPlatformID { get; set; }
        public bool PlayerEmpty
        {
            get
            {
                return (
                        string.IsNullOrWhiteSpace(PlayerName) &&
                        PlayerPlatformID == null &&
                        PlayerID == null);
            }
        }
    }
}

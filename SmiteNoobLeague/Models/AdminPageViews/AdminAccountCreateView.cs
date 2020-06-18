using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmiteNoobLeague.Models.AdminPageViews
{
    public class AdminAccountCreateView
    {
        public int AccountID { get; set; }
        [Required, MinLength(8)]
        public string AccountName { get; set; }
        [Required]
        public string AccountEmail { get; set; }
        [Required]
        public string AccountPassword { get; set; } //only for input not output
        [Required]
        public int PlayerID { get; set; }
        [Required]
        public string PlayerName { get; set; }
    }
}

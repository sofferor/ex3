using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models {
    public class User {
        [Key]
        public int UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Range(0, int.MaxValue)]
        public int Wins { get; set; }
        [Range(0, int.MaxValue)]
        public int Loses { get; set; }
        [Range(1, int.MaxValue)]
        public int Rank { get; set; }

    }
}
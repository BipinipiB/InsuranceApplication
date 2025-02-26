using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Models.Models
{
    public  class AdminModel : ApplicationUser
    {
        [Required,StringLength(100, MinimumLength = 6)]
        public string? Password { get; set; }

        [Required,Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }
    }
}

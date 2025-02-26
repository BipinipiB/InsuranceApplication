using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Insurance.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required, NotNull]
        public string FirstName { get; set; }

        [Required, NotNull]
        public string LastName { get; set; }

        [Required,NotNull]
        public string Email {  get; set; }
    }
}

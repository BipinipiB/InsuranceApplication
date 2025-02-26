using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Insurance.Models.Models
{
    public class RegisterCustomerModel
    {

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        
        public string? PolicyNumber { get; set; }

        public int? PolicyId { get; set; }


        //PolicyType is a Foreign Key
        //[Required, NotNull]
        public int? PolicyTypeId { get; set; }

        public PolicyType? PolicyType { get; set; }


        //[Required, NotNull]
        public string? Premium { get; set; }
        
//        [Required, NotNull]
        public DateTime StartDate { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem>? PolicyTypeList { get; set; }

    }
}

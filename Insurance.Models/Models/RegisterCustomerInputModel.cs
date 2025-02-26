using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Models.Models
{
    public class RegisterCustomerInputModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public int? PolicyTypeId { get; set; }

        public string? PolicyNumber { get; set; }

        public int? PolicyId { get; set; }

        [Required]
        public string Premium { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
    }
}

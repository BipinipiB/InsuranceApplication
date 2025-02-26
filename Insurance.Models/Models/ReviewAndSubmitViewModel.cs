
using System.ComponentModel.DataAnnotations;

namespace Insurance.Models.Models
{
    public class ReviewAndSubmitViewModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public PolicyType? SelectedPolicyType { get; set; }

        [Required]
        public string Premium { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
    }
}

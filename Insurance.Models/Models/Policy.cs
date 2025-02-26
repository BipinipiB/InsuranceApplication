using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Insurance.Models.Models
{
    public class Policy
    {
        [Required, NotNull]
        public int PolicyId { get; set; }

        [Required, NotNull]
        public string? PolicyNumber { get; set; }

        //PolicyType is a Foreign Key
        [Required, NotNull]
        public int? PolicyTypeId { get; set; }

        public PolicyType? PolicyType { get; set; }

        [Required, NotNull]
        public string? Premium { get; set; }

        [Required, NotNull]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsActive { get; set; }

        // Foreign key
        [Required, NotNull]
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }

        // Foreign key
        [Required, NotNull]
        public string? CreatedById { get; set; } 
        public ApplicationUser? CreatedBy { get; set; }

        // Foreign key
        [Required, NotNull]
        public string? UpdatedById { get; set; } 
        public ApplicationUser? UpdatedBy { get; set; } 

        [Required, NotNull]
        public DateTime? CreatedOn { get; set; }

        [Required, NotNull]
        public DateTime? UpdatedOn { get; set; }
    }


}

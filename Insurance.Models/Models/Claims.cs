using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Models.Models
{
    public class Claims
    {
        //primaryKey
        [Required, NotNull]
        public int Id { get; set; }

        //ClaimNumber
        [Required, NotNull]
        public string ClaimNumber { get; set; }

        //ClaimStatusId
        //Explicityly link "status" to the claimstatustable table
        [ForeignKey("ClaimStatusId")]
        public int ClaimStatusId { get; set; }
        public ClaimStatuses? claimStatus { get; set; }

        public DateTime SubmittedOn{ get; set; }

        // Foreign key
        [Required, NotNull]
        //Explicityly link "submittedby" to the ApplicationUser table
        [ForeignKey("Submitter")]
        public string? SubmittedBy { get; set; }
        public ApplicationUser? Submitter { get; set; }

        // Foreign key
        [Required, NotNull]
        //Explicityly link "submittedby" to the ApplicationUser table
        [ForeignKey("Updater")]
        public string? UpdatedBy { get; set; }
        public ApplicationUser? Updater { get; set; }


        [Required, NotNull]
        public DateTime UpdatedOn { get; set; }


    }
}

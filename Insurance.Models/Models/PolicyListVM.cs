using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Models.Models
{
    public  class PolicyListVM
    {

        [Required, NotNull]
        public int PolicyId { get; set; }

        [Required, NotNull]
        public string? PolicyNumber { get; set; }

        public string PolicyType { get; set; }

        [Required, NotNull]
        public string? Premium { get; set; }

        [Required, NotNull]
        public DateTime StartDate { get; set; }

        public bool IsActive { get; set; }

        // Foreign key
        [Required, NotNull]
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }

        public string PolicyHolderName { get; set; }


        public string PolicyHolderEmail { get; set; }





    }
}

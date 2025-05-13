using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Models.Models
{
    public class ClaimStatuses
    {
        [Required, NotNull]
        public int Id { get; set; }

        [Required, NotNull]
        public string Name { get; set; }

        [Required, NotNull]
        public bool IsActive { get; set; }

    }
}

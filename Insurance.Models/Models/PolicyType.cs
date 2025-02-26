using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Insurance.Models.Models
{
    public class PolicyType
    {

        [Required, NotNull]
        public int Id { get; set; }

        [Required, NotNull]
        public string? Name { get; set; }

        [Required, NotNull]
        public bool IsActive { get; set; }
    }
}

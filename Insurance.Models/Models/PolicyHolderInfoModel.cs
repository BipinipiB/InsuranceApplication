using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Models.Models
{
    public class PolicyHolderInfoModel
    {
        public string? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string? Email { get; set; }

        public string? PolicyNumber { get; set; }

        public string? PolicyType { get; set; }

        public string? StartDate { get; set; }


    }
}


using Microsoft.AspNetCore.Mvc.Rendering;

namespace Insurance.Models.Models
{
    public class PolicyVM
    {

        public Policy Policy { get; set; }

        public IEnumerable<SelectListItem> PolicyTypeList { get; set; }
    }
}

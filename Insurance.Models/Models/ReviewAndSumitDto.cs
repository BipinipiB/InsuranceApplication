using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Models.Models
{
    public class ReviewAndSumitDto
    {

        public List<QuestionDto> ClaimInfo { get; set; }

        public List<QuestionDto> PaymentInfo { get; set; }
    }
}

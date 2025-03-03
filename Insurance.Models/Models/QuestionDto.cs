using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Models.Models
{
    public class QuestionDto
    {

        public int Id { get; set; }

        //max lenght is 1024 chars for question column
        [StringLength(1024)]
        public string QuestionLabel { get; set; }

        public int QuestionTypeId { get; set; }

        public string QuestionTypeName { get; set; }
        public string Step { get; set; }

        public bool IsActive { get; set; }


        // Properties to capture different types of answers
        public string TextAnswer { get; set; }
        public bool? YesNoAnswer { get; set; }
        //public DateTime? DateAnswer { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Models.Models
{
    public class Question
    {

        public int Id { get; set; }

        //max lenght is 1024 chars for question column
        [StringLength(1024)]
        public string QuestionLabel { get; set; }

        //QuestionTypeId Column is a Foreign Key
        [Required, NotNull]
        public int? QuestionTypeId { get; set; } 

        public QuestionTypeEntity? QuestionType { get; set; }


        public string Step { get; set; }
        public bool IsActive { get; set; }


    }


}

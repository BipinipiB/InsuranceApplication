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
    public class ClaimQuestionAnswer
    {
        //primary key
        [Required,NotNull]
        public int id { get; set; }

        //ClaimId
        [Required, NotNull]
        //Explicityly link "submittedby" to the ApplicationUser table
        [ForeignKey("Claims")]
        public int? ClaimId { get; set; }
        public Claims? claim { get; set; }

        //QuestionId
        [Required, NotNull]
        //Explicityly link "submittedby" to the ApplicationUser table
        [ForeignKey("Question")]
        public int? Questionid { get; set; }
        public Question? question { get; set; }

        //AnswerInJson
        //store answer in Json format--future proof--no need to add new column when adding new question type in future
        [Column(TypeName = "TEXT")] // this line is to store json in column without text restriction in SQL server
        public string AnswerJson { get; set; } // Stores JSON answers, including long text responses

        /* Example
         * 
         * {
            "MultipleChoice": ["Option A", "Option B"],
            "TrueFalse": true,
            "Text": "Detailed explanation here...",
            "YesNo": "Yes"
             }
        */
    }



    
}

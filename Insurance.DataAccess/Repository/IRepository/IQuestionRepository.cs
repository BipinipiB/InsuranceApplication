using Insurance.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.DataAccess.Repository.IRepository
{
    public interface IQuestionRepository
    {
        //returns all active questions
         List<QuestionDto> GetAllActiveQuestions();
    }
}

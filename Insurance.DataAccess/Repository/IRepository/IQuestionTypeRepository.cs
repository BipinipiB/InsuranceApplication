using Insurance.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.DataAccess.Repository.IRepository
{
    public interface IQuestionTypeRepository
    {

        List<QuestionTypeDto> GetAllQuestionTypes();

        QuestionTypeDto? GetQuestionTypeById(int id);
    }
}

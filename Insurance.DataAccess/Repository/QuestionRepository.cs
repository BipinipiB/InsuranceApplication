using Insurance.DataAccess.Data;
using Insurance.DataAccess.Repository.IRepository;
using Insurance.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.DataAccess.Repository
{
    public class QuestionRepository : IQuestionRepository
    {

        private readonly ApplicationDbContext _db;

        public QuestionRepository(ApplicationDbContext db)
        {

            _db = db;
        }

        public List<QuestionDto> GetAllActiveQuestions()
        {
            var QuestionsObj = _db.Questions.Where(x => x.IsActive == true).ToList();

            List<QuestionDto> QuestionsDto = new();

            foreach (var obj in QuestionsObj)
            {
                QuestionDto questionDto = new()
                {
                    Id = obj.Id,
                    QuestionLabel = obj.QuestionLabel,
                    QuestionTypeName = _db.QuestionTypeEntities.FirstOrDefault(x=>x.Id == obj.QuestionTypeId).Name,
                    QuestionTypeId = (int)obj.QuestionTypeId,
                    Step = obj.Step,
                    IsActive = obj.IsActive
                };
                QuestionsDto.Add(questionDto);
            }

            return QuestionsDto;
        }
    }
}

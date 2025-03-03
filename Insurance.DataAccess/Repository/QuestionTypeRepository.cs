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
    public class QuestionTypeRepository : IQuestionTypeRepository
    {

        private static ApplicationDbContext _db;

        public QuestionTypeRepository(ApplicationDbContext db)
        {
            _db = db;
           
        }

        public List<QuestionTypeDto> GetAllQuestionTypes()
        {

            var QuestionTypeObj = _db.QuestionTypeEntities.Where(x=>x.IsActive == true).ToList();


            List<QuestionTypeDto> QuestionTypeList = new();

            foreach (var obj in QuestionTypeObj)
            {
                QuestionTypeDto questionTypeDto = new()
                {
                    Id = obj.Id,
                    Name = obj.Name,
                    IsActive = obj.IsActive
                };
                QuestionTypeList.Add(questionTypeDto);

            }

            return QuestionTypeList;

        }

        public QuestionTypeDto GetQuestionTypeById(int id)
        {

            var questionTypeObj = _db.QuestionTypeEntities.FirstOrDefault(x => x.Id == id);

            QuestionTypeDto questionTypeDto  = new();

            if(questionTypeObj == null)
            {
                return questionTypeDto;
            }
            else
            {
                questionTypeDto = new()
                {
                    Id = questionTypeObj.Id,
                    Name = questionTypeObj.Name,
                    IsActive = questionTypeObj.IsActive
                };

                return questionTypeDto;
            }
                
        }
    }

}
            
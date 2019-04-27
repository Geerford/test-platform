using Application.Interfaces;
using Core;
using Interfaces;
using System.Collections.Generic;

namespace Services.Business.Service
{
    public class QuestionService : IQuestionService
    {
        IUnitOfWork Database { get; set; }

        public QuestionService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Create(Question model)
        {
            Database.Question.Create(model);
            Database.Save();
        }

        public void Delete(Question model)
        {
            Database.Question.Delete(model.ID);
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public void Edit(Question model)
        {
            Database.Question.Update(model);
            Database.Save();
        }

        public Question Get(int? id)
        {
            if (id == null)
            {
                throw new ValidationException("Не задан ID", "");
            }
            Question item = Database.Question.Get(id.Value);
            if (item == null)
            {
                throw new ValidationException("Сущность не найдена", "");
            }
            return item;
        }

        public IEnumerable<Question> GetAll()
        {
            return Database.Question.GetAll();
        }
    }
}
using Application.Interfaces;
using Core;
using Interfaces;
using System.Collections.Generic;

namespace Services.Business.Service
{
    public class AnswerService : IAnswerService
    {
        IUnitOfWork Database { get; set; }

        public AnswerService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Create(Answer model)
        {
            Database.Answer.Create(model);
            Database.Save();
        }

        public void Delete(Answer model)
        {
            Database.Answer.Delete(model.ID);
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public void Edit(Answer model)
        {
            Database.Answer.Update(model);
            Database.Save();
        }

        public Answer Get(int? id)
        {
            if (id == null)
            {
                throw new ValidationException("Не задан ID", "");
            }
            Answer item = Database.Answer.Get(id.Value);
            if (item == null)
            {
                throw new ValidationException("Сущность не найдена", "");
            }
            return item;
        }

        public IEnumerable<Answer> GetAll()
        {
            return Database.Answer.GetAll();
        }
    }
}
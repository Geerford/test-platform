using Application.Interfaces;
using Core;
using Interfaces;
using System.Collections.Generic;

namespace Services.Business.Service
{
    public class GradeService : IGradeService
    {
        IUnitOfWork Database { get; set; }

        public GradeService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Create(Grade model)
        {
            Database.Grade.Create(model);
            Database.Save();
        }

        public void Delete(Grade model)
        {
            Database.Grade.Delete(model.ID);
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public void Edit(Grade model)
        {
            Database.Grade.Update(model);
            Database.Save();
        }

        public Grade Get(int? id)
        {
            if (id == null)
            {
                throw new ValidationException("Не задан ID", "");
            }
            Grade item = Database.Grade.Get(id.Value);
            if (item == null)
            {
                throw new ValidationException("Сущность не найдена", "");
            }
            return item;
        }

        public IEnumerable<Grade> GetAll()
        {
            return Database.Grade.GetAll();
        }
    }
}
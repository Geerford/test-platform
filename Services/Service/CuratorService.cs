using Application.Interfaces;
using Core;
using Interfaces;
using System.Collections.Generic;

namespace Services.Business.Service
{
    public class CuratorService : ICuratorService
    {
        IUnitOfWork Database { get; set; }

        public CuratorService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Create(Curator model)
        {
            Database.Curator.Create(model);
            Database.Save();
        }

        public void Delete(Curator model)
        {
            Database.Curator.Delete(model.ID);
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public void Edit(Curator model)
        {
            Database.Curator.Update(model);
            Database.Save();
        }

        public Curator Get(int? id)
        {
            if (id == null)
            {
                throw new ValidationException("Не задан ID", "");
            }
            Curator item = Database.Curator.Get(id.Value);
            if (item == null)
            {
                throw new ValidationException("Сущность не найдена", "");
            }
            return item;
        }

        public IEnumerable<Curator> GetAll()
        {
            return Database.Curator.GetAll();
        }
    }
}
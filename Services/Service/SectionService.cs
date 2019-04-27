using Application.Interfaces;
using Core;
using Interfaces;
using System.Collections.Generic;

namespace Services.Business.Service
{
    public class SectionService : ISectionService
    {
        IUnitOfWork Database { get; set; }

        public SectionService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Create(Section model)
        {
            Database.Section.Create(model);
            Database.Save();
        }

        public void Delete(Section model)
        {
            Database.Section.Delete(model.ID);
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public void Edit(Section model)
        {
            Database.Section.Update(model);
            Database.Save();
        }

        public Section Get(int? id)
        {
            if (id == null)
            {
                throw new ValidationException("Не задан ID", "");
            }
            Section item = Database.Section.Get(id.Value);
            if (item == null)
            {
                throw new ValidationException("Сущность не найдена", "");
            }
            return item;
        }

        public IEnumerable<Section> GetAll()
        {
            return Database.Section.GetAll();
        }
    }
}
using Application.Interfaces;
using Core;
using Interfaces;
using System.Collections.Generic;

namespace Services.Business.Service
{
    public class TemplateService : ITemplateService
    {
        IUnitOfWork Database { get; set; }

        public TemplateService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Create(Template model)
        {
            Database.Template.Create(model);
            Database.Save();
        }

        public void Delete(Template model)
        {
            Database.Template.Delete(model.ID);
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public void Edit(Template model)
        {
            Database.Template.Update(model);
            Database.Save();
        }

        public Template Get(int? id)
        {
            if (id == null)
            {
                throw new ValidationException("Не задан ID", "");
            }
            Template item = Database.Template.Get(id.Value);
            if (item == null)
            {
                throw new ValidationException("Сущность не найдена", "");
            }
            return item;
        }

        public IEnumerable<Template> GetAll()
        {
            return Database.Template.GetAll();
        }
    }
}
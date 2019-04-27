using Application.Interfaces;
using Core;
using Interfaces;
using System.Collections.Generic;

namespace Services.Business.Service
{
    public class ActivityService : IActivityService
    {
        IUnitOfWork Database { get; set; }

        public ActivityService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Create(Activity model)
        {
            Database.Activity.Create(model);
            Database.Save();
        }

        public void Delete(Activity model)
        {
            Database.Activity.Delete(model.ID);
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public void Edit(Activity model)
        {
            Database.Activity.Update(model);
            Database.Save();
        }

        public Activity Get(int? id)
        {
            if (id == null)
            {
                throw new ValidationException("Не задан ID", "");
            }
            Activity item = Database.Activity.Get(id.Value);
            if (item == null)
            {
                throw new ValidationException("Сущность не найдена", "");
            }
            return item;
        }

        public IEnumerable<Activity> GetAll()
        {
            return Database.Activity.GetAll();
        }
    }
}
using Application.Interfaces;
using Core;
using Interfaces;
using System.Collections.Generic;

namespace Services.Business.Service
{
    public class TaskService : ITaskService
    {
        IUnitOfWork Database { get; set; }

        public TaskService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Create(Task model)
        {
            Database.Task.Create(model);
            Database.Save();
        }

        public void Delete(Task model)
        {
            Database.Task.Delete(model.ID);
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public void Edit(Task model)
        {
            Database.Task.Update(model);
            Database.Save();
        }

        public Task Get(int? id)
        {
            if (id == null)
            {
                throw new ValidationException("Не задан ID", "");
            }
            Task item = Database.Task.Get(id.Value);
            if (item == null)
            {
                throw new ValidationException("Сущность не найдена", "");
            }
            return item;
        }

        public IEnumerable<Task> GetAll()
        {
            return Database.Task.GetAll();
        }
    }
}
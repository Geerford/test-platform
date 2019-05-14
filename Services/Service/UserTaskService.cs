using Application.Interfaces;
using Core;
using Interfaces;
using System.Collections.Generic;

namespace Services.Business.Service
{
    public class UserTaskService : IUserTaskService
    {
        IUnitOfWork Database { get; set; }

        public UserTaskService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Create(UserTask model)
        {
            Database.UserTask.Create(model);
            Database.Save();
        }

        public void Delete(UserTask model)
        {
            Database.UserTask.Delete(model.UserID, model.TaskID);
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public void Edit(UserTask model)
        {
            Database.UserTask.Update(model);
            Database.Save();
        }

        public UserTask Get(int? userID, int? taskID)
        {
            if (userID == null || taskID == null)
            {
                throw new ValidationException("Не задан ID", "");
            }
            UserTask item = Database.UserTask.Get(userID.Value, taskID.Value);
            if (item == null)
            {
                throw new ValidationException("Сущность не найдена", "");
            }
            return item;
        }

        public IEnumerable<UserTask> GetAll()
        {
            return Database.UserTask.GetAll();
        }
    }
}
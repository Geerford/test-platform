using Application.Interfaces;
using Core;
using Interfaces;
using System.Collections.Generic;

namespace Services.Business.Service
{
    public class UserService : IUserService
    {
        IUnitOfWork Database { get; set; }

        public UserService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Create(User model)
        {
            Database.User.Create(model);
            Database.Save();
        }

        public void Delete(User model)
        {
            Database.User.Delete(model.ID);
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public void Edit(User model)
        {
            Database.User.Update(model);
            Database.Save();
        }

        public User Get(int? id)
        {
            if (id == null)
            {
                throw new ValidationException("Не задан ID", "");
            }
            User item = Database.User.Get(id.Value);
            if (item == null)
            {
                throw new ValidationException("Сущность не найдена", "");
            }
            return item;
        }

        public IEnumerable<User> GetAll()
        {
            return Database.User.GetAll();
        }
    }
}
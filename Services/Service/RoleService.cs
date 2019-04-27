using Application.Interfaces;
using Core;
using Interfaces;
using System.Collections.Generic;

namespace Services.Business.Service
{
    public class RoleService : IRoleService
    {
        IUnitOfWork Database { get; set; }

        public RoleService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Create(Role model)
        {
            Database.Role.Create(model);
            Database.Save();
        }

        public void Delete(Role model)
        {
            Database.Role.Delete(model.ID);
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public void Edit(Role model)
        {
            Database.Role.Update(model);
            Database.Save();
        }

        public Role Get(int? id)
        {
            if (id == null)
            {
                throw new ValidationException("Не задан ID", "");
            }
            Role item = Database.Role.Get(id.Value);
            if (item == null)
            {
                throw new ValidationException("Сущность не найдена", "");
            }
            return item;
        }

        public IEnumerable<Role> GetAll()
        {
            return Database.Role.GetAll();
        }
    }
}
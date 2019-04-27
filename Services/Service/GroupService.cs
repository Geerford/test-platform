using Application.Interfaces;
using Core;
using Interfaces;
using System.Collections.Generic;

namespace Services.Business.Service
{
    public class GroupService : IGroupService
    {
        IUnitOfWork Database { get; set; }

        public GroupService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Create(Group model)
        {
            Database.Group.Create(model);
            Database.Save();
        }

        public void Delete(Group model)
        {
            Database.Group.Delete(model.ID);
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public void Edit(Group model)
        {
            Database.Group.Update(model);
            Database.Save();
        }

        public Group Get(int? id)
        {
            if (id == null)
            {
                throw new ValidationException("Не задан ID", "");
            }
            Group item = Database.Group.Get(id.Value);
            if (item == null)
            {
                throw new ValidationException("Сущность не найдена", "");
            }
            return item;
        }

        public IEnumerable<Group> GetAll()
        {
            return Database.Group.GetAll();
        }
    }
}
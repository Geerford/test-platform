using Application.Interfaces;
using Core;
using Interfaces;
using System.Collections.Generic;

namespace Services.Business.Service
{
    public class GroupSectionService : IGroupSectionService
    {
        IUnitOfWork Database { get; set; }

        public GroupSectionService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Create(GroupSection model)
        {
            Database.GroupSection.Create(model);
            Database.Save();
        }

        public void Delete(GroupSection model)
        {
            Database.GroupSection.Delete(model.GroupID, model.SectionID);
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public void Edit(GroupSection model)
        {
            Database.GroupSection.Update(model);
            Database.Save();
        }

        public GroupSection Get(int? groupID, int? sectionID)
        {
            if (groupID == null || sectionID == null)
            {
                throw new ValidationException("Не задан ID", "");
            }
            GroupSection item = Database.GroupSection.Get((int)groupID, (int)sectionID);
            if (item == null)
            {
                throw new ValidationException("Сущность не найдена", "");
            }
            return item;
        }

        public IEnumerable<GroupSection> GetAll()
        {
            return Database.GroupSection.GetAll();
        }
    }
}
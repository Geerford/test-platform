using Core;
using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface IGroupSectionService
    {
        void Create(GroupSection model);

        void Delete(GroupSection model);

        void Dispose();

        void Edit(GroupSection model);

        GroupSection Get(int? groupID, int? sectionID);

        IEnumerable<GroupSection> GetAll();
    }
}
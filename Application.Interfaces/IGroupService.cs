using Core;
using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface IGroupService
    {
        void Create(Group model);

        void Delete(Group model);

        void Dispose();

        void Edit(Group model);

        Group Get(int? id);

        IEnumerable<Group> GetAll();
    }
}
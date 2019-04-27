using Core;
using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface IRoleService
    {
        void Create(Role model);

        void Delete(Role model);

        void Dispose();

        void Edit(Role model);

        Role Get(int? id);

        IEnumerable<Role> GetAll();
    }
}

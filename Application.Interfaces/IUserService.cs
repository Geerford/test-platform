using Core;
using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface IUserService
    {
        void Create(User model);

        void Delete(User model);

        void Dispose();

        void Edit(User model);

        User Get(int? id);

        IEnumerable<User> GetAll();
    }
}
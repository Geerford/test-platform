using Core;
using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface ITypeService
    {
        void Create(Type model);

        void Delete(Type model);

        void Dispose();

        void Edit(Type model);

        Type Get(int? id);

        IEnumerable<Type> GetAll();
    }
}
using Core;
using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface ICuratorService
    {
        void Create(Curator model);

        void Delete(Curator model);

        void Dispose();

        void Edit(Curator model);

        Curator Get(int? id);

        IEnumerable<Curator> GetAll();
    }
}
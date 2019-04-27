using Core;
using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface ISectionService
    {
        void Create(Section model);

        void Delete(Section model);

        void Dispose();

        void Edit(Section model);

        Section Get(int? id);

        IEnumerable<Section> GetAll();
    }
}

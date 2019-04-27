using Core;
using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface IGradeService
    {
        void Create(Grade model);

        void Delete(Grade model);

        void Dispose();

        void Edit(Grade model);

        Grade Get(int? id);

        IEnumerable<Grade> GetAll();
    }
}
using Core;
using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface ICourseService
    {
        void Create(Course model);

        void Delete(Course model);

        void Dispose();

        void Edit(Course model);

        Course Get(int? id);

        IEnumerable<Course> GetAll();
    }
}

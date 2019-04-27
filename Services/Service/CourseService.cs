using Application.Interfaces;
using Core;
using Interfaces;
using System.Collections.Generic;

namespace Services.Business.Service
{
    public class CourseService : ICourseService
    {
        IUnitOfWork Database { get; set; }

        public CourseService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Create(Course model)
        {
            Database.Course.Create(model);
            Database.Save();
        }

        public void Delete(Course model)
        {
            Database.Course.Delete(model.ID);
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public void Edit(Course model)
        {
            Database.Course.Update(model);
            Database.Save();
        }

        public Course Get(int? id)
        {
            if (id == null)
            {
                throw new ValidationException("Не задан ID", "");
            }
            Course item = Database.Course.Get(id.Value);
            if (item == null)
            {
                throw new ValidationException("Сущность не найдена", "");
            }
            return item;
        }

        public IEnumerable<Course> GetAll()
        {
            return Database.Course.GetAll();
        }
    }
}
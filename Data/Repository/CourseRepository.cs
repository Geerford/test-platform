using Contracts;
using Core;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Infrastructure.Data.Repository
{
    public class CourseRepository : IRepository<Course>
    {
        private Context Database;

        public CourseRepository(Context context)
        {
            Database = context;
        }
        public void Create(Course item)
        {
            Database.Course.Add(item);
        }

        public void Delete(int id)
        {
            Course item = Database.Course.Find(id);
            if (item != null)
            {
                Database.Course.Remove(item);
            }
        }

        public Course Get(int id)
        {
            return Database.Course.Where(x => x.ID == id)
                                  .Include(x => x.Section)
                                  .FirstOrDefault();
        }

        public IEnumerable<Course> GetAll()
        {
            return Database.Course.Include(x => x.Section);
        }

        public void Update(Course item)
        {
            Database.Entry(item).State = EntityState.Modified;
        }
    }
}
using Contracts;
using Core;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Infrastructure.Data.Repository
{
    public class TestRepository : IRepository<Test>
    {
        private Context Database;

        public TestRepository(Context context)
        {
            Database = context;
        }
        public void Create(Test item)
        {
            Database.Test.Add(item);
        }

        public void Delete(int id)
        {
            Test item = Database.Test.Find(id);
            if (item != null)
            {
                Database.Test.Remove(item);
            }
        }

        public Test Get(int id)
        {
            return Database.Test.Where(x => x.ID == id)
                                .Include(x => x.Grades)
                                .Include(x => x.Questions.Select(y => y.Answers))
                                .Include(x => x.Questions.Select(y => y.Type))
                                .Include(x => x.Section)
                                .Include(x => x.Section.Courses)
                                .FirstOrDefault();
        }

        public IEnumerable<Test> GetAll()
        {
            return Database.Test.Include(x => x.Grades)
                                .Include(x => x.Questions.Select(y => y.Answers))
                                .Include(x => x.Questions.Select(y => y.Type))
                                .Include(x => x.Section)
                                .Include(x => x.Section.Courses);
        }

        public void Update(Test item)
        {
            Database.Entry(item).State = EntityState.Modified;
        }
    }
}
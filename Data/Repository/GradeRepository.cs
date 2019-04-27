using Contracts;
using Core;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Infrastructure.Data.Repository
{
    public class GradeRepository : IRepository<Grade>
    {
        private Context Database;

        public GradeRepository(Context context)
        {
            Database = context;
        }
        public void Create(Grade item)
        {
            Database.Grade.Add(item);
        }

        public void Delete(int id)
        {
            Grade item = Database.Grade.Find(id);
            if (item != null)
            {
                Database.Grade.Remove(item);
            }
        }

        public Grade Get(int id)
        {
            return Database.Grade.Where(x => x.ID == id)
                                 .Include(x => x.Test)
                                 .Include(x => x.User)
                                 .FirstOrDefault();
        }

        public IEnumerable<Grade> GetAll()
        {
            return Database.Grade.Include(x => x.Test)
                                 .Include(x => x.User);
        }

        public void Update(Grade item)
        {
            Database.Entry(item).State = EntityState.Modified;
        }
    }
}
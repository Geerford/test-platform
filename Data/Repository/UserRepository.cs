using Contracts;
using Core;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Infrastructure.Data.Repository
{
    public class UserRepository : IRepository<User>
    {
        private Context Database;

        public UserRepository(Context context)
        {
            Database = context;
        }
        public void Create(User item)
        {
            Database.User.Add(item);
        }

        public void Delete(int id)
        {
            User item = Database.User.Find(id);
            if (item != null)
            {
                Database.User.Remove(item);
            }
        }
        
        public User Get(int id)
        {
            return Database.User.Where(x => x.ID == id)
                                .Include(x => x.Curator)
                                .Include(x => x.Role)
                                .Include(x => x.Report)
                                .Include(x => x.CurrentCurator)
                                .Include(x => x.Group)
                                .Include(x => x.Activities)
                                .Include(x => x.Grades)
                                .FirstOrDefault();
        }

        public IEnumerable<User> GetAll()
        {
            return Database.User.Include(x => x.Curator)
                                .Include(x => x.Role)
                                .Include(x => x.Report)
                                .Include(x => x.CurrentCurator)
                                .Include(x => x.Group)
                                .Include(x => x.Activities)
                                .Include(x => x.Grades);
        }

        public void Update(User item)
        {
            Database.Entry(item).State = EntityState.Modified;
        }
    }
}
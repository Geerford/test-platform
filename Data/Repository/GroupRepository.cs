using Contracts;
using Core;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Infrastructure.Data.Repository
{
    public class GroupRepository : IRepository<Group>
    {
        private Context Database;

        public GroupRepository(Context context)
        {
            Database = context;
        }
        public void Create(Group item)
        {
            Database.Group.Add(item);
        }

        public void Delete(int id)
        {
            Group item = Database.Group.Find(id);
            if (item != null)
            {
                Database.Group.Remove(item);
            }
        }

        public Group Get(int id)
        {
            return Database.Group.Where(x => x.ID == id)
                                 .Include(x => x.Students)
                                 .FirstOrDefault();
        }

        public IEnumerable<Group> GetAll()
        {
            return Database.Group.Include(x => x.Students);
        }

        public void Update(Group item)
        {
            Database.Entry(item).State = EntityState.Modified;
        }
    }
}
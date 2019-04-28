using Contracts;
using Core;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Infrastructure.Data.Repository
{
    public class RoleRepository : IRepository<Role>
    {
        private Context Database;

        public RoleRepository(Context context)
        {
            Database = context;
        }
        public void Create(Role item)
        {
            Database.Role.Add(item);
        }

        public void Delete(int id)
        {
            Role item = Database.Role.Find(id);
            if (item != null)
            {
                Database.Role.Remove(item);
            }
        }

        public Role Get(int id)
        {
            return Database.Role.Where(x => x.ID == id)
                                .Include(x => x.Users)
                                .FirstOrDefault();
        }

        public IEnumerable<Role> GetAll()
        {
            return Database.Role.Include(x => x.Users);
        }

        public void Update(Role item)
        {
            Database.Entry(item).State = EntityState.Modified;
        }
    }
}
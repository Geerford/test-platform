using Contracts;
using Core;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Infrastructure.Data.Repository
{
    public class CuratorRepository : IRepository<Curator>
    {
        private Context Database;

        public CuratorRepository(Context context)
        {
            Database = context;
        }
        public void Create(Curator item)
        {
            Database.Curator.Add(item);
        }

        public void Delete(int id)
        {
            Curator item = Database.Curator.Find(id);
            if (item != null)
            {
                Database.Curator.Remove(item);
            }
        }

        public Curator Get(int id)
        {
            return Database.Curator.Where(x => x.ID == id)
                                   .Include(x => x.User)
                                   .Include(x => x.Students)
                                   .FirstOrDefault();
        }

        public IEnumerable<Curator> GetAll()
        {
            return Database.Curator.Include(x => x.User)
                                   .Include(x => x.Students);
        }

        public void Update(Curator item)
        {
            Database.Entry(item).State = EntityState.Modified;
        }
    }
}
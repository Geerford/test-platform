using Contracts;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Infrastructure.Data.Repository
{
    public class TypeRepository : IRepository<Core.Type>
    {
        private Context Database;

        public TypeRepository(Context context)
        {
            Database = context;
        }
        public void Create(Core.Type item)
        {
            Database.Type.Add(item);
        }

        public void Delete(int id)
        {
            Core.Type item = Database.Type.Find(id);
            if (item != null)
            {
                Database.Type.Remove(item);
            }
        }

        public Core.Type Get(int id)
        {
            return Database.Type.Where(x => x.ID == id)
                                .Include(x => x.Questions)
                                .FirstOrDefault();
        }

        public IEnumerable<Core.Type> GetAll()
        {
            return Database.Type.Include(x => x.Questions);
        }

        public void Update(Core.Type item)
        {
            Database.Entry(item).State = EntityState.Modified;
        }
    }
}
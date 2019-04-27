using Contracts;
using Core;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Infrastructure.Data.Repository
{
    public class ActivityRepository : IRepository<Activity>
    {
        private Context Database;

        public ActivityRepository(Context context)
        {
            Database = context;
        }
        public void Create(Activity item)
        {
            Database.Activity.Add(item);
        }

        public void Delete(int id)
        {
            Activity item = Database.Activity.Find(id);
            if (item != null)
            {
                Database.Activity.Remove(item);
            }
        }

        public Activity Get(int id)
        {
            return Database.Activity.Where(x => x.ID == id)
                                    .Include(x => x.User)
                                    .FirstOrDefault();
        }

        public IEnumerable<Activity> GetAll()
        {
            return Database.Activity
                           .Include(x => x.User);
        }

        public void Update(Activity item)
        {
            Database.Entry(item).State = EntityState.Modified;
        }
    }
}
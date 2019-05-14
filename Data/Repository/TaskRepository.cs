using Contracts;
using Core;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Infrastructure.Data.Repository
{
    public class TaskRepository : IRepository<Task>
    {
        private Context Database;

        public TaskRepository(Context context)
        {
            Database = context;
        }
        public void Create(Task item)
        {
            Database.Task.Add(item);
        }

        public void Delete(int id)
        {
            Task item = Database.Task.Find(id);
            if (item != null)
            {
                Database.Task.Remove(item);
            }
        }

        public Task Get(int id)
        {
            return Database.Task.Where(x => x.ID == id)
                                .Include(x => x.Section)
                                .FirstOrDefault();
        }

        public IEnumerable<Task> GetAll()
        {
            return Database.Task.Include(x => x.Section);
        }

        public void Update(Task item)
        {
            Database.Entry(item).State = EntityState.Modified;
        }
    }
}
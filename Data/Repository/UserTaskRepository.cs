using Contracts;
using Core;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Infrastructure.Data.Repository
{
    public class UserTaskRepository : IMMRepository<UserTask>
    {
        private Context Database;

        public UserTaskRepository(Context context)
        {
            Database = context;
        }
        public void Create(UserTask item)
        {
            Database.UserTask.Add(item);
        }

        public void Delete(int userID, int taskID)
        {
            UserTask item = Database.UserTask.Where(x => x.UserID == userID && x.TaskID == taskID)
                .FirstOrDefault();
            if (item != null)
            {
                Database.UserTask.Remove(item);
            }
        }

        public UserTask Get(int userID, int taskID)
        {
            return Database.UserTask.Where(x => x.UserID == userID && x.TaskID == taskID)
                .FirstOrDefault();
        }

        public IEnumerable<UserTask> GetAll()
        {
            return Database.UserTask;
        }

        public void Update(UserTask item)
        {
            Database.Entry(item).State = EntityState.Modified;
        }
    }
}
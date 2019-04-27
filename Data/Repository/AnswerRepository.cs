using Contracts;
using Core;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Infrastructure.Data.Repository
{
    public class AnswerRepository : IRepository<Answer>
    {
        private Context Database;

        public AnswerRepository(Context context)
        {
            Database = context;
        }
        public void Create(Answer item)
        {
            Database.Answer.Add(item);
        }

        public void Delete(int id)
        {
            Answer item = Database.Answer.Find(id);
            if (item != null)
            {
                Database.Answer.Remove(item);
            }
        }

        public Answer Get(int id)
        {
            return Database.Answer.Where(x => x.ID == id)
                                  .Include(x => x.Question)
                                  .FirstOrDefault();
        }

        public IEnumerable<Answer> GetAll()
        {
            return Database.Answer.Include(x => x.Question);
        }

        public void Update(Answer item)
        {
            Database.Entry(item).State = EntityState.Modified;
        }
    }
}
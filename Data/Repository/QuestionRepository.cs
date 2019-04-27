using Contracts;
using Core;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Infrastructure.Data.Repository
{
    public class QuestionRepository : IRepository<Question>
    {
        private Context Database;

        public QuestionRepository(Context context)
        {
            Database = context;
        }
        public void Create(Question item)
        {
            Database.Question.Add(item);
        }

        public void Delete(int id)
        {
            Question item = Database.Question.Find(id);
            if (item != null)
            {
                Database.Question.Remove(item);
            }
        }

        public Question Get(int id)
        {
            return Database.Question.Where(x => x.ID == id)
                                    .Include(x => x.Test)
                                    .Include(x => x.Type)
                                    .FirstOrDefault();
        }

        public IEnumerable<Question> GetAll()
        {
            return Database.Question.Include(x => x.Test)
                                    .Include(x => x.Type);
        }

        public void Update(Question item)
        {
            Database.Entry(item).State = EntityState.Modified;
        }
    }
}
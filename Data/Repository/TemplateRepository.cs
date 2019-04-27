using Contracts;
using Core;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Infrastructure.Data.Repository
{
    public class TemplateRepository : IRepository<Template>
    {
        private Context Database;

        public TemplateRepository(Context context)
        {
            Database = context;
        }
        public void Create(Template item)
        {
            Database.Template.Add(item);
        }

        public void Delete(int id)
        {
            Template item = Database.Template.Find(id);
            if (item != null)
            {
                Database.Template.Remove(item);
            }
        }

        public Template Get(int id)
        {
            return Database.Template.Where(x => x.ID == id)
                                    .Include(x => x.Reports)
                                    .FirstOrDefault();
        }

        public IEnumerable<Template> GetAll()
        {
            return Database.Template.Include(x => x.Reports);
        }

        public void Update(Template item)
        {
            Database.Entry(item).State = EntityState.Modified;
        }
    }
}
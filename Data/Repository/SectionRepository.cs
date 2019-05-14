using Contracts;
using Core;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Infrastructure.Data.Repository
{
    public class SectionRepository : IRepository<Section>
    {
        private Context Database;

        public SectionRepository(Context context)
        {
            Database = context;
        }
        public void Create(Section item)
        {
            Database.Section.Add(item);
        }

        public void Delete(int id)
        {
            Section item = Database.Section.Find(id);
            if (item != null)
            {
                Database.Section.Remove(item);
            }
        }

        public Section Get(int id)
        {
            return Database.Section.Where(x => x.ID == id)
                                .Include(x => x.Courses)
                                .Include(x => x.Tests)
                                .Include(x => x.Tasks)
                                .FirstOrDefault();
        }

        public IEnumerable<Section> GetAll()
        {
            return Database.Section.Include(x => x.Courses)
                                   .Include(x => x.Tests)
                                   .Include(x => x.Tasks);
        }

        public void Update(Section item)
        {
            Database.Entry(item).State = EntityState.Modified;
        }
    }
}
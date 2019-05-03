using Contracts;
using Core;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Infrastructure.Data.Repository
{
    public class ReportRepository : IRepository<Report>
    {
        private Context Database;

        public ReportRepository(Context context)
        {
            Database = context;
        }
        public void Create(Report item)
        {
            Database.Report.Add(item);
        }

        public void Delete(int id)
        {
            Report item = Database.Report.Find(id);
            if (item != null)
            {
                Database.Report.Remove(item);
            }
        }

        public Report Get(int id)
        {
            return Database.Report.Where(x => x.ID == id)
                                  .Include(x => x.User)
                                  .FirstOrDefault();
        }

        public IEnumerable<Report> GetAll()
        {
            return Database.Report.Include(x => x.User);
        }

        public void Update(Report item)
        {
            Database.Entry(item).State = EntityState.Modified;
        }
    }
}
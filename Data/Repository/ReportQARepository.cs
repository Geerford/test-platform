using Contracts;
using Core;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Infrastructure.Data.Repository
{
    public class ReportQARepository : IMMRepository<ReportQA>
    {
        private Context Database;

        public ReportQARepository(Context context)
        {
            Database = context;
        }
        public void Create(ReportQA item)
        {
            Database.ReportQA.Add(item);
        }

        public void Delete(int reportID, int templateID)
        {
            ReportQA item = Database.ReportQA.Where(x => x.ReportID == reportID && x.TemplateID == templateID)
                                             .FirstOrDefault();
            if (item != null)
            {
                Database.ReportQA.Remove(item);
            }
        }

        public ReportQA Get(int reportID, int templateID)
        {
            return Database.ReportQA.Where(x => x.ReportID == reportID && x.TemplateID == templateID)
                                .FirstOrDefault();
        }

        public IEnumerable<ReportQA> GetAll()
        {
            return Database.ReportQA;
        }

        public void Update(ReportQA item)
        {
            Database.Entry(item).State = EntityState.Modified;
        }
    }
}
using Application.Interfaces;
using Core;
using Interfaces;
using System.Collections.Generic;

namespace Services.Business.Service
{
    public class ReportService : IReportService
    {
        IUnitOfWork Database { get; set; }

        public ReportService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Create(Report model)
        {
            Database.Report.Create(model);
            Database.Save();
        }

        public void Delete(Report model)
        {
            Database.Report.Delete(model.ID);
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public void Edit(Report model)
        {
            Database.Report.Update(model);
            Database.Save();
        }

        public Report Get(int? id)
        {
            if (id == null)
            {
                throw new ValidationException("Не задан ID", "");
            }
            Report item = Database.Report.Get(id.Value);
            if (item == null)
            {
                throw new ValidationException("Сущность не найдена", "");
            }
            return item;
        }

        public IEnumerable<Report> GetAll()
        {
            return Database.Report.GetAll();
        }
    }
}
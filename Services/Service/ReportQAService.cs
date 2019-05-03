using Application.Interfaces;
using Core;
using Interfaces;
using System.Collections.Generic;

namespace Services.Business.Service
{
    public class ReportQAService : IReportQAService
    {
        IUnitOfWork Database { get; set; }

        public ReportQAService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Create(ReportQA model)
        {
            Database.ReportQA.Create(model);
            Database.Save();
        }

        public void Delete(ReportQA model)
        {
            Database.ReportQA.Delete(model.ReportID, model.TemplateID);
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public void Edit(ReportQA model)
        {
            Database.ReportQA.Update(model);
            Database.Save();
        }

        public ReportQA Get(int? reportID, int? templateID)
        {
            if (reportID == null || templateID == null)
            {
                throw new ValidationException("Не задан ID", "");
            }
            ReportQA item = Database.ReportQA.Get(reportID.Value, templateID.Value);
            if (item == null)
            {
                throw new ValidationException("Сущность не найдена", "");
            }
            return item;
        }

        public IEnumerable<ReportQA> GetAll()
        {
            return Database.ReportQA.GetAll();
        }
    }
}
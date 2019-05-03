using Core;
using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface IReportQAService
    {
        void Create(ReportQA model);

        void Delete(ReportQA model);

        void Dispose();

        void Edit(ReportQA model);

        ReportQA Get(int? reportID, int? templateID);

        IEnumerable<ReportQA> GetAll();
    }
}
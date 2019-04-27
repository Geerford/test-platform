using Core;
using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface IReportService
    {
        void Create(Report model);

        void Delete(Report model);

        void Dispose();

        void Edit(Report model);

        Report Get(int? id);

        IEnumerable<Report> GetAll();
    }
}
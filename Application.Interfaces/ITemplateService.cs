using Core;
using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface ITemplateService
    {
        void Create(Template model);

        void Delete(Template model);

        void Dispose();

        void Edit(Template model);

        Template Get(int? id);

        IEnumerable<Template> GetAll();
    }
}

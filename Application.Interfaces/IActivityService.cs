using Core;
using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface IActivityService
    {
        void Create(Activity model);

        void Delete(Activity model);

        void Dispose();

        void Edit(Activity model);

        Activity Get(int? id);

        IEnumerable<Activity> GetAll();
    }
}

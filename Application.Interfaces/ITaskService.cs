using Core;
using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface ITaskService
    {
        void Create(Task model);

        void Delete(Task model);

        void Dispose();

        void Edit(Task model);

        Task Get(int? id);

        IEnumerable<Task> GetAll();
    }
}
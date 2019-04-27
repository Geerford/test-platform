using Core;
using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface ITestService
    {
        void Create(Test model);

        void Delete(Test model);

        void Dispose();

        void Edit(Test model);

        Test Get(int? id);

        IEnumerable<Test> GetAll();
    }
}
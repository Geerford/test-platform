using Application.Interfaces;
using Core;
using Interfaces;
using System.Collections.Generic;

namespace Services.Business.Service
{
    public class TestService : ITestService
    {
        IUnitOfWork Database { get; set; }

        public TestService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Create(Test model)
        {
            Database.Test.Create(model);
            Database.Save();
        }

        public void Delete(Test model)
        {
            Database.Test.Delete(model.ID);
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public void Edit(Test model)
        {
            Database.Test.Update(model);
            Database.Save();
        }

        public Test Get(int? id)
        {
            if (id == null)
            {
                throw new ValidationException("Не задан ID", "");
            }
            Test item = Database.Test.Get(id.Value);
            if (item == null)
            {
                throw new ValidationException("Сущность не найдена", "");
            }
            return item;
        }

        public IEnumerable<Test> GetAll()
        {
            return Database.Test.GetAll();
        }
    }
}
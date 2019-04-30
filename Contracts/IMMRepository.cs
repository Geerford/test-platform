using System.Collections.Generic;

namespace Contracts
{
    public interface IMMRepository<T> where T : class
    {
        IEnumerable<T> GetAll();

        T Get(int groupID, int sectionID);

        void Create(T item);

        void Update(T item);

        void Delete(int groupID, int sectionID);
    }
}
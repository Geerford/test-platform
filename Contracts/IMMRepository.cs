using System.Collections.Generic;

namespace Contracts
{
    public interface IMMRepository<T> where T : class
    {
        IEnumerable<T> GetAll();

        T Get(int leftID, int rightID);

        void Create(T item);

        void Update(T item);

        void Delete(int leftID, int rightID);
    }
}
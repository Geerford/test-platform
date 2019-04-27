using Core;
using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface IAnswerService
    {
        void Create(Answer model);

        void Delete(Answer model);

        void Dispose();

        void Edit(Answer model);

        Answer Get(int? id);

        IEnumerable<Answer> GetAll();
    }
}
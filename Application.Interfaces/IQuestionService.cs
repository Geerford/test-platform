using Core;
using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface IQuestionService
    {
        void Create(Question model);

        void Delete(Question model);

        void Dispose();

        void Edit(Question model);

        Question Get(int? id);

        IEnumerable<Question> GetAll();
    }
}
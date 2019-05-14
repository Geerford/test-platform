using Core;
using System;
using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface IUserTaskService
    {
        void Create(UserTask model);

        void Delete(UserTask model);

        void Dispose();

        void Edit(UserTask model);

        UserTask Get(int? userID, int? taskID);

        IEnumerable<UserTask> GetAll();
    }
}
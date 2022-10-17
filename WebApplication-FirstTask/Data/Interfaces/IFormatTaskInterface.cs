using System;
using WebApplication_FirstTask.Models;

namespace WebApplication_FirstTask.Data.Interfaces
{
    public interface IFormatTaskInterface
    {
        IEnumerable<TaskFormat> GetAll();

        TaskFormat GetById(int id);
        TaskFormat Add(TaskFormat formatTask);

        TaskFormat Update(TaskFormat formatTask);

        void Delete(int id);

        int Commit();
    }
}


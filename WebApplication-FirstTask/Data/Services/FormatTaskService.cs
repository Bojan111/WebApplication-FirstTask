using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using WebApplication_FirstTask.Data.Interfaces;
using WebApplication_FirstTask.Models;

namespace WebApplication_FirstTask.Data.Services
{
    public class FormatTaskService : IFormatTaskInterface
    {
        private readonly ApplicationDbContext context;
     

        public FormatTaskService(ApplicationDbContext context)
        {
            this.context = context;
          
        }

        public TaskFormat Add(TaskFormat formatTask)
        {
            var temp = context.FormatTasks.Add(formatTask);
            return temp.Entity;
        }

        public int Commit()
        {
            return context.SaveChanges();
        }

        public void Delete(int id)
        {
            var getById = context.FormatTasks.FirstOrDefault(x => x.Id == id);
            if(getById != null)
            {
                context.FormatTasks.Remove(getById);
            }
        }

        public IEnumerable<TaskFormat> GetAll()
        {
            
            return context.FormatTasks.OrderBy(x => x.Number).ToList();
        }


        public TaskFormat GetById(int id)
        {
            return context.FormatTasks.FirstOrDefault(x => x.Id == id);
        }

        public TaskFormat Update(TaskFormat formatTask)
        {
            context.Entry(formatTask).State = EntityState.Modified;
            return formatTask;
        }
    }
}


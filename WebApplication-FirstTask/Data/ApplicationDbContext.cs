using System;
using Microsoft.EntityFrameworkCore;
using WebApplication_FirstTask.Models;

namespace WebApplication_FirstTask.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<TaskFormat> FormatTasks { get; set; }
    }
}


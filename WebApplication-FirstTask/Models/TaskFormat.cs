using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication_FirstTask.Models
{
    public class TaskFormat
    {
        [Key]
        public int Id { get; set; }

        public string Color { get; set; }

        public int Number { get; set; }

        public string Label { get; set; }
    }


}


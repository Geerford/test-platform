using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core
{
    public class Section
    {
        [Key]
        public int ID { get; set; }

        public string Description { get; set; }

        public ICollection<Course> Courses { get; set; }

        public ICollection<Test> Tests { get; set; }

        public ICollection<Task> Tasks { get; set; }
    }
}
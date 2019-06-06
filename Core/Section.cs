using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core
{
    public class Section
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Данное поле должно быть заполнено")]
        public string Description { get; set; }

        public ICollection<Course> Courses { get; set; }

        public ICollection<Test> Tests { get; set; }

        public ICollection<Task> Tasks { get; set; }
    }
}
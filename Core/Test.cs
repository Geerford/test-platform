using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core
{
    public class Test
    {
        [Key]
        public int ID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int SectionID { get; set; }
        
        public Section Section { get; set; }

        public ICollection<Grade> Grades { get; set; }

        public ICollection<Question> Questions { get; set; }
    }
}
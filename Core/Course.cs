using System.ComponentModel.DataAnnotations;

namespace Core
{
    public class Course
    {
        [Key]
        public int ID { get; set; }

        public string Description { get; set; }

        public string Link { get; set; }

        public int SectionID { get; set; }

        public Section Section { get; set; }
    }
}
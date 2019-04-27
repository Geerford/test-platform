using System.ComponentModel.DataAnnotations;

namespace Core
{
    public class Grade
    {
        [Key]
        public int ID { get; set; }

        public double Value { get; set; }

        public int UserID { get; set; }

        public User User { get; set; }

        public int TestID { get; set; }

        public Test Test { get; set; }
    }
}
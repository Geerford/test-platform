using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core
{
    public class Question
    {
        [Key]
        public int ID { get; set; }

        public string Description { get; set; }

        public int TestID { get; set; }

        public Test Test { get; set; }

        public ICollection<Answer> Answers { get; set; }

        public int TypeID { get; set; }

        public Type Type { get; set; }
    }
}
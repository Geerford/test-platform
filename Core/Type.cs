using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core
{
    public class Type
    {
        [Key]
        public int ID { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

        public ICollection<Question> Questions { get; set; }
    }
}
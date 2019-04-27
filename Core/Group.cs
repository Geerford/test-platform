using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core
{
    public class Group
    {
        [Key]
        public int ID { get; set; }

        public string Description { get; set; }

        public string University { get; set; } 

        public string Department { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public ICollection<User> Students { get; set; }
    }
}
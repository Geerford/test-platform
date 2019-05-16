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

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Start { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime End { get; set; }

        public virtual ICollection<User> Students { get; set; }

        public virtual Report Report { get; set; }
    }
}
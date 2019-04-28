using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core
{
    public class Role
    {
        [Key]
        public int ID { get; set; }

        public string Description { get; set; }

        public string Value { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
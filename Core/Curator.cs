using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core
{
    public class Curator
    {
        [Key]
        [ForeignKey("User")]
        public int ID { get; set; }

        public virtual User User { get; set; }

        public ICollection<User> Students { get; set; }
    }
}
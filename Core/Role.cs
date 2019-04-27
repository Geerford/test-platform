using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core
{
    public class Role
    {
        [Key]
        [ForeignKey("User")]
        public int ID { get; set; }

        public string Description { get; set; }

        public string Value { get; set; }

        public virtual User User { get; set; }
    }
}
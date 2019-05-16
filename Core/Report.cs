using System.ComponentModel.DataAnnotations;

namespace Core
{
    public class Report
    {
        [Key]
        public int ID { get; set; }

        public string Link { get; set; }

        public virtual User User { get; set; }

        public virtual Group Group { get; set; }
    }
}

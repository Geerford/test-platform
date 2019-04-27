using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core
{
    public class Report
    {
        public Report()
        {
            Templates = new HashSet<Template>();
        }

        [Key]
        public int ID { get; set; }

        public string Link { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<Template> Templates { get; set; }
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core
{
    public class Template
    {
        public Template()
        {
            Reports = new HashSet<Report>();
        }

        [Key]
        public int ID { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Report> Reports { get; set; }
    }
}
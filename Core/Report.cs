using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core
{
    public class Report
    {
        [Key]
        public int ID { get; set; }

        public string Link { get; set; }

        public virtual User User { get; set; }
    }
}

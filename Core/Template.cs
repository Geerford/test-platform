using System.ComponentModel.DataAnnotations;

namespace Core
{
    public class Template
    {
        [Key]
        public int ID { get; set; }

        public string Description { get; set; }
    }
}
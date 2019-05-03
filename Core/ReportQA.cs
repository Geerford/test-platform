using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core
{
    public class ReportQA
    {
        [Key, Column(Order = 0)]
        public int ReportID { get; set; }

        [Key, Column(Order = 1)]
        public int TemplateID { get; set; }

        public string Description { get; set; }
    }
}
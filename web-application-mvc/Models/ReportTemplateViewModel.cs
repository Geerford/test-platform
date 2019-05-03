using System.Collections.Generic;

namespace web_application_mvc.Models
{
    public class ReportTemplateViewModel
    {
        public int ReportID { get; set; }

        public string ReportLink { get; set; }

        public int ID { get; set; } 

        public Dictionary<string, string> Templates { get; set; }
    }
}
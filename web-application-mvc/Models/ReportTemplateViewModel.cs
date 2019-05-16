using Core;
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

    public class ReportGroupViewModel
    {
        public int ReportID { get; set; }

        public string ReportLink { get; set; }

        public int ID { get; set; }
    }

    public class ReportStudents
    {
        public User Student { get; set; }

        public double TestAVG { get; set; }

        public double TaskAVG { get; set; }

        public double SUM { get; set; }
    }
}
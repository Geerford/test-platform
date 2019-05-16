using Core;
using System.Collections.Generic;

namespace web_application_mvc.Models
{
    public class ReportViewModel
    {
        public User User { get; set; }

        public IEnumerable<Grade> Grades { get; set; }

        public IEnumerable<Activity> Activities { get; set; }
        
        public IEnumerable<ExtentionTaskViewModel> Tasks { get; set; }
    }

    public class ReportListViewModel
    {
        public IEnumerable<Report> Students { get; set; }

        public IEnumerable<Report> Groups { get; set; }
    }
}
using Core;
using System.Collections.Generic;

namespace web_application_mvc.Models
{
    public class ReportViewModel
    {
        public User User { get; set; }

        public IEnumerable<Grade> Grades { get; set; }

        public IEnumerable<Activity> Activities { get; set; }
    }
}
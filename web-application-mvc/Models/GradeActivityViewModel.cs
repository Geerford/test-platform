using Core;
using System.Collections.Generic;

namespace web_application_mvc.Models
{
    public class GradeActivityViewModel
    {
        public IEnumerable<Grade> Grades { get; set; }

        public IEnumerable<Activity> Activities { get; set; }
    }
}
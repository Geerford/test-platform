using Core;
using System.Collections.Generic;

namespace web_application_mvc.Models
{
    public class GradeActivityViewModel
    {
        public IEnumerable<Grade> Grades { get; set; }

        public IEnumerable<Activity> Activities { get; set; }

        public IEnumerable<StudentTaskViewModelPartial> Tasks { get; set; }
    }

    public class CuratorProfileViewModel
    {
        public IEnumerable<ExtentionTaskViewModel> Checked { get; set; }

        public IEnumerable<ExtentionTaskViewModel> Unchecked { get; set; }
    }
    
    public class ExtentionTaskViewModel
    {
        public int ID { get; set; }

        public int? UserID { get; set; }

        public User User { get; set; }

        public int? TaskID { get; set; }

        public Task Task { get; set; }

        public int? SectionID { get; set; }

        public Section Section { get; set; }

        public string Answer { get; set; }

        public string Grade { get; set; }

        public string Comment { get; set; }
    }
}
using Core;
using System.Collections.Generic;

namespace web_application_mvc.Models
{
    public class SectionViewModel
    {
        public Section Section { get; set; }

        public IEnumerable<Test> Tests { get; set; }

        public IEnumerable<Course> Courses { get; set; }
    }
}
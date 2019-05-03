using Core;
using System.Collections.Generic;

namespace web_application_mvc.Models
{
    public class TestViewModel
    {
        public Test Test { get; set; }

        public IEnumerable<Question> Questions { get; set; }
    }
}
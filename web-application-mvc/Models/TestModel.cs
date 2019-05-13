using System.Collections.Generic;

namespace web_application_mvc.Models
{
    public class TestModel
    {
        public int? ID { get; set; }

        public string Description { get; set; }

        public string Section { get; set; }

        public string Title { get; set; }

        public List<QuestionViewModel> Questions { get; set; }
    }
}
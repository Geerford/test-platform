using System.Collections.Generic;

namespace web_application_mvc.Models
{
    public class QuestionViewModel
    {
        public string Description { get; set; }

        public string Type { get; set; }

        public List<AnswerViewModel> Answers { get; set; }        
    }
}
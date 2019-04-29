using System.Collections.Generic;
using System.Web.Mvc;

namespace web_application_mvc.Models
{
    public class TestVM
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<SelectListItem> Questions { get; set; }

    }

    public class QuestionVM
    {
        public int QuestionID { get; set; }
        public string QuestionText { get; set; }
        public string QuestionType { get; set; }
        public string Anwser { get; set; }
        public ICollection<ChoiceVM> Choices { get; set; }
    }

    public class ChoiceVM
    {
        public int ChoiceID { get; set; }
        public string ChoiceText { get; set; }
    }

    public class AnswersVM
    {
        public int QuestionID { get; set; }
        public string QuestionText { get; set; }
        public string Answer { get; set; }
        public bool IsCorrect { get; set; }
    }
}
using System.Collections.Generic;
using System.Web.Mvc;

namespace web_application_mvc.Models
{
    public class QuizVM
    {
        public int QuizID { get; set; }
        public string QuizName { get; set; }
        public List<SelectListItem> ListOfQuizz { get; set; }

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

    public class QuizAnswersVM
    {
        public int QuestionID { get; set; }
        public string QuestionText { get; set; }
        public string AnswerQ { get; set; }
        public bool isCorrect { get; set; }
    }
}
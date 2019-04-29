using Core;
using System.Collections.Generic;
using System.Linq;

namespace web_application_mvc.Models
{
    public class TestViewModel
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int SectionID { get; set; }

        public Section Section { get; set; }

        public ICollection<Grade> Grades { get; set; }

        public ICollection<QuestionViewModel> Questions { get; set; }

        public TestViewModel() { }

        public TestViewModel(Test item)
        {
            ID = item.ID;
            Title = item.Title;
            Description = item.Description;
            SectionID = item.SectionID;
            Section = item.Section;
            Grades = item.Grades;
            Questions = ConvertQuestions(item.Questions.ToList());
        }

        public static explicit operator TestViewModel(Test item)
        {
            return new TestViewModel(item);
        }

        private List<QuestionViewModel> ConvertQuestions(List<Question> questions)
        {
            List<QuestionViewModel> result = new List<QuestionViewModel>();
            foreach (var item in questions)
            {
                result.Add((QuestionViewModel)item);
            }
            return result;
        }
    }
}
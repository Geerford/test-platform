using Core;
using System.Collections.Generic;

namespace web_application_mvc.Models
{
    public class QuestionViewModel
    {
        public int ID { get; set; }

        public string Description { get; set; }

        public int TestID { get; set; }

        public Test Test { get; set; }

        public ICollection<Answer> Answers { get; set; }

        public int TypeID { get; set; }

        public Type Type { get; set; }

        public Answer CurrentAnswer { get; set; }

        public QuestionViewModel() { }

        public QuestionViewModel(Question item)
        {            
            ID = item.ID;
            Description = item.Description;
            TestID = item.TestID;
            Test = item.Test;
            Answers = item.Answers;
            TypeID = item.TypeID;
            Type = item.Type;
        }

        public static explicit operator QuestionViewModel(Question item)
        {
            return new QuestionViewModel(item);
        }
    }
}
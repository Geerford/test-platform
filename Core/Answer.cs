using System.ComponentModel.DataAnnotations;

namespace Core
{
    public class Answer
    {
        [Key]
        public int ID { get; set; }

        public string Desctiption { get; set; }

        public bool Correct { get; set; }

        public int QuestionID { get; set; }

        public Question Question { get; set; }
    }
}
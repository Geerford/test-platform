using System.ComponentModel.DataAnnotations;

namespace Core
{
    public class Task
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Данное поле должно быть заполнено")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Данное поле должно быть заполнено")]
        public string Title { get; set; }

        public int SectionID { get; set; }

        public Section Section { get; set; }
    }
}
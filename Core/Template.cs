using System.ComponentModel.DataAnnotations;

namespace Core
{
    public class Template
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Данное поле должно быть заполнено")]
        public string Description { get; set; }
    }
}
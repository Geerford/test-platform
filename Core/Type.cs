using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core
{
    public class Type
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Данное поле должно быть заполнено")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Данное поле должно быть заполнено")]
        public string Status { get; set; }

        public ICollection<Question> Questions { get; set; }
    }
}
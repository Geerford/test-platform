using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core
{
    public class Role
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Данное поле должно быть заполнено")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Данное поле должно быть заполнено")]
        public string Value { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
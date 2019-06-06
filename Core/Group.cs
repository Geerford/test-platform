using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core
{
    public class Group
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Данное поле должно быть заполнено")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Данное поле должно быть заполнено")]
        public string University { get; set; }

        [Required(ErrorMessage = "Данное поле должно быть заполнено")]
        public string Department { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Required(ErrorMessage = "Данное поле должно быть заполнено")]
        public DateTime Start { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Required(ErrorMessage = "Данное поле должно быть заполнено")]
        public DateTime End { get; set; }

        public virtual ICollection<User> Students { get; set; }

        public virtual Report Report { get; set; }
    }
}
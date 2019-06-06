using System;
using System.ComponentModel.DataAnnotations;

namespace Core
{
    public class Activity
    {
        [Key]
        public int ID { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Required(ErrorMessage = "Данное поле должно быть заполнено")]
        public DateTime Date { get; set; }

        public int UserID { get; set; }

        public User User { get; set; }
    }
}

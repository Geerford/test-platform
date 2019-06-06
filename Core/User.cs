using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core
{
    public class User
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Данное поле должно быть заполнено")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Данное поле должно быть заполнено")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Данное поле должно быть заполнено")]
        public string Midname { get; set; }

        [Required(ErrorMessage = "Почта должна быть заполнена")]
        [RegularExpression(@"^[\w-]+(\.[\w-]+)*@([a-z0-9-]+(\.[a-z0-9-]+)*?\.[a-z]{2,6}|(\d{1,3}\.){3}\d{1,3})(:\d{4})?$", ErrorMessage = "Введите корректную почту")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Телефон должен быть заполнен")]
        [RegularExpression(@"^(\+\s?)?((?<!\+.*)\(\+?\d+([\s\-\.]?\d+)?\)|\d+)([\s\-\.]?(\(\d+([\s\-\.]?\d+)?\)|\d+))*(\s?(x|ext\.?)\s?\d+)?$", ErrorMessage = "Введите корректный телефон")]
        public string Phone { get; set; }

        public bool Status { get; set; } // IsActive

        public bool Safety { get; set; } //IsRead

        public virtual Curator Curator { get; set; }
        
        public virtual int? RoleID { get; set; }

        public virtual Role Role { get; set; }

        public virtual Report Report { get; set; }

        public virtual int? CurrentCuratorID { get; set; }

        public virtual Curator CurrentCurator { get; set; }

        public virtual int? GroupID { get; set; }

        public virtual Group Group { get; set; }

        public ICollection<Activity> Activities { get; set; }

        public ICollection<Grade> Grades { get; set; }
    }
}
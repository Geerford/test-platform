using System.ComponentModel.DataAnnotations;

namespace web_application_mvc.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Данное поле должно быть заполнено")]
        [RegularExpression(@"^[a-zA-Zа-яА-Я]+$", ErrorMessage = "Используйте символы алфавита")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Данное поле должно быть заполнено")]
        [RegularExpression(@"^[a-zA-Zа-яА-Я]+$", ErrorMessage = "Используйте символы алфавита")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Данное поле должно быть заполнено")]
        [RegularExpression(@"^[a-zA-Zа-яА-Я]+$", ErrorMessage = "Используйте символы алфавита")]
        public string Midname { get; set; }

        [Required(ErrorMessage = "Данное поле должно быть заполнено")]
        [RegularExpression(@"^[\w-]+(\.[\w-]+)*@([a-z0-9-]+(\.[a-z0-9-]+)*?\.[a-z]{2,6}|(\d{1,3}\.){3}\d{1,3})(:\d{4})?$", ErrorMessage = "Введите корректную почту")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Данное поле должно быть заполнено")]
        [DataType(DataType.Password)]   
        public string Password { get; set; }

        [Required(ErrorMessage = "Данное поле должно быть заполнено")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }

        [Required(ErrorMessage = "Данное поле должно быть заполнено")]
        [RegularExpression(@"^(\+\s?)?((?<!\+.*)\(\+?\d+([\s\-\.]?\d+)?\)|\d+)([\s\-\.]?(\(\d+([\s\-\.]?\d+)?\)|\d+))*(\s?(x|ext\.?)\s?\d+)?$", ErrorMessage = "Введите корректный телефон")]
        public string Phone { get; set; }
    }
}
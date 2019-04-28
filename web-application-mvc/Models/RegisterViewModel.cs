using System.ComponentModel.DataAnnotations;

namespace web_application_mvc.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Данное поле должно быть заполнено")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Данное поле должно быть заполнено")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Данное поле должно быть заполнено")]
        public string Midname { get; set; }

        [Required(ErrorMessage = "Данное поле должно быть заполнено")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Данное поле должно быть заполнено")]
        [DataType(DataType.Password)]   
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }

        [Required(ErrorMessage = "Данное поле должно быть заполнено")]
        public string Phone { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace web_application_mvc.Models
{
    public class LoginViewModel
    {
        [AllowHtml]
        [Required(ErrorMessage = "Почта должна быть заполнена")]
        public string Email { get; set; }

        [AllowHtml]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Пароль должен быть заполнен")]
        public string Password { get; set; }
    }
}
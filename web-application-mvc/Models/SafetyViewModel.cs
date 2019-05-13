using System;
using System.ComponentModel.DataAnnotations;

namespace web_application_mvc.Models
{
    public class SafetyViewModel
    {
        [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
        public class MustBeTrueAttribute : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                return value != null && value is bool && (bool)value;
            }
        }

        [Display(Name = "Я полностью прочитал технику безопасности и принимаю ее")]
        [Required]
        [MustBeTrue(ErrorMessage = "Пожалуйста, прочитайте и примите технику безопасности")]
        public bool Status { get; set; }
    }
}
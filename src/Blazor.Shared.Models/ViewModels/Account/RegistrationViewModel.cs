using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Blazor.Shared.Models.ViewModels.Account
{
    public class RegistrationViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,16}$",
            ErrorMessage = "Пароль должен состоять из 8-16 символов, а также должен включать: заглавную и строчную буквы, цифру, символ.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of birth")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [RegularExpression("^(?:male|Male|female|Female)$",
            ErrorMessage = "Пол может быть указан только как мужской или женский.")]
        public string Gender { get; set; }

        [StringLength(200, ErrorMessage = "Максимальная длина описания не может превышать 200 символов.")]
        [Display(Name = "AboutUser")]
        public string AboutUser { get; set; }
    }
}

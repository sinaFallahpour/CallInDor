using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.DTO.Account
{
    public class ResetPassword
    {
        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(6, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(20, ErrorMessage = "The maximum {0} length is {1} characters")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "{0} is  Required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [Display(Name = "ConfirmPassword")]
        public string ConfirmPassword { get; set; }



    }
}

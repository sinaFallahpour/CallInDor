using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.DTO.Account
{
    public class AdminChangePasswordDTO
    {
        [Required(ErrorMessage = "{0} is  Required")]
        [Display(Name = "UserName")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(6, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(20, ErrorMessage = "The maximum {0} length is {1} characters")]
        [DataType(DataType.Password)]
        [Display(Name = "NewPassword")]
        public string NewPassword { get; set; }

    }
}

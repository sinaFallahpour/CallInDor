using Domain.CustonValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.DTO.Account
{
    public class RegisterDTO
    {


        [Required(ErrorMessage = "{0} is  Required")]
        [CheckCultureIsValid(ErrorMessage = "InvalidCultureName")]
        [Display(Name = "CultureName")]
        public string  CultureName  { get; set; }
        


        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(10, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(10, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }



        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(6, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(20, ErrorMessage = "The maximum {0} length is {1} characters")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }


        [Required(ErrorMessage = "{0} is  Required")]
        [MaxLength(20, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "CountryCode")]
        public string CountryCode { get; set; }


        public bool IsCompany { get; set; }
    }
}

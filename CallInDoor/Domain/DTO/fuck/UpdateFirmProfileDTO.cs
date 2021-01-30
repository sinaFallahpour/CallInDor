using Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.DTO.Account
{
    public class UpdateFirmProfileDTO
    {




        [MaxLength(80, ErrorMessage = "The maximum {0} length is {1} characters")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [Display(Name = "Email")]
        public string Email { get; set; }





        [Display(Name = "Bio")]
        [MinLength(8, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(600, ErrorMessage = "The maximum {0} length is {1} characters")]
        public string Bio { get; set; }







        //این ها از جدول شرکتی میاد

        [Display(Name = "firm name")]
        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(600, ErrorMessage = "The maximum {0} length is {1} characters")]
        public string FirmName { get; set; }


        [Display(Name = "firm manager name")]
        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(600, ErrorMessage = "The maximum {0} length is {1} characters")]
        public string FirmManagerName { get; set; }



        [NotMapped]
        public IFormFile FirmLogo { get; set; }



        [MinLength(10, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(10, ErrorMessage = "The maximum {0} length is {1} characters")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "{0} must be numeric")]
        [Display(Name = "NationalCode")]
        public string NationalCode { get; set; }


        [MinLength(10, ErrorMessage = "invalid {0}")]
        [MaxLength(10, ErrorMessage = "invalid {0}")]
        [Display(Name = "code posti")]
        public string CodePosti { get; set; }

        [MaxLength(2000, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "address")]
        public string FirmAddress { get; set; }

        [MaxLength(11, ErrorMessage = "The maximum {0} length is {1} characters")]
        [MinLength(11, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "firm code")]
        public string ShenaseMeliSherkat { get; set; }

        [MaxLength(2000, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "Country")]
        public string FirmCountry { get; set; }


        [MaxLength(2000, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "state")]
        public string FirmState { get; set; }





        /// <summary>
        /// شناسه ملی
        /// </summary>
        [MinLength(3, ErrorMessage = "please inter more than 3")]
        [MaxLength(20, ErrorMessage = "please enter  lett than 20")]
        public string FirmNationalID { get; set; }


        /// <summary>
        /// تایخ ثبت شرکت
        /// </summary>
        [MaxLength(30, ErrorMessage = "please enter  lett than 30")]
        public string FirmDateOfRegistration { get; set; }



        /// <summary>
        /// شناسه ثبت
        /// </summary> 
        [MinLength(3, ErrorMessage = "please inter more than 3")]
        [MaxLength(20, ErrorMessage = "please enter  lett than 20")]
        public string FirmRegistrationID { get; set; }




        public List<int> ServeCastegoriesIds { get; set; }





    }





}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.DTO.Settings
{
    public class SettingsGetDTO
    {


        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [Display(Name = "Aboutus (persian)")]
        //[AllowHtml]
        public string Aboutus { get; set; }

        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [Display(Name = "AboutUs (English)")]
        //[AllowHtml]
        public string AboutusEnglish { get; set; }



        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [Display(Name = "Address (Persian)")]
        //[AllowHtml]
        public string Address { get; set; }

        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [Display(Name = "Address (English)")]
        //[AllowHtml]
        public string AddressEnglish { get; set; }



        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [Display(Name = "PhoneNumber (Persian)")]
        //[AllowHtml]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [Display(Name = "PhoneNumber (English)")]
        //[AllowHtml]
        public string PhoneNumberEnglish { get; set; }



        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [Display(Name = "Email (Persian)")]
        //[AllowHtml]
        public string Email { get; set; }


        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [Display(Name = "Email (English)")]
        //[AllowHtml]
        public string EmailEnglish { get; set; }



    }
}

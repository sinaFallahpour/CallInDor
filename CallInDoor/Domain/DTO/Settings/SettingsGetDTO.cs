using System;
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




        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [Display(Name = "Profile Confirmation Notification Text (Persian)")]
        public string ProfileConfirmNotificationText { get; set; }

        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [Display(Name = "Profile Confirmation Notification Text (English)")]
        public string ProfileConfirmNotificationTextEnglish { get; set; }




        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [Display(Name = "Profile Rejection Notification Text (Persian)")]
        public string ProfileRejectNotificationText { get; set; }

        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [Display(Name = "Profile Rejection Notification Text (English)")]
        public string ProfileRejectNotificationTextEnglish { get; set; }




        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [Display(Name = "Service Confirmation Notification Text (Persian)")]
        public string ServiceConfirmNotificationText { get; set; }

        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [Display(Name = "Service Confirmation Notification Text (English)")]
        public string ServiceConfirmNotificationTextEnglish { get; set; }



        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [Display(Name = "Service Rejection Notification Text (Persian)")]
        public string ServiceRejectNotificationText { get; set; }

        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [Display(Name = "Service Rejection Notification Text (English)")]
        public string ServiceRejectNotificationTextEnglish { get; set; }
        
        
        
        
        
        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [Display(Name = "provider limit time for reject request")]
        public string ProviderLimitTimeForRejectRequest { get; set; }






        //       public const string ProfileConfirmNotificationKeyName = "ProfileConfirmNotification";
        //public const string ServiceConfimNotificationKeyName = "ServiceConfimNotification";




    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.DTO.Service
{
    public class fucks
    {


        [Required(ErrorMessage = "asasa")]
        public string ssss { get; set; }


        [Required(ErrorMessage = " asas is  Required")]
        [MaxLength(30, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "UserName")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "{0} is  Required")]
        [MaxLength(200, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "ServiceName")]
        public string ServiceName { get; set; }





        public bool BeTranslate { get; set; }




        [Required(ErrorMessage = "{0} is  Required")]
        [Display(Name = "FreeMessageCount")]
        [Range(0, int.MaxValue, ErrorMessage = "")]
        public int? FreeMessageCount { get; set; }

        

        [Required(ErrorMessage = "{0} is  Required")]
        [Display(Name = "IsServiceReverse")]
        public bool IsServiceReverse { get; set; }


        [Required(ErrorMessage = "{0} is  Required")]
        [Display(Name = "PriceForNativeCustomer")]
        public string  PriceForNativeCustomer { get; set; }


        [Required(ErrorMessage = "{0} is  Required")]
        [Range(0, int.MaxValue, ErrorMessage = "")]
        [Display(Name = "PriceForNonNativeCustomer")]
        public double? PriceForNonNativeCustomer { get; set; }





        /// <summary>
        ///  تاریخ درج
        /// </summary>
        public DateTime CreateDate { get; set; }


        [Required(ErrorMessage = "{0} is  Required")]
        [Display(Name = "ServiceType")]







        //public string UserId { get; set; }

        public int? CatId { get; set; }

        public int? SubCatId { get; set; }
    }
}

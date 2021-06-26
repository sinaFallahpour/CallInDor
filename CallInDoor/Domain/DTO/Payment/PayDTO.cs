using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.DTO.Payment
{
    public class PayDTO
    {
        //[Display(Name = "DiscountCode")]
        //public string DiscountCode { get; set; }


        /// <summary>
        /// قیمت برای کاربران             
        /// </summary>
        [Required(ErrorMessage = "{0} is  Required")]
        [Range(0, double.MaxValue, ErrorMessage = "The {0} should be between {1} and {2}")]
        [Display(Name = "Amount")]
        public double Amount { get; set; }
    }
}

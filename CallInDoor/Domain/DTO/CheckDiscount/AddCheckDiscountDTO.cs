using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.DTO.CheckDiscount
{
    public class AddCheckDiscountDTO
    {
        public int? Id { get; set; }



        /// <summary>
        /// عنوان این تخفیف فارسی
        /// </summary>
        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(100, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "persian title")]
        public string PersianTitle { get; set; }

        /// <summary>
        /// عنوان این تخفیف انگلیسی
        /// </summary>
        /// 


        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(100, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "english title")]
        public string EnglishTitle { get; set; }



        /// <summary>
        /// کد تخفیف
        /// </summary>
        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(5, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(100, ErrorMessage = "The maximum {0} length is {1} characters")]
        [Display(Name = "code")]
        public string Code { get; set; }


        /// <summary>
        /// درصد تخفیف
        /// که روی قیمت سرویس لحاظ میشه
        /// </summary>
        /// 
        [Range(0, 100, ErrorMessage = "invalid percent")]
        public int Percent { get; set; }


        [Range(1, 365, ErrorMessage = " invalid day count")]
        public int? DayCount { get; set; }

        [Range(1, 24, ErrorMessage = " invalid hour Count ")]
        public int? HourCount { get; set; }





        #region  Relation

        /// آیدی سرویس
        /// 

        [Required(ErrorMessage = "{0} is  Required")]
        [Display(Name = "service")]
        public int? ServiceId { get; set; }

        #endregion



        ///// <summary>
        ///// زمان پایان کد تخفیف
        ///// </summary>
        //public DateTime ExpireTime { get; set; }


    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.DTO.Account
{
    public class CreateServiceDTO
    {

        public int Id { get; set; }



        #region  top ten package
        [Range(1, 365, ErrorMessage = "invalid day count")]
        public int? DayCount { get; set; }

        [Range(1, 24, ErrorMessage = "invalid hour")]
        public int? HourCount { get; set; }




        /// <summary>
        /// آیا مدارک برلی کسی که این از این نوع دسته بندی سرویس ثبت میکند الزامیست یا خیر
        /// </summary>
        public bool IsProfileOptional { get; set; }


        /// <summary>
        /// تعداد کاربرانی که میتوانند این بسته را برای تاپ تن شدن بخرند
        /// </summary>
        [Required(ErrorMessage = "{0} is required")]
        [Range(1, int.MaxValue, ErrorMessage = "invalid users Count")]
        public int? UsersCount { get; set; }


        [Required(ErrorMessage = "{0} is resquired")]
        [Range(0, double.MaxValue, ErrorMessage = "The {0} should be between {1} and {2}")]
        [Display(Name = "top provider package price")]
        public double? TopTenPackagePrice { get; set; }


        #endregion




        public List<RequiredFiles> RequiredFiles { get; set; }


        [Required(ErrorMessage = "{0} is resquired")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(100, ErrorMessage = "The maximum {0} length is {1} characters")]
        public string Name { get; set; }





        [Required(ErrorMessage = "{0} is resquired")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(100, ErrorMessage = "The maximum {0} length is {1} characters")]
        public string PersianName { get; set; }





        /// <summary>
        /// رنگ سرویس
        /// </summary>
        [Required(ErrorMessage = "{0} is resquired")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(100, ErrorMessage = "The maximum {0} length is {1} characters")]
        public string Color { get; set; }




        /// <summary>
        /// حداقل فیمت برای برای  سرویس  تایپ هایی  از نوع سرویس
        /// </summary>
        [Required(ErrorMessage = "{0} is resquired")]
        [Range(1, double.MaxValue, ErrorMessage = "The  {0} should be between {1} and {2}")]
        [Display(Name = "Minimm Price For Service")]
        public double MinPriceForService { get; set; }




        /// <summary>
        /// حداقل زمان برای سرویس های Voice//VideCall//voiceCall
        /// </summary>
        [Required(ErrorMessage = "{0} is resquired")]
        [Range(1, double.MaxValue, ErrorMessage = "The  {0} should be between {1} and {2} minutes")]
        [Display(Name = "Minimm Session Time")]
        public double? MinSessionTime { get; set; }






        /// <summary>
        /// حداقل قیمت مجاز برای کاربران تیتیو برای سرویس های چت یا وویس یا ویدیو
        /// </summary>
        [Required(ErrorMessage = "{0} is resquired")]
        [Range(1, double.MaxValue, ErrorMessage = "The  {0} should be between {1} and {2}  ")]
        [Display(Name = "Accepted Minimm Price For Native user")]
        public double? AcceptedMinPriceForNative { get; set; }






        /// <summary>
        /// حداقل قیمت مجاز برای کاربران غیر تیتیو برای سرویس های چت یا وویس یا ویدیو
        /// </summary>
        [Required(ErrorMessage = "{0} is resquired")]
        [Range(1, double.MaxValue, ErrorMessage = "The  {0} should be between {1} and {2}  ")]
        [Display(Name = "Accepted Minimm Price For Non Native user")]
        public double? AcceptedMinPriceForNonNative { get; set; }




        [Required(ErrorMessage = "{0} is resquired")]
        [Range(0, 100, ErrorMessage = "{0} is Invalid")]
        [Display(Name = "Site Percent")]
        public int? SitePercent { get; set; }


        /// <summary>
        /// آیا فعال است
        /// </summary>
        public bool IsEnabled { get; set; }



        //[MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(2000, ErrorMessage = "The maximum {0} length is {1} characters")]
        public string Tags { get; set; }


        //[MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(2000, ErrorMessage = "The maximum {0} length is {1} characters")]
        public string PersinaTags { get; set; }




        [Required(ErrorMessage = "{0} is resquired")]
        public string RoleId { get; set; }


    }



    public class RequiredFiles
    {

        public int? Id { get; set; }
        public string FileName { get; set; }
        public string PersianFileName { get; set; }
    }
}

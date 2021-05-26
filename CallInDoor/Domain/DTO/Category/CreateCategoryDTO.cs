using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.DTO.Category
{
    public class CreateCategoryDTO
    {


        public int Id { get; set; }

        /// <summary>
        /// عنوان  انگلیسی دسته بندی
        /// </summary>
        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(100, ErrorMessage = "The maximum {0} length is {1} characters")]
        public string Title { get; set; }


        [Required(ErrorMessage = "{0} is  Required")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(100, ErrorMessage = "The maximum {0} length is {1} characters")]
        public string PersianTitle { get; set; }


        public IFormFile Image { get; set; }


        /// <summary>
        /// آیا فعال است؟
        /// </summary>
        public bool IsEnabled { get; set; }


        /// <summary>
        /// ایا این دسته بندی برای کورس ها است
        /// </summary>
        public bool IsForCourse { get; set; }

        /// <summary>
        /// آیا این دسته بندی از نوع ساب کتوری است
        /// </summary>
        public bool IsSubCategory { get; set; }


        /// <summary>
        /// آیا ایم دسته یندی برای کارپرداز است
        /// </summary>
        public bool IsSupplier { get; set; }


        #region  Relation 

        /// <summary>
        /// آیدی سر دسته
        /// </summary>
        public int? ParentId { get; set; }



        /// <summary>
        /// آیدی سرویس
        /// </summary>
        public int? ServiceId { get; set; }


        #endregion

    }




}

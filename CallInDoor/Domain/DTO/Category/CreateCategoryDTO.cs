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
        [Required(ErrorMessage = "{0} is resquired")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(100, ErrorMessage = "The maximum {0} length is {1} characters")]
        public string Title { get; set; }


        [Required(ErrorMessage = "{0} is resquired")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        [MaxLength(100, ErrorMessage = "The maximum {0} length is {1} characters")]
        public string PersianTitle { get; set; }



        /// <summary>
        /// آیا فعال است؟
        /// </summary>
        public bool IsEnabled { get; set; }




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

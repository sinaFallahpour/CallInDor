using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.DTO.Category
{
    public class CreateAreaDTO
    {

        public int Id { get; set; }

        /// <summary>
        /// عنوان انگلیسی تخصص
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


        /// <summary>
        /// ایا این حوضه حرفه ایی است؟
        /// </summary>
        public bool IsProfessional { get; set; }


        /// <summary>
        /// نخصص هاش برای اگر 
        /// if IsProfessinal == true این فیلد مقدار میگیرد 
        /// </summary>
        //[MaxLength(2000, ErrorMessage = "The maximum {0} length is {1} characters")]
        //public string Specialities { get; set; }


        #region  Relation 

        /// <summary>
        /// آیدی سرویس
        /// </summary>
        public int? ServiceId { get; set; }
        

        public List<Speciality> Specialities { get; set; }


        #endregion

    }


    public class Speciality{

        [Required(ErrorMessage = "{0} is resquired")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        public string PersianName { get; set; }

        [Required(ErrorMessage = "{0} is resquired")]
        [MinLength(1, ErrorMessage = "The minimum {0} length is {1} characters")]
        public string EnglishName { get; set; }

     }

}




using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("Category")]
    public class CategoryTBL : BaseEntity<int>
    {

        /// <summary>
        /// عنوان  انگلیسی دسته بندی
        /// </summary>
        /// 
        [MaxLength(200)]
        public string Title { get; set; }



        [MaxLength(200)]
        public string PersianTitle { get; set; }




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

        #region  Relation 

        /// <summary>
        /// آیدی سر دسته
        /// </summary>
        public int? ParentId { get; set; }

        public virtual CategoryTBL Parent { get; set; }


        /// <summary>
        /// آیدی سرویس
        /// </summary>
        public int? ServiceId { get; set; }

        public virtual ServiceTBL Service { get; set; }

        public virtual List<CategoryTBL> Children { get; set; }

        #endregion

    }
}

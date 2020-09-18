using System;
using System.Collections.Generic;
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
        public string Title { get; set; }



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

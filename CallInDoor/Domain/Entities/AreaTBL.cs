using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("Area")]
    public class AreaTBL : BaseEntity<int>
    {


        /// <summary>
        /// عنوان  انگلیسی دسته بندی
        /// </summary>
        public string Title { get; set; }

        [MaxLength(120)]
        public string PersianTitle { get; set; }


        /// <summary>
        /// آیا فعال است؟
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// ایا این حوضه حرفه ایی است؟
        /// </summary>
        public bool IsProfessional { get; set; }


        //[MaxLength(3000)]
        //public string Specialities { get; set; }


        #region  Relation 





        /// <summary>
        /// آیدی سرویس
        /// </summary>
        public int? ServiceId { get; set; }

        public virtual ServiceTBL Service { get; set; }



        public  ICollection<SpecialityTBL> Specialities { get; set; }
        #endregion

    }
}

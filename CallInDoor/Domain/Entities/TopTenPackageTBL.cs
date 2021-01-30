using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{

    /// <summary>
    /// جدول تاپ تن هایی که ادمین ثبت میکند برای هر سرویس تایپ خواص
    /// </summary>
    [Table("TopTenPackage")]
    public class TopTenPackageTBL : BaseEntity<int>
    {



        /// <summary>
        /// فیمت این بسته چقدره
        /// </summary>
        public double Price { get; set; }


        /// <summary>
        /// تاریخ درج هچس کسشره
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// تعدادافرادی که میتونن ازین پکیج استفاده کنند
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// تعداد روز  که فرد منیتواند تاپ تن باشد
        /// </summary>
        public int? DayCount { get; set; }

        /// <summary>
        ///تعداد ساعت که فرد منیتواند تاپ تن باشد
        /// </summary>
        public int? HourCount { get; set; }

        #region  Relation

        [ForeignKey("ServiceId")]
        public ServiceTBL ServiceTbl { get; set; }
        public int? ServiceId { get; set; }





        #endregion



    }
}

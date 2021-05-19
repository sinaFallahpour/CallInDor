using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{

    //جدول کد تخیف های ما
    //که برای سرویس هایی که میخواد یخره لحاظ میشه
    [Table("CheckDiscount")]
    public class CheckDiscountTBL : BaseEntity<int>
    {


        /// <summary>
        /// عنوان این تخفیف فارسی
        /// </summary>
        public string PersianTitle { get; set; }

        /// <summary>
        /// عنوان این تخفیف انگلیسی
        /// </summary>
        public string EnglishTitle { get; set; }



        /// <summary>
        /// کد تخفیف
        /// </summary>
        public string Code { get; set; }


        /// <summary>
        /// درصد تخفیف
        /// که روی قیمت سرویس لحاظ میشه
        /// </summary>
        public int Percent { get; set; }


        /// <summary>
        /// زمان پایان کد تخفیف
        /// </summary>
        public DateTime ExpireTime { get; set; }






        /// <summary>
        /// تعداد روز
        /// </summary>
        public int? DayCount { get; set; }

        /// <summary>
        ///تعداد ساعت
        /// </summary>
        public int? HourCount { get; set; }




        #region  

        /// <summary>
        /// آیدی سرویس
        /// </summary>
        public int? ServiceId { get; set; }

        [ForeignKey("ServiceId")]
        public ServiceTBL Service { get; set; }




        public List<TransactionTBL> Transactions { get; set; }

        public List<BuyiedPackageTBL> BuyiedPackageTBLs { get; set; }


        #endregion

    }
}

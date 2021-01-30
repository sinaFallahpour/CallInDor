using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    /// جدولی که معلوم میکند چقدر زمان برای کلاینت باقی مانده برای یک ریکوست خواص
    /// استباه بالایی 
    /// این جدول ثبت تک تک زمان هایه که استفادهکرده کاربر
    [Table("UsedPeriodedChat")]
    public class UsedPeriodedChatTBL : BaseEntity<int>
    {


        ///// <summary>
        ///// زمان باقی مانده از آن سرویس برحسب ثانیه
        ///// </summary>
        //public int ReaminingTime { get; set; }


        /// <summary>
        /// زمان استفاده شده از آن سرویس بر حسب ثانیه
        /// </summary>
        public int UsedTime { get; set; }



        public string ClientUserName { get; set; }



        public string ProviderUserName { get; set; }



        /// <summary>
        /// تاریخ وساعت شروع چت برای کلاینت
        /// </summary>
        public DateTime StartTime { get; set; }


        /// <summary>
        /// تاریخ وساعت پایان چت برای کلاینت
        /// </summary>
        public DateTime EndTime { get; set; }



        #region relatoin


        public int? ServiceRequestId { get; set; }

        [ForeignKey("ServiceRequestId")]
        public ServiceRequestTBL ServiceRequestTBL { get; set; }


        #endregion



    }
}

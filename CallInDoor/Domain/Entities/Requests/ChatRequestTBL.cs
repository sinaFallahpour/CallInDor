using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities.Requests
{
    /// <summary>
    ///  جدول اطلاعات اضافی یم ریکوست که مخصوص چت هستند
    /// </summary>
    [Table("ChatRequest")]
    public class ChatRequestTBL : BaseEntity<int>
    {


        /// <summary>
        /// زمانی که پروایدر اکسپت کرد
        /// </summary>
        public DateTime AcceptDate { get; set; }



        #region Chat-Voice(Free(unlimited))


        /// <summary>
        /// تعداد پیامهای رایگان که تا حالا استفاده کردیم
        /// </summary>
        public int? FreeUsageMessageCount { get; set; }


        /// <summary>
        /// تعداد پیامهای رایگان که تا میتوانیم استفاده کردیم
        /// </summary>
        public int? FreeMessageCount { get; set; }



        #endregion



        #region Chat-Voice(Session or prioded)



        /// <summary>
        /// تعداد کل پیام هایی که کاربر خریده
        /// </summary>
        public int AllMessageCount_LimitedChat { get; set; }

        /// <summary>
        /// تعداد پیام هایی که تاحالا استفاده کرده 
        /// </summary>
        public int UsedMessageCount_LimitedChat { get; set; }



        /// <summary>
        /// آیا این از نوع لیمیت است؟
        /// </summary>
        public bool IsLimitedChat { get; set; }


        ///////// <summary>
        ///////// ایا کاربر پلن خریده یا نه
        ///////// </summary>
        //////public bool HasPlan_LimitedChatVoice { get; set; }



        /// <summary>
        /// آیا این پلن تمام شده؟ یعنی زمانش منقضی شده یا نه
        /// </summary>
        public DateTime ExpireTime_LimitedChatVoice { get; set; }


        //public int? BuyiedPackageId { get; set; }

        //[ForeignKey("BuyiedPackageId")]
        //public BuyiedPackageTBL BuyiedPackageTBL { get; set; }



        /////لیست جدول پیام های باقی مانه ی من
        ////public List<UsedPeriodedChatTBL> UsedPeriodedChatTBLs { get; set; }








        #endregion








        #region relation   


        // relation With BaseRequestServiceTBL
        public BaseRequestServiceTBL BaseRequestServiceTBL { get; set; }

        [ForeignKey("BaseRequestServiceTBL")]
        public int? BaseRequestId { get; set; }


        /// لیست پیام های پت (البته اگرا ازنوع  چت سشن یا پریودیک  باشد ) 
        public List<ChatForLimitedServiceMessagesTBL> ChatForLimitedServiceMessagesTBL { get; set; }

        /// لیست پیام های چت (البته اگرا ازنوع چت باشد ) 
        public List<ChatServiceMessagesTBL> ChatServiceMessagesTBL { get; set; }


        #endregion




    }

}

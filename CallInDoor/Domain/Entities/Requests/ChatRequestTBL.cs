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

        #region Chat-Voice(Free(unlimited))

        /// تعداد پیامهای رایگان که تا حالا استفاده کردیم
        public int? FreeUsageMessageCount { get; set; }
        /// تعداد پیامهای رایگان که تا میتوانیم استفاده کردیم
        public int? FreeMessageCount { get; set; }
        /// قیمت کلی برای کل پیام هایی که داده شد   
        /// این تا حالا اتصلا استفاده نشد
        //public double TotalPrice { get; set; } 
        /// این پراپرتی فقط برای سرویس هایی از نوع چت یا وویس یا کال جواب میده

        public PackageType PackageType { get; set; }
        ///این زمان 8 ساعت بعد از ثبت سرویس است مه کاربرانت دی اکتیو باید یه ا  پاسخ دهند 
        public DateTime WhenTheRequestShouldBeAnswered { get; set; }

        /// قیمت برای کاربرا ن نان نیتیو
        public double? PriceForNonNativeCustomer { get; set; }

        /// قینت برای کاربران نیتیو
        public double? PriceForNativeCustomer { get; set; }


        //تعداد کل پیام هایی که کلاینت فرستاد
        //public int ClientMessageCount_FreeeChatVoice  { get; set; }




        #endregion



        #region Chat-Voice(Session or prioded)



        //if (chatVoiceValueFromRedis == null)
        //    if (chatVoiceValueFromRedis.RequestStatusForRedis == RequestStatusForRedis.BadPlan)

        //if (chatVoiceValueFromRedis.EndTime == null && DateTime.Now <= chatVoiceValueFromRedis.EndTime)


        ///// <summary>
        ///// زمان شروع آخرین چت ممکن
        ///// </summary>
        //public DateTime StartTime_PeriodedChatVoice { get; set; }//---------------------------------------------------------------

        ///// <summary>
        ///// زمان پایان آخرین چت ممکن
        ///// </summary>
        //public DateTime EndTime_PeriodedChatVoice { get; set; }//---------------------------------------------------------------



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


        /// <summary>
        /// ایا کاربر پلن خریده یا نه
        /// </summary>
        public bool HasPlan_LimitedChatVoice { get; set; }



        /// <summary>
        /// آیا این پلن تمام شده؟ یعنی زمانش منقضی شده یا نه
        /// </summary>
        public DateTime ExpireTime_LimitedChatVoice { get; set; }


        //packageId
        public int? BuyiedPackageId { get; set; }

        [ForeignKey("BuyiedPackageId")]
        public BuyiedPackageTBL BuyiedPackageTBL { get; set; }



        /////لیست جدول پیام های باقی مانه ی من
        ////public List<UsedPeriodedChatTBL> UsedPeriodedChatTBLs { get; set; }




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

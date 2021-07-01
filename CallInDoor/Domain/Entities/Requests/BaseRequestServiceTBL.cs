using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities.Requests
{
    /// <summary>
    /// جدول اصلی ریکوست های سرویس ها
    /// این جدول تمام فیلد هایی اصلی بین درخواست هایه  چت و ویدیو وکال و کورس و سرویس را شامل میشود
    /// </summary>
    [Table("BaseServiceRequest")]
    public class BaseRequestServiceTBL : BaseEntity<int>
    {


        #region  مشترک برای همه
        /// <summary>
        /// نام  کاربری مشتری یا همون سرویس گیرنده یاهمون سرویس خواهنده
        /// </summary>
        public string ClienUserName { get; set; }

        /// <summary>
        /// نام کاربری سرویس دهنده
        /// </summary>
        public string ProvideUserName { get; set; }



        /// چه کسی پیام را فرستاد
        //public SendetMesageType SendetMesageType { get; set; }

        ///// این ریکوست به چه نوع سرویسی زده شود
        ///// مثلا چته یا وویسه یا ویدیوعه یا کورسه یا سرویسی
        //public ServiceType ServiceType { get; set; }


        /// <summary>
        /// video or voice  or chat or course or service  shayad ham  karpardaz
        //value should be 0,1,2,3,4,5
        /// </summary>
        public string ServiceTypes { get; set; }


        public ServiceRequestStatus ServiceRequestStatus { get; set; }


        /// تاریخ درخواست سرویس
        public DateTime CreateDate { get; set; }









        /// <summary>
        ///زمانی که آخرین پیام فرستاده شده
        /// </summary>
        public DateTime LastTimeTheMessageWasSent { get; set; }



        #endregion


        //#region Chat-Voice(Free(unlimited))

        ///// تعداد پیامهای رایگان که تا حالا استفاده کردیم
        //public int? FreeUsageMessageCount { get; set; }//=====================================================================
        ///// تعداد پیامهای رایگان که تا میتوانیم استفاده کردیم
        //public int? FreeMessageCount { get; set; }//=====================================================================
        ///// قیمت کلی برای کل پیام هایی که داده شد   
        ///// این تا حالا اتصلا استفاده نشد
        ////public double TotalPrice { get; set; }//=======================================================================

        /// این پراپرتی فقط برای سرویس هایی از نوع چت یا وویس یا کال جواب میده
        public PackageType PackageType { get; set; }//==== 
        ///این زمان 8 ساعت بعد از ثبت سرویس است مه کاربرانت دی اکتیو باید یه ا  پاسخ دهند 
        public DateTime WhenTheRequestShouldBeAnswered { get; set; } 

        ///////////////// قیمت برای کاربرا ن نان نیتیو
        //////////////public double? PriceForNonNativeCustomer { get; set; }

        ///////////////// قینت برای کاربران نیتیو
        //////////////public double? PriceForNativeCustomer { get; set; }




        /// <summary>
        /// قینت      
        /// </summary>
        public double? Price { get; set; }




        ////تعداد کل پیام هایی که کلاینت فرستاد
        ////public int ClientMessageCount_FreeeChatVoice  { get; set; }

        ///// لیست پیام های پت (البته اگرا ازنوع چت باشد ) 
        //public List<ChatServiceMessagesTBL> ChatServiceMessagesTBL { get; set; }//===== 



        //#endregion



        //#region Chat-Voice(Session or prioded)



        ////if (chatVoiceValueFromRedis == null)
        ////    if (chatVoiceValueFromRedis.RequestStatusForRedis == RequestStatusForRedis.BadPlan)

        ////if (chatVoiceValueFromRedis.EndTime == null && DateTime.Now <= chatVoiceValueFromRedis.EndTime)


        /////// <summary>
        /////// زمان شروع آخرین چت ممکن
        /////// </summary>
        ////public DateTime StartTime_PeriodedChatVoice { get; set; }//---------------------------------------------------------------

        /////// <summary>
        /////// زمان پایان آخرین چت ممکن
        /////// </summary>
        ////public DateTime EndTime_PeriodedChatVoice { get; set; }//---------------------------------------------------------------



        ///// <summary>
        ///// تعداد کل پیام هایی که کاربر خریده
        ///// </summary>
        //public int AllMessageCount_LimitedChat { get; set; }

        ///// <summary>
        ///// تعداد پیام هایی که تاحالا استفاده کرده 
        ///// </summary>
        //public int UsedMessageCount_LimitedChat { get; set; }



        ///// <summary>
        ///// آیا این از نوع لیمیت است؟
        ///// </summary>
        //public bool IsLimitedChat { get; set; }//---------------------------------------------------------------


        ///// <summary>
        ///// ایا کاربر پلن خریده یا نه
        ///// </summary>
        //public bool HasPlan_LimitedChatVoice { get; set; }//---------------------------------------------------------------


        ///////// <summary>
        ///////// آیا این پلن تمام شده؟ یعنی زمانش منقضی شده یا نه
        ///////// </summary>
        //////public bool IsExpired_LimitedChatVoice { get; set; }//---------------------------------------------------------------


        ///// <summary>
        ///// آیا این پلن تمام شده؟ یعنی زمانش منقضی شده یا نه
        ///// </summary>
        //public DateTime ExpireTime_LimitedChatVoice { get; set; }//---------------------------------------------------------------



        //////برحسب ثانیه این duration
        //////زمان کلی که دارد
        ////public int? DurationSecond_PeriodedChatVoice { get; set; }//---------------------------------------------------------------




        /////// <summary>
        /////// زمان باقی مانده از آن سرویس برحسب ثانیه
        /////// </summary>
        ////public int? ReaminingTime_PeriodedChatVoice { get; set; }//---------------------------------------------------------------




        ////packageId
        //public int? BuyiedPackageId { get; set; }

        //[ForeignKey("BuyiedPackageId")]
        //public BuyiedPackageTBL BuyiedPackageTBL { get; set; }


        ///// لیست پیام های پت (البته اگرا ازنوع  چت سشن یا پریودیک  باشد ) 
        //public List<ChatForLimitedServiceMessagesTBL> ChatForLimitedServiceMessagesTBL { get; set; }



        ///////لیست جدول پیام های باقی مانه ی من
        //////public List<UsedPeriodedChatTBL> UsedPeriodedChatTBLs { get; set; }


        //#endregion




        #region  Relation 


        /// آیدی سرویسی که بهش ریکوست زده میشه   
        public int? BaseServiceId { get; set; }

        [ForeignKey("BaseServiceId")]
        public BaseMyServiceTBL BaseMyServiceTBL { get; set; }


        /// لیست پیام های پت (البته اگرا ازنوع  چت سشن یا پریودیک  باشد ) 
        public List<ChatForLimitedServiceMessagesTBL> ChatForLimitedServiceMessagesTBL { get; set; }


        /// لیست پیام های پت (البته اگرا ازنوع چت باشد ) 
        public List<ChatServiceMessagesTBL> ChatServiceMessagesTBL { get; set; }


        /// <summary>
        /// رابطه 1 به 1 با جدول چت ریریکوست    
        /// </summary>
        public ChatRequestTBL ChatRequestTBL { get; set; }


        /// <summary>
        /// رابطه 1 به 1 با جدول کال ریکوست    
        /// </summary>
        public CallRequestTBL CallRequestTBL { get; set; }



        /// <summary>
        ///  کد تخفیف  که کلاینت برای این درخواست استفاده میکند
        /// </summary>
        [ForeignKey("CheckDiscountId")]
        public CheckDiscountTBL CheckDiscountTBL { get; set; }
        public int? CheckDiscountId { get; set; }




        #endregion




    }
}

using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace Domain.Entities
{
    /// <summary>
    /// جدول ذخیره کاربرانی که در یک  سرویس خاص عضو یک کمپانی هستند
    /// </summary>
    [Table("CompanyServiceUser")]
    public class CompanyServiceUserTBL : BaseEntity<int>
    {


        public DateTimeOffset CreateDate { get; set; }


        /// <summary>
        /// ایا توسط ادمین رد شده تا تایید شده
        /// </summary>
        public ConfirmStatus ConfirmStatus { get; set; }


        /// <summary>
        /// آیا شخص از زیر مجموعه بودن خارج شده
        /// شاید شرکت اورا حذف کند شاید خودش لفت دهد
        /// </summary>
        public bool IsDeleted { get; set; }



        #region 



        /// <summary>
        ///نام کاربری کاربری که در این سرویس عضو شده است
        /// </summary>
        public string subSetUserName { get; set; }


        /// <summary>
        /// نام کاربری کمپانی 
        /// </summary>
        public string CompanyUserName { get; set; }



        [ForeignKey("ServiceId")]
        public ServiceTBL ServiceTBL { get; set; }
        public int? ServiceId { get; set; }






        #endregion

    }
}

using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    public class AppUser : IdentityUser
    {



        /// <summary>
        /// تا زمانی که کاربر نمیتواند درخواست قبول کند بخاطر رد کردن درخواست دریگران موقع آنلاین بودن
        /// </summary>
        public DateTime LimiteTimeOfRecieveRequest { get; set; }



        /// <summary>
        /// آیا پروایدر  آزاد است
        /// </summary>
        public bool IsFree { get; set; }



        /// <summary>
        /// زبان فعلی کاربر
        /// </summary>
        public string CultureName { get; set; }

        public string Name { get; set; }
        public string LastName { get; set; }
        public string Bio { get; set; }
        public string ImageAddress { get; set; }
        public string VideoAddress { get; set; }

        public Gender Gender { get; set; }
        public string NationalCode { get; set; }
        [MaxLength(40)]
        public string BirthDate { get; set; }
        //آیا پروفایل کاربرر قابلیت ادیت کردن را دارد یا نه
        public bool IsEditableProfile { get; set; }



        public string ConnectionId { get; set; }



        /// <summary>
        /// آخرین چتی که داشته برای کدام ریکوستش بود  
        /// این آیدی آون ریکوست است
        /// </summary>
        public int CurentRequestId { get; set; }

        /// <summary>
        /// notification Id baraye safhe Chat ha
        /// </summary>
        public string ChatNotificationId { get; set; }


        /// <summary>
        /// موجودی کیف پول
        /// </summary>
        public double? WalletBalance { get; set; }

        //آیا کاربر سازمانیست؟
        public bool IsCompany { get; set; }

        //وضعیت تایید پروفایل
        public ProfileConfirmType ProfileConfirmType { get; set; }

        //آیا من یک کاربر اکتیو هستم یا خیر(برای پاسخ به مشتریان)
        public bool IsOnline { get; set; }

        /// <summary>
        ///user role
        /// </summary>
        //public string Role { get; set; }

        /// <summary>
        /// serial number for jwt token
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        /// کد تایید
        /// </summary>
        public int? verificationCode { get; set; }

        /// <summary>
        /// تاریخ انقضا کد تایید
        /// </summary>
        public DateTime verificationCodeExpireTime { get; set; }

        /// <summary>
        /// کد کشور
        /// </summary>
        public string CountryCode { get; set; }


        /// <summary>
        /// تعداد ستاره های کاربر
        /// </summary>
        public int StarCount { get; set; }

        /// <summary>
        /// تعداد ستاره های زیر 3 یه کاربر
        /// </summary>
        public int Under3StarCount { get; set; }


        public DateTime CreateDate { get; set; }


        #region  Relation

        /// <summary>
        /// لیست سرویس هایی را که  غیر فعال کرده
        /// </summary>
        public List<BaseMyServiceTBL> BaseMyServiceTBLs { get; set; }





        /// <summary>
        /// پروفایل اکانت شرکتی  
        /// </summary>
        public FirmProfileTBL FirmProfile { get; set; }



        public ICollection<FieldTBL> Fields { get; set; }





        //-------------------  Relation with ProfileCertificateTBL ----------------



        //public virtual ICollection<User_FieldTBL> UsersFields { get; set; }


        /// <summary>
        /// سرویس های من از نوع پت یا وویس  یا ویدیو
        /// </summary>
        //public virtual ICollection<BaseMyServiceTBL> MyChatServices { get; set; }

        public List<IdentityUserRole<string>> UserRolesTBL { get; set; }

        #endregion
    }
}

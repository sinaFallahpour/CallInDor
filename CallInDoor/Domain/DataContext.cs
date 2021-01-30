using Domain.Entities;
using Helper.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DataContext : IdentityDbContext<AppUser, AppRole, string>
    {


        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);
        //    builder.Entity<AppUser>().HasIndex(u => u.UserName).IsUnique(true);
        //}




        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);



            builder.Entity<MyChatServiceTBL>()
                .HasOne(c => c.BaseMyChatTBL)
                .WithOne(c => c.MyChatsService)
                .HasForeignKey<MyChatServiceTBL>(c => c.BaseId);

            builder.Entity<MyServiceServiceTBL>()
                          .HasOne(c => c.BaseMyChatTBL)
                          .WithOne(c => c.MyServicesService)
                          .HasForeignKey<MyServiceServiceTBL>(c => c.BaseId);



            #region  Index
            builder.Entity<BaseMyServiceTBL>()
               .HasIndex(x => new { x.ServiceName, x.ServiceType });
            //.HasName("");

            builder.Entity<BaseMyServiceTBL>()
              .HasIndex(x => new { x.ServiceName });
            //.HasName("");



            builder.Entity<MyChatServiceTBL>()
              .HasIndex(x => new { x.PriceForNativeCustomer, x.PriceForNonNativeCustomer });



            #endregion


        }





        /// <summary>
        /// جدول تاپ تن هایی که ادمین ثبت میکند برای هر سرویس تایپ خواص
        /// </summary>
        public DbSet<TopTenPackageTBL> TopTenPackageTBL { get; set; }


        /// <summary>
        ///جدول واسط کاربر و تاپ تن
        /// </summary>
        public DbSet<User_TopTenPackageTBL> User_TopTenPackageTBL { get; set; }




        /// <summary>
        //جدول کد تخیف های ما
        //که برای سرویس هایی که میخواد یخره لحاظ میشه
        /// </summary>
        public DbSet<CheckDiscountTBL> CheckDiscountTBL { get; set; }





        ///// <summary>
        ///// جدولی که معلوم میکند چقدر زمان برای کلاینت باقی مانده برای یک ریکوست خواص
        ///// </summary>
        //public UsedPeriodedChatTBL UsedPeriodedChatTBL { get; set; }



        /// <summary>
        /// جدول ریکوست های سرویس ها
        /// </summary>
        public DbSet<ServiceRequestTBL> ServiceRequestTBL { get; set; }



        /// <summary>
        /// بسته عایی که کاربر خریده برای درخواست به سرویس ها
        /// </summary>
        public DbSet<BuyiedPackageTBL> BuyiedPackageTBL { get; set; }




        /// <summary>
        ///جدول کل چت هایه سرویس هایی که ازنوه چت وویس از نوع سشن یا پ=ریودیک هستند را ذخیره میکند
        /// </summary>
        public DbSet<ChatForLimitedServiceMessagesTBL> ChatForLimitedServiceMessagesTBL { get; set; }


        /// <summary>
        ///جدول کل چت هایه سرویس هایی که ازنوه چت وویس از نوع فیری هستند را ذخیره میکند
        /// </summary>
        public DbSet<ChatServiceMessagesTBL> ChatServiceMessagesTBL { get; set; }



        /// <summary>
        ///این جدول تیکت های کاربر به ادمین وادمین به کاربر است
        /// </summary>
        public DbSet<TiketTBL> TiketTBL { get; set; }

        /// <summary>
        ///جدول پیامهای یک تیکت 
        /// </summary>
        public DbSet<TiketMessagesTBL> TiketMessagesTBL { get; set; }



        /// <summary>
        /// تراکنش های کاربر
        /// </summary>
        public DbSet<TransactionTBL> TransactionTBL { get; set; }



        /// <summary>
        /// کارت های کاربر
        /// </summary>
        public DbSet<CardTBL> CardTBL { get; set; }


        /// <summary>
        ///مدرک هایی که کاربر آپلود کرده است در پروفایلش
        /// </summary>
        public DbSet<ProfileCertificateTBL> ProfileCertificateTBL { get; set; }

        public DbSet<SettingTBL> SettingsTBL { get; set; }


        //جدول نوتیفیکیشن های کاربر
        public DbSet<NotificationTBL> NotificationTBL { get; set; }


        /// <summary>
        /// پرمیشن های کاربر  
        /// </summary>
        public DbSet<Permissions> Permissions { get; set; }




        /// <summary>
        /// جدول واسط role - permission
        /// </summary>
        public DbSet<Role_Permission> Role_Permission { get; set; }


        /// <summary>
        /// این جذول سوالل های نظرسنجی که از از یوزر بعد از درخواست سرویس پرسیده میشه
        /// </summary>
        public DbSet<QuestionPullTBL> QuestionPullTBL { get; set; }

        /// <summary>
        /// جواب سوال ها
        /// </summary>
        public DbSet<AnswerTBL> AnswerTBL { get; set; }




        /// <summary>
        ///  سرویس(حوضه کاری)یه جورایی
        /// </summary>
        public DbSet<ServiceTBL> ServiceTBL { get; set; }

        /// <summary>
        /// این جدول میاد تمام فایل هایی که یک سرویس تایپ خواص لازم دارد را ذخیره میکند
        /// </summary>
        public DbSet<ServidceTypeRequiredCertificatesTBL> ServidceTypeRequiredCertificatesTBL { get; set; }



        /// <summary>
        ///   تگ های  یک سرویس(حوضه کاری) س  
        /// </summary>
        public DbSet<ServiceTagsTBL> ServiceTags { get; set; }



        /// <summary>
        ///دسته هایی که هر حوضه میتونن داشته باشن 
        /// </summary>
        public DbSet<CategoryTBL> CategoryTBL { get; set; }


        /// <summary>
        /// حوضه تخصصی هر یک از حوضه ها
        /// </summary>
        public DbSet<AreaTBL> AreaTBL { get; set; }




        /// <summary>
        ///  تخصص های هر Area  
        /// </summary>
        public DbSet<SpecialityTBL> SpecialityTBL { get; set; }





        ///// <summary>
        /////مدرک ها   
        ///// </summary>
        //public DbSet<DegreeTBL> DegreeTBL { get; set; }


        /// <summary>
        ///رشته  های برای یک مدرک 
        /// </summary>
        public DbSet<FieldTBL> FieldTBL { get; set; }





        public DbSet<BaseMyServiceTBL> BaseMyServiceTBL { get; set; }




        /// <summary>
        /// جدول کامنت های سرویس
        /// </summary>
        public DbSet<ServiceCommentsTBL> ServiceCommentsTBL { get; set; }


        /// <summary>
        /// جدول نظر سنجی سرویس
        /// </summary>
        public DbSet<ServiceSurveyTBL> ServiceSurveyTBL { get; set; }




        /// <summary>
        /// این جدول سرویس های که فقط از جنس چت و وویس و ویدیو هستند
        /// </summary>
        public DbSet<MyChatServiceTBL> MyChatServiceTBL { get; set; }



        /// <summary>
        /// این جدول سرویس های که فقط از جنس  سرویس هستند
        /// </summary>
        public DbSet<MyServiceServiceTBL> MyServiceServiceTBL { get; set; }


        /// <summary>
        /// این جدول سرویس های که فقط از جنس کورس هستند
        /// </summary>
        public DbSet<MyCourseServiceTBL> MyCourseServiceTBL { get; set; }

        /// <summary>
        /// تاپیک های کورس های من
        /// </summary>
        public DbSet<MyCourseTopics> MyCourseTopicsTBL { get; set; }




        public DbSet<Test> Tests { get; set; }


    }
}

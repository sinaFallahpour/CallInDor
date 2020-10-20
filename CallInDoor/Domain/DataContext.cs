using Domain.Entities;
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


        }



        /// <summary>
        /// پرمیشن های کاربر  
        /// </summary>
        public DbSet<Permissions> Permissions { get; set; }


        /// <summary>
        ///  سرویس(حوضه کاری)یه جورایی
        /// </summary>
        public DbSet<ServiceTBL> ServiceTBL { get; set; }

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



        /// <summary>
        ///کاربر مدرک رشته تحصیلی
        /// </summary>
        public DbSet<User_FieldTBL> UserField { get; set; }




        /// <summary>
        ///  این جدول پدر مشترک های بین سرویس ها توی این قرار میگیرد
        /// </summary>
        public DbSet<BaseMyServiceTBL> BaseMyServiceTBL { get; set; }




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

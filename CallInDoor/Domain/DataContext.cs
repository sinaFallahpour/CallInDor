﻿using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DataContext : IdentityDbContext<AppUser>
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

            //builder.Entity<MyChatServiceTBL>()
            //  .HasOne(m => m.BaseMyChatTBL)
            //  .WithOne(c => c.MyChatServiceTBL)
            //  .HasForeignKey<BaseMyServiceTBL>(x => x.MyChatId);



           
            //.HasOne(u => u.AppUser)
            //.WithMany(a => a.UserActivities)
            //.HasForeignKey(u => u.AppUserId);
        }



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
        ///مدرک ها   
        /// </summary>
        public DbSet<DegreeTBL> DegreeTBL { get; set; }


        /// <summary>
        ///رشته  های برای یک مدرک 
        /// </summary>
        public DbSet<FieldTBL> FieldTBL { get; set; }



        /// <summary>
        ///کاربر مدرک رشته تحصیلی
        /// </summary>
        public DbSet<User_Degree_FieldTBL> UserDegreeField { get; set; }




        /// <summary>
        ///  این جدول پدر مشترک های بین سرویس ها توی این قرار میگیرد
        /// </summary>
        public DbSet<BaseMyServiceTBL> BaseMyServiceTBL { get; set; }




        /// <summary>
        /// این جدول سرویس های که فقط از جنس چت و وویس و ویدیو هستند
        /// </summary>
        public DbSet<MyChatServiceTBL> MyChatServiceTBL { get; set; }




    }
}

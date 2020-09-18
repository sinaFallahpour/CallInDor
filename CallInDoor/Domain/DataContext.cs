using Domain.Entities;
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



        //public DataContext(DbContextOptions options) : base(options)
        //{
        //}



        /// <summary>
        ///  سرویس(حوضه کاری)یه جورایی
        /// </summary>
        public DbSet<ServiceTBL> ServiceTBL { get; set; }


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
        public DbSet<User_Degree_Field> UserDegreeField { get; set; }


    }
}

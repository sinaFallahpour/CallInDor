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

    }
}

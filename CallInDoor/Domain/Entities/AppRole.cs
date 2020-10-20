using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class AppRole : IdentityRole
    {

        public AppRole(string name)
        {
            this.Name = name;
        }


        public bool IsEnabled { get; set; }



        #region  relation

        public ICollection<ServiceTBL> Services { get; set; }

        public ICollection<Permissions> Permissions;

        #endregion




    }
}

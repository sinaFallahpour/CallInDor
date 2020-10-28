﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    public class AppUser : IdentityUser
    {

        public string Name { get; set; }
        public string LastName { get; set; }
        public string Bio { get; set; }
        public string ImageAddress { get; set; }



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


        public DateTime  CreateDate { get; set; }


        #region  Relation

        public virtual ICollection<User_FieldTBL> UsersFields { get; set; }


        /// <summary>
        /// سرویس های من از نوع پت یا وویس  یا ویدیو
        /// </summary>
        //public virtual ICollection<BaseMyServiceTBL> MyChatServices { get; set; }


        #endregion 
    }
}

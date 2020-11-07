using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTO.Account
{
    public class GetListOfUsersForVerification
    {

        //public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string ImageAddress { get; set; }

        public bool IsEditableProfile { get; set; }
        //آیا کاربر سازمانیست؟
        public bool IsCompany { get; set; }

        //وضعیت تایید پروفایل
        public ProfileConfirmType ProfileConfirmType { get; set; }

        public string CountryCode { get; set; }

        public DateTime CreateDate { get; set; }

        public bool isLockOut { get; set; }

    }
}

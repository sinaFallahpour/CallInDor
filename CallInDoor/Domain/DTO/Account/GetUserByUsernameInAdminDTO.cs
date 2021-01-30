using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTO.Account
{
    public class GetUserByUsernameInAdminDTO
    {

        public string Email { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime CreateDate { get; set; }
        public string Bio { get; set; }
        public string CountryCode { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public string VideoAddress { get; set; }
        public string ImageAddress { get; set; }

        public bool isLockOut { get; set; }
        public bool IsCompany { get; set; }
        public Gender Gender { get; set; }
        public bool IsEditableProfile { get; set; }

        public List<Fields> Fields { get; set; }
        public List<ProfileCertificates> requiredFiles { get; set; }

        //Fields = c.Fields.Select(c => new { c.Title, c.DegreeType

    }



    public class Fields
    {
        public string Title { get; set; }
        public DegreeType DegreeType { get; set; }

    }


    public class ProfileCertificates
    {
        public string ServiceName { get; set; }

        public int? ServiceId { get; set; }

        public ProfileConfirmType ProfileConfirmType { get; set; }


        public List<Files> Files { get; set; }
        //public string FileAddress { get; set; }

    }


    public class Files
    {
        public string ServiceName { get; set; }
        public int? ServiceId { get; set; }

        public string FileAddress { get; set; }

    }

}

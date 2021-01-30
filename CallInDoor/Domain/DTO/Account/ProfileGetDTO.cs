using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTO.Account
{
    public class ProfileGetDTO
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Bio { get; set; }
        public string ImageAddress { get; set; }
        public string VideoAddress { get; set; }
        public Gender Gender { get; set; }
        public bool IsCompany { get; set; }
        public bool IsEditableProfile { get; set; }
        public ProfileConfirmType ProfileConfirmType { get; set; }

        public string NationalCode { get; set; }
        public string BirthDate { get; set; }
        public List<FiledsDTO> Fields { get; set; }
        public List<ProfileCertificateDTO> ProfileCertificate { get; set; }
    }
}

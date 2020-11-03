using System;
using System.Collections.Generic;
using System.Security.Authentication.ExtendedProtection;
using System.Text;

namespace Domain.DTO.Account
{
    public class ProfileCertificateDTO
    {
        public int ServiceId { get; set; }

        public string  ServiceName { get; set; }

        public List<string>  RequiredCertificate { get; set; }

    }
}

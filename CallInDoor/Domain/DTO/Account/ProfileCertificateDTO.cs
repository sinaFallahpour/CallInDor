﻿using System;
using System.Collections.Generic;
using System.Security.Authentication.ExtendedProtection;
using System.Text;

namespace Domain.DTO.Account
{
    public class ProfileCertificateDTO
    {
        public int? ServiceId { get; set; }

        public string ServiceName { get; set; }

        public string ServicePersianName { get; set; }

        public List<RequiredCertificate> RequiredCertificate { get; set; }

    }



    public class RequiredCertificate
    {

        public int? Id { get; set; }
        public string FileName { get; set; }
        public string PersianFileName { get; set; }
    }

}

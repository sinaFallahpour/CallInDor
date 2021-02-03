using Domain.Enums;
using Domain.Migrations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTO.Service
{
    public class ProvideServicesDTO
    {

        public int Id { get; set; }
        public string ServiceName { get; set; }
        public string UserName { get; set; }
        public DateTime CreateDate { get; set; }
        public ServiceType ServiceType { get; set; }
        public ConfirmedServiceType ConfirmedServiceType { get; set; }
        public string ServiceTypeName { get; set; }
        /// <summary>
        /// ایا شرکتی آن را غیر فعال کرده یا خیر
        /// </summary>
        public bool IsDisabledByCompany { get; set; }


    }
}

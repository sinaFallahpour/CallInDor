using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTO.Service
{
    public class ServiceCategoryDTO
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public string Color { get; set; }

        public bool IsDisabledByCompany { get; set; }
    }
}

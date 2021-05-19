using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTO.Service
{
    public class SearchResponseDTO
    {
        //public string ServiceName { get; set; }

        public string UserId { get; set; }
        public string Username { get; set; }

        public string Name { get; set; }
        public string LastName { get; set; }

        public string ImageAddress { get; set; }

        public string CategoryName { get; set; }
        //public string CategoryPersianName { get; set; }

        public string SubCategoryName { get; set; }
        //public string SubCategoryPersianName { get; set; }

        public string Bio { get; set; }

        //public ServiceType ServiceType  { get; set; }

        /// <summary>
        /// تعداد ستاره ها
        /// </summary>
        public int StarCount { get; set; }


        //////public List<ServiceType> ServiceTypes { get; set; }
        public string ServiceTypes { get; set; }


        //public PackageType? PackageType  { get; set; }

        //نام سرویس هایی که کاربر  برای خودش ثبت کرد
        //public List<string> ServiceCategoryName { get; set; }


        //public string  ServiceName { get; set; }

        //public string TheServiceName { get; set; }
        //public ServiceType ServiceType { get; set; }






    }

}

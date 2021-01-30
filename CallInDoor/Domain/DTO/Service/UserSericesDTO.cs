using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTO.Service
{
    public class UserSericesDTO
    {

        public string ServiceName { get; set; }

        public string ServiceCateGoryName { get; set; }
        public string ServiceCateGoryPersianName { get; set; }

        public string CategoryName { get; set; }

        public string CategoryPersianName { get; set; }


        public string SubcategoryName { get; set; }
        public string SubcategoryPersianName { get; set; }
        public PackageType? PackageType { get; set; }


        public int? FreeMessageCount { get; set; }

        public int ? Duration { get; set; }

        public double? ServicePrice { get; set; }
        public double? CoursePrice { get; set; }


        public ServiceType ServiceType { get; set; }


        //serviceCateGoryName = c.ServiceTbl.Name,
        //           serviceCateGoryPersianName = c.ServiceTbl.PersianName,

        //           ServiceName=  c.ServiceName,


        //           categoryName = c.CategoryTBL.Title,
        //           categoryPersianName = c.CategoryTBL.PersianTitle,

        //           SubcategoryName = c.SubCategoryTBL.Title,
        //           SubcategoryPersianName = c.SubCategoryTBL.PersianTitle,
        //           c.MyChatsService.PackageType,

        //           c.MyChatsService.FreeMessageCount,
        //           c.MyChatsService.Duration,

        //           ServicePrice = c.MyServicesService.Price,




    }
}

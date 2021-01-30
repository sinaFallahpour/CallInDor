using Domain.Enums;
using Domain.Migrations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTO.Service
{
    public class SearchDTO
    {
        public string ServiceName { get; set; }
        public int? Page { get; set; }
        public int? PerPage { get; set; }
        public ServiceType? ServiceType { get; set; }


        public int? ServiceCatgoryId { get; set; }

        public int? CategoryId { get; set; }
        public int? SubCateGoryId { get; set; }

        //public FilterServiceType? FilterServiceType { get; set; }


        public bool OnlyOnlineProvider { get; set; }
        public bool OnlyCompanyProvider { get; set; }
        public bool OnlyTrustedProvider { get; set; }

        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }

        public PackageType? PackageType { get; set; }


        //public OrderPriceForSearchType? OrderForSearchType { get; set; }




        //public bool IsPriceAsc { get; set; }
       public bool IsPriceDesc { get; set; }
        public bool IsPopular { get; set; }


    }
}

using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTO.RequestService
{
    public class RequestWithBaseseServeicVM
    {
        public ServiceRequestTBL requestFromDB { get; set; }
        public int SitePercent { get; set; }
        public int ServiceTBLId { get; set; }
        public bool IsDeleted_baseService { get; set; }
        public int? MessageCount_baseService { get; set; }
        public double? PriceForNativeCustomer_baseService { get; set; }
        public double? PriceForNonNativeCustomer_baseService { get; set; }


        //requestFromDB = c,
        //                   c.BaseMyServiceTBL.ServiceTbl.SitePercent,
        //                   ServiceTBLId = c.BaseMyServiceTBL.ServiceTbl.Id,

        //                   IsDeleted_baseService = c.BaseMyServiceTBL.IsDeleted,

        //                   MessageCount_baseService = c.BaseMyServiceTBL.MyChatsService.MessageCount,
        //                   PriceForNativeCustomer_baseService = c.BaseMyServiceTBL.MyChatsService.PriceForNativeCustomer,
        //                   PriceForNonNativeCustomer_baseService = c.BaseMyServiceTBL.MyChatsService.PriceForNonNativeCustomer,


    }
}

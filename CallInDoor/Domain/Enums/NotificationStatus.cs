using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Enums
{
    public enum NotificationStatus
    {
        [Description("Proifle Confirmation")]
        [Display(Name = "Proifle Confirmation")]
        ProifleConfirmation,

        [Description("Proifle ProfileRejection")]
        [Display(Name = "Proifle ProfileRejection")]
        ProfileRejection,

        [Description("Service Confirmation")]
        [Display(Name = "Service Confirmation")]
        ServiceConfirmation,

        [Description("Service Rejection")]
        [Display(Name = "Service Rejection")]
        ServiceRejection,

        [Description("Request Confirmation")]
        [Display(Name = "Request Confirmation")]
        RequestConfirmation,

        [Description("Request Rejection")]
        [Display(Name = "Request Rejection")]
        RequestRejection,


        [Description("request to company")]
        [Display(Name = "request to company")]
        RequestToCompany,


        [Description("Accept request to Company")]
        [Display(Name = "Accept request to Company")]
        AcceptRequestToCompany,




        /// <summary>
        /// زمانی  که کمپانی یکی لز زیرمجموعه های قبلی اش را حذف کند
        /// </summary>
        [Description("company delete the subset")]
        [Display(Name = "company delete the subset")]
        CompanyDeleteTheSubset,




        /// <summary>
        /// تایید درخواست  برداشت پول
        /// </summary>
        [Description("accept widthrawl request")]
        [Display(Name = "accept widthrawl request")]
        AcceptWidthrawlRequest,

        /// <summary>
        /// رد درخواست برداشت پول 
        /// </summary>
        [Description("reject widthrawl request")]
        [Display(Name = "reject widthrawl request")]
        RejectWidthrawlRequest

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.DTO.Service
{
    public class RejectProvideServicesInAdminDTO
    {


        [Required(ErrorMessage = "{0} is  Required")]
        [Display(Name = "ServiceId ")]
        public int? ServiceId { get; set; }


        [Required(ErrorMessage = "{0} is  Required")]
        [Display(Name = "reason for reject")]
        public string RejectReason { get; set; }
    }
}

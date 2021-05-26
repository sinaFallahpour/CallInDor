using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.DTO.UserWithdrawlRequest
{
    public class RejectUserWithdrawlRequestInAdminDTO
    {
        [Required(ErrorMessage = "{0} is  Required")]
        public int? RequestId { get; set; }


        [Required(ErrorMessage = "{0} is  Required")]
        [Display(Name = "reason of reject")]
        public string RejectReason { get; set; }
    
    }

}

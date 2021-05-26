using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTO.UserWithdrawlRequest
{
    public class GetAllRequestForAdminDTO
    {

         
        public int? Page { get; set; }
        
        public int? PerPage { get; set; }

        public string SearchedWord { get; set; }

        public DateTime DateTime { get; set; }

        public WithdrawlRequestStatus? WithdrawlRequestStatus { get; set; }

 
    }
}

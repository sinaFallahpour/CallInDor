using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTO.Account
{
    public class ResponseType
    {
        public List<GetListOfUsersForVerification> Users { get; set; }
        public double TotalPages { get; set; }
    }
}

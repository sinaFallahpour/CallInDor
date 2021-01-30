using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTO.Service
{
    public class customModel
    {
        public string ClientUserName { get; set; }
        public string ProviderUserName { get; set; }
    }


    public class customModeWithId {
        public int  Id { get; set; }

        public string SenderUserName { get; set; }
        //public SendetMesageType SendetMesageType { get; set; }
    }
}

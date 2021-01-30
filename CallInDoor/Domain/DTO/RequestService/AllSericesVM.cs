using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTO.RequestService
{
    public class AllSericesVM

    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastTimeTheMessageWasSent { get; set; }
        public string ImageAddress { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int UnreadMessageCount { get; set; }
        //آیا فرسی یل پریودت است یا نه
        public bool IsPerodedOrsesionChat { get; set; }


    }
}

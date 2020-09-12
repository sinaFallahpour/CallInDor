using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTO.Response
{
    public class DataFormat
    {

        public int Status { get; set; }
        public string  Message { get; set; }

        public object data { get; set; }

    }
}

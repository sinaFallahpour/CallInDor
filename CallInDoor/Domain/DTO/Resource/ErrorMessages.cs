using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.DTO.Resource
{

    public class ErrorMessageDictionary
    {
        public List<ErrorMessages> Dictionary { get; set; }
    }

    public class ErrorMessages
    {

        public string Key { get; set; }
        public List<Languages> Languages { get; set; }
    }



    public class Languages
    {
        public string Header { get; set; }
        public string Val { get; set; }
    }


}

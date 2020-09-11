using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTO.Response
{
    public class ApiOkResponse : ApiResponse
    {
        public object Result { get; }

        public ApiOkResponse(object result, string message)
            : base(200, message)
        {
            Result = result;
        }
    }
}

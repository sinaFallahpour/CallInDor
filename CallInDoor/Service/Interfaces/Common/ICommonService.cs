using Domain.DTO.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Http.ModelBinding;

namespace Service.Interfaces.Common
{
    public interface ICommonService
    {

        ApiOkResponse OkResponse(object data, string message);

        ApiResponse ErrorResponse(int status, string message);

        List<string> ModelStateError(ModelStateDictionary modelState);
    }
}

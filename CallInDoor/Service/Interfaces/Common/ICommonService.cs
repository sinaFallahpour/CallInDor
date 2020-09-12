using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Http.ModelBinding;

namespace Service.Interfaces.Common
{
    public interface ICommonService
    {
        List<string> ModelStateError(ModelStateDictionary modelState);
    }
}

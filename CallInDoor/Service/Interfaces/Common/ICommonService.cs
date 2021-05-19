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
        ApiOkResponse OkResponse(object data, bool isAdmin);

        ApiResponse ErrorResponse(int status, string message);

        object NotFoundErrorReponse(bool isAdmin);

        object UnAuthorizeErrorReponse(bool isAdmin);
        object ForbiddenErrorReponse(bool isAdmin);

        List<string> ModelStateError(ModelStateDictionary modelState);


        public bool IsPersianLanguage();


        /// <summary>
        /// گرفتن زبان فعلی کاربر
        /// </summary>
        /// <returns></returns>
        public string GetCurrentCulture();


        /// <summary>
        ///    گرفتن مقدار نیم داینامیک مدل های دیتا بیسی 
        /// </summary>
        /// <returns></returns>
        public string GetNameByCulture(object item);

        

    }
}

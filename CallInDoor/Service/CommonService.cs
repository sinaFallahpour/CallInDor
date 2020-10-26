using Domain.DTO.Response;
using Service.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Http.ModelBinding;

namespace Service
{
    public class CommonService : ICommonService
    {



        /// <summary>
        /// Ok Response
        /// </summary>
        /// <param name="data"></param>
        /// <param name="status"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public ApiOkResponse OkResponse(object data, string message)
        {
            return new ApiOkResponse(new DataFormat()
            {
                Status = 1,
                data = data,
                Message = message
            },
            message
            );
        }




        /// <summary>
        /// not found unauthorize forbidden ,.... error response
        /// </summary>
        /// <param name="data"></param>
        /// <param name="status"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public ApiResponse ErrorResponse(int status, string message)
        {
            return new ApiResponse(status, message);
        }





        ///// <summary>
        ///// not found unauthorize forbidden ,.... error response
        ///// </summary>
        ///// <param name="data"></param>
        ///// <param name="status"></param>
        ///// <param name="message"></param>
        ///// <returns></returns>
        //public ApiBadRequestResponse BadRequestResponse(int status, string message)
        //{
        //    return new ApiResponse(404, message);
        //}





        public List<string> ModelStateError(ModelStateDictionary modelState)
        {
            var errors = new List<string>();
            foreach (var item in modelState.Values)
            {
                foreach (var err in item.Errors)
                {
                    errors.Add(err.ErrorMessage);
                }
            }
            return errors;
        }


    }
}

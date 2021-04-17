using Domain;
using Domain.DTO.Response;
using Domain.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Service.Interfaces.Common;
using Service.Interfaces.Resource;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.Http.ModelBinding;

namespace Service
{
    public class CommonService : ICommonService
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private IStringLocalizer<ShareResource> _localizerShared;
        private readonly IResourceServices _resourceServices;
        public CommonService(IHostingEnvironment hostingEnvironment,
            IStringLocalizer<ShareResource> localizerShared,
            IResourceServices resourceServices)
        {
            _hostingEnvironment = hostingEnvironment;
            _localizerShared = localizerShared;
            _resourceServices = resourceServices;
        }

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
        /// Ok Response
        /// </summary>
        /// <param name="data"></param>
        /// <param name="status"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public ApiOkResponse OkResponse(object data, bool isAdmin)
        {
            if (isAdmin)
            {
                return new ApiOkResponse(new DataFormat()
                {
                    Status = 1,
                    data = data,
                    Message = PubicMessages.SuccessMessage
                },
                PubicMessages.SuccessMessage
                );
            }

            return new ApiOkResponse(new DataFormat()
            {
                Status = 1,
                data = data,
                //Message = _localizerShared["SuccessMessage"].Value.ToString()
                Message = _resourceServices.GetErrorMessageByKey("SuccessMessage")
            },
            _resourceServices.GetErrorMessageByKey("SuccessMessage")
          //_localizerShared["SuccessMessage"].Value.ToString()
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





        public object NotFoundErrorReponse(bool isAdmin)
        {
            List<string> erros = new List<string>();

            if (isAdmin)
            {
                //return new ApiResponse(404, PubicMessages.NotFoundMessage) as ApiResponse;
                erros.Add(PubicMessages.NotFoundMessage);
                return new ApiBadRequestResponse(erros, 404) as ApiResponse;
            }
            erros.Add(_resourceServices.GetErrorMessageByKey("NotFound"));

            return new ApiBadRequestResponse(erros, 404) as ApiBadRequestResponse;
        }


        public object UnAuthorizeErrorReponse(bool isAdmin)
        {
            List<string> erros = new List<string>();
            if (isAdmin)
            {
                //return new ApiResponse(401, PubicMessages.UnAuthorizeMessage) as ApiResponse;
                erros.Add(PubicMessages.UnAuthorizeMessage);
                return new ApiBadRequestResponse(erros, 401) as ApiResponse;
            }
            erros.Add(_resourceServices.GetErrorMessageByKey("UnauthorizedMessage"));
            return new ApiBadRequestResponse(erros, 401) as ApiBadRequestResponse;
        }


        public object ForbiddenErrorReponse(bool isAdmin)
        {
            List<string> erros = new List<string>();
            if (isAdmin)
            {
                //return new ApiResponse(403, PubicMessages.ForbiddenMessage) as ApiResponse;
                erros.Add(PubicMessages.ForbiddenMessage);
                return new ApiBadRequestResponse(erros, 403) as ApiResponse;
            }
            erros.Add(_resourceServices.GetErrorMessageByKey("ForniddenMessage"));
            return new ApiBadRequestResponse(erros, 401) as ApiBadRequestResponse;
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



        public string SvaeFileToHost(string path, IFormFile file)
        {
            try
            {
                if (file == null)
                    return null;
                string uniqueFileName = null;
                var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, path);
                uniqueFileName = (Guid.NewGuid().ToString().GetImgUrlFriendly() + "_" + file.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                //model.PhotoAddress = "/Upload/Slider/" + uniqueFileName;
                return path + uniqueFileName;
            }
            catch
            {
                return null;
            }
        }


        public bool IsPersianLanguage()
        {
            if (CultureInfo.CurrentCulture.Name == PublicHelper.persianCultureName)
                return true;
            return false;
        }

    }
}

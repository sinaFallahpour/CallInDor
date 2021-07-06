using Domain;
using Domain.DTO.Language;
using Domain.DTO.Response;
using Domain.Entities;
using Domain.Utilities;
using Helper.Models.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Service.Interfaces.Common;
using Service.Interfaces.Resource;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
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

        public string GetCurrentCulture()
        {
            return CultureInfo.CurrentCulture.Name;
        }

        public string GetNameByCulture(object item)
        {
            string value = "";

            (string value, bool succsseded) res;
            foreach (PropertyInfo propertyInfo in item.GetType().GetProperties())
            {
                //CategoryTBL
                if (item.GetType() == typeof(CategoryTBL))
                {
                    LanguageDTO model = new LanguageDTO
                    {
                        Persian = "PersianTitle",
                        English = "Title",
                        Arab = "ArabTitle",
                    };
                    //value 
                    res = GetObjectValue(item, model, propertyInfo);
                    if (res.succsseded)
                    {
                        value = res.value;
                        break;
                    }


                }


                //AnswerTBL
                if (item.GetType() == typeof(AnswerTBL))
                {
                    LanguageDTO model = new LanguageDTO
                    {
                        Persian = "Text",
                        English = "EnglishText",
                        Arab = "ArabText",
                    };
                    //value = GetObjectValue(item, model, propertyInfo);

                    res = GetObjectValue(item, model, propertyInfo);
                    if (res.succsseded)
                    {
                        value = res.value;
                        break;
                    }
                }

                //AreaTBL
                if (item.GetType() == typeof(AreaTBL))
                {
                    LanguageDTO model = new LanguageDTO
                    {
                        Persian = "PersianTitle",
                        English = "Title",
                        Arab = "ArabTitle",
                    };
                    //value = GetObjectValue(item, model, propertyInfo);
                    res = GetObjectValue(item, model, propertyInfo);
                    if (res.succsseded)
                    {
                        value = res.value;
                        break;
                    }
                }

                //CheckDiscountTBL
                if (item.GetType() == typeof(CheckDiscountTBL))
                {
                    LanguageDTO model = new LanguageDTO
                    {
                        Persian = "PersianTitle",
                        English = "EnglishTitle",
                        Arab = "ArabTitle",
                    };
                    //value = GetObjectValue(item, model, propertyInfo);

                    res = GetObjectValue(item, model, propertyInfo);
                    if (res.succsseded)
                    {
                        value = res.value;
                        break;
                    }
                }


                //NotificationTBL
                if (item.GetType() == typeof(NotificationTBL))
                {
                    LanguageDTO model = new LanguageDTO
                    {
                        Persian = "TextPersian",
                        English = "EnglishText",
                        Arab = "ArabText",
                    };
                    //value = GetObjectValue(item, model, propertyInfo);

                    res = GetObjectValue(item, model, propertyInfo);
                    if (res.succsseded)
                    {
                        value = res.value;
                        break;
                    }
                }

                //QuestionPullTBL
                if (item.GetType() == typeof(QuestionPullTBL))
                {
                    LanguageDTO model = new LanguageDTO
                    {
                        Persian = "Text",
                        English = "EnglishText",
                        Arab = "ArabText",
                    };
                    //value = GetObjectValue(item, model, propertyInfo);

                    res = GetObjectValue(item, model, propertyInfo);
                    if (res.succsseded)
                    {
                        value = res.value;
                        break;
                    }
                }


                //ServiceTagsTBL
                if (item.GetType() == typeof(ServiceTagsTBL))
                {
                    LanguageDTO model = new LanguageDTO
                    {
                        Persian = "PersianTagName",
                        English = "TagName",
                        Arab = "ArabTagName",
                    };
                    //value = GetObjectValue(item, model, propertyInfo);

                    res = GetObjectValue(item, model, propertyInfo);
                    if (res.succsseded)
                    {
                        value = res.value;
                        break;
                    }
                }


                //ServiceTBL
                if (item.GetType() == typeof(ServiceTBL))
                {
                    LanguageDTO model = new LanguageDTO
                    {
                        Persian = "PersianName",
                        English = "Name",
                        Arab = "ArabName",
                    };
                    //value = GetObjectValue(item, model, propertyInfo);

                    res = GetObjectValue(item, model, propertyInfo);
                    if (res.succsseded)
                    {
                        value = res.value;
                        break;
                    }
                }


                //ServidceTypeRequiredCertificatesTBL
                if (item.GetType() == typeof(ServidceTypeRequiredCertificatesTBL))
                {
                    LanguageDTO model = new LanguageDTO
                    {
                        Persian = "PersianFileName",
                        English = "FileName",
                        Arab = "ArabFileName",
                    };
                    //value = GetObjectValue(item, model, propertyInfo);


                    res = GetObjectValue(item, model, propertyInfo);
                    if (res.succsseded)
                    {
                        value = res.value;
                        break;
                    }
                }

                //SettingTBL
                if (item.GetType() == typeof(SettingTBL))
                {
                    LanguageDTO model = new LanguageDTO
                    {
                        Persian = "Value",
                        English = "EnglishValue",
                        Arab = "ArabValue",
                    };
                    //value = GetObjectValue(item, model, propertyInfo);
                    res = GetObjectValue(item, model, propertyInfo);
                    if (res.succsseded)
                    {
                        value = res.value;
                        break;
                    }
                }

                //SpecialityTBL
                if (item.GetType() == typeof(SpecialityTBL))
                {
                    LanguageDTO model = new LanguageDTO
                    {
                        Persian = "PersianName",
                        English = "EnglishName",
                        Arab = "ArabName",
                    };
                    //value = GetObjectValue(item, model, propertyInfo);

                    res = GetObjectValue(item, model, propertyInfo);
                    if (res.succsseded)
                    {
                        value = res.value;
                        break;
                    }
                }

            }
            //return value;
            return value;
        }

        private (string value, bool succsseded) GetObjectValue(object item, LanguageDTO model, PropertyInfo propertyInfo)
        {

            bool isSuccsseded = false;
            string value = "";

            var curentCulture = GetCurrentCulture();
            if (curentCulture == PublicHelper.persianCultureName)
            {
                if (propertyInfo.Name == model.Persian)
                {
                    isSuccsseded = true;
                    value = propertyInfo.GetValue(item, null).ToString();
                }
            }
            else if (curentCulture == PublicHelper.arabCultureName)
            {
                if (propertyInfo.Name == model.Arab)
                {
                    isSuccsseded = true;
                    value = propertyInfo.GetValue(item, null).ToString();
                }
            }
            else
            {
                if (propertyInfo.Name == model.English)
                {
                    isSuccsseded = true;
                    value = propertyInfo.GetValue(item, null).ToString();
                }
            }
            return (value, isSuccsseded);
        }





    }
}

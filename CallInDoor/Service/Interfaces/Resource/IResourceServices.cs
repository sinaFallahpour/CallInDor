using Domain.DTO.Resource;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces.Resource
{
    public abstract class IResourceServices
    {


        /// <summary>
        /// برگرداندن یک نام کلید برای  داینامیک دیتا های سایت
        /// </summary>
        /// <param name="KeyName"></param>
        /// <param name="id"></param>
        public abstract string SetKeyName(string KeyName, object id);


        /// <summary>
        ///ولیدیت کردن هدر accept language   
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public abstract (bool succsseded, List<string> result) ValidateAcceptLanguageHeader();

        /// <summary>
        /// گرفتن ادرس فایل ریسورس فعلی
        /// </summary>
        /// <returns></returns>
        public abstract string GetCurrentResourceFileAddress();

        /// <summary>
        ///ولیدیت کردن هدر accept language   
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public abstract (bool succsseded, List<string> result) AddToShareResource(EditDataAnotationAndErrorMessageDTO mode);

        public abstract List<KeyValueDTO> GetDataAnotationAndErrorMessages();




        /// <summary>
        /// گرفتن header acept language
        /// </summary>
        /// <returns></returns>
        public abstract string GetAcceptLanguageHeader();

    }
}

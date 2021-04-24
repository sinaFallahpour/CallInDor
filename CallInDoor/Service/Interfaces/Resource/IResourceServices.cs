using Domain.DTO.Resource;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces.Resource
{
    public abstract class IResourceServices
    {
        public abstract string GetCurrentAcceptLanguageHeader();

        public abstract ErrorMessageDictionary GetErrorMessageObject();

        public abstract List<KeyValueDTO> GetAllErrorMessage(LanguageHeader languageHeader);

        /// <summary>
        /// Get value of a key by key name
        /// </summary>
        /// <param name="key"></param>
        /// <param name="languageHeader"></param>
        /// <returns></returns>
        public abstract string GetErrorMessageByKey(string key);

        /// <summary>
        /// Edit value Of ErrorMessages in json File
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorMessageDictionary"></param>
        /// <param name="headerName"></param>
        /// <returns></returns>
        public abstract (bool succsseded, List<string> result) EditJsonResource(EditErrorMessageDTO2 model, string headerName);




        /// <summary>
        /// convert static word json file to C# Object
        /// </summary>
        /// <returns></returns>
        public abstract ErrorMessageDictionary GetStaticWordObject();



        /// <summary>
        /// Edit Value Of Static Word of site  in json File
        /// </summary>
        /// <param name="model"></param>
        /// <param name="headerName"></param>
        /// <returns></returns>
        public abstract (bool succsseded, List<string> result) EditStaticWordJsonResource(EditStaticWordDTO2 model, string headerName);



        /// <summary>
        /// Get All Static Word Of Site Drfom Json File
        /// </summary>
        /// <param name="languageHeader"></param>
        /// <returns></returns>
        public abstract List<KeyValueDTO> GetAllStaticWord(LanguageHeader languageHeader);

















        ///// <summary>
        ///// برگرداندن یک نام کلید برای  داینامیک دیتا های سایت
        ///// </summary>
        ///// <param name="KeyName"></param>
        ///// <param name="id"></param>
        //public abstract string SetKeyName(string KeyName, object id);


        ///// <summary>
        /////ولیدیت کردن هدر accept language   
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //public abstract (bool succsseded, List<string> result) ValidateAcceptLanguageHeader();

        ///// <summary>
        ///// گرفتن ادرس فایل ریسورس فعلی
        ///// </summary>
        ///// <returns></returns>
        //public abstract string GetCurrentResourceFileAddress();

        ///// <summary>
        /////ولیدیت کردن هدر accept language   
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //public abstract (bool succsseded, List<string> result) AddToShareResource(EditDataAnotationAndErrorMessageDTO mode);

        //public abstract List<KeyValueDTO> GetDataAnotationAndErrorMessages();





        ///// <summary>
        ///// گرفتن header acept language
        ///// </summary>
        ///// <returns></returns>
        //public abstract string GetAcceptLanguageHeader();





    }
}

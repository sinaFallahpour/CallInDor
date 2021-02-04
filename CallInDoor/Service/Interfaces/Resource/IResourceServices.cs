using System;
using System.Collections.Generic;
using System.Text;

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


    }
}

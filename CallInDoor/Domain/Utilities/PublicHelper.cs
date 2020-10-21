using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Utilities
{
    public static class PublicHelper
    {

        public const string ADMINROLE = "Admin";
        public const string USERROLE = "User";

        public const string USERPHOTOADDRESS = "PhotoAddress";


        public const string SECREKEY = "This is the secret key and its very important";

        public const string SerialNumberClaim = "serialNumber";


        public const string EngCultureName = "en-US";
        public const string persianCultureName = "fa-IR";

        public const string swaggerProductionUrl = "https://api.callindoor.ir";
        public const string swaggerdevelopmentUrl = "https://localhost:44377";

    }

    public static class PubicMessages
    {
        public const string UnAuthorizeMessage = "You are Unauthorized";
        public const string NotFoundMessage = " Not Found";
        public const string ForbidenMessage = "Forbidden";
        public const string BadRequestMessage = "Bad Request";
        public const string InternalServerMessage = "An unhandled error occurred";

        public const string SuccessMessage = "Successful Submit";
        public const string ErrorMessage = "Operation Failed";



        //  public static void SetNotFound(str) { 
        //NotFoundMessage= 
        //  }

    }


}

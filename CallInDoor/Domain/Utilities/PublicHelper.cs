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





        public const string AboutUsKeyName = "AboutUs";
        public const string AddressKeyName = "Address";
        public const string PhoneNumberKeyName = "PhoneNumber";
        public const string EmailKeyName = "Email";

        public const string ProfileConfirmNotificationKeyName = "ProfileConfirmNotification";
        public const string ProfileRejectNotificationKeyName = "ProfileRejection";

        public const string ServiceConfimNotificationKeyName = "ServiceConfimNotification";
        public const string ServiceRejectionKeyName = "ServiceRejection";






        //کلید redis
        public const string Redis_ChatVoiceDurationKeyName = "ChatVoiceDuration";







        //public string ServiceRejectNotificationText { get; set; }


        //public string ServiceRejectNotificationTextEnglish { get; set; }




        //public string ProfileRejectNotificationText { get; set; }


        //public string ProfileRejectNotificationTextEnglish { get; set; }








        public const string Question1KeyName = "Question1";
        public const string Question2KeyName = "Question2";
        public const string Question3KeyName = "Question3";
        public const string Question4KeyName = "Question4";
        public const string Question5KeyName = "Question5";


        public const string Answer10KeyName = "Answer10";
        public const string Answer11KeyName = "Answer11";
        public const string Answer12KeyName = "Answer12";
        public const string Answer13KeyName = "Answer13";
        public const string Answer20KeyName = "Answer20";
        public const string Answer21KeyName = "Answer21";
        public const string Answer22KeyName = "Answer22";
        public const string Answer23KeyName = "Answer23";
        public const string Answer30KeyName = "Answer30";
        public const string Answer31KeyName = "Answer31";
        public const string Answer32KeyName = "Answer32";
        public const string Answer33KeyName = "Answer33";
        public const string Answer40KeyName = "Answer40";
        public const string Answer41KeyName = "Answer41";
        public const string Answer42KeyName = "Answer42";
        public const string Answer43KeyName = "Answer43";
        public const string Answer50KeyName = "Answer50";
        public const string Answer51KeyName = "Answer51";
        public const string Answer52KeyName = "Answer52";
        public const string Answer53KeyName = "Answer53";



    }

    public static class PubicMessages
    {
        public const string UnAuthorizeMessage = "Unauthorized";
        public const string ForbiddenMessage = "Forbidden";
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

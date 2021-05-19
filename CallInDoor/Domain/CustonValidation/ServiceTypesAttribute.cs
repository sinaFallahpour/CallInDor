using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.CustonValidation
{
    public class ServiceTypesAttribute : ValidationAttribute
    {
        public string ServiceTypes { get; set; }

        public override bool IsValid(object value)
        {
            bool isValid = true;
            var strValue = value as string;
            var types = strValue.Split(",");
            var requiresServiceType = ServiceTypes.Split(",");
            foreach (var item in requiresServiceType)
            {
                isValid = IsValidItem(item, types);
                if (!isValid)
                {
                    isValid = false;
                    break;
                }
            }
            return isValid;
        }



        public bool IsValidItem(string serviceTypes, string[] value)
        {
            bool isValid = true;
            //var serviceTypesIntValue = serviceTypes;
            //var serviceTypesSrrtingValue = serviceTypesIntValue.ToString();

            bool isExist = false;
            foreach (var item in value)
            {
                int number;
                bool success = Int32.TryParse(item, out number);
                if (!success) {
                    isValid = false;
                    break;
                }
                
                var IsInServiceType = Enum.IsDefined(typeof(ServiceType), number);
                if (!IsInServiceType)
                {
                    isValid = false;
                    break;
                }

                if (item == serviceTypes.ToString())
                    isExist = true;
            }
            if (isExist == false)
                isValid = false;

            return isValid;
        }



    }
}

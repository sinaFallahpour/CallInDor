using Domain.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.CustonValidation
{
    public class CheckCultureIsValidAttribute : ValidationAttribute
    {

        public override bool IsValid(object value)
        {
            var strValue = value as string;
            bool isValid = PublicHelper.IsCultureValid(strValue);
            return isValid;
        }

    }
}

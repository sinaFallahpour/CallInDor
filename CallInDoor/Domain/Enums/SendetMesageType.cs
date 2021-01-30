using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Enums
{
    /// <summary>
    /// چه کسی پیام را فرستاد
    /// </summary>
    public enum SendetMesageType
    {
        [Description("Client")]
        [Display(Name = "Client")]
        Client,
        [Description("Provider")]
        [Display(Name = "Provider")]
        Provider
    }
}

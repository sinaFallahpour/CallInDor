using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Enums
{
    public enum OrderForSearchType
    {

        [Description("Popular")]
        [Display(Name = "Popular")]
        Popular,
        [Description("PriceAsc")]
        [Display(Name = "PriceAsc")]
        PriceAsc,
        [Description("PriceDec")]
        [Display(Name = "PriceDec")]
        PriceDec,

    }
}

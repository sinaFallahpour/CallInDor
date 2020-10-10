using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Domain.Enums
{
    public enum DegreeType
    {
        [Description("کارشناسی")]
        Masters,
        [Description("کارشناسی ارشد")]
        SeniorMaster,
        [Description("دکترا")]
        Doctorate,
        [Description("فوق دکترا")]
        SeniorDoctorate,
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Domain.Enums
{
    public enum DegreeType
    {
      
        [Description("زیر دیپلم")]
        UnderDiploma,
        [Description("دیپلم")]
        Diploma,
        [Description("فوق دیپلم")]
        Assosiate,
        [Description("کارشناسی")]
        Bachelor,
        [Description("کارشناسی ارشد")]
        Masters,
        [Description("دکترا")]
        Doctorate,
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTO.fuck
{
    class sadhgjd
    {


        public string ds(IServiceProvider  ser)
        {
            var sds = ser.GetRequiredService<DbContext>();
            return "sas";
        }

    }
}

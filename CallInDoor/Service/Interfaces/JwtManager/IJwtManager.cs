using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces.JwtManager
{
    public interface IJwtManager
    {
        //string CreateToken(AppUser user);
        Task<string> CreateToken(AppUser user);

    }
}

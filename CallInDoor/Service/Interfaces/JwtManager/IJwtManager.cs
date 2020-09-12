using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interfaces.JwtManager
{
    public interface IJwtManager
    {
        string CreateToken(AppUser user);
    }
}

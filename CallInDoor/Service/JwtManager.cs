using Domain.Entities;
using Domain.Utilities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
//using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Service.Interfaces.JwtManager;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Domain;
using System.Linq;

namespace Service
{
    public class JwtManager : IJwtManager
    {
        private readonly SymmetricSecurityKey _key;
        private readonly DataContext _context;

        public JwtManager(IConfiguration config,
               DataContext context
            )
        {
            _context = context;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(PublicHelper.SECREKEY));
        }

        public string CreateToken(AppUser user)
        {


            //var query = (from u in _context.Users.Where(c => c.Id == user.Id)
            //             join ur in _context.UserRoles
            //             on u.Id equals ur.UserId
            //             join r in _context.Roles
            //             on ur.RoleId equals r.Id
            //             join per in _context.Permissions
            //             on r.Id equals per.RoleId
            //             //where r.Name != "User"
            //             select new
            //             {
            //                 u,
            //                 r,
            //                 r.Permissions,
            //                 ur,
            //                 per,
            //                 //u.Id,
            //                 //u.UserName,
            //                 //ur.RoleId,
            //                 //PhoneNumber = ReomeveSomeString(u.PhoneNumber, u.CountryCode),
            //                 //CountryCode = u.CountryCode,
            //                 //roleName = r.Name,
            //                 //roleId = r.Id
            //             }).AsQueryable();

            //var uw = query.FirstOrDefault();
            //var  ds  =  u.r
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                new Claim("role", user.Role),
                new Claim(PublicHelper.SerialNumberClaim, user.SerialNumber)
            };

            // generate signing credentials
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds,
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}

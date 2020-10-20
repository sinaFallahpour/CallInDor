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
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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

        public async Task<string> CreateToken(AppUser user)
        {


            ////////////var query = (from u in _context.Users.Where(c => c.Id == user.Id)
            ////////////             join ur in _context.UserRoles
            ////////////             on u.Id equals ur.UserId
            ////////////             join r in _context.Roles
            ////////////             on ur.RoleId equals r.Id
            ////////////             join rolPer in _context.Role_Permission
            ////////////             on r.Id equals rolPer.RoleId
            ////////////             //where r.Name != "User"
            ////////////             select new
            ////////////             {
            ////////////                 u,
            ////////////                 r,
            ////////////                 r.Role_Permissions,
            ////////////                 ur,
            ////////////                 rolPer,
            ////////////                 //u.Id,
            ////////////                 //u.UserName,
            ////////////                 //ur.RoleId,
            ////////////                 //PhoneNumber = ReomeveSomeString(u.PhoneNumber, u.CountryCode),
            ////////////                 //CountryCode = u.CountryCode,
            ////////////                 //roleName = r.Name,
            ////////////                 //roleId = r.Id
            ////////////             }).AsQueryable();






            var query1 = (from u in _context.Users.Where(c => c.Id == user.Id)
                          join ur in _context.UserRoles
                          on u.Id equals ur.UserId
                          join r in _context.Roles
                         on ur.RoleId equals r.Id
                          select new
                          {
                              u.Id,
                              u.UserName,
                              u.SerialNumber,
                              ur.RoleId,
                              roleName = r.Name
                          }).AsQueryable();

            var res = await query1.FirstOrDefaultAsync();
            var permissions = await _context.Role_Permission.Where(c => c.RoleId == res.RoleId)
                .Select(c => new
                {
                    c.Permissions.ActionName,
                })
                .ToListAsync();

            //var query = (from u in _context.Users.Where(c => c.Id == user.Id)
            //             join ur in _context.UserRoles
            //             on u.Id equals ur.UserId



            //             join r in _context.Roles
            //             on ur.RoleId equals r.Id
            //             join rolPer in _context.Role_Permission
            //             on r.Id equals rolPer.RoleId
            //             //where r.Name != "User"
            //             select new
            //             {
            //                 u,
            //                 r,
            //                 r.Role_Permissions,
            //                 ur,
            //                 rolPer,
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

            var claims = new ClaimsIdentity();
            foreach (var permission in permissions)
            {
                claims.AddClaims(new[]
                {
                    new Claim(PublicPermissions.Permission, permission.ActionName)
                });
            }
            claims.AddClaims(new[]
               {
                    new Claim(JwtRegisteredClaimNames.UniqueName, res.UserName),
                    new Claim("role", res.roleName),
                    new Claim(PublicHelper.SerialNumberClaim, res.SerialNumber)
                });


            //claims.AddClaims(new[]
            //  {
            //    new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
            //    new Claim("role", res.roleName),
            //    new Claim(PublicHelper.SerialNumberClaim, user.SerialNumber)
            // });

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


using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces.Company;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using System.Linq;
using Service.Interfaces.Account;
using Domain.Enums;

namespace Service
{
    public class CompanyService : ICompanyService
    {

        private readonly DataContext _context;
        private readonly IAccountService _accountService;

        public CompanyService(DataContext context, IAccountService accountService)
        {
            _context = context;
            _accountService = accountService;
        }


        public Task<bool> GetCompanyServiceUserTBL(string userId, int serviceTBLId)
        {
            var currentUserName = _accountService.GetCurrentUserName();
            
            var isExist = _context.CompanyServiceUserTBL
                                    .AsNoTracking()
                                    .AnyAsync(c => c.CompanyUserName == currentUserName && c.ConfirmStatus == ConfirmStatus.Confirmed &&
                                    c.subSetUserName == userId && c.ServiceId == serviceTBLId);                                
            return isExist;
        }
    }
}

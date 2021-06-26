using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces.Account;
using Service.Interfaces.Discount;
using Service.Interfaces.Resource;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class DiscountService : IDiscountService
    {

        private readonly DataContext _context;
        private readonly IAccountService _accountService;
        private readonly IResourceServices _resourceServices;

        public DiscountService(DataContext context, IAccountService accountService, IResourceServices resourceServices)
        {
            _context = context;
            _accountService = accountService;
            _resourceServices = resourceServices;
        }



        public async Task<CheckDiscountTBL> GetDiscountByCode(string discountCode)
        {
            var discountFromDB = await _context.CheckDiscountTBL.FirstOrDefaultAsync(c => c.Code == discountCode);
            return discountFromDB;
        }



        public async Task<(bool succsseded, List<string> result)> ValidateDiscount(CheckDiscountTBL discountFromDB)
        {
            bool IsValid = true;
            List<string> Errors = new List<string>();
            string currentUsername = _accountService.GetCurrentUserName();

            if (discountFromDB == null)
            {
                IsValid = false;
                Errors.Add(_resourceServices.GetErrorMessageByKey("InvalidDiscountCode"));
                return (IsValid, Errors);
            }

            if (discountFromDB.ExpireTime < DateTime.Now)
            {
                IsValid = false;
                Errors.Add(_resourceServices.GetErrorMessageByKey("InvalidDiscountCode"));
                return (IsValid, Errors);
            }

            var isUsedDiscount = await _context.DiscountUsedByUserTBL.AnyAsync(c => c.UserName == currentUsername && c.CheckDiscountId == discountFromDB.Id);
            if (isUsedDiscount)
            {
                IsValid = false;
                Errors.Add(_resourceServices.GetErrorMessageByKey("YouUsedDiscountCode"));
                return (IsValid, Errors);
            }

            return (IsValid, Errors);

        }
    }
}

using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces.Discount
{
    public interface IDiscountService
    {
        public Task<CheckDiscountTBL> GetDiscountByCode(string discountCode);
        public Task<(bool succsseded, List<string> result)> ValidateDiscount(CheckDiscountTBL model);
    }
}

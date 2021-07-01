using Domain;
using Domain.DTO.Transaction;
using Domain.Entities;
using Domain.Entities.Requests;
using Domain.Enums;
using Service.Interfaces.Account;
using Service.Interfaces.Resource;
using Service.Interfaces.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class TransactionService : ITransactionService
    {

        private readonly DataContext _context;
        private readonly IAccountService _accountService;
        private readonly IResourceServices _resourceServices;

        public TransactionService(DataContext context, IAccountService accountService, IResourceServices resourceServices)
        {
            _context = context;
            _accountService = accountService;
            _resourceServices = resourceServices;
        }





        public IQueryable<TransactionTBL> Filter(GetAllTransactionInAdmin model, IQueryable<TransactionTBL> QueryAble)
        {
            if (!string.IsNullOrEmpty(model.SearchedWord))
            {
                QueryAble = QueryAble.Where(c =>
                           c.Username.ToLower().StartsWith(model.SearchedWord.ToLower())
                           || c.Username.ToLower().Contains(model.SearchedWord.ToLower()));
            }

            if (model.CreateDate != null)
            {
                QueryAble = QueryAble
                  .Where(c => c.CreateDate > model.CreateDate);
            }


            if (model.TransactionStatus != null)
            {
                QueryAble = QueryAble
                  .Where(c => c.TransactionStatus == model.TransactionStatus);
            }

            if (model.TransactionType != null)
            {
                QueryAble = QueryAble
                 .Where(c => c.TransactionType == model.TransactionType);
            }

            //if (model.ServiceTypeWithDetails != null)
            //{
            //    QueryAble = QueryAble
            //     .Where(c => c.ServiceTypeWithDetails == model.ServiceTypeWithDetails);
            //}

            if (!string.IsNullOrEmpty(model.ServiceTypeWithDetails))
            {
                QueryAble = QueryAble.Where(c => c.ServiceTypeWithDetails.Contains(model.ServiceTypeWithDetails));
            }


            if (model.TransactionConfirmedStatus != null)
            {
                QueryAble = QueryAble
                 .Where(c => c.TransactionConfirmedStatus == model.TransactionConfirmedStatus);
            }

            return QueryAble;
        }

        public async Task<ClientProviderShoulPayVM> HandleCaLlTransaction(BaseRequestServiceTBL model)
        {
            try
            {
                var prices = model.Price;
                var sitePercent = model.BaseMyServiceTBL.ServiceTbl.SitePercent;
                var discountPercent = model.CheckDiscountTBL == null ? 0 : model.CheckDiscountTBL.Percent;

                double clientShouldPay = 0;
                double providerShouldPay = 0;
                double porsant = 0;

                clientShouldPay = (double)model.Price - (double)(model.Price * (discountPercent / 100));
                providerShouldPay = (double)model.Price - (double)(model.Price * (sitePercent / 100));
                porsant = (double)model.Price * (sitePercent / 100);

                var clientTransaction = new TransactionTBL()
                {
                    Amount = clientShouldPay,
                    Username = model.ClienUserName,
                    ProviderUserName = model.ProvideUserName,
                    ClientUserName = model.ClienUserName,
                    CreateDate = DateTime.Now,
                    BaseMyServiceId = model.BaseServiceId,
                    //////ServiceTypeWithDetails = ServiceTypeWithDetails.ChatVoiceLimited,
                    ServiceTypeWithDetails = model.ServiceTypes,
                    TransactionType = TransactionType.WhiteDrawl,
                    TransactionStatus = TransactionStatus.ServiceTransaction,
                    TransactionConfirmedStatus = TransactionConfirmedStatus.Confirmed,
                    CardTBL = null,
                    CheckDiscountId = model.CheckDiscountId,
                    Description = $"widthrawl (as client of service) provider=[${model.ProvideUserName}] TransactionStatus=[${TransactionStatus.ServiceTransaction}]"
                };
                var providerTransaction = new TransactionTBL()
                {
                    Amount = providerShouldPay,
                    Username = model.ProvideUserName,
                    ProviderUserName = model.ProvideUserName,
                    ClientUserName = model.ClienUserName,
                    CreateDate = DateTime.Now,
                    BaseMyServiceId = model.BaseServiceId,
                    //////ServiceTypeWithDetails = ServiceTypeWithDetails.ChatVoiceLimited,
                    ServiceTypeWithDetails = model.ServiceTypes,
                    TransactionType = TransactionType.Deposit,
                    TransactionStatus = TransactionStatus.ServiceTransaction,
                    TransactionConfirmedStatus = TransactionConfirmedStatus.Confirmed,
                    CardTBL = null,
                    CheckDiscountId = model.CheckDiscountId,
                    Description = $"deposit (as provider of service) provider=[${model.ProvideUserName}] TransactionStatus=[${TransactionStatus.ServiceTransaction}]"
                };
                var commissiontTransaction = new TransactionTBL()
                {
                    Amount = porsant,
                    Username = "Admin",
                    ProviderUserName = model.ProvideUserName,
                    ClientUserName = model.ClienUserName,
                    CreateDate = DateTime.Now,
                    BaseMyServiceId = model.BaseServiceId,
                    //////ServiceTypeWithDetails = ServiceTypeWithDetails.ChatVoiceLimited,
                    ServiceTypeWithDetails = model.ServiceTypes,
                    TransactionType = TransactionType.Deposit,
                    TransactionStatus = TransactionStatus.Commission,
                    TransactionConfirmedStatus = TransactionConfirmedStatus.Confirmed,
                    CardTBL = null,
                    CheckDiscountId = model.CheckDiscountId,
                    Description = $"this is commision witch site shoul get from Provider provider=[${model.ProvideUserName}] TransactionStatus=[${TransactionStatus.Commission}]"
                };


                List<TransactionTBL> transactions = new List<TransactionTBL>() {
              clientTransaction,
              providerTransaction,
              commissiontTransaction,
            };
                await _context.TransactionTBL.AddRangeAsync(transactions);

                var clientProviderShoulPayVM = new ClientProviderShoulPayVM()
                {
                    ClientShouldPay = clientShouldPay,
                    ProviderShouldGet = providerShouldPay,
                };
                return clientProviderShoulPayVM;


            }
            catch
            {
                return null;
            }

        }




        /// <summary>
        /// ولیدت کردن پول برای خرید پکیج هایه سرویسی مثل چت و...
        /// </summary>
        /// <param name="model"></param>
        /// <param name="clientFromDB"></param>
        /// <returns></returns>
        //public (bool succsseded, List<string> result) ValidateWallet(AppUser clientFromDB, bool isNative, int SitePercent, double? PriceForNativeCustomer, double? PriceForNonNativeCustomer, DiscountVM disCountPercentFromDb)
        public (bool succsseded, List<string> result) ValidateWallet(BaseRequestServiceTBL model, AppUser clientFromDB)
        {

            bool IsValid = true;
            List<string> Errors = new List<string>();

            if (clientFromDB.WalletBalance <= 0)
            {
                IsValid = false;
                Errors.Add(_resourceServices.GetErrorMessageByKey("NotEnoughtBalance"));
                return (IsValid, Errors);
            }


            var prices = model.Price;
            var sitePercent = model.BaseMyServiceTBL.ServiceTbl.SitePercent;
            var discountPercent = model.CheckDiscountTBL == null ? 0 : model.CheckDiscountTBL.Percent;

            double clientShouldPay = 0;
            double providerShouldPay = 0;
            double porsant = 0;

            clientShouldPay = (double)model.Price - (double)(model.Price * (discountPercent / 100));
            providerShouldPay = (double)model.Price - (double)(model.Price * (sitePercent / 100));
            porsant = (double)model.Price * (sitePercent / 100);

            if (clientFromDB.WalletBalance < clientShouldPay)
            {
                IsValid = false;
                Errors.Add(_resourceServices.GetErrorMessageByKey("NotEnoughtBalance"));
                return (IsValid, Errors);
            }

            if (clientFromDB.WalletBalance - clientShouldPay <= 0)
            {
                IsValid = false;
                Errors.Add(_resourceServices.GetErrorMessageByKey("NotEnoughtBalance"));
                return (IsValid, Errors);
            }




            //var discountPercent = model.CheckDiscountTBL == null ? 0 : model.CheckDiscountTBL.Percent;
            //var discountPercent = disCountPercentFromDb == null ? 0 : disCountPercentFromDb.Percent;


            //////////var reducepercent = discountPercent /*+ SitePercent*/;
            //////////var calculatedBalanc = ((100 - reducepercent) * PriceForNativeCustomer) / 100;

            //////////if (clientFromDB.WalletBalance < calculatedBalanc)
            //////////{
            //////////    IsValid = false;
            //////////    Errors.Add(_resourceServices.GetErrorMessageByKey("NotEnoughtBalance"));
            //////////    return (IsValid, Errors);
            //////////}
            //////////if (clientFromDB.WalletBalance - calculatedBalanc <= 0)
            //////////{
            //////////    IsValid = false;
            //////////    Errors.Add(_resourceServices.GetErrorMessageByKey("NotEnoughtBalance"));
            //////////    return (IsValid, Errors);
            //////////}







            //if (isNative)
            //{
            //    var discountPercent = disCountPercentFromDb == null ? 0 : disCountPercentFromDb.Percent;

            //    var reducepercent = discountPercent /*+ SitePercent*/;
            //    var calculatedBalanc = ((100 - reducepercent) * PriceForNativeCustomer) / 100;

            //    if (clientFromDB.WalletBalance < calculatedBalanc)
            //    {
            //        IsValid = false;
            //        Errors.Add(_resourceServices.GetErrorMessageByKey("NotEnoughtBalance"));
            //        return (IsValid, Errors);
            //    }
            //    if (clientFromDB.WalletBalance - calculatedBalanc <= 0)
            //    {
            //        IsValid = false;
            //        Errors.Add(_resourceServices.GetErrorMessageByKey("NotEnoughtBalance"));
            //        return (IsValid, Errors);
            //    }
            //}
            //else
            //{
            //    var discountPercent = disCountPercentFromDb == null ? 0 : disCountPercentFromDb.Percent;
            //    var reducepercent = discountPercent;
            //    var calculatedBalanc = ((100 - reducepercent) * PriceForNonNativeCustomer) / 100;

            //    if (clientFromDB.WalletBalance < calculatedBalanc)
            //    {
            //        IsValid = false;
            //        Errors.Add(_resourceServices.GetErrorMessageByKey("NotEnoughtBalance"));
            //        return (IsValid, Errors);
            //    }
            //    if (clientFromDB.WalletBalance - calculatedBalanc <= 0)
            //    {
            //        IsValid = false;
            //        Errors.Add(_resourceServices.GetErrorMessageByKey("NotEnoughtBalance"));
            //        return (IsValid, Errors);
            //    }
            //}

            return (IsValid, Errors);
        }





    }
}

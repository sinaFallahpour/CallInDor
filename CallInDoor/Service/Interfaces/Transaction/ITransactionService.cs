using Domain.DTO.Transaction;
using Domain.Entities;
using Domain.Entities.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces.Transaction
{
    public interface ITransactionService
    {
        IQueryable<TransactionTBL> Filter(GetAllTransactionInAdmin model, IQueryable<TransactionTBL> QueryAble);


        /// <summary>
        /// تراکنش های مربوط به کال
        /// </summary>
        /// <returns></returns>
        Task<ClientProviderShoulPayVM> HandleCaLlTransaction(BaseRequestServiceTBL model);


        public (bool succsseded, List<string> result) ValidateWallet(BaseRequestServiceTBL model, AppUser clientFromDB);

    }
}

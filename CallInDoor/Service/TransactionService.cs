using Domain.DTO.Transaction;
using Domain.Entities;
using Service.Interfaces.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service
{
    public class TransactionService : ITransactionService
    {
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
    }
}

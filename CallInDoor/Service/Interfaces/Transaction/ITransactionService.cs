using Domain.DTO.Transaction;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Interfaces.Transaction
{
    public interface ITransactionService
    {
        IQueryable<TransactionTBL> Filter(GetAllTransactionInAdmin model, IQueryable<TransactionTBL> QueryAble);


    }
}

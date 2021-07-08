﻿using Domain.DTO.Transaction;
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


        ///// <summary>
        ///// تراکنش های مربوط به کال
        ///// </summary>
        ///// <returns></returns>
        //Task<ClientProviderShoulPayVM> HandleCaLlTransaction(BaseRequestServiceTBL model);

        public Task<ClientShoulPayVM> HandleCallTransactionForClient(BaseRequestServiceTBL model, CheckDiscountTBL discountFromDb);


        /// <summary>
        /// تراکنش برای پروایدر
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns
        public Task<ProviderShouldGetVM> HandleCallTransactionForProvider(BaseRequestServiceTBL model);





        public (bool succsseded, List<string> result) ValidateWallet(BaseRequestServiceTBL model, AppUser clientFromDB);

         


    }



}

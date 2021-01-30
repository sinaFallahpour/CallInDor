using Domain.Enums;
using System;

namespace Domain.DTO.Transaction
{
    public class GetAllTransactionInAdmin
    {

        public int? Page { get; set; }
        public int? PerPage { get; set; }
        public string SearchedWord { get; set; }
        public DateTime? CreateDate { get; set; }
        //public double? Amount { get; set; }
        public TransactionStatus? TransactionStatus { get; set; }
        public TransactionType? TransactionType { get; set; }
        public ServiceTypeWithDetails? ServiceTypeWithDetails { get; set; }
        public TransactionConfirmedStatus? TransactionConfirmedStatus { get; set; }

    }
}

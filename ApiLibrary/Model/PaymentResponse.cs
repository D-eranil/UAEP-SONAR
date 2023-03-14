using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrary.Model
{
    
    public class Balance
    {
        public string Value { get; set; }
    }

    public class Amount
    {
        public string Value { get; set; }
    }

    public class Fees
    {
        public string Value { get; set; }
    }

    public class Transaction
    {
        public string PaymentPortal { get; set; }
        public string PaymentPage { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseClass { get; set; }
        public string ResponseDescription { get; set; }
        public string ResponseClassDescription { get; set; }
        public string TransactionID { get; set; }
        public Balance Balance { get; set; }
        public Amount Amount { get; set; }
        public Fees Fees { get; set; }
        public object Payer { get; set; }
        public string UniqueID { get; set; }
    }

    public class PaymentRegistrationResponse
    {
        public Transaction Transaction { get; set; }
    }


    // public class Balance
    //{
    //    public string Value { get; set; }
    //}

    //public class Amount
    //{
    //    public string Value { get; set; }
    //}

    //public class Fees
    //{
    //    public string Value { get; set; }
    //}

    public class Payer
    {
        public string Information { get; set; }
    }

    public class ResponseTransaction
    {
        public string ResponseCode { get; set; }
        public string ResponseClass { get; set; }
        public string ResponseDescription { get; set; }
        public string ResponseClassDescription { get; set; }
        public string Language { get; set; }
        public string ApprovalCode { get; set; }
        public string Account { get; set; }
        public Balance Balance { get; set; }
        public string OrderID { get; set; }
        public Amount Amount { get; set; }
        public Fees Fees { get; set; }
        public string CardNumber { get; set; }
        public Payer Payer { get; set; }
        public string CardToken { get; set; }
        public string CardBrand { get; set; }
        public string UniqueID { get; set; }
    }

    public class Root
    {
        public Transaction Transaction { get; set; }
    }
}


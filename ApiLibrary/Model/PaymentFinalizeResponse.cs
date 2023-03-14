using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrary.Model
{
    public class PaymentFinalizeResponse
    {
        public FinalizeTransaction Transaction { get; set; }
    }    

    //public class Payer
    //{
    //    public string Information { get; set; }
    //}

    public class FinalizeTransaction
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

   
}

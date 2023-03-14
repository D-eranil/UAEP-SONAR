using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrary.Model
{
    public class PdfTicketData
    {
        public List<BarcodeTicketItem> BarcodeTicketItemList { get; set; }        
        public string FileName { get; set; }
    }

    public class BarcodeTicketItem
    {
        public string TicketNumber { get; set; }
        public string UnitPrice { get; set; }
        public string Date { get; set; }
        public string ItemNo { get; set; }
        public string itemDiscount { get; set; }
    }
}

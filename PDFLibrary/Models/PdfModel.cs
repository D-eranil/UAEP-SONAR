using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PDFLibrary.Models
{
    public class PdfDataModel
    {
        public string Date { get; set; }
        public string UnitPrice { get; set; }
        public string BarcodeImage { get; set; }
        public string BarcodeText { get; set; }
    }
}
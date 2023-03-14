using ApiLibrary.Helper;
using ApiLibrary.Model;
using IronBarCode;
using PDFLibrary.Models;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PDFLibrary.Controllers
{
    public class PDFController : Controller
    {
        public JsonResult Index(PdfTicketData pdfTicketData)
        {
            return Json("abc", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GeneratePDF(PdfTicketData pdfTicketData)
        {
            try
            {
                //List<PdfDataModel> barCodeLst = Barcode.GetBarcode(pdfTicketData.BarcodeTicketItemList);

                List<PdfDataModel> barCodeLst = new List<PdfDataModel>();
                foreach (var item in pdfTicketData.BarcodeTicketItemList)
                {
                    var MyBarCode = IronBarCode.BarcodeWriter.CreateBarcode(item.TicketNumber, BarcodeEncoding.Code128);
                    MyBarCode.ResizeTo(item.TicketNumber.Length * 40, 80); //item.TicketNumber.Length * 40
                    var test = MyBarCode.ToImage();
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        test.Save(memoryStream, ImageFormat.Jpeg);
                        var barcodeImage = "data:image/png;base64," + Convert.ToBase64String(memoryStream.ToArray());
                        PdfDataModel pdfDataModel = new PdfDataModel
                        {
                            BarcodeImage = barcodeImage,
                            BarcodeText = item.TicketNumber,
                            Date = item.Date,
                            UnitPrice = item.UnitPrice
                        };
                        barCodeLst.Add(pdfDataModel);
                    }
                }

                //List<PdfDataModel> barCodeLst = Barcode.GetBarcode(pdfTicketData.BarcodeTicketItemList);
                //List<PdfDataModel> barCodeLst = new List<PdfDataModel>();
                //foreach (var item in pdfTicketData.BarcodeTicketItemList)
                //{
                //    using (MemoryStream memoryStream = new MemoryStream())
                //    {
                //        using (Bitmap bitMap = new Bitmap(item.TicketNumber.Length * 40, 80))
                //        {
                //            using (Graphics graphics = Graphics.FromImage(bitMap))
                //            {
                //                Font oFont = new Font("Code 128", 100);
                //                PointF point = new PointF(2f, 2f);
                //                SolidBrush whiteBrush = new SolidBrush(Color.White);
                //                graphics.FillRectangle(whiteBrush, 0, 0, bitMap.Width, bitMap.Height);
                //                SolidBrush blackBrush = new SolidBrush(Color.Black);
                //                graphics.DrawString("*" + item.TicketNumber + "*", oFont, blackBrush, point);
                //            }
                //            bitMap.Save(memoryStream, ImageFormat.Jpeg);
                //            var barcodeImage = "data:image/png;base64," + Convert.ToBase64String(memoryStream.ToArray());

                //            PdfDataModel pdfDataModel = new PdfDataModel
                //            {
                //                BarcodeImage = barcodeImage,
                //                BarcodeText = item.TicketNumber,
                //                Date = item.Date,
                //                UnitPrice = item.UnitPrice
                //            };
                //            barCodeLst.Add(pdfDataModel);
                //        }
                //    }
                //}

                ViewAsPdf pdf = new Rotativa.ViewAsPdf("TicketPDF", barCodeLst)
                {
                    FileName = pdfTicketData.FileName + ".pdf",
                    CustomSwitches = "--page-offset 0 --footer-center [page] --footer-font-size 20",
                    //PageSize = Rotativa.Options.Size.A3,
                    MinimumFontSize = 23
                };

                byte[] pdfData = pdf.BuildFile(ControllerContext);
                string fullPath = PropertyReader.ReadProperty("pdffilepath") + pdf.FileName;
                using (var fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                {
                    fileStream.Write(pdfData, 0, pdfData.Length);
                }

                return Json(pdf.FileName, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}
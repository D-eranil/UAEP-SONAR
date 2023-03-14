using ApiLibrary.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace PDFLibrary.Models
{
    public class Barcode
    {
        public static List<PdfDataModel> GetBarcode(List<BarcodeTicketItem> barcodeTicketItemList)
        {
            List<PdfDataModel> listPdfDataModel = new List<PdfDataModel>();
            try
            {               
                foreach (var item in barcodeTicketItemList)
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        using (Bitmap bitMap = new Bitmap(item.TicketNumber.Length * 40, 80))
                        {
                            using (Graphics graphics = Graphics.FromImage(bitMap))
                            {
                                Font oFont = new Font("Code 128", 60);
                                PointF point = new PointF(2f, 2f);
                                SolidBrush whiteBrush = new SolidBrush(Color.White);
                                graphics.FillRectangle(whiteBrush, 0, 0, bitMap.Width, bitMap.Height);
                                SolidBrush blackBrush = new SolidBrush(Color.Black);
                                graphics.DrawString("*" + item.TicketNumber + "*", oFont, blackBrush, point);
                            }
                            bitMap.Save(memoryStream, ImageFormat.Jpeg);
                            var barcodeImage = "data:image/png;base64," + Convert.ToBase64String(memoryStream.ToArray());

                            PdfDataModel pdfDataModel = new PdfDataModel
                            {
                                BarcodeImage = barcodeImage,
                                BarcodeText = item.TicketNumber,
                                Date = item.Date,
                                UnitPrice = item.UnitPrice
                            };
                            listPdfDataModel.Add(pdfDataModel);
                        }
                    }
                }
                return listPdfDataModel;
            }
            catch (Exception ex)
            {
            }
            return listPdfDataModel;
        }
    }
}
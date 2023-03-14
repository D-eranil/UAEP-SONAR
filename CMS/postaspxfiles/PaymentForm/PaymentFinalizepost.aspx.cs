using ApiLibrary;
using ApiLibrary.Helper;
using ApiLibrary.Model;
using CMS.CustomTables;
using CMS.DataEngine;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMS.Helpers;
using Newtonsoft.Json;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using System.Web.Hosting;
//using Rotativa;
//using System.Web.Mvc;

public partial class PaymentFinalizepost : System.Web.UI.Page
{
    string CurrentCultureName = System.Threading.Thread.CurrentThread.CurrentCulture.ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        CallPaymentFinalize();

    }
    public void SendEmailAndRecordToPOS(string orderId,string numberofItems,string transactionId, List<BarcodeTicketItem> barcodeTicketItemList,HttpRequest req)
    {
        try
        {
            ImportTransactionApipost importTransactionApipost = new ImportTransactionApipost();
            importTransactionApipost.CallImportTransactionFinalize(orderId, numberofItems, JsonConvert.SerializeObject(barcodeTicketItemList));
            SendClientEmail(Server, req, orderId, transactionId + ".pdf");//PropertyReader.ReadProperty("pdffilepath")+ 
        }
        catch (Exception ex)
        {
            Log(ex);
        }
    }
    protected void CallPaymentFinalize()
    {
        try
        {

            Page.Validate();
            if (Page.IsValid)
            {

                PaymentFinalizeResponse paymentFinalizeResponse = CallPaymentFinalizeGateway();
                string orderId = "";
                string Status = paymentFinalizeResponse.Transaction.ResponseClassDescription;
                string responseCode = paymentFinalizeResponse.Transaction.ResponseCode;
                string uniqueId = paymentFinalizeResponse.Transaction.UniqueID;
                if (!string.IsNullOrEmpty(paymentFinalizeResponse.Transaction.OrderID))
                {
                    orderId = paymentFinalizeResponse.Transaction.OrderID;
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                }
                else if (!string.IsNullOrEmpty(Request["orderId"]))
                {
                    orderId = Request["orderId"];
                }

                string customTableClassName = "TicketPayment.Form";
                CustomTableItem customTableItem = CustomTableItemProvider.GetItems(customTableClassName).WhereEquals("OrderId", orderId).TopN(1).FirstOrDefault();

                string transactionId = ValidationHelper.GetString(customTableItem.GetValue("TransactionID"), "");
                string numberofItems = Request["numberOFItems"];



                List<GetPriceTicketData> itemList = JsonConvert.DeserializeObject<List<GetPriceTicketData>>(Request["ticketdata"]);

                if (paymentFinalizeResponse != null)
                {

                    UpdateCustomTableData(Status, responseCode, uniqueId, transactionId, orderId);   //udpate CMS entry pending = Approve, extra add ResponseCode, ResponseDe = "Success" Unique Id,
                    if (responseCode == "0")
                    {
                        var Req = Request;
                        List<BarcodeTicketItem> barcodeTicketItemList = EntryTicket(itemList);
                        var data = new
                        {
                            status = "success",
                            registrationResponseData = paymentFinalizeResponse,
                            barcodeTicketItems = barcodeTicketItemList,
                            FileName = transactionId + ".pdf"
                        };


                        Task.Run(() =>
                        SendEmailAndRecordToPOS(orderId.ToString(), numberofItems.ToString(), transactionId, barcodeTicketItemList, Req)
                            );
               
               
                        Response.Write(JsonConvert.SerializeObject(data));


                    }
                    else if (responseCode == "51")
                    {
                        var data = new
                        {
                            status = "InsufficientFund",
                            registrationResponseData = "51 -- Not Sufficient Fund"
                        };

                        Response.Write(JsonConvert.SerializeObject(data));
                    }

                    else if (responseCode == "5")
                    {

                        var data = new
                        {
                            status = "DoNotHonor",
                            registrationResponseData = "5 -- Do not honor"
                        };

                        Response.Write(JsonConvert.SerializeObject(data));

                    }

                    else if (responseCode == "91")
                    {
                        var data = new
                        {
                            status = "IssuerInoperative",
                            registrationResponseData = " 91 -- Issuer Switch inoperative"
                        };

                        Response.Write(JsonConvert.SerializeObject(data));

                    }
                    else if (responseCode == "5052")
                    {
                        var data = new
                        {
                            status = "IssuerInoperative",
                            registrationResponseData = "5052 -- Issuer Switch inoperative"
                        };

                        Response.Write(JsonConvert.SerializeObject(data));

                    }
                    else if (responseCode == "6824")
                    {
                        var data = new
                        {
                            status = "Cancel",
                            registrationResponseData = "6824 -- Issuer Switch inoperative"
                        };

                        Response.Write(JsonConvert.SerializeObject(data));

                    }
                    else //if new code will produce from bank
                    {
                        var data = new
                        {
                            status = "IssuerInoperative",
                            registrationResponseData = "Issuer Switch inoperative"
                        };

                        Response.Write(JsonConvert.SerializeObject(data));
                    }



                }

            }
            else
            {
                var data = new
                {
                    status = "fail",
                    registrationResponseData = "Fail Because Page is not Valid"
                };
                Response.Write(JsonConvert.SerializeObject(data));
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.InnerException);
            Log(ex);
        }

    }

    private void UpdateCustomTableData(string status, string responseCode, string uniqueid, string transactionID, string orderID)
    {
        try
        {
            string customTableClassName = "TicketPayment.Form";
            DataClassInfo customTable = DataClassInfoProvider.GetDataClassInfo(customTableClassName);
            if (customTable != null && orderID != null)
            {

                var customTableData = CustomTableItemProvider.GetItems(customTableClassName).WhereStartsWith("OrderId", orderID);
                if (customTableData != null)
                {
                    foreach (CustomTableItem item in customTableData)
                    {
                        item.SetValue("UniqueId", uniqueid);
                        item.SetValue("Status", status);
                        item.SetValue("ResponseCode", responseCode);
                        //item.SetValue("TransactionId", transactionID);
                        item.Update();
                    }
                }
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    private PaymentFinalizeResponse CallPaymentFinalizeGateway()
    {
        try
        {
            Finalization finalization = new Finalization();
            finalization.UserName = PropertyReader.ReadProperty("UserName");
            finalization.Password = PropertyReader.ReadProperty("Password");

            string customTableClassName = "TicketPayment.Form";
            CustomTableItem customTableItem = CustomTableItemProvider.GetItems(customTableClassName).WhereEquals("OrderId", Request["orderId"]).TopN(1).FirstOrDefault();

            finalization.TransactionID = ValidationHelper.GetString(customTableItem.GetValue("TransactionID"), "");

            // finalization.TransactionID = Request["TransactionID"];


            finalization.Customer = "Demo Merchant";//PropertyReader.StripHTML(Request["Fullname"]);

            PaymentFinalizeModel paymentFinalizeModel = new PaymentFinalizeModel();
            paymentFinalizeModel.Finalization = finalization;

            PaymentFinalizeResponse paymentRegistrationResponse = PaymentHelper.PaymentFinalizeApi(paymentFinalizeModel);

            return paymentRegistrationResponse;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    private List<BarcodeTicketItem> EntryTicket(List<GetPriceTicketData> ticketData)
    {
        string LocationPath = Server.MapPath("/UaepTickets/ticket.pdf");
        //PDFHelper.ExportPdf("<div class='pdf-wrapper'><div class='main-image'><img src = '$jpg1$'></div><div class='edit-area'><div class='price'>10.00 AED</div><div class='date'>25/09/2020</div><div class='barcode-img'><img style = 'margin-left:40px;' src='$jpg2$' alt='barcode'></div><div class='barcode-txt'>20159575</div></div></div>", LocationPath);

        string customTableClassName = "UAEP.RandomNumbersGeneration";
        CustomTableItem customTableItem = CustomTableItemProvider.GetItems(customTableClassName).WhereEquals("ItemID", "1").TopN(1).FirstOrDefault();
        List<string> ticketnumbers = new List<string>();
        string ticketNumberCount = "";

        int cmsTicketNumber = int.Parse(ValidationHelper.GetString(customTableItem.GetValue("TicketNumber"), "0"));

        CustomTableItem customTableItems = CustomTableItemProvider.GetItems("TicketPayment.Form").WhereEquals("OrderId", Request["OrderID"]).TopN(1).FirstOrDefault();
        List<BarcodeTicketItem> barcodeTicketItemList = new List<BarcodeTicketItem>();
        foreach (var item in ticketData)
        {
            int quantity = int.Parse(item.qty);
            for (var i = 0; i < quantity; i++)
            {
                cmsTicketNumber = cmsTicketNumber + 1;
                ticketNumberCount = String.Format("{0:000000}", cmsTicketNumber);
                string ticketNumber = "6" + ticketNumberCount; //"W" + DateTime.Now.ToString("yyyy") 
                ticketnumbers.Add(ticketNumber);
                BarcodeTicketItem barcodeTicketItem = new BarcodeTicketItem()
                {
                    TicketNumber = ticketNumber,
                    Date = ValidationHelper.GetDateTime(customTableItems.GetValue("RequestedDate", ""), DateTime.Now).ToString("dd/MM/yyyy"),//DateTime.Now.Date.ToString("dd-MM-yyy"),
                    UnitPrice = item.unitPrice,
                    ItemNo = item.itemNo,
                    itemDiscount = item.itemDiscount
                };
                barcodeTicketItemList.Add(barcodeTicketItem);
            }
        }
        PdfTicketData pdfTicketData = new PdfTicketData
        {
            BarcodeTicketItemList = barcodeTicketItemList,
            FileName = ValidationHelper.GetString(customTableItems.GetValue("TransactionId"), "")
        };
        string pdfFileName = PDFHelper.GeneratePDF(pdfTicketData);
        DataClassInfo customTable = DataClassInfoProvider.GetDataClassInfo(customTableClassName);
        if (customTable != null)
        {

            var customTableData = CustomTableItemProvider.GetItems(customTableClassName).WhereStartsWith("ItemID", "1");
            if (customTableData != null && !String.IsNullOrEmpty(ticketNumberCount))
            {
                foreach (CustomTableItem item in customTableData)
                {
                    item.SetValue("TicketNumber", ticketNumberCount);
                    //  item.SetValue("PdfFileName", pdfFileName);
                    item.Update();
                }
            }
        }
        return barcodeTicketItemList;
    }

   
    public void SendClientEmail(HttpServerUtility Server, HttpRequest Request, string orderID, string filePath = "")
    {
        string customTableClassName = "TicketPayment.Form";
        CustomTableItem customTableItem = CustomTableItemProvider.GetItems(customTableClassName).WhereEquals("OrderId", orderID).TopN(1).FirstOrDefault();

        string userEmail = ValidationHelper.GetString(customTableItem.GetValue("Email"), "");
        string fullName = ValidationHelper.GetString(customTableItem.GetValue("FullName"), "");


        string templateFile = "/email-templates/email_template_SingleEntryTicket.html";

        StreamReader sr = new StreamReader(Server.MapPath(templateFile));
        string email = sr.ReadToEnd();
        string date = DateTime.Now.ToString("MMM dd yyyy");
        email = email.Replace("$date$", date);
        email = email.Replace("$datefooter$", DateTime.Now.Year.ToString());
        email = email.Replace("$MainHeading$", "Entry Tickets");
        email = email.Replace("$FullName$", fullName);

        sr.Close();
        sr.Dispose();

        if (ValidationHelper.GetString(customTableItem.GetValue("ImportTransactionApiResponse"), "") == "OK")
        {

            EmailSender.SendEmail(PropertyReader.ReadProperty("fromEmail"), userEmail, PropertyReader.ReadProperty("ccEmail"), "Your Umm Al Emarat Park tickets are booked!", email, "", filePath);

        }
    }
    public string getFile(string path, List<String> list, List<String> values)
    {
        StreamReader reader = new StreamReader(path);
        string temp = reader.ReadToEnd();
        for (int i = 0; i < list.Count; i++)
            temp = temp.Replace(list[i], values[i]);
        reader.Close();
        reader.Dispose();
        return temp;
    }
    public string SaveFile(HttpPostedFile file, string virtualPath)
    {
        virtualPath = virtualPath.Trim();
        string path = HostingEnvironment.MapPath(virtualPath);
        DirectoryInfo dinfo = new DirectoryInfo(path);

        if (!dinfo.Exists)
            dinfo.Create();
        string str = Request["email"].Replace("@", "_at_").Replace(".", "_dot_") + Guid.NewGuid().ToString().Substring(0, 3) + file.FileName.Substring(file.FileName.LastIndexOf('.'));
        string fileName = str;
        file.SaveAs(path + fileName);
        return virtualPath + fileName;

    }

    public static void Log(Exception ex)
    {
        try
        {
            string logBasePath = "/Email-Logs/";
            string fileName = DateTime.Now.ToString("dd-MM-yyyy") + "-LogFile.txt";
            string filePath = HostingEnvironment.MapPath(logBasePath + fileName);
            StreamWriter sw = new StreamWriter(filePath, true);
            //sw.Write("\r\n ---------------------------- " + DateTime.Now.ToString + " ---------------------------------\r\n");
            sw.WriteLine(ex.ToString());
            sw.WriteLine(ex.TargetSite);
            sw.WriteLine(ex.StackTrace);
            Exception inner = ex.InnerException;
            while (inner != null)
            {
                sw.WriteLine("----------------------- Inner Exception ------------------------------");
                sw.WriteLine(inner.ToString());
                sw.WriteLine(inner.TargetSite);
                sw.WriteLine(inner.StackTrace);
                inner = inner.InnerException;
            }
            sw.Flush();
            sw.Dispose();
        }
        catch { }
    }

    public static class EmailSender
    {
        public static void SendEmail(string from, string toEmail, string ccEmail, string subject, string body, string bccEmail, string attachmentFilePath = "")
        {

            try
            {
                MailMessage mail = new MailMessage();
                mail.IsBodyHtml = true;
                mail.From = new MailAddress(from);
                MailAddressCollection col = GetAddresses(toEmail);
                foreach (MailAddress add in col)
                {
                    mail.To.Add(add);
                }
                col = GetAddresses(ccEmail);
                foreach (MailAddress add in col)
                {
                    mail.CC.Add(add);
                }
                col = GetAddresses(bccEmail);
                foreach (MailAddress add in col)
                {
                    mail.Bcc.Add(add);
                }
                if (!string.IsNullOrEmpty(attachmentFilePath))
                {

                    Attachment attachment;

                    attachment = new Attachment("D:\\Tfs Azure Development\\Uaep\\Solution\\Website\\CMS\\UaepTickets\\" + attachmentFilePath);

                    mail.Attachments.Add(attachment);
                }


                mail.Subject = subject;
                mail.Body = body;

                SmtpClient smtp = new SmtpClient();

                try
                {
                    smtp.Send(mail);
                }
                catch (Exception ex)
                {
                    Log(ex);
                }
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        public static MailAddressCollection GetAddresses(string addressCollection)
        {
            string[] str = null;
            if (String.IsNullOrEmpty(addressCollection))
                return new MailAddressCollection();
            if (addressCollection.IndexOf(',') > -1)
            {
                str = addressCollection.Split(new char[] { ',' });
            }
            else
                str = new string[] { addressCollection };
            MailAddressCollection list = new MailAddressCollection();
            foreach (string add in str)
            {
                list.Add(new MailAddress(add));
            }
            return list;
        }

        public static void Log(Exception ex)
        {
            try
            {
                string logBasePath = "/Email-Logs/";
                string fileName = DateTime.Now.ToString("dd-MM-yyyy") + "-LogFile.txt";
                string filePath = HostingEnvironment.MapPath(logBasePath + fileName);
                StreamWriter sw = new StreamWriter(filePath, true);
                //sw.Write("\r\n ---------------------------- " + DateTime.Now.ToString + " ---------------------------------\r\n");
                sw.WriteLine(ex.ToString());
                sw.WriteLine(ex.TargetSite);
                sw.WriteLine(ex.StackTrace);
                Exception inner = ex.InnerException;
                while (inner != null)
                {
                    sw.WriteLine("----------------------- Inner Exception ------------------------------");
                    sw.WriteLine(inner.ToString());
                    sw.WriteLine(inner.TargetSite);
                    sw.WriteLine(inner.StackTrace);
                    inner = inner.InnerException;
                }
                sw.Flush();
                sw.Dispose();
            }
            catch { }
        }
    }
}
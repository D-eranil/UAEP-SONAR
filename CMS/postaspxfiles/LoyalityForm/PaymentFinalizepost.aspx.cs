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
using System.Web.Hosting;
using System.Threading.Tasks;
//using Rotativa;
//using System.Web.Mvc;

public partial class loyalityPaymentFinalizepost : System.Web.UI.Page
{
    string CurrentCultureName = System.Threading.Thread.CurrentThread.CurrentCulture.ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        CallPaymentFinalize();
        
    }

    public void SendEmailAndRecordToPOS(string orderId, string numberofItems, string transactionId,GetPriceTicketData TicketData, HttpRequest req,HttpServerUtility Server, CustomTableItem customTableItem)
    {
        try
        {
            loyalityImportTransactionApipost objimportTransactionApipost = new loyalityImportTransactionApipost();
            if (ValidationHelper.GetString(customTableItem.GetValue("ImportTransactionApiResponse"), "") != "OK")
            {
                objimportTransactionApipost.CallImportTransactionFinalizeCard(orderId, numberofItems, TicketData.itemNo, TicketData.KeyValue);

                SendClientEmail(Server, req, orderId);
                SendLoyaltyFormDetailToAdmin(req,orderId);
            }
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
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();

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
                }
                else if (!string.IsNullOrEmpty(Request["orderId"]))
                {
                     orderId = Request["orderId"];
                }

                string customTableClassName = "LoyalityCardPayment.Form";
                CustomTableItem customTableItem = CustomTableItemProvider.GetItems(customTableClassName).WhereEquals("OrderId", orderId).TopN(1).FirstOrDefault();

                string transactionId = ValidationHelper.GetString(customTableItem.GetValue("TransactionID"), "");
                //string transactionId = Request["TransactionID"];
                string numberofItems = Request["numberOFItems"];



                List<GetPriceTicketData> itemList = javaScriptSerializer.Deserialize<List<GetPriceTicketData>>(Request["ticketdata"]);

                if (paymentFinalizeResponse != null)
                {

                    LoyaltyUpdateCustomTableData(Status, responseCode, uniqueId, transactionId, orderId);   //udpate CMS entry pending = Approve, extra add ResponseCode, ResponseDe = "Success" Unique Id,
                    if (responseCode == "0")
                    {
                       // List<BarcodeTicketItem> barcodeTicketItemList = EntryTicket(itemList);
                        var data = new
                        {
                            status = "success",
                            registrationResponseData = paymentFinalizeResponse,
                            //barcodeTicketItems = barcodeTicketItemList,
                            FileName = transactionId
                        };
                          Task.Run(() =>
                        SendEmailAndRecordToPOS(orderId.ToString(), numberofItems.ToString(), transactionId, itemList.FirstOrDefault(), Request, Server, customTableItem)
                           );

                        Response.Write(javaScriptSerializer.Serialize(data));


                    }
                    else if (responseCode == "51")
                    {
                        var data = new
                        {
                            status = "InsufficientFund",
                            registrationResponseData = "51 -- Not Sufficient Fund"
                        };

                        Response.Write(javaScriptSerializer.Serialize(data));
                    }

                    else if (responseCode == "5")
                    {

                        var data = new
                        {
                            status = "DoNotHonor",
                            registrationResponseData = "5 -- Do not honor"
                        };

                        Response.Write(javaScriptSerializer.Serialize(data));

                    }

                    else if (responseCode == "91")
                    {
                        var data = new
                        {
                            status = "IssuerInoperative",
                            registrationResponseData = " 91 -- Issuer Switch inoperative"
                        };

                        Response.Write(javaScriptSerializer.Serialize(data));

                    }
                    else if (responseCode == "5052")
                    {
                        var data = new
                        {
                            status = "IssuerInoperative",
                            registrationResponseData = "5052 -- Issuer Switch inoperative"
                        };

                        Response.Write(javaScriptSerializer.Serialize(data));

                    }
                    else if (responseCode == "6824")
                    {
                        var data = new
                        {
                            status = "Cancel",
                            registrationResponseData = "6824 -- Issuer Switch inoperative"
                        };

                        Response.Write(javaScriptSerializer.Serialize(data));

                    }
                    else //if new code will produce from bank
                    {
                        var data = new
                        {
                            status = "IssuerInoperative",
                            registrationResponseData = "Issuer Switch inoperative"
                        };

                        Response.Write(javaScriptSerializer.Serialize(data));
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
                Response.Write(javaScriptSerializer.Serialize(data));
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.InnerException);
            Log(ex);
        }

    }

    private void LoyaltyUpdateCustomTableData(string status, string responseCode, string uniqueid, string transactionID, string orderID)
    {
        try
        {
            string customTableClassName = "LoyalityCardPayment.Form";
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
                        item.SetValue("TransactionId", transactionID);
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
            string customTableClassName = "LoyalityCardPayment.Form";
            CustomTableItem customTableItem = CustomTableItemProvider.GetItems(customTableClassName).WhereEquals("OrderId", Request["orderId"]).TopN(1).FirstOrDefault();

            finalization.TransactionID = ValidationHelper.GetString(customTableItem.GetValue("TransactionID"), "");
            //if (Request["TransactionID"] != null)
            //{
            //    finalization.TransactionID = Request["TransactionID"];
            //}

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



    //private List<BarcodeTicketItem> EntryTicket(List<GetPriceTicketData> ticketData)
    //{
    //    string LocationPath = Server.MapPath("/UaepTickets/ticket.pdf");
    //    //PDFHelper.ExportPdf("<div class='pdf-wrapper'><div class='main-image'><img src = '$jpg1$'></div><div class='edit-area'><div class='price'>10.00 AED</div><div class='date'>25/09/2020</div><div class='barcode-img'><img style = 'margin-left:40px;' src='$jpg2$' alt='barcode'></div><div class='barcode-txt'>20159575</div></div></div>", LocationPath);
        
    //    string customTableClassName = "UAEP.RandomNumbersGeneration";
    //    CustomTableItem customTableItem = CustomTableItemProvider.GetItems(customTableClassName).WhereEquals("ItemID", "1").TopN(1).FirstOrDefault();
    //    List<string> ticketnumbers = new List<string>();
    //    string ticketNumberCount = "";
    //    int cmsTicketNumber = int.Parse(ValidationHelper.GetString(customTableItem.GetValue("TicketNumber"), ""));

    //    CustomTableItem customTableItems = CustomTableItemProvider.GetItems("LoyalityCardPayment.Form").WhereEquals("OrderId", Request["OrderID"]).TopN(1).FirstOrDefault();
    //    List<BarcodeTicketItem> barcodeTicketItemList = new List<BarcodeTicketItem>();
    //    foreach (var item in ticketData)
    //    {
    //        int quantity = int.Parse(item.qty);
    //        for (var i = 0; i < quantity; i++)
    //        {
    //            cmsTicketNumber = cmsTicketNumber + 1;
    //            ticketNumberCount = String.Format("{0:0000000}", cmsTicketNumber);
    //            string ticketNumber = "6"+ ticketNumberCount; //"W" + DateTime.Now.ToString("yyyy") 
    //            ticketnumbers.Add(ticketNumber);
    //            BarcodeTicketItem barcodeTicketItem = new BarcodeTicketItem()
    //            {
    //                TicketNumber = ticketNumber,
    //                Date = ValidationHelper.GetDateTime(customTableItems.GetValue("RequestedDate", ""), DateTime.Now).ToString("dd/MM/yyyy"),//DateTime.Now.Date.ToString("dd-MM-yyy"),
    //                UnitPrice = item.unitPrice,
    //                ItemNo = item.itemNo
    //            };
    //            barcodeTicketItemList.Add(barcodeTicketItem);
    //        }
    //    }
    //    PdfTicketData pdfTicketData = new PdfTicketData
    //    {
    //        BarcodeTicketItemList = barcodeTicketItemList,
    //        FileName = Request["TransactionID"]
    //    };
    //    string pdfFileName = PDFHelper.GeneratePDF(pdfTicketData);
    //    DataClassInfo customTable = DataClassInfoProvider.GetDataClassInfo(customTableClassName);
    //    if (customTable != null)
    //    {

    //        var customTableData = CustomTableItemProvider.GetItems(customTableClassName).WhereStartsWith("ItemID", "1");
    //        if (customTableData != null)
    //        {
    //            foreach (CustomTableItem item in customTableData)
    //            {
    //                item.SetValue("TicketNumber", ticketNumberCount);
    //              //  item.SetValue("PdfFileName", pdfFileName);
    //                item.Update();
    //            }
    //        }
    //    }
    //    return barcodeTicketItemList;
    //}


    private void SendLoyaltyFormDetailToAdmin(HttpRequest Request,string orderID)
    {
        List<String> list = new List<string>();
        List<String> values = new List<string>();

        CustomTableItem customTableItems = CustomTableItemProvider.GetItems("LoyalityCardPayment.Form").WhereEquals("OrderId", orderID).TopN(1).FirstOrDefault();

        if (ValidationHelper.GetString(customTableItems.GetValue("ImportTransactionApiResponse"), "") != "OK")
        {
            list.Add("$ApplicationNumber$");
            if (!String.IsNullOrEmpty(ValidationHelper.GetString(customTableItems.GetValue("TransactionId"), "")))
                values.Add(ValidationHelper.GetString(customTableItems.GetValue("TransactionId"), ""));
            else
                values.Add(String.Empty);

            list.Add("$RequestDate$");
            if (!String.IsNullOrEmpty(ValidationHelper.GetString(customTableItems.GetValue("RequestedDate"), "")))
                values.Add(ValidationHelper.GetString(customTableItems.GetValue("RequestedDate"), ""));
            else
                values.Add(String.Empty);

            list.Add("$OrderId$");
            if (!String.IsNullOrEmpty(orderID))
                values.Add(orderID);
            else
                values.Add(String.Empty);

            list.Add("$Fname$");
            if (!String.IsNullOrEmpty(ValidationHelper.GetString(customTableItems.GetValue("FullName"), "")))
                values.Add(ValidationHelper.GetString(customTableItems.GetValue("FullName"), ""));
            else
                values.Add(String.Empty);


            list.Add("$Email$");
            if (!String.IsNullOrEmpty(ValidationHelper.GetString(customTableItems.GetValue("Email"), "")))
                values.Add(ValidationHelper.GetString(customTableItems.GetValue("Email"), ""));
            else
                values.Add(String.Empty);


            list.Add("$MobileNumber$");
            if (!String.IsNullOrEmpty(ValidationHelper.GetString(customTableItems.GetValue("MobileNumber"), "")))
                values.Add(ValidationHelper.GetString(customTableItems.GetValue("MobileNumber"), ""));
            else
                values.Add(String.Empty);


            list.Add("$DateofBirth$");
            if (!String.IsNullOrEmpty(ValidationHelper.GetString(customTableItems.GetValue("DateofBirth"), "")))
                values.Add(ValidationHelper.GetString(customTableItems.GetValue("DateofBirth"), ""));
            else
                values.Add(String.Empty);

            list.Add("$ClubCode$");
            if (!String.IsNullOrEmpty(ValidationHelper.GetString(customTableItems.GetValue("ClubCode"), "")))
            {
                values.Add(ValidationHelper.GetString(customTableItems.GetValue("ClubCode"), ""));
            }
            else
                values.Add(String.Empty);

            list.Add("$MembershipCardName$");
            if (!String.IsNullOrEmpty(ValidationHelper.GetString(customTableItems.GetValue("GoldCard"), "")))
            {
                values.Add("Fitness Loyalty Card");
            }
            else if (!String.IsNullOrEmpty(ValidationHelper.GetString(customTableItems.GetValue("SilverCard"), "")))
            {
                values.Add("Silver Loyalty Card");
            }
            else
            {

                values.Add(String.Empty);
            }

            list.Add("$TotalAmount$");
            if (!String.IsNullOrEmpty(ValidationHelper.GetString(customTableItems.GetValue("TotalAmount"), "")))
                values.Add(ValidationHelper.GetString(customTableItems.GetValue("TotalAmount"), ""));
            else
                values.Add(String.Empty);

            list.Add("$Picture$");
            if (!String.IsNullOrEmpty(ValidationHelper.GetString(customTableItems.GetValue("Picture"), "")))
                values.Add(ValidationHelper.GetString(customTableItems.GetValue("Picture"), "").Replace("C:/inetpub/wwwroot/CMS/", ""));//C:\inetpub\wwwroot\CMS\ 
            else
                values.Add(String.Empty);

            list.Add("$EmiratesIdPicture$");
            if (!String.IsNullOrEmpty(ValidationHelper.GetString(customTableItems.GetValue("EmiratesIdPicture"), "")))
                values.Add(ValidationHelper.GetString(customTableItems.GetValue("EmiratesIdPicture"), "").Replace("C:/inetpub/wwwroot/CMS/", ""));//UAT -> C:\inetpub\wwwroot\CMS\  local ->  D:/Tfs Azure Development/Uaep/Solution/Website/CMS/
            else
                values.Add(String.Empty);


            list.Add("$date$");
            values.Add(DateTime.Now.ToString("MMM dd yyyy"));

            list.Add("$datefooter$");
            values.Add(DateTime.Now.Year.ToString());


            list.Add("$MainHeading$");
            values.Add("Umm Al Emarat Park: Loyalty Cards");




            string subject = "Loyalty Card Approval";
            string templateFile = "/email-templates/email_template_LoyaltyCardAdmin.html";

            string toEmail = string.Empty;
            toEmail = PropertyReader.ReadProperty("General-question");

            string body = getFile(HostingEnvironment.MapPath(templateFile), list, values);
          
                EmailSender.SendEmail(PropertyReader.ReadProperty("fromEmail"), toEmail, PropertyReader.ReadProperty("ccEmail"), subject, body, PropertyReader.ReadProperty("bccEmail"));
           
        }
        
    }

    public void SendClientEmail(HttpServerUtility Server, HttpRequest Request, string orderID)
    {
        string customTableClassName = "LoyalityCardPayment.Form";
        CustomTableItem customTableItem = CustomTableItemProvider.GetItems(customTableClassName).WhereEquals("OrderId", orderID).TopN(1).FirstOrDefault();

        string userEmail = ValidationHelper.GetString(customTableItem.GetValue("Email"), "");
        string fullName = ValidationHelper.GetString(customTableItem.GetValue("FullName"), "");


        string templateFile = "/email-templates/email_template_LoyaltyCard.html";

        StreamReader sr = new StreamReader(Server.MapPath(templateFile));
        string email = sr.ReadToEnd();
        string date = DateTime.Now.ToString("MMM dd yyyy");
        email = email.Replace("$date$", date);
        email = email.Replace("$datefooter$", DateTime.Now.Year.ToString());
        email = email.Replace("$MainHeading$", "Entry Tickets");
        email = email.Replace("$FullName$", fullName);
        email = email.Replace("$ApplicationNumber$", ValidationHelper.GetString(customTableItem.GetValue("TransactionID"), ""));
        sr.Close();
        sr.Dispose();

        EmailSender.SendEmail(PropertyReader.ReadProperty("fromEmail"), userEmail, PropertyReader.ReadProperty("ccEmail"), "Thank you for your purchase! Application Number " + ValidationHelper.GetString(customTableItem.GetValue("TransactionID"), ""), email, "");
       

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
        string path = HttpContext.Current.Server.MapPath(virtualPath);
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
            string logBasePath = "/TicketingLogs/";
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
        public static void SendEmail(string from, string toEmail, string ccEmail, string subject, string body, string bccEmail)
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
                string logBasePath = "/TicketingLogs/";
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
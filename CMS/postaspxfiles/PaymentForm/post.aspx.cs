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
using Newtonsoft.Json;
using CMS.Helpers;
using System.Web.Hosting;

public partial class paymentform : System.Web.UI.Page
{
    string CurrentCultureNameTicket = CMS.Localization.LocalizationContext.CurrentCulture.CultureCode.ToLower();
    protected void Page_Load(object sender, EventArgs e)
    {
        
            EntryTicketSubmit();
        
    }
    protected void EntryTicketSubmit()
    {
        try
        {
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            if (Session["FormCaptcha"] != null && !string.IsNullOrEmpty(Request["captcha"]))
            {
                if (Session["FormCaptcha"].ToString().ToLower() == Request["captcha"].ToLower())
                {
                    

                    Page.Validate();
                    if (Page.IsValid)
                    {
                        this.SaveEntryTicket();

                        PaymentRegistrationResponse paymentRegistrationResponse =  CallPaymentGateway();
                       var a = paymentRegistrationResponse.Transaction.TransactionID;
                        var data = new
                        {
                            status = "success",
                            registrationResponseData = paymentRegistrationResponse
                        };
                       
                        Response.Write(javaScriptSerializer.Serialize(data));
                    }
                    else
                    {
                        var data = new
                        {
                            status = "fail",
                            registrationResponseData = "fail"
                        };

                        Response.Write(javaScriptSerializer.Serialize(data));
                       
                    }
                }
                else
                {
                    var data = new
                    {
                        status = "invalid",
                        registrationResponseData = "invalid"
                    };

                    Response.Write(javaScriptSerializer.Serialize(data));
                    
                }
            }
            else
            {
                var data = new
                {
                    status = "invalid",
                    registrationResponseData = "invalid"
                };

                Response.Write(javaScriptSerializer.Serialize(data));
              
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.InnerException);
            TicketingLog(ex);
        }

    }

    private void SaveEntryTicket()
    {
        try
        {
            string customTableClassName = "TicketPayment.Form";
            DataClassInfo customTable = DataClassInfoProvider.GetDataClassInfo(customTableClassName);

            CustomTableItem customTableItem = CustomTableItemProvider.GetItems(customTableClassName).OrderByDescending(x=>x.ItemID).FirstOrDefault();
            int orderNum = int.Parse(ValidationHelper.GetString(customTableItem.GetValue("OrderID"), ""));

            string OrderNumber = String.Format("{0:00000}", orderNum);
            if (customTable != null && Request["orderid"] != OrderNumber)
            {
                CustomTableItem newCustomTableItem = CustomTableItem.New(customTableClassName);
                newCustomTableItem.SetValue("FullName", ApiLibrary.Helper.PropertyReader.StripHTML(Request["Fullname"]));
                newCustomTableItem.SetValue("MobileNumber", "+" + Request["telCode"] + " " + Request["number"]);
                newCustomTableItem.SetValue("Email", Request["email"]);
                //newCustomTableItem.SetValue("Adult", Request["adult"]);
                //newCustomTableItem.SetValue("Child3", Request["child"]);
                //newCustomTableItem.SetValue("Child3plus", Request["child3"]);
                //newCustomTableItem.SetValue("SpecialAbilities", Request["special"]);
                //newCustomTableItem.SetValue("SpecialCompanion", Request["specialcompanion"]);                
                newCustomTableItem.SetValue("OrderId", Request["orderid"]);
                newCustomTableItem.SetValue("SubTotal", Request["subtotal"]);
                newCustomTableItem.SetValue("RequestedDate", Request["date"]);
                newCustomTableItem.SetValue("TotalAmount", Request["totalamount"]);
                newCustomTableItem.SetValue("Discount", Request["discount"]);

                newCustomTableItem.SetValue("Status", Request["status"]);

                string[] temp = Request["tickets"].TrimEnd(',').Split(',' , ':');

                for (int i = 0; i < temp.Length; i++)
                {

                    newCustomTableItem.SetValue(temp[i], temp[i + 1]);
                    i++;
                }

                newCustomTableItem.Insert();
                
               
                    //foreach (string tick in temp)
                    //{

                    //}



                }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    private PaymentRegistrationResponse CallPaymentGateway()
    {
        try
        {
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
    Request.ApplicationPath.TrimEnd('/') + "/";
            Registration registration = new Registration();
            registration.Currency = ApiLibrary.Helper.PropertyReader.ReadProperty("Currency");
             if (CurrentCultureNameTicket == "en-us")
            {
                registration.ReturnPath = baseUrl + ApiLibrary.Helper.PropertyReader.ReadProperty("PaymentReturnUrl") + "?" + Request["orderid"];
            } else if (CurrentCultureNameTicket == "ar-ae")
            {
                registration.ReturnPath = baseUrl + ApiLibrary.Helper.PropertyReader.ReadProperty("PaymentReturnUrlAr") + "?" + Request["orderid"];
            }
            registration.TransactionHint = ApiLibrary.Helper.PropertyReader.ReadProperty("PaymentTransactionHint");
            registration.OrderName = ApiLibrary.Helper.PropertyReader.ReadProperty("OrderName");
            registration.Channel = ApiLibrary.Helper.PropertyReader.ReadProperty("Channel");
            registration.UserName = ApiLibrary.Helper.PropertyReader.ReadProperty("UserName");
            registration.Password = ApiLibrary.Helper.PropertyReader.ReadProperty("Password");
            registration.OrderID = Request["orderid"]; 
            registration.Amount = Request["totalamount"];
            registration.Customer = "Demo Merchant";//PropertyReader.StripHTML(Request["Fullname"]);

            PaymentModel paymentModel = new PaymentModel();
            paymentModel.Registration = registration;

            //var json = new JavaScriptSerializer().Serialize(registration);

            //LogString(json);

            PaymentRegistrationResponse paymentRegistrationResponse = PaymentHelper.PaymentRegistrationApi(paymentModel);
            if (paymentRegistrationResponse != null)
            {
                string customTableClassName = "TicketPayment.Form";
                DataClassInfo customTable = DataClassInfoProvider.GetDataClassInfo(customTableClassName);
                if (customTable != null && Request["orderid"] != null)
                {

                    var customTableData = CustomTableItemProvider.GetItems(customTableClassName).WhereStartsWith("OrderId", Request["orderid"]);
                    if (customTableData != null)
                    {
                        foreach (CustomTableItem item in customTableData)
                        {                            
                            item.SetValue("TransactionId", paymentRegistrationResponse.Transaction.TransactionID);
                            item.Update();
                        }
                    }
                }
                Session["TransacitonId"] = paymentRegistrationResponse.Transaction.TransactionID;
                
                return paymentRegistrationResponse;
            }
            return new PaymentRegistrationResponse();

        }
        catch (Exception ex)
        {
            throw ex;
        }        
    }
    
    public static void TicketingLog(Exception ex)
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

    public static void LogString(string msg)
    {
        try
        {
           // string logBasePath = PropertyReader.ReadProperty("CustomLogPath");
            string logBasePath = "/TicketingLogs/";
            string fileName = "UAEP-CustomLogs-" + DateTime.Now.ToString("dd-MM-yyyy") + ".txt";
            string filePath = HostingEnvironment.MapPath(logBasePath + fileName);
            StreamWriter sw = new StreamWriter(filePath, true);
            sw.Write("\r\n ---------------------------- " + DateTime.Now.ToString() + " ---------------------------------\r\n");
            sw.WriteLine(msg);
            sw.Flush();
            sw.Dispose();
        }
        catch { }
    }

}
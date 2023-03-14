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
using System.Web.Hosting;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class loyalityform : System.Web.UI.Page
{
    string CurrentCultureNameLoyalty = CMS.Localization.LocalizationContext.CurrentCulture.CultureCode.ToLower();
    protected void Page_Load(object sender, EventArgs e)
    {
        
            LoyaltyCardSubmit();
        
    }
    protected void LoyaltyCardSubmit()
    {
        try
        {
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            if (Session["FormCaptchaCard"] != null && !string.IsNullOrEmpty(Request["captcha"]))
            {
                if (Session["FormCaptchaCard"].ToString().ToLower() == Request["captcha"].ToLower())
                {
                    

                    Page.Validate();
                    if (Page.IsValid)
                    {
                        this.SaveEntryTicket();

                        PaymentRegistrationResponse paymentRegistrationResponse =  CallPaymentGateway();
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
            LoyaltyLog(ex);
        }

    }

    private void SaveEntryTicket()
    {
        try
        {
            string customTableClassName = "LoyalityCardPayment.Form";
            DataClassInfo customTable = DataClassInfoProvider.GetDataClassInfo(customTableClassName);
            if (customTable != null)
            {
                
                CustomTableItem newCustomTableItem = CustomTableItem.New(customTableClassName);
                newCustomTableItem.SetValue("FullName", ApiLibrary.Helper.PropertyReader.StripHTML(Request["Fullname"]));
                newCustomTableItem.SetValue("MobileNumber", "+" + Request["telCode"] + " " + Request["number"]);
                newCustomTableItem.SetValue("Email", Request["email"]);
                newCustomTableItem.SetValue("ClubCode", Request["ClubCode"]);
                if (Request["fitness"] != "")
                {
                    newCustomTableItem.SetValue("GoldCard", Request["fitness"]);
                }
                else
                {
                    newCustomTableItem.SetValue("GoldCard", "0");
                }
                if (Request["silver"] != "")
                {
                    newCustomTableItem.SetValue("SilverCard", Request["silver"]);
                }
                else
                {
                    newCustomTableItem.SetValue("SilverCard", "0");
                }
                
                newCustomTableItem.SetValue("OrderId", Request["orderid"]);
                newCustomTableItem.SetValue("SubTotal", Request["subtotal"]);
                newCustomTableItem.SetValue("RequestedDate", DateTime.Now.ToString("dd-MM-yyyy"));
                newCustomTableItem.SetValue("DateofBirth", Request["date"]);
                newCustomTableItem.SetValue("TotalAmount", Request["totalamount"]);
                newCustomTableItem.SetValue("Discount", Request["discount"]);

                newCustomTableItem.SetValue("Status", Request["status"]);
                
                string filePath = ApiLibrary.Helper.PropertyReader.ReadProperty("ProfileImagePath") + Request["orderid"] + "Profile." +Request["pictureExtension"]; //"D:/Tfs Azure Development/Uaep/Solution/Website/CMS/FormPictures/MyImag";
                string base64Iamge = Request["picture"];               
                string base64IamgeRepairFinal = "";

                if (Request["pictureExtension"] == "jpg")
                {
                    base64IamgeRepairFinal = base64Iamge.Replace("data:image/jpeg;base64,", "");
                }
                else if (Request["pictureExtension"] == "jpeg")
                {
                    base64IamgeRepairFinal = base64Iamge.Replace("data:image/jpeg;base64,", "");

                }
                else if (Request["pictureExtension"] == "png")
                {
                    base64IamgeRepairFinal = base64Iamge.Replace("data:image/png;base64,", "");
                }
                else if (Request["pictureExtension"] == "gif")
                {
                    base64IamgeRepairFinal = base64Iamge.Replace("data:image/gif;base64,", "");
                }
                else if (Request["pictureExtension"] == "bmp")
                {
                    base64IamgeRepairFinal = base64Iamge.Replace("data:image/bmp;base64,", "");
                }

                string base64IamgeRepair = base64IamgeRepairFinal.Replace(" ", "+");

                File.WriteAllBytes(filePath, Convert.FromBase64String(base64IamgeRepair));

                string secondFilePath = ApiLibrary.Helper.PropertyReader.ReadProperty("ProfileImagePath") + Request["orderid"] + "Emirates." + Request["emiratesIdExtension"]; //"D:/Tfs Azure Development/Uaep/Solution/Website/CMS/FormPictures/MyImag";
                string secondBase64Image = Request["emiratesidpicture"];
                string base64SecondIamgeRepairFinal = "";

                if (Request["emiratesIdExtension"] == "jpg")
                {
                    base64SecondIamgeRepairFinal = secondBase64Image.Replace("data:image/jpeg;base64,", "");
                }
                else if (Request["emiratesIdExtension"] == "jpeg")
                {
                    base64SecondIamgeRepairFinal = secondBase64Image.Replace("data:image/jpeg;base64,", "");

                }
                else if (Request["emiratesIdExtension"] == "png")
                {
                    base64SecondIamgeRepairFinal = secondBase64Image.Replace("data:image/png;base64,", "");
                }
                else if (Request["emiratesIdExtension"] == "gif")
                {
                    base64SecondIamgeRepairFinal = secondBase64Image.Replace("data:image/gif;base64,", "");
                }
                else if (Request["emiratesIdExtension"] == "bmp")
                {
                    base64SecondIamgeRepairFinal = secondBase64Image.Replace("data:image/bmp;base64,", "");
                }


                string secondBase64IamgeRepair = base64SecondIamgeRepairFinal.Replace(" ", "+");
                File.WriteAllBytes(secondFilePath, Convert.FromBase64String(secondBase64IamgeRepair));

                newCustomTableItem.SetValue("Picture", filePath);
                newCustomTableItem.SetValue("EmiratesIdPicture", secondFilePath);

                newCustomTableItem.Insert();

                
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
            if (CurrentCultureNameLoyalty == "en-us")
            {
                registration.ReturnPath = baseUrl + ApiLibrary.Helper.PropertyReader.ReadProperty("CardPaymentReturnUrl") + "?" + Request["orderid"];
            }else if (CurrentCultureNameLoyalty == "ar-ae")
            {
                registration.ReturnPath = baseUrl + ApiLibrary.Helper.PropertyReader.ReadProperty("CardPaymentReturnUrlAr") + "?" + Request["orderid"];
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

            PaymentRegistrationResponse paymentRegistrationResponse = PaymentHelper.PaymentRegistrationApi(paymentModel);
            if (paymentRegistrationResponse != null)
            {

                string customTableClassName = "LoyalityCardPayment.Form";
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
    public static void LoyaltyLog(Exception ex)
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
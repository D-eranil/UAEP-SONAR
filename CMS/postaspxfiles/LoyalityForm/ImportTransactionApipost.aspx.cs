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
using CMS.Base;
using CMSApp;
using System.Web.Hosting;

public partial class loyalityImportTransactionApipost : System.Web.UI.Page
{
    //string CurrentCultureName = System.Threading.Thread.CurrentThread.CurrentCulture.ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["functionName"] == "SendLoyaltyTransactionToPOS")
        {
            SendLoyaltyTransactionToPOS();
        }
    }
    public static void LogInfo(string info, string name)
    {
        try
        {
            string logBasePath = "/TicketingLogs/";//PropertyReader.ReadProperty("LogBasePath");
            string fileName = name + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + ".txt";
            string filePath = HttpContext.Current.Server.MapPath(logBasePath + fileName);
            StreamWriter sw = new StreamWriter(filePath, true);
            //sw.Write("\r\n ---------------------------- " + DateTime.Now.ToString + " ---------------------------------\r\n");
            sw.WriteLine(info);
            sw.Flush();
            sw.Dispose();
        }
        catch { }
    }
    public void CallImportTransactionFinalizeCard(string orderId,string numberofItems,string itemNo,string KeyValue)
    {
        try
        {
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();

            Page.Validate();
            if (Page.IsValid)
            {

                //List<GetPriceTicketData> itemList = javaScriptSerializer.Deserialize<List<GetPriceTicketData>>(Request["ticketdata"]);
                //string orderId = Request["orderId"];
               // string numberofItems = Request["numberOFItems"];
                //string itemNo = Request["itemNo"];
                //string KeyValue = Request["KeyValue"];
                //   var barcodeTicketItemList = Request["barcodeTicketItems"];

                 string ResponseOk = CallImportTransactionApi(orderId, numberofItems, itemNo, KeyValue); //POS Final API


                //    if (ResponseOk == "OK")
                //    {
                //        Response.Write("success");
                //    }
                //    else
                //    {
                //        Response.Write("fail");
                //    }
                //}
                //else
                //{
                //    Response.Write("fail");
                //}
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.InnerException);
            LoyaltyLog(ex);
        }

    }




    public string CallImportTransactionApi(string orderID, string numberofItems, string itemNo, string KeyValue)
    {
        string ResponseOk = "";
        try
        {
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            string customTableClassName = "LoyalityCardPayment.Form";

            string xmlChild = "";
            string xmlMain = "";


            CustomTableItem customTableItem = CustomTableItemProvider.GetItems(customTableClassName).WhereEquals("OrderId", orderID).TopN(1).FirstOrDefault();
            string subTotal = ValidationHelper.GetString(customTableItem.GetValue("SubTotal"), "");
            string totalAmount = ValidationHelper.GetString(customTableItem.GetValue("TotalAmount"), "");
            string Discount = ValidationHelper.GetString(customTableItem.GetValue("Disocunt"), "");
            string Date = ValidationHelper.GetDateTime(customTableItem.GetValue("RequestedDate", ""), DateTime.Now).ToString("yyyy-MM-dd");
            string receiptNumber = "WR" + ValidationHelper.GetString(customTableItem.GetValue("TransactionId"), "");
            string storeNumber = "S1001";
            string numberOfItems = numberofItems;
            string mobNumb = ValidationHelper.GetString(customTableItem.GetValue("MobileNumber"), "");
            string memberCardNumber = mobNumb.Replace("+", " ");
            string unitPrice = subTotal;
            string tenderType = "Online Payment";
            string lineNumber = "10000";

            string customTableClassNames = "UAEP.RandomNumbersGeneration";
            CustomTableItem customTableItems = CustomTableItemProvider.GetItems(customTableClassNames).WhereEquals("ItemID", "1").TopN(1).FirstOrDefault();
            int cmsCardNumber = int.Parse(ValidationHelper.GetString(customTableItems.GetValue("CardNumber"), "")) + 1;
            string cardNumberCount = String.Format("{0:0000000}", cmsCardNumber);
            string cardNumber = cardNumberCount;
            // int customerEntryNumberTable = int.Parse(ValidationHelper.GetString(customTableItems.GetValue("CustomerEntryNumber"), "")) + 1;
            int customerNoTable = int.Parse(ValidationHelper.GetString(customTableItems.GetValue("CustomerNo"), "")) + 1;
            string customerEntryNumber = ValidationHelper.GetString(customTableItem.GetValue("TransactionId"), ""); // customerEntryNumberTable.ToString()
            string customerNo = memberCardNumber;//"WC"+customerNoTable.ToString();
            string customerContactno = customerNo;
            string customerName = ValidationHelper.GetString(customTableItem.GetValue("FullName"), "");
            string customerAddress = ValidationHelper.GetString(customTableItem.GetValue("Address"), "");
            string customerEmail = ValidationHelper.GetString(customTableItem.GetValue("Email"), "");
            string clubCode = ValidationHelper.GetString(customTableItem.GetValue("ClubCode"), "");
            string schemeCode = KeyValue.ToUpper();


            customTableItems.SetValue("CardNumber", cmsCardNumber);
            //customTableItems.SetValue("CustomerEntryNumber", customerEntryNumberTable);
            customTableItems.SetValue("CustomerNo", customerNoTable);
            customTableItems.Update();

            InsertBarCodeLoyaltyTable(orderID, numberofItems, itemNo, KeyValue);

            xmlChild = GetPriceXmlChildFileCard(itemNo, "1", subTotal, totalAmount, Discount, Date, receiptNumber, lineNumber, storeNumber, numberOfItems, memberCardNumber, tenderType, cardNumber, unitPrice); //GetPriceXmlChildFile(item.itemNo, item.qty);


            xmlMain = GetPriceXmlFile(xmlChild, customerEntryNumber, customerNo, customerContactno, customerName, customerAddress, customerEmail, subTotal, totalAmount, Discount, Date, receiptNumber, storeNumber, numberOfItems, memberCardNumber, tenderType, cardNumber, clubCode, schemeCode);


            string Status = ImportTransactionApi.ImportTransactionXmlApi(xmlMain);

            ResponseOk = Status;

            // update pos response in cms
            DataClassInfo customTable = DataClassInfoProvider.GetDataClassInfo(customTableClassName);
            if (customTable != null && orderID != null)
            {

                var customTableData = CustomTableItemProvider.GetItems(customTableClassName).WhereStartsWith("OrderId", orderID);
                string ImportTransactionApiResponse = ValidationHelper.GetString(customTableItem.GetValue("ImportTransactionApiResponse"), "");
                if (customTableData != null && ImportTransactionApiResponse != "OK")
                {

                    foreach (CustomTableItem item in customTableData)
                    {
                        item.SetValue("RetryCount", ResponseOk == "OK" ? 0 : 1);
                        item.SetValue("POSStatus", ResponseOk == "OK" ? POSResponse.Sucess.ToString() : POSResponse.Failure.ToString());
                        item.SetValue("ImportTransactionApiResponse", ResponseOk == "OK" ? ResponseOk : null);
                        item.Update();
                    }
                }
            }



        }
        catch (Exception ex)
        {
            ResponseOk = ex.InnerException.ToString();
            throw ex;
        }
        return ResponseOk;

    }

    public string GetPriceXmlFile(string innerXML, string customerEntryNumber, string customerNo, string customerContactno, string customerName, string customerAddress, string customerEmail, string subTotal, string totalAmount, string Discount, string Date, string receiptNumber, string storeNumber, string numberOfItems, string memberCardNumber, string tenderType, string ticketNumber, string clubCode, string schemeCode)
    {

        List<String> list = new List<string>();
        List<String> values = new List<string>();

        list.Add("$TicketItems$");
        if (!String.IsNullOrEmpty(innerXML))
            values.Add(innerXML);
        else
            values.Add(String.Empty);

        list.Add("$customerEntryno$");
        if (!String.IsNullOrEmpty(customerEntryNumber))
            values.Add(customerEntryNumber);
        else
            values.Add(String.Empty);

        list.Add("$customerNo$");
        if (!String.IsNullOrEmpty(customerNo))
            values.Add(customerNo);
        else
            values.Add(String.Empty);

        list.Add("$customerClubcode$");
        if (!String.IsNullOrEmpty(clubCode))
            values.Add(clubCode);
        else
            values.Add(String.Empty);

        list.Add("$customerSchemecode$");
        if (!String.IsNullOrEmpty(schemeCode))
            values.Add(schemeCode);
        else
            values.Add(String.Empty);

        list.Add("$customerContactno$");
        if (!String.IsNullOrEmpty(customerContactno))
            values.Add(customerContactno);
        else
            values.Add(String.Empty);

        list.Add("$customerMobileno$");
        if (!String.IsNullOrEmpty(customerContactno))
            values.Add(customerContactno);
        else
            values.Add(String.Empty);

        list.Add("$customerName$");
        if (!String.IsNullOrEmpty(customerName))
            values.Add(customerName);
        else
            values.Add(String.Empty);

        list.Add("$customerEmail$");
        if (!String.IsNullOrEmpty(customerEmail))
            values.Add(customerEmail);
        else
            values.Add(String.Empty);

        list.Add("$headerReceiptNo$");
        if (!String.IsNullOrEmpty(receiptNumber))
            values.Add(receiptNumber);
        else
            values.Add(String.Empty);

        list.Add("$headerStoreNo$");
        if (!String.IsNullOrEmpty(storeNumber))
            values.Add(storeNumber);
        else
            values.Add(String.Empty);

        list.Add("$headerTansactionDate$");
        if (!String.IsNullOrEmpty(Date))
            values.Add(Date);
        else
            values.Add(String.Empty);

        list.Add("$headerOriginialDate$");
        values.Add(DateTime.Now.ToString("yyyy-MM-dd"));

        list.Add("$headerTransactionTime$");
        values.Add(DateTime.Now.ToString("HH:mm:ss"));


        list.Add("$headerNoOfItem$");
        if (!String.IsNullOrEmpty(numberOfItems))
            values.Add(numberOfItems);
        else
            values.Add(String.Empty);

        list.Add("$headerMembeCardNo$");
        if (!String.IsNullOrEmpty(memberCardNumber))
            values.Add(memberCardNumber);
        else
            values.Add(String.Empty);

        list.Add("$headerNetAmount$");
        if (!String.IsNullOrEmpty(totalAmount))
            values.Add(totalAmount);
        else
            values.Add(String.Empty);

        list.Add("$headerGrossAmount$");
        if (!String.IsNullOrEmpty(subTotal))
            values.Add(subTotal);
        else
            values.Add(String.Empty);

        list.Add("$HeaderDiscountAmount$");
        if (!String.IsNullOrEmpty(Discount))
            values.Add(Discount);
        else
            values.Add(String.Empty);

        list.Add("$headerTenderType$");
        if (!String.IsNullOrEmpty(tenderType))
            values.Add(tenderType);
        else
            values.Add(String.Empty);

        list.Add("$HeaderPaymentAmount$");
        if (!String.IsNullOrEmpty(totalAmount))
            values.Add(totalAmount);
        else
            values.Add(String.Empty);


        list.Add("$headerNetPrice$");
        if (!String.IsNullOrEmpty(totalAmount))
            values.Add(totalAmount);
        else
            values.Add(String.Empty);



        string templateFile = "~/XMLApiRequests/SendBankResponseCard.xml";

        return LoyaltygetFile(HostingEnvironment.MapPath(templateFile), list, values);
    }

    public string GetPriceXmlChildFileCard(string itemNo, string qty, string subTotal, string totalAmount, string Discount, string Date, string receiptNumber, string lineNumber, string storeNumber, string numberOfItems, string memberCardNumber, string tenderType, string ticketNumber, string unitPrice)
    {

        List<String> list = new List<string>();
        List<String> values = new List<string>();
        int random_number = new Random().Next(10000, 99999);
        list.Add("$lineReceiptNo$");
        if (!String.IsNullOrEmpty(receiptNumber))
            values.Add(receiptNumber);
        else
            values.Add(String.Empty);


        list.Add("$lineTransDate$");
        if (!String.IsNullOrEmpty(Date))
            values.Add(Date);
        else
            values.Add(String.Empty);

        list.Add("$lineTransTime$");
        values.Add(DateTime.Now.ToString("HH:mm:ss"));

        list.Add("$lineNo$");
        if (!String.IsNullOrEmpty(lineNumber))
            values.Add(lineNumber);
        else
            values.Add(String.Empty);


        list.Add("$lineStoreNo$");
        if (!String.IsNullOrEmpty(storeNumber))
            values.Add(storeNumber);
        else
            values.Add(String.Empty);


        list.Add("$lineItemNo$");
        if (!String.IsNullOrEmpty(itemNo))
            values.Add(itemNo);
        else
            values.Add(String.Empty);

        list.Add("$lineQuantity$");
        if (!String.IsNullOrEmpty(qty))
            values.Add(qty);
        else
            values.Add(String.Empty);


        list.Add("$lineUnitPrice$");
        if (!String.IsNullOrEmpty(unitPrice))
            values.Add(unitPrice);
        else
            values.Add(String.Empty);

        list.Add("$lineTicketNo$");
        if (!String.IsNullOrEmpty(ticketNumber))
            values.Add(ticketNumber);
        else
            values.Add(String.Empty);

        list.Add("$lineNetAmount$");
        if (!String.IsNullOrEmpty(totalAmount))
            values.Add(totalAmount);
        else
            values.Add(String.Empty);


        list.Add("$lineDiscountAmount$");
        if (!String.IsNullOrEmpty(Discount))
            values.Add(Discount);
        else
            values.Add(String.Empty);

        list.Add("$lineAmount$");
        if (!String.IsNullOrEmpty(totalAmount))
            values.Add(totalAmount);
        else
            values.Add(String.Empty);


        string templateFile = "~/XMLApiRequests/CardItems.xml";

        return LoyaltygetFile(HostingEnvironment.MapPath(templateFile), list, values);
    }

    public string LoyaltygetFile(string path, List<String> list, List<String> values)
    {
        StreamReader reader = new StreamReader(path);
        string temp = reader.ReadToEnd();
        for (int i = 0; i < list.Count; i++)
            temp = temp.Replace(list[i], values[i]);
        reader.Close();
        reader.Dispose();
        return temp;
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

    /// <summary>
    /// SendTransactionToPOS
    /// </summary>
    public void SendLoyaltyTransactionToPOS()
    {
        string funcName = "SendLoyaltyTransactionToPOS";
        try
        {
            string SchedulerJobTableClassName = "SchedulerJobServices.Table";
            string ResponseOk = "";
            while (true)
            {
                var SchedulerJobresult = CustomTableItemProvider.GetItems(SchedulerJobTableClassName).WhereEquals("ServiceId", 2).OrderByDescending(x => x.ItemID).FirstOrDefault();
                if (SchedulerJobresult["NextJobExectutionDateTime"] != null)
                {
                    var ScehedulerDateTime = Convert.ToDateTime(SchedulerJobresult["NextJobExectutionDateTime"]);
                    if (ScehedulerDateTime <= DateTime.Now)
                    {
                        string customTableClassName = "LoyalityCardPayment.Form";
                        DataClassInfo customTable = DataClassInfoProvider.GetDataClassInfo(customTableClassName);
                        List<ImportApiTicketData> barCodeList = new List<ImportApiTicketData>();
                        ImportApiTicketData objImportApiTicketData = new ImportApiTicketData();
                        var result = CustomTableItemProvider.GetItems(customTableClassName).WhereEquals("ImportTransactionApiResponse", null).And().WhereNotEquals("RetryCount", 3).And().WhereEquals("ResponseCode", "0").And().WhereGreaterThan("ItemCreatedWhen", Convert.ToDateTime(SchedulerJobresult["FetchTransactionGreaterThan"])).OrderByDescending(x => x.ItemID);

                        foreach (CustomTableItem item in result)
                        {

                            if (Convert.ToInt32(item["RetryCount"]) < 3)
                            {
                                bool ExistOnBarCode = CheckIfLoyaltyOrderExistOnBarCodeTickets(item["OrderId"].ToString());
                                if (ExistOnBarCode == true)
                                    ResponseOk = CallSchedulerImportTransactionApi(item["OrderId"].ToString(), item["numberofitems"]?.ToString()); //POS Final API
                                else
                                    LoyaltySchedulerLog(funcName, "Transaction Does Not Exist on BarCode Table");
                            }
                            else
                                LoyaltySchedulerLog(funcName, "Transaction Needs to be Manually Setteled");
                        }
                        bool CheckifSendToPOSExist = CustomTableItemProvider.GetItems(customTableClassName).WhereEquals("ImportTransactionApiResponse", null).And().WhereEquals("ResponseCode", "0").And().WhereGreaterThan("ItemCreatedWhen", Convert.ToDateTime(SchedulerJobresult["FetchTransactionGreaterThan"])).And().WhereNotEquals("RetryCount", 3).OrderByDescending(x => x.ItemID).Count() > 0 ? true : false;

                        if (CheckifSendToPOSExist == false)
                            UpdateLoyaltySchedulerTime(ValidationHelper.GetInteger(SettingsHelper.AppSettings["SendToPOSNotExist"], +12));

                        else
                            UpdateLoyaltySchedulerTime(ValidationHelper.GetInteger(SettingsHelper.AppSettings["SendToPOSExist"], +6));
                    }
                }

                if (Convert.ToBoolean(SchedulerJobresult["JobServiceEnabled"]) == true)
                    continue;
                else
                    break;
            }

        }
        catch (Exception ex)
        {
            LoyaltySchedulerLog(funcName, ex?.Message, ex);
        }
    }
    /// <summary>
    /// Insert Data to Custom Table BarCodeTickets.Form
    /// </summary>
    /// <param name="barCodeList"></param>
    /// <param name="orderID"></param>
    private void InsertBarCodeLoyaltyTable(string orderID, string numberofItems, string ItemNo, string KeyValue)
    {
        string funcName = "InsertBarCodeLoyaltyTable";
        try
        {
            string customTableClassName = "BarCodeLoyalty.table";
            DataClassInfo customTable = DataClassInfoProvider.GetDataClassInfo(customTableClassName);

            CustomTableItem newCustomTableItem = CustomTableItem.New(customTableClassName);
            newCustomTableItem.SetValue("OrderID", orderID);
            newCustomTableItem.SetValue("itemNo", ItemNo);
            newCustomTableItem.SetValue("itemNo", ItemNo);
            newCustomTableItem.SetValue("KeyValue", KeyValue);
            newCustomTableItem.Insert();
        }
        catch (Exception ex)
        {
            LoyaltySchedulerLog(funcName, "Order ID is " + orderID + ex?.Message, ex);
        }
    }
    /// <summary>
    /// CheckIfOrderExistOnBarCodeTickets
    /// </summary>
    /// <param name="OrderId"></param>
    /// <returns></returns>
    private bool CheckIfLoyaltyOrderExistOnBarCodeTickets(string OrderId)
    {
        string funcName = "CheckIfLoyaltyOrderExistOnBarCodeTickets";
        List<CustomTableItem> barCodeList = new List<CustomTableItem>();
        try
        {
            string BarCodeTableClassName = "BarCodeLoyalty.table";
            barCodeList = CustomTableItemProvider.GetItems(BarCodeTableClassName).WhereEquals("OrderID", OrderId).OrderByDescending(x => x.ItemID).ToList();

        }
        catch (Exception ex)
        {
            LoyaltySchedulerLog(funcName, "Order ID is " + OrderId + ex?.Message, ex);
        }
        return barCodeList?.Count > 0 ? true : false;
    }
    /// <summary>
    /// CallImportTransactionApi
    /// </summary>
    /// <param name="OrderId"></param>
    /// <param name="numberofItems"></param>
    /// <returns></returns>
    public string CallSchedulerImportTransactionApi(string orderID, string numberofItems)
    {
        string ResponseOk = "", funcName = "CallSchedulerImportTransactionApi";
        try
        {
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            string customTableClassName = "LoyalityCardPayment.Form";

            string xmlChild = "";
            string xmlMain = "";

            string BarCodeTableClassName = "BarCodeLoyalty.table";
            var result = CustomTableItemProvider.GetItems(BarCodeTableClassName).WhereEquals("OrderID", orderID).OrderByDescending(x => x.ItemID).FirstOrDefault();


            CustomTableItem customTableItem = CustomTableItemProvider.GetItems(customTableClassName).WhereEquals("OrderId", orderID).TopN(1).FirstOrDefault();
            string subTotal = ValidationHelper.GetString(customTableItem.GetValue("SubTotal"), "");
            string totalAmount = ValidationHelper.GetString(customTableItem.GetValue("TotalAmount"), "");
            string Discount = ValidationHelper.GetString(customTableItem.GetValue("Disocunt"), "");
            string Date = ValidationHelper.GetDateTime(customTableItem.GetValue("RequestedDate", ""), DateTime.Now).ToString("yyyy-MM-dd");
            string receiptNumber = "WR" + ValidationHelper.GetString(customTableItem.GetValue("TransactionId"), "");
            string storeNumber = "S1001";
            string numberOfItems = numberofItems;
            string mobNumb = ValidationHelper.GetString(customTableItem.GetValue("MobileNumber"), "");
            string memberCardNumber = mobNumb.Replace("+", " ");
            string unitPrice = subTotal;
            string tenderType = "Online Payment";
            string lineNumber = "10000";

            string customTableClassNames = "UAEP.RandomNumbersGeneration";
            CustomTableItem customTableItems = CustomTableItemProvider.GetItems(customTableClassNames).WhereEquals("ItemID", "1").TopN(1).FirstOrDefault();
            int cmsCardNumber = int.Parse(ValidationHelper.GetString(customTableItems.GetValue("CardNumber"), "")) + 1;
            string cardNumberCount = String.Format("{0:0000000}", cmsCardNumber);
            string cardNumber = cardNumberCount;
            // int customerEntryNumberTable = int.Parse(ValidationHelper.GetString(customTableItems.GetValue("CustomerEntryNumber"), "")) + 1;
            int customerNoTable = int.Parse(ValidationHelper.GetString(customTableItems.GetValue("CustomerNo"), "")) + 1;
            string customerEntryNumber = ValidationHelper.GetString(customTableItem.GetValue("TransactionId"), ""); // customerEntryNumberTable.ToString()
            string customerNo = memberCardNumber;//"WC"+customerNoTable.ToString();
            string customerContactno = customerNo;
            string customerName = ValidationHelper.GetString(customTableItem.GetValue("FullName"), "");
            string customerAddress = ValidationHelper.GetString(customTableItem.GetValue("Address"), "");
            string customerEmail = ValidationHelper.GetString(customTableItem.GetValue("Email"), "");
            string clubCode = ValidationHelper.GetString(customTableItem.GetValue("ClubCode"), "");
            string schemeCode = result["KeyValue"].ToString().ToUpper();


            customTableItems.SetValue("CardNumber", cmsCardNumber);
            //customTableItems.SetValue("CustomerEntryNumber", customerEntryNumberTable);
            customTableItems.SetValue("CustomerNo", customerNoTable);
            customTableItems.Update();

            xmlChild = GetPriceXmlChildFileCard(result["ItemNo"].ToString(), "1", subTotal, totalAmount, Discount, Date, receiptNumber, lineNumber, storeNumber, numberOfItems, memberCardNumber, tenderType, cardNumber, unitPrice); //GetPriceXmlChildFile(item.itemNo, item.qty);

            xmlMain = GetPriceXmlFile(xmlChild, customerEntryNumber, customerNo, customerContactno, customerName, customerAddress, customerEmail, subTotal, totalAmount, Discount, Date, receiptNumber, storeNumber, numberOfItems, memberCardNumber, tenderType, cardNumber, clubCode, schemeCode);
            LogInfo(xmlMain, "RequestLog");

            string Status = ImportTransactionApi.ImportTransactionXmlApi(xmlMain);

            ResponseOk = Status;


            DataClassInfo customTable = DataClassInfoProvider.GetDataClassInfo(customTableClassName);
            if (customTable != null && orderID != null)
            {
                var customTableData = CustomTableItemProvider.GetItems(customTableClassName).WhereStartsWith("OrderId", orderID);
                string ImportTransactionApiResponse = ValidationHelper.GetString(customTableItem.GetValue("ImportTransactionApiResponse"), "");
                if (customTableData != null && ImportTransactionApiResponse != "OK")
                {
                    foreach (CustomTableItem item in customTableData)
                    {
                        if (Convert.ToInt32(item["RetryCount"]) < 3)
                        {
                            item.SetValue("RetryCount", Convert.ToInt32(item["RetryCount"]) + 1);
                            item.SetValue("POSStatus", (Convert.ToInt32(item["RetryCount"]) != 3) ? ((ResponseOk == "OK" && ResponseOk != null) ? POSResponse.Sucess.ToString() : POSResponse.Failure.ToString()) : (Convert.ToInt32(item["RetryCount"]) == 3 && (ResponseOk == "OK" && ResponseOk != null)) ? POSResponse.Sucess.ToString() : POSResponse.ManualSettled.ToString());
                            item.SetValue("ImportTransactionApiResponse", ResponseOk == "OK" ? ResponseOk : null);
                            item.Update();
                        }
                    }
                }
            }
            else
                LoyaltySchedulerLog(funcName, "Transaction needs to be Manually Settled " + orderID);
        }
        catch (Exception ex)
        {
            ResponseOk = ex.InnerException.ToString();
            LoyaltySchedulerLog(funcName, ex?.Message, ex);
        }
        return ResponseOk;
    }
    /// <summary>
    /// UpdateSchedulerTime
    /// </summary>
    /// <param name="Hours"></param>
    private void UpdateLoyaltySchedulerTime(int Hours)
    {
        string funcName = "UpdateLoyaltySchedulerTime";
        try
        {
            string SchedulerJobTableClassName = "SchedulerJobServices.Table";
            var UpdateSchedulerJobresult = CustomTableItemProvider.GetItems(SchedulerJobTableClassName).WhereEquals("ServiceId", 2).OrderByDescending(x => x.ItemID).FirstOrDefault();
            UpdateSchedulerJobresult.SetValue("NextJobExectutionDateTime", DateTime.Now.AddHours(+Hours));
            UpdateSchedulerJobresult.Update();
        }
        catch (Exception ex)
        {
            LoyaltySchedulerLog(funcName, ex?.Message, ex);
        }
    }
    /// <summary>
    /// SchedulerLog
    /// </summary>
    /// <param name="ex"></param>
    public static void LoyaltySchedulerLog(string funcName, string Message = null, Exception ex = null)
    {
        try
        {
            string logBasePath = "/LoyaltySchedulerLog/";
            string fileName = DateTime.Now.ToString("dd-MM-yyyy") + "-LogFile.txt";
            string filePath = HostingEnvironment.MapPath(logBasePath + fileName);
            StreamWriter sw = new StreamWriter(filePath, true);
            sw.WriteLine(funcName);
            sw.WriteLine(Message);
            if (ex != null)
            {

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
            }
            else
            {
                sw.WriteLine(Message);
            }
            sw.Flush();
            sw.Dispose();
        }
        catch { }
    }
    public enum POSResponse
    {
        Sucess = 0,
        Failure = 1,
        ManualSettled = 3
    }
}

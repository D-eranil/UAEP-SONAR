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
using System.Web.Hosting;
using CMSApp;

public partial class ImportTransactionApipost : System.Web.UI.Page
{
    string CurrentCultureName = System.Threading.Thread.CurrentThread.CurrentCulture.ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["functionName"] == "SendTransactionToPOS")
        {
            SendTransactionToPOS();
        }


    }
    public void CallImportTransactionFinalize(string orderId, string numberofItems, string barcodeTicketItems)
    {
        try
        {
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();

            Page.Validate();
            if (Page.IsValid)
            {
                string ResponseOk = CallImportTransactionApi(orderId, numberofItems, null, barcodeTicketItems); //POS Final API
            }
        }
        catch (Exception ex)
        {
            Log(ex);
        }
        }



    string Date;
    public string CallImportTransactionApi(string orderID, string numberofItems, string RequestData, string barCodes)
    {
        string ResponseOk = "";
        try
        {

            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            string customTableClassName = "TicketPayment.Form";

            string xmlChild = "";
            string xmlMain = "";


            List<ImportApiTicketData> barCodeList = javaScriptSerializer.Deserialize<List<ImportApiTicketData>>(barCodes);

            CustomTableItem customTableItem = CustomTableItemProvider.GetItems(customTableClassName).WhereEquals("OrderId", orderID).TopN(1).FirstOrDefault();
            string subTotal = ValidationHelper.GetString(customTableItem.GetValue("SubTotal"), "");
            string totalAmount = ValidationHelper.GetString(customTableItem.GetValue("TotalAmount"), "");
            string Discount = ValidationHelper.GetString(customTableItem.GetValue("Discount"), "");
            string Date = ValidationHelper.GetDateTime(customTableItem.GetValue("RequestedDate", ""), DateTime.Now).ToString("yyyy-MM-dd");//ValidationHelper.GetString(customTableItem.GetValue("ItemCreatedWhen"), "");

            string receiptNumber = "WR" + ValidationHelper.GetString(customTableItem.GetValue("TransactionId"), "");//random_number.ToString();
            string storeNumber = "S1001";
            string numberOfItems = numberofItems;
            string memberCardNumber = "";
            string unitPrice = "";
            string tenderType = "Online Payment"; //card
            string lineNumber = "";
            string ticketNumber = ""; //ValidationHelper.GetString(customTableItem.GetValue("TicketNumber"), "");
            int count = 0;

            string BarCodeTicketsTableName = "BarCodeTickets.Form";
            DataClassInfo BarCodeTicketsTable = DataClassInfoProvider.GetDataClassInfo(BarCodeTicketsTableName);
            bool InsertBarCodeTickets = false;
            foreach (var item in barCodeList)
            {
                // for (var i = 0; i < int.Parse(item.qty); i++ ) {
                //    ticketNumber = barCodeList[i].ticketNumber;

                unitPrice = item.unitPrice;
                ticketNumber = item.ticketNumber;
                count += 10000;
                lineNumber = count.ToString();


                //ticketNumber = "W20210000003";

                if (InsertBarCodeTickets == false)
                {
                    InsertBarCodeTicketsTable(barCodeList, orderID, numberofItems);
                    InsertBarCodeTickets = true;
                }
                xmlChild += GetPriceXmlChildFile(item.itemNo, "1", subTotal, totalAmount, Discount, Date, receiptNumber, lineNumber, storeNumber, numberOfItems, memberCardNumber, tenderType, ticketNumber, unitPrice, item.itemDiscount); //GetPriceXmlChildFile(item.itemNo, item.qty);
                                                                                                                                                                                                                                            // }
            }
            xmlMain = GetPriceXmlFile(xmlChild, subTotal, totalAmount, Discount, Date, receiptNumber, storeNumber, numberOfItems, memberCardNumber, tenderType, ticketNumber);
            LogInfo(xmlMain, "RequestLog");

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
                        item.SetValue("ImportTransactionApiResponse", ResponseOk);
                        item.Update();

                    }
                }
            }




        }
        catch (Exception ex)
        {
            ResponseOk = ex.InnerException.ToString();
        }
        return ResponseOk;

    }

    public string GetPriceXmlFile(string innerXML, string subTotal, string totalAmount, string Discount, string Date, string receiptNumber, string storeNumber, string numberOfItems, string memberCardNumber, string tenderType, string ticketNumber)
    {

        try
        {
            List<String> list = new List<string>();
            List<String> values = new List<string>();

            list.Add("$TicketItems$");
            if (!String.IsNullOrEmpty(innerXML))
                values.Add(innerXML);
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



            string templateFile = "~/XMLApiRequests/SendBankResponse.xml";

            return getFile(HostingEnvironment.MapPath(templateFile), list, values);
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    public string GetPriceXmlChildFile(string itemNo, string qty, string subTotal, string totalAmount, string Discount, string Date, string receiptNumber, string lineNumber, string storeNumber, string numberOfItems, string memberCardNumber, string tenderType, string ticketNumber, string unitPrice, string itemDiscount)
    {
        try
        {
            List<String> list = new List<string>();
            List<String> values = new List<string>();
            int random_number = new Random().Next(10000, 99999);
            list.Add("$lineReceiptNo$");
            if (!String.IsNullOrEmpty(receiptNumber))
                values.Add(receiptNumber);
            else
                values.Add(String.Empty);

            list.Add("$lineTicketExpiryDate$");
            if (!String.IsNullOrEmpty(Date))
                values.Add(Date);
            else
                values.Add(String.Empty);

            list.Add("$lineTicketActivateDate$");
            if (!String.IsNullOrEmpty(Date))
                values.Add(Date);
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
            if (!String.IsNullOrEmpty(itemDiscount))
                values.Add(itemDiscount);
            else
                values.Add(String.Empty);

            list.Add("$lineAmount$");
            if (!String.IsNullOrEmpty(totalAmount))
                values.Add(totalAmount);
            else
                values.Add(String.Empty);


            string templateFile = "~/XMLApiRequests/TicketItems.xml";
            return getFile(HostingEnvironment.MapPath(templateFile), list, values);
        }
        catch (Exception ex)
        {
            throw ex;
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
    public static void LogInfo(string info, string name)
    {
        try
        {
            string logBasePath = "/TicketingLogs/";//PropertyReader.ReadProperty("LogBasePath");
            string fileName = name + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + ".txt";
            string filePath = HostingEnvironment.MapPath(logBasePath + fileName);
            StreamWriter sw = new StreamWriter(filePath, true);
            //sw.Write("\r\n ---------------------------- " + DateTime.Now.ToString + " ---------------------------------\r\n");
            sw.WriteLine(info);
            sw.Flush();
            sw.Dispose();
        }
        catch { }
    }
    /// <summary>
    /// SendTransactionToPOS
    /// </summary>
    public void SendTransactionToPOS()
    {
        string funcName = "SendTransactionToPOS";
        try
        {
            string SchedulerJobTableClassName = "SchedulerJobServices.Table";
            string ResponseOk = "";
            while (true)
            {
                var SchedulerJobresult = CustomTableItemProvider.GetItems(SchedulerJobTableClassName).WhereEquals("ServiceId", 1).OrderByDescending(x => x.ItemID).FirstOrDefault();
                if (SchedulerJobresult["NextJobExectutionDateTime"] != null)
                {
                    var ScehedulerDateTime = Convert.ToDateTime(SchedulerJobresult["NextJobExectutionDateTime"]);
                    if (ScehedulerDateTime <= DateTime.Now)
                    {
                        string customTableClassName = "TicketPayment.Form";
                        DataClassInfo customTable = DataClassInfoProvider.GetDataClassInfo(customTableClassName);
                        List<ImportApiTicketData> barCodeList = new List<ImportApiTicketData>();
                        ImportApiTicketData objImportApiTicketData = new ImportApiTicketData();
                        var result = CustomTableItemProvider.GetItems(customTableClassName).WhereEquals("ImportTransactionApiResponse", null).And().WhereNotEquals("RetryCount", 3).And().WhereEquals("ResponseCode", "0").And().WhereGreaterThan("ItemCreatedWhen", Convert.ToDateTime(SchedulerJobresult["FetchTransactionGreaterThan"])).OrderByDescending(x => x.ItemID);

                        foreach (CustomTableItem item in result)
                        {

                            if (Convert.ToInt32(item["RetryCount"]) < 3)
                            {
                                bool ExistOnBarCode = CheckIfOrderExistOnBarCodeTickets(item["OrderId"].ToString());
                                if (ExistOnBarCode == true)
                                    ResponseOk = CallImportTransactionApi(item["OrderId"].ToString(), item["numberofitems"]?.ToString()); //POS Final API
                                else
                                    SchedulerLog(funcName, "Transaction Does Not Exist on BarCode Table");

                            }
                            else
                                SchedulerLog(funcName, "Transaction Needs to be Manually Setteled");
                        }
                        bool CheckifSendToPOSExist = CustomTableItemProvider.GetItems(customTableClassName).WhereEquals("ImportTransactionApiResponse", null).And().WhereEquals("ResponseCode", "0").And().WhereGreaterThan("ItemCreatedWhen", Convert.ToDateTime(SchedulerJobresult["FetchTransactionGreaterThan"])).And().WhereNotEquals("RetryCount", 3).OrderByDescending(x => x.ItemID).Count() > 0 ? true : false;

                        if (CheckifSendToPOSExist == false)
                            UpdateSchedulerTime(ValidationHelper.GetInteger(SettingsHelper.AppSettings["SendToPOSNotExist"], +12));

                        else
                            UpdateSchedulerTime(ValidationHelper.GetInteger(SettingsHelper.AppSettings["SendToPOSExist"], +6));
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
            SchedulerLog(funcName, ex?.Message, ex);
        }
    }
    /// <summary>
    /// Insert Data to Custom Table BarCodeTickets.Form
    /// </summary>
    /// <param name="barCodeList"></param>
    /// <param name="orderID"></param>
    private void InsertBarCodeTicketsTable(List<ImportApiTicketData> barCodeList, string orderID, string numberofItems)
    {
        string funcName = "InsertBarCodeTicketsTable";
        try
        {
            string customTableClassName = "BarCodeTickets.Form";
            DataClassInfo customTable = DataClassInfoProvider.GetDataClassInfo(customTableClassName);

            foreach (var item in barCodeList)
            {
                CustomTableItem newCustomTableItem = CustomTableItem.New(customTableClassName);

                newCustomTableItem.SetValue("ticketNumber", item.ticketNumber);
                newCustomTableItem.SetValue("OrderID", orderID);
                newCustomTableItem.SetValue("unitPrice", item.unitPrice);
                newCustomTableItem.SetValue("Date", item.Date);
                newCustomTableItem.SetValue("itemNo", item.itemNo);
                newCustomTableItem.SetValue("itemDiscount", item.itemDiscount == null ? "0" : item.itemDiscount);
                newCustomTableItem.SetValue("numberofItems", numberofItems);
                newCustomTableItem.Insert();
            }
        }
        catch (Exception ex)
        {
            SchedulerLog(funcName, "Order ID is " + orderID + ex?.Message, ex);
        }
    }
    /// <summary>
    /// CheckIfOrderExistOnBarCodeTickets
    /// </summary>
    /// <param name="OrderId"></param>
    /// <returns></returns>
    private bool CheckIfOrderExistOnBarCodeTickets(string OrderId)
    {
        string funcName = "CheckIfOrderExistOnBarCodeTickets";
        List<CustomTableItem> barCodeList = new List<CustomTableItem>();
        try
        {
            string BarCodeTableClassName = "BarCodeTickets.Form";
            barCodeList = CustomTableItemProvider.GetItems(BarCodeTableClassName).WhereEquals("OrderID", OrderId).OrderByDescending(x => x.ItemID).ToList();

        }
        catch (Exception ex)
        {
            SchedulerLog(funcName, "Order ID is " + OrderId + ex?.Message, ex);
        }
        return barCodeList?.Count > 0 ? true : false;
    }
    /// <summary>
    /// CallImportTransactionApi
    /// </summary>
    /// <param name="OrderId"></param>
    /// <param name="numberofItems"></param>
    /// <returns></returns>
    public string CallImportTransactionApi(string OrderId, string numberofItems)
    {
        string ResponseOk = "", funcName = "CallImportTransactionApi";
        try
        {
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            string customTableClassName = "TicketPayment.Form";

            string xmlChild = "";
            string xmlMain = "";

            string BarCodeTableClassName = "BarCodeTickets.Form";
            List<CustomTableItem> barCodeList = CustomTableItemProvider.GetItems(BarCodeTableClassName).WhereEquals("OrderID", OrderId).OrderByDescending(x => x.ItemID).ToList();

            CustomTableItem customTableItem = CustomTableItemProvider.GetItems(customTableClassName).WhereEquals("OrderId", OrderId).TopN(1).FirstOrDefault();
            string subTotal = ValidationHelper.GetString(customTableItem.GetValue("SubTotal"), "");
            string totalAmount = ValidationHelper.GetString(customTableItem.GetValue("TotalAmount"), "");
            string Discount = ValidationHelper.GetString(customTableItem.GetValue("Discount"), "");
            string Date = ValidationHelper.GetDateTime(customTableItem.GetValue("RequestedDate", ""), DateTime.Now).ToString("yyyy-MM-dd");//ValidationHelper.GetString(customTableItem.GetValue("ItemCreatedWhen"), "");

            string receiptNumber = "WR" + ValidationHelper.GetString(customTableItem.GetValue("TransactionId"), "");//random_number.ToString();
            string storeNumber = "S1001";
            string memberCardNumber = "";
            string unitPrice = "";
            string tenderType = "Online Payment";
            string lineNumber = "";
            string ticketNumber = "";
            int count = 0;


            foreach (CustomTableItem item in barCodeList)
            {
                unitPrice = item["unitPrice"].ToString();
                ticketNumber = item["ticketNumber"].ToString();
                count += 10000;
                lineNumber = count.ToString();
                xmlChild += GetPriceXmlChildFile(item["itemNo"].ToString(), "1", subTotal, totalAmount, Discount, Date, receiptNumber, lineNumber, storeNumber, numberofItems, memberCardNumber, tenderType, ticketNumber, unitPrice, item["itemDiscount"].ToString()); //GetPriceXmlChildFile(item.itemNo, item.qty);
                                                                                                                                                                                                                                                                        // }
            }
            xmlMain = GetPriceXmlFile(xmlChild, subTotal, totalAmount, Discount, Date, receiptNumber, storeNumber, numberofItems, memberCardNumber, tenderType, ticketNumber);

            LogInfo(xmlMain, "RequestLog");
            string Status = ImportTransactionApi.ImportTransactionXmlApi(xmlMain);
            LogInfo("Import api Ends" + Status, "ImportApiEnding");
            ResponseOk = Status;


            DataClassInfo customTable = DataClassInfoProvider.GetDataClassInfo(customTableClassName);
            if (customTable != null && OrderId != null)
            {

                var customTableData = CustomTableItemProvider.GetItems(customTableClassName).WhereStartsWith("OrderId", OrderId);
                string ImportTransactionApiResponse = ValidationHelper.GetString(customTableItem.GetValue("ImportTransactionApiResponse"), "");
                if (customTableData != null && ImportTransactionApiResponse != "OK")
                {
                    foreach (CustomTableItem item in customTableData)
                    {
                        if (Convert.ToInt32(item["RetryCount"]) < 3)
                        {
                            item.SetValue("RetryCount", Convert.ToInt32(item["RetryCount"]) + 1);
                            item.SetValue("POSStatus", (Convert.ToInt32(item["RetryCount"]) != 3) ? ((ResponseOk == "OK" && ResponseOk != null) ? POSResponse.Sucess.ToString() : POSResponse.Failure.ToString()) : (Convert.ToInt32(item["RetryCount"]) == 3 && (ResponseOk == "OK" && ResponseOk != null)) ? POSResponse.Sucess.ToString() : POSResponse.ManualSettled.ToString());
                            item.SetValue("ImportTransactionApiResponse", ResponseOk);
                            item.Update();
                        }
                    }
                }


            }
            else
                SchedulerLog(funcName, "Transaction needs to be Manually Settled " + OrderId);
        }
        catch (Exception ex)
        {
            ResponseOk = ex.InnerException.ToString();
            SchedulerLog(funcName, ex?.Message, ex);
        }
        return ResponseOk;
    }
    /// <summary>
    /// UpdateSchedulerTime
    /// </summary>
    /// <param name="Hours"></param>
    private void UpdateSchedulerTime(int Hours)
    {
        string funcName = "UpdateSchedulerTime";
        try
        {

            string SchedulerJobTableClassName = "SchedulerJobServices.Table";
            var UpdateSchedulerJobresult = CustomTableItemProvider.GetItems(SchedulerJobTableClassName).WhereEquals("ServiceId", 1).OrderByDescending(x => x.ItemID).FirstOrDefault();
            UpdateSchedulerJobresult.SetValue("NextJobExectutionDateTime", DateTime.Now.AddHours(+Hours));
            UpdateSchedulerJobresult.Update();


        }
        catch (Exception ex)
        {
            SchedulerLog(funcName, ex?.Message, ex);
        }
    }
    /// <summary>
    /// SchedulerLog
    /// </summary>
    /// <param name="ex"></param>
    public static void SchedulerLog(string funcName, string Message = null, Exception ex = null)
    {
        try
        {
            string logBasePath = "/SchedulerLog/";
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

}


public enum POSResponse
{
    Sucess = 0,
    Failure = 1,
    ManualSettled = 3
}


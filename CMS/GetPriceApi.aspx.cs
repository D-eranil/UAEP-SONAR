using CMS.DocumentEngine;
using CMS.Helpers;
using CMS.Membership;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Net;
using System.Xml.Serialization;
using ApiLibrary.Helper;
using ApiLibrary.Model;
using Newtonsoft.Json;
using CMS.CustomTables;
using CMS.DataEngine;
using System.Web.Hosting;

public partial class GetPriceApi : System.Web.UI.Page
{
    private static string[] formats = new string[]
    {
        "MM/dd/yyyy hh:mm tt",
        "MM/dd/yyyy hh:mm",
        "M/dd/yyyy H:mm:ss tt",
        "MM/dd/yyyy",
        "M/dd/yyyy H:mm:ss"
    };
    private static DateTime ParseDate(string input)
    {
        return DateTime.ParseExact(input, formats, CultureInfo.InvariantCulture, DateTimeStyles.None);
    }

    string CurrentCultureName = CMS.Localization.LocalizationContext.CurrentCulture.CultureCode.ToLower();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request["ticketdata"].Length > 0)
        {
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            try
            {
                List<GetPriceTicketData> itemList = javaScriptSerializer.Deserialize<List<GetPriceTicketData>>(Request["ticketdata"]);
                string xmlChild = "";
                string xmlMain = "";
                foreach (var item in itemList)
                {
                    //if (item.qty != "0")
                    //{
                        xmlChild += GetPriceXmlChildFile(item.itemNo, item.qty);
                    //}
                }
                xmlMain = GetPriceXmlFile(xmlChild);

                GetPriceApiResponse returnXML = GetPriceApiHelper.GetPriceXmlApi(xmlMain);
                GetPriceResponse getPriceResponse = JsonConvert.DeserializeObject<GetPriceResponse>(returnXML.Body.GetItemPrice_Result.Return_value);
                    Response.Write(javaScriptSerializer.Serialize(getPriceResponse));
                
            }
            catch (Exception ex)
            {
                Response.Write(ex.InnerException);
                Log(ex);
            }
        }
    }

    public string GetPriceXmlFile(string innerXML)
    {

        List<String> list = new List<string>();
        List<String> values = new List<string>();

        list.Add("$ItemDetails$");
        if (!String.IsNullOrEmpty(innerXML))
            values.Add(innerXML);
        else
            values.Add(String.Empty);


        string templateFile = "~/XMLApiRequests/GetPrice.xml";

        return getFile(HostingEnvironment.MapPath(templateFile), list, values);
    }

    public string GetPriceXmlChildFile(string itemNo, string qty)
    {

        string customTableClassName = "UAEP.RandomNumbersGeneration";
        CustomTableItem customTableItem = CustomTableItemProvider.GetItems(customTableClassName).WhereEquals("ItemID", "1").TopN(1).FirstOrDefault();

        int orderNum = int.Parse(ValidationHelper.GetString(customTableItem.GetValue("OrderID"), ""));
        orderNum = orderNum + 1;
        string OrderNumber = String.Format("{0:00000}", orderNum);
        
        DataClassInfo customTable = DataClassInfoProvider.GetDataClassInfo(customTableClassName);
        if (customTable != null)
        {

            var customTableData = CustomTableItemProvider.GetItems(customTableClassName).WhereStartsWith("ItemID", "1");
            if (customTableData != null)
            {
                foreach (CustomTableItem item in customTableData)
                {
                    item.SetValue("OrderID", OrderNumber);
                    item.Update();
                }
            }
        }

        List<String> list = new List<string>();
        List<String> values = new List<string>();
        
        list.Add("$RequestId$");
         if (!String.IsNullOrEmpty(OrderNumber))
        values.Add(OrderNumber);
        else
            values.Add(String.Empty);


        list.Add("$RequestDate$");
        //if (!String.IsNullOrEmpty(Request["name"]))
        values.Add(DateTime.Now.ToString("yyyy-MM-dd"));
        //else
        //values.Add(String.Empty);


        list.Add("$RequestTime$");
        // if (!String.IsNullOrEmpty(Request["name"]))
        values.Add(DateTime.Now.ToString("hh:mm:ss"));
        //else
        //    values.Add(String.Empty);


        list.Add("$RequestItem$");
        if (!String.IsNullOrEmpty(itemNo))
            values.Add(itemNo);
        else
            values.Add(String.Empty);


        list.Add("$RequestQuantity$");
        if (!String.IsNullOrEmpty(qty))
            values.Add(qty);
        else
            values.Add(String.Empty);


        string templateFile = "~/XMLApiRequests/GetItemDetails.xml";

        return getFile(HostingEnvironment.MapPath(templateFile), list, values);
    }

    public static string getFile(string path, List<String> list, List<String> values)
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
            string filePath = HttpContext.Current.Server.MapPath(logBasePath + fileName);
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
            string filePath = HttpContext.Current.Server.MapPath(logBasePath + fileName);
            StreamWriter sw = new StreamWriter(filePath, true);
            //sw.Write("\r\n ---------------------------- " + DateTime.Now.ToString + " ---------------------------------\r\n");
            sw.WriteLine(info);
            sw.Flush();
            sw.Dispose();
        }
        catch { }
    }
}


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
using CMS.DataEngine;
using CMS.CustomTables;

public partial class SendBankResponsetoPosApi : System.Web.UI.Page
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


        string customTableClassName = "customtable.sampletable";

        // Gets the custom table
        DataClassInfo customTable = DataClassInfoProvider.GetDataClassInfo(customTableClassName);
        if (customTable != null)
        {
            // Gets the first custom table record whose value in the 'ItemText' starts with 'New text'
            CustomTableItem item = CustomTableItemProvider.GetItems(customTableClassName)
                                                                .WhereStartsWith("ItemText", "New text")
                                                                .FirstObject;

            if (item != null)
            {
                // Deletes the custom table record from the database
                item.Delete();
            }
        }







        if (Request["ticketdata"].Length > 0)
        {
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            List<SendBankResponsetoPosData> itemList = javaScriptSerializer.Deserialize<List<SendBankResponsetoPosData>>(Request["ticketdata"]);
            string xmlChild = "";
            string xmlMain = "";
            foreach (var item in itemList)
            {
                xmlChild += GetPriceXmlChildFile(item.itemNo, item.qty);            
            }
            xmlMain = GetPriceXmlFile(xmlChild);

            PaymentPosReponse returnXML = SendResponsetoPosApiHelper.PaymentPosReponseXmlApi(xmlMain);
            PosResponseData getPriceResponse = JsonConvert.DeserializeObject<PosResponseData>(returnXML.Body.GetItemPrice_Result.Return_value);
            Response.Write(javaScriptSerializer.Serialize(getPriceResponse));
           
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

        return getFile(Server.MapPath(templateFile), list, values);
    }

    public string GetPriceXmlChildFile(string itemNo, string qty)
    {

        List<String> list = new List<string>();
        List<String> values = new List<string>();
        int random_number = new Random().Next(10000, 99999);
        list.Add("$RequestId$");
        // if (!String.IsNullOrEmpty())
        values.Add(random_number.ToString());
        //else
        //    values.Add(String.Empty);


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

        return getFile(Server.MapPath(templateFile), list, values);
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
}


//    public string GetAllPagesEvents(string path , string pageType = "UAEP.Ticketing_Items")
//    {
//        try
//        {
            
//            TreeProvider tree = new TreeProvider(MembershipContext.AuthenticatedUser);
//            var nodes = tree.SelectNodes()
//                .Path(path)
//                .Type(pageType)
//                .OnCurrentSite()
//                .Culture(CurrentCultureName);

//            var Count = tree.SelectNodes().Path(path).Type(pageType).OnCurrentSite().Culture(CurrentCultureName).Count();
//            var serializer = new JavaScriptSerializer();
            

//            List<TicketsDataModel> EventList = nodes.Select(t => new TicketsDataModel(t)).OrderByDescending(x => x.start).ToList();
           
//           // var nonFiltered = EventList;
           
//            var finalString = serializer.Serialize(EventList);
//            return finalString ;
           
         
//        }
//        catch (Exception ex)
//        {
//            Response.Write(ex.InnerException);
//            throw ex;
//        }
//    }
    
//}


//public class TicketsDataModel
//{
//    public string TicketType { get; set; }
//    public string KeyValue { get; set; }
//    public string UnitPrice { get; set; }
//    public string ItemCreatedWhen { get; set; }
//    public string ItemCreatedTime { get; set; }
//   // public string ItemID { get; set; }
//    public string start { get; set; }

//    public TicketsDataModel(CMS.DocumentEngine.TreeNode item)
//    {

//        TicketType = ValidationHelper.GetString(item.GetValue("TicketType", ""), "");
//        KeyValue = ValidationHelper.GetString(item.GetValue("KeyValue", ""), "");
//        UnitPrice = ValidationHelper.GetString(item.GetValue("UnitPrice", ""), "");
//        //ItemID = ValidationHelper.GetString(item.GetValue("PageID", ""), "");
//        start = ValidationHelper.GetDateTime(item.GetValue("StartDate", ""), DateTime.Now).ToString("yyyy/MM/dd HH:mm:ss"); //ValidationHelper.GetString(item.GetValue("Number", ""), "") 
//        ItemCreatedWhen = ValidationHelper.GetDateTime(item.GetValue("ItemCreatedWhen", ""), DateTime.Now).ToString("yyyy/MM/dd HH:mm:ss"); //ValidationHelper.GetString(item.GetValue("Number", ""), "") 
//        ItemCreatedTime = ValidationHelper.GetDateTime(item.GetValue("ItemCreatedWhen", ""), DateTime.Now).ToString("HH:mm:ss"); //ValidationHelper.GetString(item.GetValue("Number", ""), "") 



//    }
//}



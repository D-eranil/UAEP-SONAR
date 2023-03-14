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

public partial class GetTicketsItems : System.Web.UI.Page
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
        // return DateTime.ParseExact(input, formats, CultureInfo.InvariantCulture, DateTimeStyles.None);
        return DateTime.ParseExact(input, formats, CultureInfo.InvariantCulture, DateTimeStyles.None);
    }

    string CurrentCultureName = CMS.Localization.LocalizationContext.CurrentCulture.CultureCode.ToLower();
    
    //string CurrentCultureName = System.Threading.Thread.CurrentThread.CurrentCulture.ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.RequestType.ToUpper() == "GET")
        {
            string paths = "/Tickets-and-Loyalty-Programs/%";
            string pageType = "UAEP.Ticketing_Items";
            if (Request["path"] != null && !string.IsNullOrEmpty(Request["path"]))
            {
				paths = Convert.ToString(Request["path"]);
            }
            if (Request["pagetype"] != null && !string.IsNullOrEmpty(Request["pagetype"]))
            {
                pageType = Convert.ToString(Request["pagetype"]);
            }
            Response.Write(GetAllPagesEvents(paths, pageType) );
            

        }
    }

    public string GetAllPagesEvents(string path , string pageType)
    {
        try
        {
            
            TreeProvider tree = new TreeProvider(MembershipContext.AuthenticatedUser);
            var nodes = tree.SelectNodes()
                .Path(path)
                .Type(pageType)
                .OnCurrentSite()
                .Culture(CurrentCultureName);

            var Count = tree.SelectNodes().Path(path).Type(pageType).OnCurrentSite().Culture(CurrentCultureName).Count();
            var serializer = new JavaScriptSerializer();
            

            List<TicketsDataModel> EventList = nodes.Select(t => new TicketsDataModel(t)).OrderByDescending(x => x.start).ToList();
           
        
            var finalString = serializer.Serialize(EventList);
            return finalString ;
           
          
        }
        catch (Exception ex)
        {
            Response.Write(ex.InnerException);
            throw ex;
        }
    }
    
}

//public class EventsListDataModel
//{
//    public List<EventsDataModel> ListData { get; set; }
   
//}

public class TicketsDataModel
{
    public string ItemNo { get; set; }
    public string TicketType { get; set; }
    public string KeyValue { get; set; }
    public string UnitPrice { get; set; }
    public string ItemCreatedWhen { get; set; }
    public string ItemCreatedTime { get; set; }
    public string ClubCode { get; set; }
    public string start { get; set; }

    public TicketsDataModel(CMS.DocumentEngine.TreeNode item)
    {
        ItemNo = ValidationHelper.GetString(item.GetValue("ItemNumber", ""), "");
        TicketType = ValidationHelper.GetString(item.GetValue("TicketType", ""), "");
        TicketType = ValidationHelper.GetString(item.GetValue("TicketType", ""), "");
        KeyValue = ValidationHelper.GetString(item.GetValue("KeyValue", ""), "");        
        UnitPrice = ValidationHelper.GetString(item.GetValue("UnitPrice", ""), "");
        ClubCode = ValidationHelper.GetString(item.GetValue("ClubCode", ""), "");
        start = ValidationHelper.GetDateTime(item.GetValue("StartDate", ""), DateTime.Now).ToString("yyyy/MM/dd HH:mm:ss"); //ValidationHelper.GetString(item.GetValue("Number", ""), "") 
        ItemCreatedWhen = ValidationHelper.GetDateTime(item.GetValue("ItemCreatedWhen", ""), DateTime.Now).ToString("yyyy/MM/dd HH:mm:ss"); //ValidationHelper.GetString(item.GetValue("Number", ""), "") 
        ItemCreatedTime = ValidationHelper.GetDateTime(item.GetValue("ItemCreatedWhen", ""), DateTime.Now).ToString("HH:mm:ss"); //ValidationHelper.GetString(item.GetValue("Number", ""), "") 



    }
}



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

public partial class GetAllEvents : System.Web.UI.Page
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
            Response.Write(GetAllPagesEvents());
            

        }
    }

    public string GetAllPagesEvents(string path = "/Events-Calendar/%", string pageType = "UAEP.Events")
    {
        try
        {
            //return DocumentHelper.GetDocuments()
            //.Type("UAEP.Events", q => q.Columns
            //("Title", "Description", "StartDate", "EndDate", "Image", "Url", "Color")
            //.Path("/events-calendar", PathTypeEnum.Children)
            //.OrderBy("Startdate"))
            //.OnCurrentSite();

            //var serializer = new JavaScriptSerializer();
            //var data = serializer.Serialize(documents);
            //return data;

            //var finalString = serializer.Serialize(data);
            //return finalString;

            TreeProvider tree = new TreeProvider(MembershipContext.AuthenticatedUser);
            var nodes = tree.SelectNodes()
                .Path(path)
                .Type(pageType)
                .OnCurrentSite()
                .Culture(CurrentCultureName);

            var serializer = new JavaScriptSerializer();
            

            List<EventsDataModel> EventList = nodes.Select(t => new EventsDataModel(t)).Where(y => y.daysOfWeek[0] != "" && y.IsRecurringEvent == true ).OrderByDescending(x => x.start).ToList();
            List<NonRecurringEventsDataModel> NonRecurringEventList = nodes.Select(t => new NonRecurringEventsDataModel(t)).Where(y => y.IsRecurringEvent == false ).OrderByDescending(x => x.start).ToList();

            // var nonFiltered = EventList;

            for (int i = 0; i < EventList.Count; i++ )
            {
               
                    string temp = EventList[i].daysOfWeek[0];
                    string[] authorsList = temp.Split(new string[] { "|" }, StringSplitOptions.None);
                    for (int x = 0; x < authorsList.Length; x++)
                    {
                        EventList[i].daysOfWeek[x] = authorsList[x];
                    }
               
            }
            string json;
            var finalString = serializer.Serialize(EventList);
            finalString = finalString.TrimEnd(']');
            finalString = finalString.TrimStart('[');
            var finalString1 = serializer.Serialize(NonRecurringEventList);
            finalString1 = finalString1.TrimEnd(']');
            finalString1 = finalString1.TrimStart('[');
            if (finalString1 == "") {
                json = "[" + finalString + "]";
            }
            else if (finalString == "") {
                json = "[" + finalString1 + "]";
            }
            else
            {
                json = "[" + finalString + "," + finalString1 + "]";
            }
            return json;
           
           // EventsListDataModel data = new EventsListDataModel();
           // data.ListData = EventList;
            //var finalString = serializer.Serialize(data);
            //return finalString;

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
//public class DaysOfWeek
//{
//    public string NoOfDays { get; set; }
//}
public class EventsDataModel
{
    
    public string title { get; set; }
    public string description { get; set; }
    public string[] daysOfWeek{ get; set; } = new string[7];
    public bool IsRecurringEvent { get; set; }
    //public DateTime start { get; set; }
    //public DateTime end { get; set; }
    public string start { get; set; }
    public string end { get; set; }
    public string startRecur { get; set; }
    public string endRecur { get; set; }  
    public string image_url { get; set; }
    public string url { get; set; }
    public string backgroundColor { get; set; }
    public string allday { get; set; }
    

    public EventsDataModel(CMS.DocumentEngine.TreeNode item)
    {
        string recurringStartDate = "";
        string recurringEndDate = "";
        title = ValidationHelper.GetString(item.GetValue("Title", ""), "");
        allday = "true";
        description = ValidationHelper.GetString(item.GetValue("Description", ""), "").Replace("\n", "<br/>");
        //string[] days = daysOfWeek.Split(", ");
        IsRecurringEvent = ValidationHelper.GetBoolean(item.GetValue("IsRecurringEvent", false), false);
        daysOfWeek[0] = ValidationHelper.GetString(item.GetValue("DaysOfWeek", ""), "");  
        string startTime = ValidationHelper.GetDateTime(item.GetValue("StartDate", ""), DateTime.Now).ToString("yyyy-MM-dd");
        string endTime = ValidationHelper.GetDateTime(item.GetValue("EndDate", ""), DateTime.Now).ToString("yyyy-MM-dd");
        //start = DateTime.ParseExact(startTime, "MM/dd/yyyy" , CultureInfo.InvariantCulture, DateTimeStyles.None);
        //end = DateTime.ParseExact(endTime, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);
        start = startTime;
        end = endTime;
        if (item.GetValue("RecurringStartDate", "") != "") {
            recurringStartDate = ValidationHelper.GetDateTime(item.GetValue("RecurringStartDate", ""), DateTime.Now).ToString("yyyy-MM-dd");
        }
        startRecur = recurringStartDate;
        if (item.GetValue("RecurringEndDate", "") != "")
        {
            recurringEndDate = ValidationHelper.GetDateTime(item.GetValue("RecurringEndDate", ""), DateTime.Now).AddDays(1).ToString("yyyy-MM-dd");
        }
        endRecur = recurringEndDate;
        image_url = ValidationHelper.GetString(item.GetValue("Image", ""), "").Replace("~", "");
        url = ValidationHelper.GetString(item.GetValue("Url", ""), "").Replace("~", "");
        backgroundColor = ValidationHelper.GetString(item.GetValue("Color", ""), "");
    }
}

public class NonRecurringEventsDataModel
{

    public string title { get; set; }
    public string description { get; set; }
    public bool IsRecurringEvent { get; set; }    
    //  public string[] daysOfWeek { get; set; } = new string[7];
    //public string[] daysOfWeek { get; set; } = new string[1];
    //public DateTime start { get; set; }
    //public DateTime end { get; set; }
    public string start { get; set; }
    public string end { get; set; }
    //public string startRecur { get; set; }
    //public string endRecur { get; set; }
    public string image_url { get; set; }
    public string url { get; set; }
    public string backgroundColor { get; set; }
    public string allday { get; set; }


    public NonRecurringEventsDataModel(CMS.DocumentEngine.TreeNode item)
    {
        string recurringStartDate = "";
        string recurringEndDate = "";
        title = ValidationHelper.GetString(item.GetValue("Title", ""), "");
        allday = "true";
        IsRecurringEvent = ValidationHelper.GetBoolean(item.GetValue("IsRecurringEvent", false), false);
        description = ValidationHelper.GetString(item.GetValue("Description", ""), "").Replace("\n", "<br/>");
        //string[] days = daysOfWeek.Split(", ");
        //daysOfWeek[0] = ValidationHelper.GetString(item.GetValue("DaysOfWeek", ""), "");
        string startTime = ValidationHelper.GetDateTime(item.GetValue("StartDate", ""), DateTime.Now).ToString("yyyy-MM-dd");
        string endTime = ValidationHelper.GetDateTime(item.GetValue("EndDate", ""), DateTime.Now).ToString("yyyy-MM-dd");
        //start = DateTime.ParseExact(startTime, "MM/dd/yyyy" , CultureInfo.InvariantCulture, DateTimeStyles.None);
        //end = DateTime.ParseExact(endTime, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);
        start = startTime;
        end = endTime;
        //if (item.GetValue("RecurringStartDate", "") != "")
        //{
        //    recurringStartDate = ValidationHelper.GetDateTime(item.GetValue("RecurringStartDate", ""), DateTime.Now).AddDays(1).ToString("yyyy-MM-dd");
        //}
        //startRecur = recurringStartDate;
        //if (item.GetValue("RecurringEndDate", "") != "")
        //{
        //    recurringEndDate = ValidationHelper.GetDateTime(item.GetValue("RecurringEndDate", ""), DateTime.Now).AddDays(1).ToString("yyyy-MM-dd");
        //}
        //endRecur = recurringEndDate;
        image_url = ValidationHelper.GetString(item.GetValue("Image", ""), "").Replace("~", "");
        url = ValidationHelper.GetString(item.GetValue("Url", ""), "").Replace("~", "");
        backgroundColor = ValidationHelper.GetString(item.GetValue("Color", ""), "");
    }
}


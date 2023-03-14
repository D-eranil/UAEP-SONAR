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

public partial class GetFormsDropdown : System.Web.UI.Page
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
        try
        {
            if (Request.RequestType.ToUpper() == "GET")
            {
                string paths = "";
                if (Request["path"] != null && !string.IsNullOrEmpty(Request["path"]))
                {
                    paths = Convert.ToString(Request["path"]);
                }

                Response.Write(GetAllPagesEvents(paths));


            }
        }
        catch (Exception ex)
        {
            Log(ex);
            throw ex;
        }
    }

    public string GetAllPagesEvents(string path , string pageType = "UAEP.Forms_Drop_Down_Value")
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

            var Count = tree.SelectNodes().Path(path).Type(pageType).OnCurrentSite().Culture(CurrentCultureName).Count();
            var serializer = new JavaScriptSerializer();
            

            List<EventsdataModel> EventList = nodes.Select(t => new EventsdataModel(t)).OrderByDescending(x => x.start).ToList();
           
           // var nonFiltered = EventList;
           
            var finalString = serializer.Serialize(EventList);
            return finalString ;
           
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

    public static void Log(Exception ex)
    {
        try
        {
            string logBasePath = "/Email-Logs/";
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

//public class EventsListDataModel
//{
//    public List<EventsDataModel> ListData { get; set; }
   
//}

public class EventsdataModel
{
    public string option { get; set; }
    public string start { get; set; }

    public EventsdataModel(CMS.DocumentEngine.TreeNode item)
    {
       
        option = ValidationHelper.GetString(item.GetValue("Option", ""), "");
        start = ValidationHelper.GetDateTime(item.GetValue("StartDate", ""), DateTime.Now).ToString("yyyy/MM/dd HH:mm:ss"); //ValidationHelper.GetString(item.GetValue("Number", ""), "") 


    }
}





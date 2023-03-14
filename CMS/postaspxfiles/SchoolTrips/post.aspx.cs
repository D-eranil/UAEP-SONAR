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
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

public partial class schooltrip : System.Web.UI.Page
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

    string CurrentCultureName = System.Threading.Thread.CurrentThread.CurrentCulture.ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.RequestType.ToUpper() == "POST")
        {
            var date = Request["date"];
            var time = Request["time"];
            string dateTime = date + " " + time;
           DateTime eventDateTime = ParseDate(dateTime);
            var result = DateTime.Compare(eventDateTime, DateTime.Now);

            //var datetime = DateTime.ParseExact(dateTime, "MM-dd-yyyy HH:mm tt", null);
            // DateTime eventDateTime = DateTime.ParseExact(dateTime, "MM-dd-yyyy HH:mm tt", CultureInfo.CurrentCulture);
            // var result = DateTime.Compare(datetime, DateTime.Now);
            if (result < 0) {

                Response.Write("TimeError");
            }
             else{

            ContactUsFormSubmit();
            }
        }
    }
    protected void ContactUsFormSubmit()
    {
        try
        {
            if (Session["FormCaptcha"] != null && !string.IsNullOrEmpty(Request["captcha"]))
            {
                if (Session["FormCaptcha"].ToString().ToLower() == Request["captcha"].ToLower())
                {
                    
                    Page.Validate();
                    if (Page.IsValid)
                    {
                        this.SendContactUsFormDetail();
                        Response.Write("success");


                    }
                    else
                    {
                        Response.Write("fail");
                    }
                }
                else {
                    Response.Write("invalid");
                }
            }
            else
            {
                Response.Write("invalid");
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.InnerException);
            throw ex;
        }

    }
    private void SendContactUsFormDetail()
    {
        List<String> list = new List<string>();
        List<String> values = new List<string>();
        string logopath = null;
        string headerpath = null;
        logopath = "http://" + Request.Url.DnsSafeHost + "/email-templates/img/logo.png";
        headerpath = "http://" + Request.Url.DnsSafeHost + "/email-templates/img/header.jpg";

        list.Add("$headerUrl$");
        values.Add(headerpath);

        list.Add("$logoUrl$");
        values.Add(logopath);

        list.Add("$Rname$");
        values.Add(Request["Rname"]);

        list.Add("$Schname$");
        values.Add(Request["Schname"]);

        list.Add("$Numstd$");
        values.Add(Request["Numstd"]);

        list.Add("$Email$");
        if (!String.IsNullOrEmpty(Request["email"]))
            values.Add(Request["email"]);
        else
            values.Add(String.Empty);

        list.Add("$Phone$");
        if (!String.IsNullOrEmpty(Request["number"]))
        { values.Add("+"+Request["telCode"] + Request["number"]);}
        else
            values.Add(String.Empty);

        list.Add("$SchTripType$");
        if (!String.IsNullOrEmpty(Request["type"]))
            values.Add(Request["type"]);
        else
            values.Add(String.Empty);

        //list.Add("$Question$");
        //if (!String.IsNullOrEmpty(Request["question"]))
        //    values.Add(Request["question"]);
        //else
        //    values.Add(String.Empty);

        list.Add("$Time$");
        if (!String.IsNullOrEmpty(Request["time"]))
            values.Add(Request["time"]);
        else
            values.Add(String.Empty);

        list.Add("$Message$");
        if (!String.IsNullOrEmpty(Request["message"]))
            values.Add(Request["message"]);
        else
            values.Add(String.Empty);

        list.Add("$Date$");
        if (!String.IsNullOrEmpty(Request["date"]))
            values.Add(Request["date"]);
        else
            values.Add(String.Empty);

        list.Add("$date$");
        values.Add(DateTime.Now.ToString("MMM dd yyyy"));

        list.Add("$datefooter$");
        values.Add(DateTime.Now.Year.ToString());


        list.Add("$MainHeading$");
        values.Add("Umm Al Emarat Park: School Trips");

        //  list.Add("$message$");
        //  if (!String.IsNullOrEmpty(Request["message"]))
        //      values.Add(Request["message"]);
        //  else
        //      values.Add(String.Empty);


        string subject = "School Trips";
        string templateFile = "/email-templates/email_template_SchoolTripAdmin.html";

        string body = getFile(Server.MapPath(templateFile), list, values);
        SendMail(PropertyReader.ReadProperty("fromEmail"), subject, body);
        //SendClientEmail(Server, Request, Request["email"]);


        string customTableClassName = "SchoolTrip.Form";
        DataClassInfo customTable = DataClassInfoProvider.GetDataClassInfo(customTableClassName);
        if (customTable != null)
        {
            CustomTableItem newCustomTableItem = CustomTableItem.New(customTableClassName);
            
            newCustomTableItem.SetValue("RequesterName", StripHTML(Request["Rname"]));
			newCustomTableItem.SetValue("SchoolName", StripHTML(Request["Schname"]));
            newCustomTableItem.SetValue("SchoolTripType", StripHTML(Request["type"]));
            newCustomTableItem.SetValue("RequesterMobile", "+" + Request["telCode"] + " " + Request["number"]);
            newCustomTableItem.SetValue("Email", Request["email"]);
            newCustomTableItem.SetValue("NumberOfStudents", StripHTML(Request["Numstd"]));
            newCustomTableItem.SetValue("RequestedDate", StripHTML(Request["date"]));
            newCustomTableItem.SetValue("RequestedTime", StripHTML(Request["time"]));
            newCustomTableItem.SetValue("notes", StripHTML(Request["message"]));
            newCustomTableItem.Insert();
        }

    }
    public void SendMail(string from, string subject, string body)
    {
        try
        {
            string logopath = null;
            string toEmail = string.Empty;
            logopath = "http://" + Request.Url.DnsSafeHost + "/email-templates/img/info-header.jpg";
            
            body = body.Replace("$logoUrl$", logopath);
           
           

            toEmail = PropertyReader.ReadProperty("General-question");
            

            //string category = Request["category"];

            //if (category == "Alghanim Industries Corporate Office") { toEmail = PropertyReader.ReadProperty("Alghanim-Industries-Corporate-Office"); }
            //else if (category == "Chevrolet Alghanim") { toEmail = PropertyReader.ReadProperty("Chevrolet-Alghanim"); }
            //else if (category == "Cadillac Alghanim") { toEmail = PropertyReader.ReadProperty("Cadillac-Alghanim"); }
            //else if (category == "Honda Alghanim") { toEmail = PropertyReader.ReadProperty("Honda-Alghanim"); }
            //else if (category == "Lotus Alghanim") { toEmail = PropertyReader.ReadProperty("Lotus-Alghanim"); }
            //else if (category == "Ford Alghanim") { toEmail = PropertyReader.ReadProperty("Ford-Alghanim"); }
            //else if (category == "Lincoln Alghanim") { toEmail = PropertyReader.ReadProperty("Lincoln-Alghanim"); }
            //else if (category == "Automotive Support") { toEmail = PropertyReader.ReadProperty("Automotive-Support"); }
            //else if (category == "Engineering - Home, Commercial &amp; Maintanence") { toEmail = PropertyReader.ReadProperty("Engineering-Home-Commercial-Maintanence"); }
            //else if (category == "Costa Coffee Kuwait") { toEmail = PropertyReader.ReadProperty("Costa-Coffee-Kuwait"); }
            //else if (category == "Slim Chickens") { toEmail = PropertyReader.ReadProperty("Slim-Chickens"); }
            //else if (category == "Wendy's Middle East") { toEmail = PropertyReader.ReadProperty("Wendy-Middle-East"); }
            //else if (category == "X-cite Electronics website purchase") { toEmail = PropertyReader.ReadProperty("X-cite-Electronics-website-purchase"); }
            //else if (category == "X-cite Electronics store purchase") { toEmail = PropertyReader.ReadProperty("X-cite-Electronics-store-purchase"); }
            //else if (category == "Safat Home") { toEmail = PropertyReader.ReadProperty("Safat-Home"); }
            //else if (category == "Enaya Insurance") { toEmail = PropertyReader.ReadProperty("Enaya-Insurance"); }
            //else if (category == "Xerox") { toEmail = PropertyReader.ReadProperty("Xerox"); }
            //else if (category == "ATLAS") { toEmail = PropertyReader.ReadProperty("ATLAS"); }
            //else if (category == "Alghanim Travel") { toEmail = PropertyReader.ReadProperty("Alghanim-Travel"); }


            EmailSender.SendEmail(from, toEmail, PropertyReader.ReadProperty("ccEmail"), subject, body, PropertyReader.ReadProperty("bccEmail"));
            SendClientEmail(Server, Request, Request["email"]);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void SendClientEmail(HttpServerUtility Server, HttpRequest Request, string userEmail)
    {
        string templateFile = "/email-templates/email_template_SchoolTrip.html";
        string logopath = null;
        StreamReader sr = new StreamReader(Server.MapPath(templateFile));
        string email = sr.ReadToEnd();
        string date = DateTime.Now.ToString("MMM dd yyyy");
        email = email.Replace("$date$", date);
        
        email = email.Replace("$datefooter$", DateTime.Now.Year.ToString());

        email = email.Replace("$MainHeading$", "Umm Al Emarat Park: School Trips");
        email = email.Replace("$Rname$", Request["Rname"]);
        logopath = "http://" + Request.Url.DnsSafeHost + "/email-templates/img/info-header.jpg";
        email = email.Replace("$logoUrl$", logopath);
        sr.Close();
        sr.Dispose();

        EmailSender.SendEmail(PropertyReader.ReadProperty("fromEmail"), userEmail, PropertyReader.ReadProperty("ccEmail"), "Umm Al Emarat Park : School Trip", email, "");
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
        string fileName = str; //name.Value +"'s Resume-" + Guid.NewGuid().ToString().Substring(0, 4) +file.FileName.Substring(file.FileName.LastIndexOf('.'));
        file.SaveAs(path + fileName);
        return virtualPath + fileName;

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

                //var smtp = new System.Net.Mail.SmtpClient();
                //{
                //    smtp.Host = "mxa-001e4901.gslb.pphosted.com";
                //    smtp.Port = 25;
                //    smtp.EnableSsl = false;
                //    smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                //    smtp.UseDefaultCredentials = true;
                //    smtp.Timeout = 200000;

                //}
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
    public class PropertyReader
    {

        public PropertyReader()
        {

        }
        public static string ReadProperty(string propertyName)
        {

            AppSettingsReader reader = new AppSettingsReader();
            return (string)reader.GetValue(propertyName, typeof(System.String));
        }
    }
    public string StripHTML(string input)
    {
        return Regex.Replace(input, "<.*?>", String.Empty);
    }



}



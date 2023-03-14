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

public partial class post : System.Web.UI.Page
{
    string CurrentCultureName = System.Threading.Thread.CurrentThread.CurrentCulture.ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.RequestType.ToUpper() == "POST")
        {
            ContactUsFormSubmit();
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

        list.Add("$Name$");
        if (!String.IsNullOrEmpty(Request["name"]))
            values.Add(Request["name"]);
        else
            values.Add(String.Empty); 

        list.Add("$Email$");
        if (!String.IsNullOrEmpty(Request["email"]))
            values.Add(Request["email"]);
        else
            values.Add(String.Empty);


        list.Add("$Phone$");
        if (!String.IsNullOrEmpty(Request["number"]))
        { values.Add("+" + Request["telCode"] + Request["number"]); }
        else
            values.Add(String.Empty);

        list.Add("$Age$");
        if (!String.IsNullOrEmpty(Request["age"]))
            values.Add(Request["age"]);
        else
            values.Add(String.Empty);

        list.Add("$Nationality$");
        if (!String.IsNullOrEmpty(Request["nationality"]))
            values.Add(Request["nationality"]);
        else
            values.Add(String.Empty);

        list.Add("$Duration$");
        if (!String.IsNullOrEmpty(Request["duration"]))
            values.Add(Request["duration"]);
        else
            values.Add(String.Empty);

        list.Add("$Newsletter$");
        if (!String.IsNullOrEmpty(Request["newslettr"]))
            values.Add(Request["newslettr"]);
        else
            values.Add(String.Empty);
        list.Add("$Overallexperience$");
        if (!String.IsNullOrEmpty(Request["overallexperience"]))
            values.Add(Request["overallexperience"]);
        else
            values.Add(String.Empty);

        list.Add("$Clean$");
        if (!String.IsNullOrEmpty(Request["cleanlines"]))
            values.Add(Request["cleanlines"]);
        else
            values.Add(String.Empty);

        list.Add("$valueformoney$");
        if (!String.IsNullOrEmpty(Request["valueformoney"]))
            values.Add(Request["valueformoney"]);
        else
            values.Add(String.Empty);

        list.Add("$staffprofessionalism$");
        if (!String.IsNullOrEmpty(Request["staffprofessionalism"]))
            values.Add(Request["staffprofessionalism"]);
        else
            values.Add(String.Empty);

        list.Add("$diningexperience$");
        if (!String.IsNullOrEmpty(Request["diningexperience"]))
            values.Add(Request["diningexperience"]);
        else
            values.Add(String.Empty);

        list.Add("$parkfacilities$");
        if (!String.IsNullOrEmpty(Request["parkfacilities"]))
            values.Add(Request["parkfacilities"]);
        else
            values.Add(String.Empty);

        list.Add("$operatinghours$");
        if (!String.IsNullOrEmpty(Request["operatinghours"]))
            values.Add(Request["operatinghours"]);
        else
            values.Add(String.Empty);
        list.Add("$eventsandactivities$");
        if (!String.IsNullOrEmpty(Request["eventsandactivities"]))
            values.Add(Request["eventsandactivities"]);
        else
            values.Add(String.Empty);

        list.Add("$recommendation$");
        if (!String.IsNullOrEmpty(Request["recommendation"]))
            values.Add(Request["recommendation"]);
        else
            values.Add(String.Empty);

        list.Add("$Message$");
        if (!String.IsNullOrEmpty(Request["comments"]))
            values.Add(Request["comments"]);
        else
            values.Add(String.Empty);

        list.Add("$date$");
        values.Add(DateTime.Now.ToString("MMM dd yyyy"));

        list.Add("$datefooter$");
        values.Add(DateTime.Now.Year.ToString());


        list.Add("$MainHeading$");
        values.Add("Umm Al Emarat Park: Visitor's Feedback");

        

        string subject = "Visitor's Feedback";
        string templateFile = "/email-templates/email_template_VisitorFeedbackAdmin.html";

        string body = getFile(Server.MapPath(templateFile), list, values);
        SendMail(PropertyReader.ReadProperty("fromEmail"), subject, body);
        //SendClientEmail(Server, Request, Request["email"]);

        


        string customTableClassName = "VisitorsFeedback.Form";
        DataClassInfo customTable = DataClassInfoProvider.GetDataClassInfo(customTableClassName);
        if (customTable != null)
        {
            string Comments = "No comments";
            if (!string.IsNullOrEmpty(Request["comments"]))
            {
                Comments = StripHTML(Request["comments"]);
            }
           
            CustomTableItem newCustomTableItem = CustomTableItem.New(customTableClassName);
            newCustomTableItem.SetValue("Name", StripHTML(Request["name"]));
			newCustomTableItem.SetValue("Nationality", StripHTML(Request["nationality"]));
            newCustomTableItem.SetValue("MobileNumber", "+" + Request["telCode"] + " " + Request["number"]);
            newCustomTableItem.SetValue("Email", Request["email"]);
            newCustomTableItem.SetValue("Age", StripHTML(Request["age"]));
            newCustomTableItem.SetValue("Duration", Request["duration"]);
            newCustomTableItem.SetValue("NewsLetter", Request["newslettr"]);
            newCustomTableItem.SetValue("OverallExperience", Request["overallexperience"]);
            newCustomTableItem.SetValue("Cleanliness", Request["cleanlines"]);
            newCustomTableItem.SetValue("ValueForMoney", Request["valueformoney"]);
            newCustomTableItem.SetValue("StaffsProfessionalismAndAptitude", Request["staffprofessionalism"]);
            newCustomTableItem.SetValue("DiningExperience", Request["diningexperience"]);
            newCustomTableItem.SetValue("ParkFacilities", Request["parkfacilities"]);
            newCustomTableItem.SetValue("OperatingHours", Request["operatinghours"]);
            newCustomTableItem.SetValue("EventsAndActivities", Request["eventsandactivities"]);
            newCustomTableItem.SetValue("Recommendation", Request["recommendation"]);
            newCustomTableItem.SetValue("Comments", Comments);
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
        string templateFile = "/email-templates/email_template_VisitorFeedback.html";
        string logopath = null;
        StreamReader sr = new StreamReader(Server.MapPath(templateFile));
        string email = sr.ReadToEnd();
        string date = DateTime.Now.ToString("MMM dd yyyy");
        email = email.Replace("$date$", date);

        email = email.Replace("$datefooter$", DateTime.Now.Year.ToString());

        email = email.Replace("$MainHeading$", "Visitor's Feedback");
        
        logopath = "http://" + Request.Url.DnsSafeHost + "/email-templates/img/info-header.jpg";
        email = email.Replace("$logoUrl$", logopath);
        sr.Close();
        sr.Dispose();

        EmailSender.SendEmail(PropertyReader.ReadProperty("fromEmail"), userEmail, PropertyReader.ReadProperty("ccEmail"), "Umm Al Emarate Park: Thank you for your Feedback ", email, "");
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



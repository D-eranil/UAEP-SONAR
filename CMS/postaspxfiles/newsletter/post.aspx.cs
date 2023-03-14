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

public partial class newsletter : System.Web.UI.Page
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
            var message = SendContactUsFormDetail();
            if (message == "success")
            {

                Response.Write("success");
            }
            else if (message == "alreadyExist")
            {
                Response.Write("alreadyExist");
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.InnerException);
            throw ex;
        }

    }
    private string SendContactUsFormDetail()
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

        list.Add("$date$");
        values.Add(DateTime.Now.ToString("MMM dd yyyy"));

        list.Add("$datefooter$");
        values.Add(DateTime.Now.Year.ToString());


        list.Add("$MainHeading$");
        values.Add("Umm Al Eamrat Park - Newsletter");

        list.Add("$Email$");
        if (!String.IsNullOrEmpty(Request["email"]))
            values.Add(Request["email"]);
        else
            values.Add(String.Empty);

        string subject = "Newsletter";
        string templateFile = "/email-templates/email_template_NewsLetterAdmin.html";


        string customTableClassName = "NewsLetter.Form";
        DataClassInfo customTable = DataClassInfoProvider.GetDataClassInfo(customTableClassName);


        if (customTable != null)
        {
            CustomTableItem emailObject = CustomTableItemProvider.GetItems(customTableClassName)
                                                      .WhereEquals("Email", Request["email"])
                                                      .FirstOrDefault();
            if (emailObject != null)
            {
                return "alreadyExist";
            }
            CustomTableItem newCustomTableItem = CustomTableItem.New(customTableClassName);

            newCustomTableItem.SetValue("Email", Request["email"]);

            newCustomTableItem.Insert();

            string body = getFile(Server.MapPath(templateFile), list, values);
            SendMail(PropertyReader.ReadProperty("fromEmail"), subject, body);
            // SendClientEmail(Server, Request, Request["email"]);
        }
        return "success";
    }
    public void SendMail(string from, string subject, string body)
    {
        try
        {
            string logopath = null;
            string toEmail = string.Empty;

            logopath = "https://" + Request.Url.DnsSafeHost + "/email-templates/img/info-header.jpg";
            body = body.Replace("$logoUrl$", logopath);
            toEmail = PropertyReader.ReadProperty("General-question");
            //   string category = Request["category"];


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
        string templateFile = "/email-templates/email_template_newsLetter.html";
        string logopath = null;
        StreamReader sr = new StreamReader(Server.MapPath(templateFile));
        string email = sr.ReadToEnd();
        string date = DateTime.Now.ToString("MMM dd yyyy");
        email = email.Replace("$date$", date);

        email = email.Replace("$datefooter$", DateTime.Now.Year.ToString());

        email = email.Replace("$MainHeading$", "NewsLetter");


        logopath = "http://" + Request.Url.DnsSafeHost + "/email-templates/img/info-header.jpg";
        email = email.Replace("$logoUrl$", logopath);
        sr.Close();
        sr.Dispose();

        EmailSender.SendEmail(PropertyReader.ReadProperty("fromEmail"), userEmail, PropertyReader.ReadProperty("ccEmail"), "Umm Al Emarat Park: Thank you for your Subscription", email, "");
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
                LogInfo("start",toEmail);   

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




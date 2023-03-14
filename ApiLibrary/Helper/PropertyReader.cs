using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace ApiLibrary.Helper
{
    public class PropertyReader
    {
        public PropertyReader()
        {

        }

        public static string GetSiteUrl()
        {
            string url = string.Empty;
            HttpRequest request = HttpContext.Current.Request;
            if (request.IsSecureConnection)
                url = "https://";
            else
                url = "http://";
            url += request["HTTP_HOST"] + "/";
            return url;
        }

        public static string ReadProperty(string propertyName)
        {

            AppSettingsReader reader = new AppSettingsReader();
            return (string)reader.GetValue(propertyName, typeof(System.String));
        }

        public static string GetQueryString(string skipFields = null)
        {
            HttpRequest Request = HttpContext.Current.Request;

            string url = Request.Url.Query;
            if (url.StartsWith("?"))
                url = url.Substring(1, url.Length - 1);

            string finalResult = "";
            if (!String.IsNullOrEmpty(skipFields))
            {
                string[] skip = skipFields.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string key in Request.QueryString.AllKeys)
                {
                    if (!String.IsNullOrEmpty(key))
                    {
                        bool match = false;
                        foreach (string each in skip)
                        {
                            if (key.ToLower() == each.ToLower())
                                match = true;
                        }
                        if (!match)
                            finalResult = finalResult + key + "=" + Request.QueryString[key] + "&";
                    }
                }
                if (finalResult.Length > 0)
                    finalResult = finalResult.Substring(0, finalResult.Length - 1);

            }
            if (String.IsNullOrEmpty(finalResult))
                finalResult = url;

            return finalResult;
        }
        public static string StripHTML(string input)
        {
            return Regex.Replace(input, "<.*?>", String.Empty);
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
}

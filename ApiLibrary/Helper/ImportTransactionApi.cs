using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;

namespace ApiLibrary.Helper
{
    public static class ImportTransactionApi
    {
        public static string ImportTransactionXmlApi(string requestData)
        {
            string ResponseOk = "";

            try
            {
                //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://uaeppos.sinyarhospitality.ae:7047/BC140/WS/LT_SINYAR/Codeunit/LS_Item_Price"); // Live 
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://houa-uaeppos01.pom.local:7047/BC140/WS/LT_SINYAR/Codeunit/LS_Item_Price"); // demo uat
                request.Credentials = new NetworkCredential("levtech.pos1", "g@3X*F&Z"); //demo uat
                //request.Credentials = new NetworkCredential("POS.APIAdmin", "2@2@AdminP@$$w@rd");
                byte[] bytes;
                bytes = System.Text.Encoding.ASCII.GetBytes(requestData);
                request.Headers.Add(@"SOAPAction:'urn:microsoft-dynamics-schemas/codeunit/LS_Item_Price:GetItemPrice'");
                request.ContentType = "text/xml; encoding='utf-8'";
                request.ContentLength = bytes.Length;
                request.Method = "POST";
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();
                HttpWebResponse response;
                response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {

                    ResponseOk = response.StatusCode.ToString();
                    return ResponseOk;
                }
                else
                {
                    ResponseOk = response.StatusDescription.ToString();
                    return ResponseOk;

                }

            }
            catch (Exception ex)
            {
                Log(ex);
            }
            return null;
        }
        public static void Log(Exception ex)
        {
            try
            {
                string logBasePath = "/Email-Logs/";
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
    }
}

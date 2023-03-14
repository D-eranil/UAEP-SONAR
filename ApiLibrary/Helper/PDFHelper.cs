using ApiLibrary.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ApiLibrary.Helper
{
    public class PDFHelper
    {
        public static string GeneratePDF(PdfTicketData pdfTicketData)
        {
            try
            {
                string data = JsonConvert.SerializeObject(pdfTicketData);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:8085/pdf/GeneratePDF");
                //request.Credentials = new NetworkCredential("Demo_fY9c", "Comtrust@20182018");
                byte[] bytes;
                bytes = System.Text.Encoding.ASCII.GetBytes(data);
                //request.Headers.Add("20", "application/json");
                request.Accept = "application/json";
                // request.Headers.Add("12", "application/json");
                // request.Accept = "application/json";
                request.ContentType = "application/json; charset=utf-8";
                request.ContentLength = bytes.Length;
                request.Method = "POST";
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();
                HttpWebResponse response;
                response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream responseStream = response.GetResponseStream();
                    string reader = new StreamReader(responseStream).ReadToEnd();

                    return reader;
                }


            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return null;
        }
    }
}

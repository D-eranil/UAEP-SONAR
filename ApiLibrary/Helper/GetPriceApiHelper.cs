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
using System.Xml.Serialization;

namespace ApiLibrary.Helper
{
    public static class GetPriceApiHelper
    {
        public static GetPriceApiResponse GetPriceXmlApi(string requestData)
        {


            try
            {

               HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://houa-uaeppos01.pom.local:7047/BC140/WS/LT_SINYAR/Codeunit/LS_Item_Price "); // demo local and uat
                //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://uaeppos.sinyarhospitality.ae:7047/BC140/WS/LT_SINYAR/Codeunit/LS_Item_Price "); //production
                request.Credentials = new NetworkCredential("levtech.pos1", "g@3X*F&Z"); //demo uat
                request.ServerCertificateValidationCallback = delegate { return true; };
               // request.Credentials = new NetworkCredential("POS.APIAdmin", "2@2@AdminP@$$w@rd"); // production
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
                    Stream responseStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(responseStream);

                    XmlSerializer serializer = new XmlSerializer(typeof(GetPriceApiResponse));
                    GetPriceApiResponse getPriceApiResponse = (GetPriceApiResponse)serializer.Deserialize(reader);
                    return getPriceApiResponse;

                }


            }
            catch (Exception ex)
            {
                Log(ex);
            }
            return new GetPriceApiResponse();
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

    public class GetPriceTicketData
    {
        public string itemNo { get; set; }
        public string qty { get; set; }
        public string unitPrice { get; set; }
        public string itemDiscount { get; set; }
        public string KeyValue { get; set; }

    }
    public class GetPriceResponse
    {
        [JsonProperty(PropertyName = "Request ID")]
        public string RequestID { get; set; }

        [JsonProperty(PropertyName = "Total Amount")]
        public double TotalAmount { get; set; }

        [JsonProperty(PropertyName = "Total Discount")]
        public double TotalDiscount { get; set; }

        [JsonProperty(PropertyName = "Items")]
        public List<GetPriceResponsePerItem> Items { get; set; }

    }

    public class GetPriceResponsePerItem
    {

        [JsonProperty(PropertyName = "Item No.")]
        public string ItemNo { get; set; }

        [JsonProperty(PropertyName = "Amount")]
        public double Amount { get; set; }

        [JsonProperty(PropertyName = "Discount Amount")]
        public double DiscountAmount { get; set; }


    }



}

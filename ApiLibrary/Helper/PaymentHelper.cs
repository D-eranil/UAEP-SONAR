using ApiLibrary.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrary.Helper
{
    public static class PaymentHelper
    {
        public static PaymentRegistrationResponse PaymentRegistrationApi(PaymentModel requestData)
        {
            try
            {
                string data = JsonConvert.SerializeObject(requestData);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(PropertyReader.ReadProperty("PaymentGatewayUrl"));
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
                    LogString(data + ", RequestUrl : " + PropertyReader.ReadProperty("PaymentGatewayUrl"));

                    LogString(JsonConvert.SerializeObject(reader));
                    return JsonConvert.DeserializeObject<PaymentRegistrationResponse>(reader);


                    //return getPriceApiResponse;
                }

                
            }
            catch (Exception ex)
            {
                throw(ex);
            }
            return new PaymentRegistrationResponse();


        }
       
        public static PaymentFinalizeResponse PaymentFinalizeApi(PaymentFinalizeModel requestData)
        {
            try
            {
                string data = JsonConvert.SerializeObject(requestData);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(PropertyReader.ReadProperty("PaymentGatewayUrl"));
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
                    LogString2(data + ", RequestUrl : " + PropertyReader.ReadProperty("PaymentGatewayUrl"));
                    LogString2(JsonConvert.SerializeObject(reader));
                    return JsonConvert.DeserializeObject<PaymentFinalizeResponse>(reader);

                    
                }

              
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return new PaymentFinalizeResponse();


        }
        public static void LogString(string msg)
        {
            try
            {
                // string logBasePath = PropertyReader.ReadProperty("CustomLogPath");
                string logBasePath = "/TicketingLogs/";
                string fileName = "UAEP-CustomLogs-Request-Response" + DateTime.Now.ToString("dd-MM-yyyy") + ".txt";
                string filePath = System.Web.HttpContext.Current.Server.MapPath(logBasePath + fileName);
                StreamWriter sw = new StreamWriter(filePath, true);
                sw.Write("\r\n ---------------------------- " + DateTime.Now.ToString() + " ---------------------------------\r\n");
                sw.WriteLine(msg);
                sw.Flush();
                sw.Dispose();
            }
            catch { }
        }
        public static void LogString2(string msg)
        {
            try
            {
                // string logBasePath = PropertyReader.ReadProperty("CustomLogPath");
                string logBasePath = "/TicketingLogs/";
                string fileName = "UAEP-CustomLogs-Finalize-Request-Response" + DateTime.Now.ToString("dd-MM-yyyy") + ".txt";
                string filePath = System.Web.HttpContext.Current.Server.MapPath(logBasePath + fileName);
                StreamWriter sw = new StreamWriter(filePath, true);
                sw.Write("\r\n ---------------------------- " + DateTime.Now.ToString() + " ---------------------------------\r\n");
                sw.WriteLine(msg);
                sw.Flush();
                sw.Dispose();
            }
            catch { }
        }
    }

    public class PaymentModel
    {
        public Registration Registration { get; set; }
    }

    public class Registration
    {
        public string Currency { get; set; }
        public string ReturnPath { get; set; }
        public string TransactionHint { get; set; }
        public string OrderID { get; set; }
        public string Store { get; set; }
        public string Terminal { get; set; }
        public string Channel { get; set; }
        public string Amount { get; set; }
        public string Customer { get; set; }
        public string OrderName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class PaymentFinalizeModel
    {
        public Finalization Finalization { get; set; }
    }

    public class Finalization
    {
        public string TransactionID { get; set; }
        public string Customer { get; set; }       
        public string UserName { get; set; }
        public string Password { get; set; }
    }


}

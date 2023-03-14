using ApiLibrary.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ApiLibrary.Helper
{
    public static class SendResponsetoPosApiHelper
    {
        public static PaymentPosReponse PaymentPosReponseXmlApi(string requestData)
        {


            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://houa-uaeppos01.pom.local:7047/BC140/WS/LT_SINYAR/Codeunit/LS_Item_Price");
                request.Credentials = new NetworkCredential("levtech.pos1", "g@3X*F&Z");
                byte[] bytes;
                bytes = System.Text.Encoding.ASCII.GetBytes(requestData);
                request.Headers.Add(@"SOAPAction:'urn:microsoft-dynamics-schemas/codeunit/LS_Item_Price:ImportTransIntegration'");
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

                    XmlSerializer serializer = new XmlSerializer(typeof(PaymentPosReponse));
                    PaymentPosReponse paymentPosReponse = (PaymentPosReponse)serializer.Deserialize(reader);
                    return paymentPosReponse;

                }


            }
            catch (Exception ex)
            {
                //Log(ex);
            }
            return new PaymentPosReponse();
        }

    }

    public class SendBankResponsetoPosData
    {
        public string itemNo { get; set; }
        public string qty { get; set; }
    }

    public class PosResponseData
    {
        [JsonProperty(PropertyName = "Request ID")]
        public string RequestID { get; set; }

        [JsonProperty(PropertyName = "Total Amount")]
        public int TotalAmount { get; set; }

        [JsonProperty(PropertyName = "Total Discount")]
        public int TotalDiscount { get; set; }

    }

    public class ImportApiTicketData
    {
        public string ticketNumber { get; set; }
        public string unitPrice { get; set; }
        public string Date { get; set; }       
        public string itemNo { get; set; }
        public string itemDiscount { get; set; }

    }

}



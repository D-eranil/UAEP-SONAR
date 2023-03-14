using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace ApiLibrary.Model
{

    [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class GetPriceApiResponse
    {
        [XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public Body Body { get; set; }
        [XmlAttribute(AttributeName = "Soap", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Soap { get; set; }
    }

    [XmlRoot(ElementName = "GetItemPrice_Result", Namespace = "urn:microsoft-dynamics-schemas/codeunit/LS_Item_Price")]
    public class GetItemPrice_Result
    {
        [XmlElement(ElementName = "return_value", Namespace = "urn:microsoft-dynamics-schemas/codeunit/LS_Item_Price")]
        public string Return_value { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class Body
    {
        [XmlElement(ElementName = "GetItemPrice_Result", Namespace = "urn:microsoft-dynamics-schemas/codeunit/LS_Item_Price")]
        public GetItemPrice_Result GetItemPrice_Result { get; set; }
    }



}

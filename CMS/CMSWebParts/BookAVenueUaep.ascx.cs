using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.Helpers;
using CMS.PortalEngine.Web.UI;

public partial class CMSWebParts_BookAVenueUaep : CMSAbstractWebPart
{
    #region "Properties"

    public string Path
    {
        get
        {
            return ValidationHelper.GetString(GetStringValue("Path", ""), "");
        }
        set
        {
            SetValue("Path", value);
        }
    }
    public string Path1
    {
        get
        {
            return ValidationHelper.GetString(GetStringValue("Path1", ""), "");
        }
        set
        {
            SetValue("Path1", value);
        }
    }
    public string Path2
    {
        get
        {
            return ValidationHelper.GetString(GetStringValue("Path2", ""), "");
        }
        set
        {
            SetValue("Path2", value);
        }
    }
    public string Path3
    {
        get
        {
            return ValidationHelper.GetString(GetStringValue("Path3", ""), "");
        }
        set
        {
            SetValue("Path3", value);
        }
    }
    public string Path4
    {
        get
        {
            return ValidationHelper.GetString(GetStringValue("Path4", ""), "");
        }
        set
        {
            SetValue("Path4", value);
        }
    }
    public string Path5
    {
        get
        {
            return ValidationHelper.GetString(GetStringValue("Path5", ""), "");
        }
        set
        {
            SetValue("Path5", value);
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Content loaded event handler.
    /// </summary>
    public override void OnContentLoaded()
    {
        base.OnContentLoaded();
        SetupControl();
    }


    /// <summary>
    /// Initializes the control properties.
    /// </summary>
    protected void SetupControl()
    {
        if (this.StopProcessing)
        {
            // Do not process
        }
        else
        {
            BookaVenueCtaTitle.InnerHtml = ResHelper.GetString("Submit"); //CtaTitle;
           
            hdnNodePath.Value = Path;
            hdnNodePath1.Value = Path1;
            hdnNodePath2.Value = Path2;
            hdnNodePathCorporate1.Value = Path3;
            hdnNodePathCorporate2.Value = Path4;
            hdnNodePathBookingtype.Value = Path5;
            IndividualBooking.InnerHtml = ResHelper.GetString("custom.IndividualBooking");
            CorporateBooking.InnerHtml = ResHelper.GetString("custom.CorporateBooking");
            Label2Placeholder.InnerHtml = ResHelper.GetString("custom.BookingTypeplaceholder");
            Label2.InnerHtml = ResHelper.GetString("custom.BookingType");
            queryplaceholder.InnerHtml = ResHelper.GetString("custom.queryplaceholder");
            voicorpaoratePlaceholder.InnerHtml = ResHelper.GetString("custom.queryplaceholder");
            eventTypeCorporateplaceholder.InnerHtml = ResHelper.GetString("custom.eventTypeplaceholder");
            eventTypeplaceholder.InnerHtml = ResHelper.GetString("custom.eventTypeplaceholder");
            timeplaceholder.InnerHtml = ResHelper.GetString("custom.timeplaceholder");
            voi.InnerHtml = ResHelper.GetString("custom.VenOfIneterest");
            fn.InnerHtml = ResHelper.GetString("custom.FName");
            fnp.Attributes.Add("placeholder", ResHelper.GetString("Custom.FirstNamePlaceholderValue"));
            ln.InnerHtml = ResHelper.GetString("custom.LName");
            lnp.Attributes.Add("placeholder", ResHelper.GetString("Custom.LastNamePlaceholderValue"));
            cn.InnerHtml = ResHelper.GetString("custom.CName");
            cnam.Attributes.Add("placeholder", ResHelper.GetString("Custom.CompanyNamePlaceholderValue"));
            mn.InnerHtml = ResHelper.GetString("custom.MNumber");
            ema.InnerHtml = ResHelper.GetString("custom.Email");
            em.Attributes.Add("placeholder", ResHelper.GetString("Custom.EmailPlaceholderValue"));
            et.InnerHtml = ResHelper.GetString("custom.EType");
            ed.InnerHtml = ResHelper.GetString("custom.EDate");
            eventDatepicker.Attributes.Add("placeholder", ResHelper.GetString("Custom.EventDatePlaceholderValue"));
            toe.InnerHtml = ResHelper.GetString("custom.ETime");
            des.InnerHtml = ResHelper.GetString("custom.Description");
            mssg.Attributes.Add("placeholder", ResHelper.GetString("Custom.MessagePlaceholderValue"));
            txtcaptcha.Attributes.Add("placeholder", ResHelper.GetString("Custom.CaptchaPlaceholderValue"));
            tymsgBold.InnerHtml = ResHelper.GetString("custom.BookaVenuethanksmsgbold");
            tymsg.InnerHtml = ResHelper.GetString("custom.BookaVenuethanksmsg");
            invalidtimemsg.InnerHtml = ResHelper.GetString("custom.invalidtimemsg");
            errormsg.InnerHtml = ResHelper.GetString("custom.errormsg");
            invalidcapthchamsg.InnerHtml = ResHelper.GetString("custom.invalidcapthchamsg");

        }
    }


    /// <summary>
    /// Reloads the control data.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();

        SetupControl();
    }

    #endregion
}
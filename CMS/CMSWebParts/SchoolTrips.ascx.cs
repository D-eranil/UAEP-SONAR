using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.Helpers;
using CMS.PortalEngine.Web.UI;

public partial class CMSWebParts_SchoolTrips : CMSAbstractWebPart
{
    #region "Properties"

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
            SchoolTripsCtaTitle.InnerHtml = ResHelper.GetString("Custom.SchoolTripCtaTitle");

            hdnNodePathTripType.Value = Path1;
            hdnNodePathTripTime.Value = Path2; 
            rn.InnerHtml = ResHelper.GetString("Custom.RequesterName");
            rnp.Attributes.Add("placeholder", ResHelper.GetString("Custom.RequesterNamePlaceholder"));
            rmn.InnerHtml = ResHelper.GetString("Custom.RequesterMobileNumber");
            ema.InnerHtml = ResHelper.GetString("Custom.EmailAddress");
            em.Attributes.Add("placeholder", ResHelper.GetString("Custom.EmailAddressPlaceholder"));
            sc.InnerHtml = ResHelper.GetString("Custom.SchoolName");
            scp.Attributes.Add("placeholder", ResHelper.GetString("Custom.SchoolNamePlaceholder"));
            stt.InnerHtml = ResHelper.GetString("Custom.SchoolTripType");
            sttp.InnerHtml = ResHelper.GetString("Custom.SchoolTripTypePlaceholder");
            nos.InnerHtml = ResHelper.GetString("Custom.NumberOfStudent");
            nosp.Attributes.Add("placeholder", ResHelper.GetString("Custom.NumberOfStudentPlaceholder"));
            rd.InnerHtml = ResHelper.GetString("Custom.RequestDate");
            eventDatepicker.Attributes.Add("placeholder", ResHelper.GetString("Custom.EventDatePlaceholderValue"));
            rt.InnerHtml = ResHelper.GetString("Custom.RequestTime");
            rtp.InnerHtml = ResHelper.GetString("Custom.RequestTimePlaceholder");
            notes.InnerHtml = ResHelper.GetString("Custom.Notes");
            notesp.Attributes.Add("placeholder", ResHelper.GetString("Custom.NotesPlaceholder"));
            txtcaptcha.Attributes.Add("placeholder", ResHelper.GetString("Custom.CaptchaPlaceholderValue"));
            tymsgBold.InnerHtml = ResHelper.GetString("custom.thanksmsgbold");
            tymsg.InnerHtml = ResHelper.GetString("custom.thanksmsg");
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
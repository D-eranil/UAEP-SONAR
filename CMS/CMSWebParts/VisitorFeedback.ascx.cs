using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.Helpers;
using CMS.PortalEngine.Web.UI;

public partial class CMSWebParts_VisitorFeedback : CMSAbstractWebPart
{
    #region "Properties"
    public string Title
    {
        get
        {
            return ValidationHelper.GetString(GetStringValue("Title", ""), "");
        }
        set
        {
            SetValue("Title", value);
        }
    }
    public string SubTitle
    {
        get
        {
            return ValidationHelper.GetString(GetStringValue("SubTitle", ""), "");
        }
        set
        {
            SetValue("SubTitle", value);
        }
    }
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

            formTitle.InnerHtml = Title;
            hdnNodePathAge.Value = Path;
            formSubTitle.InnerHtml = SubTitle;
            VistorFeedbackCtaTitle.InnerHtml = ResHelper.GetString("Submit");
            ageplaceholder.Attributes.Add("placeholder", ResHelper.GetString("custom.ageplaceholder"));
            nam.InnerHtml = ResHelper.GetString("custom.Name");
            np.Attributes.Add("placeholder", ResHelper.GetString("Custom.NamePlaceholderValue"));
            nationalty.InnerHtml = ResHelper.GetString("custom.Nationality");
            nation.Attributes.Add("placeholder", ResHelper.GetString("Custom.NationalityPlaceholderValue"));
            mn.InnerHtml = ResHelper.GetString("custom.MNumber");
            emai.InnerHtml = ResHelper.GetString("custom.Email");
            em.Attributes.Add("placeholder", ResHelper.GetString("Custom.EmailPlaceholderValue"));
            ag.InnerHtml = ResHelper.GetString("custom.age");
            // cit.Attributes.Add("placeholder", ResHelper.GetString("Custom.CityPlaceholderValue"));
            duration.InnerHtml = ResHelper.GetString("custom.VisitingDuration");
            nl.InnerHtml = ResHelper.GetString("custom.WantsNewsLetter");
            recommend.InnerHtml = ResHelper.GetString("custom.Recomendation");
            msg.InnerHtml = ResHelper.GetString("custom.Comments");
            cmnts.Attributes.Add("placeholder", ResHelper.GetString("Custom.MessagePlaceholderValue"));
            txtcaptcha.Attributes.Add("placeholder", ResHelper.GetString("Custom.CaptchaPlaceholderValue"));
            tymsgBold.InnerHtml = ResHelper.GetString("custom.thanksmsgbold");
            tymsg.InnerHtml = ResHelper.GetString("custom.thanksmsg");
            errormsg.InnerHtml = ResHelper.GetString("custom.errormsg");
            yes.InnerHtml = ResHelper.GetString("custom.yes");
            no.InnerHtml = ResHelper.GetString("custom.no");
            excellent.InnerHtml = ResHelper.GetString("custom.excellent");
            good.InnerHtml = ResHelper.GetString("custom.good");
            fair.InnerHtml = ResHelper.GetString("custom.fair");
            poor.InnerHtml = ResHelper.GetString("custom.poor");
            daily.InnerHtml = ResHelper.GetString("custom.daily");
            weekly.InnerHtml = ResHelper.GetString("custom.weekly");
            biweekly.InnerHtml = ResHelper.GetString("custom.biweekly");
            monthly.InnerHtml = ResHelper.GetString("custom.monthly");
            oe.InnerHtml = ResHelper.GetString("custom.overallexperience");
            cle.InnerHtml = ResHelper.GetString("custom.cleanliness");    
            vfm.InnerHtml = ResHelper.GetString("custom.valueformoney");
            spaa.InnerHtml = ResHelper.GetString("custom.staffprofessionalism");
            de.InnerHtml = ResHelper.GetString("custom.diningexperience");
            pf.InnerHtml = ResHelper.GetString("custom.parkfacilities");
            oh.InnerHtml = ResHelper.GetString("custom.operatinghours");
            eaa.InnerHtml = ResHelper.GetString("custom.eventsandactivities");
            ryes.InnerHtml = ResHelper.GetString("custom.yes");
            rno.InnerHtml = ResHelper.GetString("custom.no");
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
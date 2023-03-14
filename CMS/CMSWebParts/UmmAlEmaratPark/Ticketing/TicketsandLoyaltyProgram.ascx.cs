using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.Helpers;
using CMS.PortalEngine.Web.UI;

public partial class CMSWebParts_UmmAlEmaratPark_Ticketing_TicketsandLoyaltyProgram : CMSAbstractWebPart
{
    #region "Properties"

    public string FirstTabTitle
    {
        get
        {
            return ValidationHelper.GetString(GetStringValue("FirstTabTitle", ""), "");
        }
        set
        {
            SetValue("FirstTabTitle", value);
        }
    }
    public string FirstTabSubTitle
    {
        get
        {
            return ValidationHelper.GetString(GetStringValue("FirstTabSubTitle", ""), "");
        }
        set
        {
            SetValue("FirstTabSubTitle", value);
        }
    }
    public string FirstTabSummary
    {
        get
        {
            return ValidationHelper.GetString(GetStringValue("FirstTabSummary", ""), "");
        }
        set
        {
            SetValue("FirstTabSummary", value);
        }
    }
    public string FirstTabImage
    {
        get
        {
            return ValidationHelper.GetString(GetStringValue("FirstTabImage", ""), "");
        }
        set
        {
            SetValue("FirstTabImage", value);
        }
    }
    public string FirstTabIconOne
    {
        get
        {
            return ValidationHelper.GetString(GetStringValue("FirstTabIconOne", ""), "");
        }
        set
        {
            SetValue("FirstTabIconOne", value);
        }
    }
    public string FirstTabIconOneTitle
    {
        get
        {
            return ValidationHelper.GetString(GetStringValue("FirstTabIconOneTitle", ""), "");
        }
        set
        {
            SetValue("FirstTabIconOneTitle", value);
        }
    }
    public string FirstTabIconTwo
    {
        get
        {
            return ValidationHelper.GetString(GetStringValue("FirstTabIconTwo", ""), "");
        }
        set
        {
            SetValue("FirstTabIconTwo", value);
        }
    }
    public string FirstTabIconTwoTitle
    {
        get
        {
            return ValidationHelper.GetString(GetStringValue("FirstTabIconTwoTitle", ""), "");
        }
        set
        {
            SetValue("FirstTabIconTwoTitle", value);
        }
    }
    public string FirstTabIconThree
    {
        get
        {
            return ValidationHelper.GetString(GetStringValue("FirstTabIconThree", ""), "");
        }
        set
        {
            SetValue("FirstTabIconThree", value);
        }
    }
    public string FirstTabIconThreeTitle
    {
        get
        {
            return ValidationHelper.GetString(GetStringValue("FirstTabIconThreeTitle", ""), "");
        }
        set
        {
            SetValue("FirstTabIconThreeTitle", value);
        }
    }
    public string SecondTabTitle
    {
        get
        {
            return ValidationHelper.GetString(GetStringValue("SecondTabTitle", ""), "");
        }
        set
        {
            SetValue("SecondTabTitle", value);
        }
    }
    public string SecondTabSubTitle
    {
        get
        {
            return ValidationHelper.GetString(GetStringValue("SecondTabSubTitle", ""), "");
        }
        set
        {
            SetValue("SecondTabSubTitle", value);
        }
    }
    public string SecondTabSummary
    {
        get
        {
            return ValidationHelper.GetString(GetStringValue("SecondTabSummary", ""), "");
        }
        set
        {
            SetValue("SecondTabSummary", value);
        }
    }
    public string SecondTabImage
    {
        get
        {
            return ValidationHelper.GetString(GetStringValue("SecondTabImage", ""), "");
        }
        set
        {
            SetValue("SecondTabImage", value);
        }
    }
    public string SecondTabIconOne
    {
        get
        {
            return ValidationHelper.GetString(GetStringValue("SecondTabIconOne", ""), "");
        }
        set
        {
            SetValue("SecondTabIconOne", value);
        }
    }
    public string SecondTabIconOneTitle
    {
        get
        {
            return ValidationHelper.GetString(GetStringValue("SecondTabIconOneTitle", ""), "");
        }
        set
        {
            SetValue("SecondTabIconOneTitle", value);
        }
    }
    public string SecondTabIconTwo
    {
        get
        {
            return ValidationHelper.GetString(GetStringValue("SecondTabIconTwo", ""), "");
        }
        set
        {
            SetValue("SecondTabIconTwo", value);
        }
    }
    public string SecondTabIconTwoTitle
    {
        get
        {
            return ValidationHelper.GetString(GetStringValue("SecondTabIconTwoTitle", ""), "");
        }
        set
        {
            SetValue("SecondTabIconTwoTitle", value);
        }
    }
    public string SecondTabIconThree
    {
        get
        {
            return ValidationHelper.GetString(GetStringValue("SecondTabIconThree", ""), "");
        }
        set
        {
            SetValue("SecondTabIconThree", value);
        }
    }
    public string SecondTabIconThreeTitle
    {
        get
        {
            return ValidationHelper.GetString(GetStringValue("SecondTabIconThreeTitle", ""), "");
        }
        set
        {
            SetValue("SecondTabIconThreeTitle", value);
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
            firstTabTitle.InnerHtml = FirstTabTitle;
            secondTabTitle.InnerHtml = SecondTabTitle;
            firstTabSubTitle.InnerHtml = FirstTabSubTitle;
            firstTabSummary.InnerHtml = FirstTabSummary;
            firstTabImage.Attributes.Add("src", FirstTabImage);
            secondTabSubTitle.InnerHtml = SecondTabSubTitle;
            secondTabSummary.InnerHtml = SecondTabSummary;
            secondTabImage.Attributes.Add("src", SecondTabImage);
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
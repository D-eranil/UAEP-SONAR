using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.Helpers;
using CMS.PortalEngine.Web.UI;

public partial class CMSWebParts_NewsLetterUaep : CMSAbstractWebPart
{

    #region "Properties"
    protected string test;
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

    public string CtaTitle
    {
        get
        {
            return ValidationHelper.GetString(GetStringValue("CtaTitle", ""), "");
        }
        set
        {
            SetValue("CtaTitle", value);
        }
    }
    //protected string tests
    //{
    //    get
    //    {
    //        return ResHelper.GetString("custom.NewsletterPlaceholder");
    //        //return ValidationHelper.GetString(GetStringValue("CtaTitle", ""), "");
    //    }
    //    set
    //    {
    //        SetValue("CtaTitle", value);
    //    }
    //}


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
            
            test = ResHelper.GetString("custom.NewsletterPlaceholder");
            NewsletterTitle.InnerHtml = Title;
            NewsletterCtaTitle.InnerHtml = ResHelper.GetString("custom.NewsletterButton");
            alreadyregistermessage.InnerHtml = ResHelper.GetString("custom.alreadyregistermessage");
            newsletterthnksmsg.InnerHtml = ResHelper.GetString("custom.newsletterthnksmsg");
            inp.Attributes.Add("placeholder", test);
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
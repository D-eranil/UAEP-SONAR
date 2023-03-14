using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.Helpers;
using CMS.PortalEngine.Web.UI;

public partial class CMSWebParts_ContactUsUaep : CMSAbstractWebPart
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
    public string QueryOptions
    {
        get
        {
            return ValidationHelper.GetString(GetStringValue("QueryOptions", ""), "");
        }
        set
        {
            SetValue("QueryOptions", value);
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

    protected string[] queryOptions { get; set; }
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
            hdnNodePath.Value = Path;
            questionplaceholder.InnerHtml= ResHelper.GetString("custom.questionplaceholder");
            ContactUsCtaTitle.InnerHtml = ResHelper.GetString("Submit");//CtaTitle;
            fn.InnerHtml = ResHelper.GetString("custom.FName");
            fnp.Attributes.Add("placeholder", ResHelper.GetString("Custom.FirstNamePlaceholderValue"));
            ln.InnerHtml = ResHelper.GetString("custom.LName");
            lnp.Attributes.Add("placeholder", ResHelper.GetString("Custom.LastNamePlaceholderValue"));
            mn.InnerHtml = ResHelper.GetString("custom.MNumber");
            emai.InnerHtml = ResHelper.GetString("custom.Email");
            em.Attributes.Add("placeholder", ResHelper.GetString("Custom.EmailPlaceholderValue"));
            cty.InnerHtml = ResHelper.GetString("custom.city");
            cit.Attributes.Add("placeholder", ResHelper.GetString("Custom.CityPlaceholderValue"));
            qr.InnerHtml = ResHelper.GetString("custom.questions");
            msg.InnerHtml = ResHelper.GetString("custom.msg");
            mssg.Attributes.Add("placeholder", ResHelper.GetString("Custom.MessagePlaceholderValue"));
            txtcaptcha.Attributes.Add("placeholder", ResHelper.GetString("Custom.CaptchaPlaceholderValue"));
            tymsgBold.InnerHtml = ResHelper.GetString("custom.thanksmsgbold");
            tymsg.InnerHtml = ResHelper.GetString("custom.thanksmsg");
            errormsg.InnerHtml = ResHelper.GetString("custom.errormsg");
            invalidcapthchamsg.InnerHtml = ResHelper.GetString("custom.invalidcapthchamsg");

            //firstName.InnerHtml = CMS.Helpers.ResHelper.GetString("Custom.FName"); 
            //query.InnerText = Server.HtmlEncode(QueryOptions);

            //DataSet ds = new DataSet();
            //DataTable dt = new DataTable();
            //DataRow dr;

            //DataColumn queryText = new DataColumn("QueryText", Type.GetType("System.String"));
            //DataColumn queryValue = new DataColumn("QueryValue", Type.GetType("System.String"));

            //dt.Columns.Add(queryText);
            //dt.Columns.Add(queryValue);


            // queryOptions = QueryOptions.Split(','); //


            //for (int i = 0; i < queries.Length; i++)
            //{
            //    dr = dt.NewRow();
            //    dr["QueryText"] = queries[i];                
            //    dr["QueryValue"] = i == 0 ? " " : queries[i];
            //    dt.Rows.Add(dr);
            //}

            //ds.Tables.Add(dt);
            //query.DataSource = ds;
            //query.DataTextField = "QueryText";
            //query.DataValueField = "QueryValue";
            //query.DataBind();
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
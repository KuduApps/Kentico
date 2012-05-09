using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using CMS.PortalControls;
using CMS.GlobalHelper;
using CMS.ISearchEngine;

public partial class CMSWebParts_SmartSearch_SearchAccelerator : CMSAbstractWebPart
{
    #region "Variables"

    // Result page url
    protected string mResultPageUrl = URLHelper.CurrentURL;

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets or sets the accelerator description.
    /// </summary>
    public string AcceleratorDescription
    {
        get
        {
            return DataHelper.GetNotEmpty(GetValue("AcceleratorDescription"), GetString("srch.accelerator.description"));
        }
        set
        {
            SetValue("AcceleratorDescription", value);
        }
    }


    /// <summary>
    /// Gets or sets the accelerator name.
    /// </summary>
    public string AcceleratorName
    {
        get
        {
            return DataHelper.GetNotEmpty(GetValue("AcceleratorName"), GetString("srch.accelerator.name"));
        }
        set
        {
            SetValue("AcceleratorName", value);
        }
    }


    /// <summary>
    /// Gets or sets the accelerator button text.
    /// </summary>
    public string AcceleratorButtonText
    {
        get
        {
            return DataHelper.GetNotEmpty(GetValue("AcceleratorButtonText"), GetString("srch.accelerator.addaccelerator"));
        }
        set
        {
            SetValue("AcceleratorButtonText", value);
        }
    }


    /// <summary>
    /// Gets or sets the search results page URL.
    /// </summary>
    public string SearchResultsPageUrl
    {
        get
        {
            return DataHelper.GetNotEmpty(GetValue("SearchResultsPageUrl"), mResultPageUrl);
        }
        set
        {
            SetValue("SearchResultsPageUrl", value);
            mResultPageUrl = value;
        }
    }


    /// <summary>
    ///  Gets or sets the Search mode.
    /// </summary>
    public SearchModeEnum SearchMode
    {
        get
        {
            return SearchHelper.GetSearchModeEnum(ValidationHelper.GetString(GetValue("SearchMode"), ""));
        }
        set
        {
            SetValue("SearchMode", value.ToString());
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

        // If the accelerator request matches the ID of the control, 
        if (QueryHelper.GetString("getsearchaccelerator", "") == this.ClientID)
        {
            GetServiceDefinition();
        }

        SetupControl();
    }


    /// <summary>
    /// Initializes the control properties.
    /// </summary>
    protected void SetupControl()
    {
        if (this.StopProcessing)
        {
            return;
        }
        else
        {
            this.btnAcc.Text = this.AcceleratorButtonText;

            string serviceUrl = URLHelper.GetAbsoluteUrl(URLHelper.AddParameterToUrl(URLHelper.CurrentURL, "getsearchaccelerator", this.ClientID));

            this.btnAcc.OnClientClick = "window.external.AddService('" + serviceUrl + "'); return false;";
        }
    }


    /// <summary>
    /// Gets the service definition for the accelerator.
    /// </summary>
    public void GetServiceDefinition()
    {
        // Create a new XML response
        Response.Clear();
        Response.ContentType = "text/xml";

        string appUrl = URLHelper.GetApplicationUrl();

        // Write acelerator definition
        Response.Write(
            @"<?xml version=""1.0"" encoding=""UTF-8"" ?>
                <os:openServiceDescription xmlns:os=""http://www.microsoft.com/schemas/openservicedescription/1.0""> 
                    <os:homepageUrl>" + appUrl + @"</os:homepageUrl> 
                    <os:display> 
                        <os:name>" + this.AcceleratorName + @"</os:name> 
                        <os:icon>" + appUrl + @"/favicon.ico</os:icon> 
                        <os:description>" + this.AcceleratorDescription + @"</os:description> 
                    </os:display> 
                    <os:activity category=""Search""> 
                        <os:activityAction context=""selection""> 
                            <os:execute action=""" + URLHelper.GetAbsoluteUrl(this.SearchResultsPageUrl) + @"""> 
                                <os:parameter name=""searchtext"" value=""{selection}"" type=""text"" /> 
                                <os:parameter name=""searchmode"" value=""" + SearchHelper.GetSearchModeString(this.SearchMode) + @""" /> 
                            </os:execute> 
                        </os:activityAction> 
                    </os:activity> 
                </os:openServiceDescription>");

        RequestHelper.EndResponse();
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

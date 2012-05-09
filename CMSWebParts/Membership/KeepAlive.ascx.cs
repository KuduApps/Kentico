using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using CMS.PortalControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;

public partial class CMSWebParts_Membership_KeepAlive : CMSAbstractWebPart, ICallbackEventHandler
{
    private int mRefreshingInterval = 5;
    private string result = "";


    /// <summary>
    /// Interval between calls made to keep session alive.
    /// </summary>
    public int RefreshingInterval 
    {
        get 
        {
            this.mRefreshingInterval = ValidationHelper.GetInteger(this.GetValue("RefreshingInterval"), 5);
            return this.mRefreshingInterval;
        }
        set 
        {
            this.mRefreshingInterval = value;
            this.SetValue("RefreshingInterval", value);
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.StopProcessing && CMSContext.CurrentUser.IsAuthenticated())
        {
            // Get call back reference
            string callBackRef = this.Page.ClientScript.GetCallbackEventReference(this, null, "ProcessResult", null);

            // Get interval in miliseconds
            int refreshInt = this.RefreshingInterval * 1000;

            // Register executive JavaScript code
            this.ltlScript.Text = ScriptHelper.GetScript("function KeepSession(){" + callBackRef + " } function ProcessResult(result, context){ Timer('" + refreshInt.ToString() + "') } " +
                " function Timer(refreshingInterval){ setTimeout(\"KeepSession()\", refreshingInterval); } Timer('" + refreshInt.ToString() + "');");
        }
    }


    #region "ICallbackEventHandler Members"

    /// <summary>
    /// Gets modified result.
    /// </summary>
    public string GetCallbackResult()
    {
        return result;
    }


    /// <summary>
    /// Gets callback event result.
    /// </summary>
    public void RaiseCallbackEvent(string eventArgument)
    {
        result = (!this.Page.Session.IsNewSession) ? "KEEPING_ALIVE" : "NEW_CREATED";        
    }

    #endregion
}

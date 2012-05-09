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

using CMS.GlobalHelper;
using CMS.PortalEngine;
using CMS.PortalControls;
using CMS.UIControls;

public partial class CMSModules_PortalEngine_UI_WebParts_WebPartProperties_buttons : CMSWebPartPropertiesPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // set button text
        btnOk.Text = GetString("general.ok");
        btnApply.Text = GetString("general.apply");
        btnCancel.Text = GetString("general.cancel");

        chkRefresh.Text = GetString("WebpartProperties.Refresh");
        
        this.ltlScript.Text += ScriptHelper.GetScript("function GetRefreshStatus() { return document.getElementById('" + this.chkRefresh.ClientID + "').checked; }");

        this.btnCancel.OnClientClick = "Close(); return false;";
        this.btnApply.OnClientClick = "Apply(); return false;";
        this.btnOk.OnClientClick = "Save(); return false;";
        
        string action = ValidationHelper.GetString(Request.QueryString["tab"], "properties");
        
        switch (action)
        {
            case "properties":
                break;

            case "code":
                break;

            case "binding":
                chkRefresh.Visible = false;
                btnApply.Visible = false;
                btnOk.Visible = false;
                btnCancel.Text = GetString("WebpartProperties.Close");
                break;

            default:
               break;
        }
    }
}

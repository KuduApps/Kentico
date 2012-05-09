using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using CMS.GlobalHelper;
using CMS.PortalEngine;
using CMS.PortalControls;
using CMS.UIControls;

public partial class CMSModules_Widgets_Dialogs_WidgetProperties_Buttons : CMSDeskPage
{
    /// <summary>
    /// OnInit override - do not require site.
    /// </summary>
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        RequireSite = false;
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        SetBrowserClass();
        bool isInline = QueryHelper.GetBoolean("inline", false);
        // set button text
        btnOk.Text = GetString("general.ok");
        btnApply.Text = GetString("general.apply");
        btnCancel.Text = GetString("general.cancel");

        chkRefresh.Text = GetString("Widget.Properties.Refresh");

        if (isInline)
        {
            btnApply.Visible = false;
            chkRefresh.Visible = false;
        }

        this.ltlScript.Text += ScriptHelper.GetScript("function GetRefreshStatus() { var refresh= document.getElementById('" + this.chkRefresh.ClientID + @"');
        if (refresh != null) {
            return refresh.checked;
        }
        return false;         
        }");

        this.btnCancel.OnClientClick = "Close(); return false;";
        this.btnApply.OnClientClick = "Apply(); return false;";
        this.btnOk.OnClientClick = "Save(); return false;";
    }
}

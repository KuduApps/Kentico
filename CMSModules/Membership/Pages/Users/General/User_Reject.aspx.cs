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
using CMS.UIControls;
using CMS.CMSHelper;

public partial class CMSModules_Membership_Pages_Users_General_User_Reject : CMSUsersPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Pagetitle
        this.Title = GetString("administration.users.rejectusers");

        // Set the master page header
        this.CurrentMaster.Title.TitleText = GetString("administration.users.rejectusers");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_User/reject.png");
        this.CurrentMaster.Title.HelpTopicName = "User_Reject";

        // Initialize other properties        
        this.txtReason.Text = GetString("administration.user.reasondefault");        
        this.btnCancel.Attributes.Add("onclick", "window.close(); return false;");

        // Register scripts
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "CloseAndRefresh",
            ScriptHelper.GetScript(
                "function CloseAndRefresh()\n" +
                "{\n" +
                    "var txtReason = document.getElementById('" + this.txtReason.ClientID + "').value;\n" +
                    "var chkSendEmail = document.getElementById('" + this.chkSendEmail.ClientID + "').checked;\n" +
                    "wopener.SetRejectParam(txtReason, chkSendEmail, 'true');\n" +
                    "window.close();\n" +
                "}\n"));

        this.txtReason.Focus();

        this.btnReject.OnClientClick = "CloseAndRefresh(); return false;";

        // Register modal page scripts
        RegisterEscScript();
        RegisterModalPageScripts();
    }
}

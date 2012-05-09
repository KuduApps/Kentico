using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.CMSHelper;
using CMS.GlobalHelper;

public partial class CMSAPIExamples_Pages_Header : CMSAPIExamplePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //titleElem.TitleText = "CMS API examples";
        //titleElem.TitleImage = GetImageUrl("General/Code.png");

        lblVersion.Text = "v" + CMSContext.FullSystemSuffixVersion;

        lblUser.Text = GetString("Header.User");
        lblUserInfo.Text = HTMLHelper.HTMLEncode(CMSContext.CurrentUser.FullName);

        lnkCmsDesk.Text = GetString("header.opencmsdesk");
        lnkCmsDesk.NavigateUrl = "~/CMSDesk/default.aspx";
        lnkCmsDesk.Target = "_blank";

        lnkSiteManager.Text = GetString("header.opensitemanager");
        lnkSiteManager.NavigateUrl = "~/CMSSiteManager/default.aspx";
        lnkSiteManager.Target = "_blank";

        lnkApiExampleLogo.NavigateUrl = "~/CMSAPIExamples/default.aspx";
        lnkApiExampleLogo.Target = "_parent";

        if (RequestHelper.IsWindowsAuthentication())
        {
            pnlSignOut.Visible = false;
            PanelRight.CssClass += " HeaderWithoutSignOut";
        }
        else
        {
            pnlSignOut.BackImageUrl = GetImageUrl("Design/Buttons/SignOutButton.png");
            lblSignOut.Text = GetString("signoutbutton.signout");
        }
    }


    protected void btnSignOut_Click(object sender, EventArgs e)
    {
        SignOut();
        ltlScript.Text += ScriptHelper.GetScript("parent.location.replace('" + URLHelper.ApplicationPath.TrimEnd('/') + "/default.aspx');");
    }
}

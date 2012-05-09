using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.OnlineMarketing;

public partial class CMSModules_ContactManagement_Pages_Tools_Account_Contact_Detail : CMSContactManagementContactsPage
{
    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        RegisterEscScript();

        frameContent.Attributes.Add("src", "../Contact/Frameset.aspx" + URLHelper.Url.Query + "&dialogmode=1");
        frameFooter.Attributes.Add("src", "../DetailFooter.aspx" + URLHelper.Url.Query);
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        RegisterModalPageScripts();
    }

    #endregion
}
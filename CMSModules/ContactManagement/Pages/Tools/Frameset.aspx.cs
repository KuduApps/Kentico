using System;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.OnlineMarketing;

public partial class CMSModules_ContactManagement_Pages_Tools_Frameset : CMSContactManagementPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (ContactHelper.IsSiteManager)
        {
            contactManagementMenu.Attributes["src"] = "Header.aspx?issitemanager=1";
            contactManagementContent.Attributes["src"] = "Activities/Frameset.aspx?siteId=-1&issitemanager=1";
            rowsFrameset.Attributes["rows"] = "98, *";
        }
        else
        {
            contactManagementMenu.Attributes["src"] = "Header.aspx";
            contactManagementContent.Attributes["src"] = "Contact/List.aspx";
            rowsFrameset.Attributes["rows"] = "98, *";
        }
    }
}
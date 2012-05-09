using System;
using System.Data;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;

public partial class CMSModules_UIPersonalization_Pages_Administration_UI_Dialogs : CMSUIPersonalizationPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.editElem.SiteID = SiteID;
        this.editElem.IsLiveSite = false;
        this.editElem.HideSiteSelector = (SiteID != 0);
    }
}

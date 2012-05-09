using System;
using System.Data;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.SiteProvider;
using CMS.GlobalHelper;
using CMS.CMSHelper;

public partial class CMSModules_UIPersonalization_Pages_Administration_UI_Editor : CMSUIPersonalizationPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.editElem.SiteID = SiteID;
        this.editElem.HideSiteSelector = (SiteID != 0);
        ResourceInfo ri = ResourceInfoProvider.GetResourceInfo("CMS.WYSIWYGEditor");
        if (ri != null)
        {
            this.editElem.ResourceID = ri.ResourceId;
            this.editElem.IsLiveSite = false;
        }        
    }
}

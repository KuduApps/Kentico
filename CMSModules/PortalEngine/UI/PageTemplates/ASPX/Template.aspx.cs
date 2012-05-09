using System;
using System.Web;

using CMS.UIControls;

public partial class CMSModules_PortalEngine_UI_PageTemplates_ASPX_Template : TemplatePage
{
    protected override void CreateChildControls()
    {
        base.CreateChildControls();

        this.PageManager = this.CMSPageManager1;
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        this.ltlTags.Text = this.HeaderTags;
    }
}

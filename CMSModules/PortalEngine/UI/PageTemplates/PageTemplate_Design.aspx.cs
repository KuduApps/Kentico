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

using CMS.CMSHelper;
using CMS.SettingsProvider;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.PortalEngine;
using CMS.PortalControls;

public partial class CMSModules_PortalEngine_UI_PageTemplates_PageTemplate_Design : PortalPage
{
    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);

        // Init the page components
        this.PageManager = this.manPortal;
        this.manPortal.SetMainPagePlaceholder(this.plc);

        int pageTemplateId = QueryHelper.GetInteger("templateid", 0);
        
        // Prepare virtual page info
        PageInfo pi = new PageInfo();
        pi.DocumentCulture = CMSContext.PreferredCultureCode;
        pi.DocumentPageTemplateID = pageTemplateId;
        pi.ClassName = "CMS.Root";
        pi.NodeAliasPath = "";
        pi.DocumentNamePath = "/" + ResHelper.GetString("edittabs.design");
        pi.NodeSiteId = CMSContext.CurrentSiteID;

        CMSContext.CurrentPageInfo = pi;

        // Set the design mode
        PortalContext.SetRequestViewMode(ViewModeEnum.Design);
        ContextHelper.Add("DisplayContentInDesignMode", "0", true, false, false, DateTime.MinValue);

        this.ManagersContainer = this.plcManagers;
        this.ScriptManagerControl = this.manScript;
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        // Init the header tags
        this.ltlTags.Text = this.HeaderTags;
    }
}

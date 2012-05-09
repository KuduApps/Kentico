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

using CMS.UIControls;
using CMS.GlobalHelper;

public partial class CMSModules_PortalEngine_UI_WebContainers_Container_Edit_Frameset : CMSModalDesignPage
{
    protected int mHeight = 102;

    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
        RequireSite = false;             
    }

    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (QueryHelper.GetBoolean("editonlycode", false))
        {
            mHeight = 72;
        }
        else
        {
            // Page opened from Site Manager
            CheckAccessToSiteManager();
        }
    }
}

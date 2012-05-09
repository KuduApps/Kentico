using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;

public partial class CMSModules_DocumentTypes_Pages_Development_HierarchicalTransformations_Frameset : CMSModalDesignPage
{
    protected int mHeight = 58;

    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
        RequireSite = false;

        if (QueryHelper.GetBoolean("editonlycode", false))
        {
            mHeight = 67;
        }
        else
        {
            // Page opened from Site Manager
            CheckAccessToSiteManager();
        }
    }
}


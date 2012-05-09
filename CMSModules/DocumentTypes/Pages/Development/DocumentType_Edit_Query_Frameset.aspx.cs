using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;

public partial class CMSModules_DocumentTypes_Pages_Development_DocumentType_Edit_Query_Frameset : CMSModalDesignPage
{
    protected int mHeight = 65;

    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
        RequireSite = false;

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

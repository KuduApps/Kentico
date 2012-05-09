using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;

public partial class CMSDesk_Tools_Default : CMSToolsPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptHelper.RegisterTitleScript(this, GetString("cmsdesk.ui.tools"));
    }
}


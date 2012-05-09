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
using CMS.Controls;

public partial class CMSMasterPages_UI_TabsHeaderVertical : CMSMasterPage
{
    /// <summary>
    /// Tabs control.
    /// </summary>
    public override UITabs Tabs
    {
        get
        {
            return this.tabControlElem;
        }
    }


    /// <summary>
    /// Body panel.
    /// </summary>
    public override Panel PanelBody
    {
        get
        {
            return this.pnlBody;
        }
    }


    /// <summary>
    /// Tab master page doesn't hide page title.
    /// </summary>
    public override bool TabMode
    {
        get
        {
            return false;
        }
    }
}

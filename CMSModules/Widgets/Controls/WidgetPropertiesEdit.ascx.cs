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

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.PortalEngine;

public partial class CMSModules_Widgets_Controls_WidgetPropertiesEdit : CMSAdminEditControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        widgetFieldEditor.WidgetID = this.ItemID;
    }
}

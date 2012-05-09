using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using CMS.ExtendedControls;
using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSModules_Widgets_LiveDialogs_WidgetProperties_Properties_Frameset : LivePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.frameContent.Attributes.Add("src", "widgetproperties_properties.aspx" + URLHelper.Url.Query);
        this.frameButtons.Attributes.Add("src", "widgetproperties_buttons.aspx" + URLHelper.Url.Query);        
    }
}

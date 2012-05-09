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

public partial class CMSModules_OnlineMarketing_Pages_Widgets_WidgetProperties_Variant_Frameset : CMSDeskPage
{
    /// <summary>
    /// OnInit override - do not require site.
    /// </summary>
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        RequireSite = false;
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        this.frameContent.Attributes.Add("src", "widgetproperties_variant.aspx" + URLHelper.Url.Query);
        this.frameButtons.Attributes.Add("src", ResolveUrl("~/CMSModules/Widgets/Dialogs/widgetproperties_buttons.aspx" + URLHelper.Url.Query));        
    }
}

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

using CMS.SiteProvider;
using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSModules_Widgets_UI_Widget_Header : SiteManagerPage
{
    #region "Page events"

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        this.CurrentMaster.Title.TitleText = GetString("widgets.title");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_Widget/object.png");
        this.CurrentMaster.Title.HelpTopicName = "widgets_main";
        this.CurrentMaster.Title.HelpName = "helpTopic";
    }

    #endregion
}

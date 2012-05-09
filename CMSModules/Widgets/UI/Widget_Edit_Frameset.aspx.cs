using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;

public partial class CMSModules_Widgets_UI_Widget_Edit_Frameset : SiteManagerPage
{
    #region "Variables"

    protected string widgetheaderUrl = URLHelper.ResolveUrl("~/CMSModules/Widgets/UI/Widget_Edit_Header.aspx" + URLHelper.Url.Query);
    protected string widgeteditcontentUrl = URLHelper.ResolveUrl("~/CMSModules/Widgets/UI/Widget_Edit_General.aspx" + URLHelper.Url.Query);

    #endregion


    #region "Page events"

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    #endregion
}

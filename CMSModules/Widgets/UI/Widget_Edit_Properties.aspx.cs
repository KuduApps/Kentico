using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;

public partial class CMSModules_Widgets_UI_Widget_Edit_Properties : SiteManagerPage
{
    #region "Page events"

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        widgetProperties.ItemID = QueryHelper.GetInteger("widgetid", 0);
        CurrentMaster.Title.Visible = false;
        CurrentMaster.BodyClass += " FieldEditorBody";
    }

    #endregion
}

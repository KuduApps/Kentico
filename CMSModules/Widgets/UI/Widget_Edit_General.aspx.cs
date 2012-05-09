using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;

public partial class CMSModules_Widgets_UI_Widget_Edit_General : SiteManagerPage
{
    #region "Page events"

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        widgetGeneral.ItemID = QueryHelper.GetInteger("widgetid", 0);
        widgetGeneral.OnSaved += new EventHandler(widgetGeneral_OnSaved);
    }


    /// <summary>
    /// Handles the OnSaved event of the widgetGeneral control.
    /// </summary>
    protected void widgetGeneral_OnSaved(object sender, EventArgs e)
    {
        if (widgetGeneral.WidgetInfo != null)
        {
            // Update tree
            string script = "parent.parent.frames['widgettree'].location = '" + URLHelper.ResolveUrl("~/CMSModules/Widgets/UI/WidgetTree.aspx?widgetid=" + widgetGeneral.WidgetInfo.WidgetID) + "';";

            // Update header
            script += "parent.frames['widgetheader'].location.replace(parent.frames['widgetheader'].location);";

            ScriptHelper.RegisterStartupScript(this.Page, typeof(string), "reloadwidgettree", ScriptHelper.GetScript(script));
        }
    }

    #endregion
}

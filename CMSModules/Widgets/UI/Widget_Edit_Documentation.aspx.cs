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
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.PortalEngine;
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSModules_Widgets_UI_Widget_Edit_Documentation : SiteManagerPage
{
    #region "Variables"

    private int widgetId = 0;

    #endregion


    #region "Page events"

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        widgetId = QueryHelper.GetInteger("widgetid", 0);

        this.Title = "Widget part documentation";

        // Resource string
        btnOk.Text = GetString("General.Ok");
        string[,] actions = new string[2, 12];

        WidgetInfo wi = WidgetInfoProvider.GetWidgetInfo(widgetId);

        // set Documentation header - "View documentation" + "Generate Documentation"
        if (wi != null)
        {
            // Generate doucmentation action                
            actions[0, 0] = "HyperLink";
            actions[0, 1] = GetString("webparteditdocumentation.view");
            actions[0, 3] = "~/CMSModules/Widgets/Dialogs/WidgetDocumentation.aspx?widgetid=" + wi.WidgetName;
            actions[0, 5] = GetImageUrl("CMSModules/CMS_WebParts/viewdocumentation.png");
            actions[0, 11] = "_blank";

            if (SettingsKeyProvider.DevelopmentMode)
            {
                // Generate doucmentation action                
                actions[1, 0] = "HyperLink";
                actions[1, 1] = GetString("webparteditdocumentation.generate");
                actions[1, 3] = "~/CMSPages/Dialogs/Documentation.aspx?widget=" + wi.WidgetName;
                actions[1, 5] = GetImageUrl("CMSModules/CMS_WebParts/generatedocumentation.png");
                actions[1, 11] = "_blank";
            }
        }

        this.CurrentMaster.HeaderActions.Actions = actions;

        // HTML editor settings
        htmlText.AutoDetectLanguage = false;
        htmlText.DefaultLanguage = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        htmlText.EditorAreaCSS = "";
        htmlText.ToolbarSet = "SimpleEdit";

        // Load data
        if (!RequestHelper.IsPostBack())
        {
            if (wi != null)
            {
                htmlText.ResolvedValue = wi.WidgetDocumentation;
            }
        }
    }


    /// <summary>
    /// OK click handler, save changes.
    /// </summary>
    protected void btnOk_Click(object sender, EventArgs e)
    {
        WidgetInfo wi = WidgetInfoProvider.GetWidgetInfo(widgetId);
        if (wi != null)
        {
            wi.WidgetDocumentation = htmlText.ResolvedValue;
            WidgetInfoProvider.SetWidgetInfo(wi);

            lblInfo.Visible = true;
            lblInfo.Text = GetString("General.ChangesSaved");
        }
    }

    #endregion
}

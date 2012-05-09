using System;

using CMS.SiteProvider;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.IO;
using CMS.CMSHelper;
using CMS.SettingsProvider;

public partial class CMSModules_CssStylesheets_Pages_CssStylesheet_Header : CMSModalPage
{
    #region "Variables"

    protected int cssstylesheetid = 0;
    private CssStylesheetInfo mStylesheet = null;
    private bool mDialogMode = false;

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets edited stylesheet object.
    /// </summary>
    private CssStylesheetInfo Stylesheet
    {
        get
        {
            if (mStylesheet == null)
            {
                string cssstylesheet = QueryHelper.GetString("cssstylesheetid", String.Empty);
                cssstylesheetid = ValidationHelper.GetInteger(cssstylesheet, 0);

                mStylesheet = CssStylesheetInfoProvider.GetCssStylesheetInfo(cssstylesheetid);
                if ((mStylesheet == null) && (cssstylesheet == "default"))
                {
                    mStylesheet = CMSContext.CurrentSiteStylesheet;
                }
            }
            return mStylesheet;
        }
    }

    #endregion


    #region "Page methods"

    protected override void OnPreInit(EventArgs e)
    {
        RequireSite = false;

        // Page has been opened in CMSDesk and only stylesheet style editing is allowed
        mDialogMode = QueryHelper.GetBoolean("editonlycode", false);

        if (mDialogMode)
        {
            MasterPageFile = "~/CMSMasterPages/UI/Dialogs/TabsHeader.master";
        }
        else
        {
            // Page opened from Site Manager
            CheckAccessToSiteManager();
        }

        base.OnPreInit(e);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        SetEditedObject(Stylesheet, null);

        if (mDialogMode)
        {
            SetTitle("Objects/CMS_CSSStylesheet/object.png", GetString("CssStylesheet.EditCssStylesheet"), "general_tab13", "helpTopic");
        }
        else
        {
            SetTitle("Objects/CMS_CSSStylesheet/object.png", GetString("CssStylesheet.CssStylesheetProperties"), "general_tab13", "helpTopic");

            // Initializes page title
            InitBreadcrumbs(2);
            SetBreadcrumb(0, GetString("CssStylesheet.CssStylesheets"), "~/CMSModules/CssStylesheets/Pages/CssStylesheet_List.aspx", "_parent", null);
            SetBreadcrumb(1, (Stylesheet != null) ? Stylesheet.StylesheetDisplayName : String.Empty, null, null, null);
        }

        InitializeMenu();
    }


    /// <summary>
    /// Initializes edit menu.
    /// </summary>
    protected void InitializeMenu()
    {
        int i = 0;
        InitTabs(3, "Content");

        // General tab
        SetTab(i, GetString("General.General"), URLHelper.RemoveParameterFromUrl("CssStylesheet_General.aspx" + URLHelper.Url.Query, "saved"), "SetHelpTopic('helpTopic', 'general_tab13');");
        if (!mDialogMode)
        {
            i++;

            if (Stylesheet != null)
            {
                //// Display theme tab if theme folder available
                //string path = Server.MapPath("~/App_Themes/" + csi.StylesheetName);
                //if (Directory.Exists(path))
                //{
                
                // Don't display theme tab for external storage
                if (!StorageHelper.IsExternalStorage)
                {
                    // Theme tab
                    SetTab(i, GetString("Stylesheet.Theme"), "CssStylesheet_Theme.aspx?cssstylesheetid=" + Stylesheet.StylesheetID, "SetHelpTopic('helpTopic', 'cssstylesheet_theme');");
                    i++;
                }

                //}
            }

            // Sites tab
            SetTab(i, GetString("general.sites"), "CssStylesheet_Sites.aspx?cssstylesheetid=" + Stylesheet.StylesheetID, "SetHelpTopic('helpTopic', 'sites_tab3');");
        }
    }

    #endregion
}

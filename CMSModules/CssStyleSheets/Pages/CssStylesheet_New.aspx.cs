using System;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.ExtendedControls;
using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSModules_CssStylesheets_Pages_CssStylesheet_New : CMSModalPage
{
    #region "Variables"

    private bool dialogMode;


    private SiteInfo mSite;

    #endregion


    #region "Properties"

    private SiteInfo CurrentSite
    {
        get
        {
            if (mSite == null)
            {
                int siteId = QueryHelper.GetInteger("siteid", 0);
                if (siteId > 0)
                {
                    mSite = SiteInfoProvider.GetSiteInfo(siteId);
                }
                if (mSite == null)
                {
                    mSite = CMSContext.CurrentSite;
                }
            }
            return mSite;
        }
    }

    #endregion


    #region "Methods"

    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
        RequireSite = false;

        // Check for UI permissions
        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Content", new string[] { "Properties", "Properties.General", "General.Design", "Design.NewCSSStylesheets" }, CMSContext.CurrentSiteName))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Content", "Properties;Properties.General;General.Design;Design.NewCSSStylesheets");
        }

        // Check hash
        if (!QueryHelper.ValidateHash("hash", "saved;selectorid"))
        {
            URLHelper.Redirect(ResolveUrl(string.Format("~/CMSMessages/Error.aspx?title={0}&text={1}", ResHelper.GetString("dialogs.badhashtitle"), ResHelper.GetString("dialogs.badhashtext"))));
        }

        // Page has been opened from CMSDesk
        dialogMode = QueryHelper.GetBoolean("usedialog", false);

        if (dialogMode)
        {
            MasterPageFile = "~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master";

            // Check 'Design Web site' permission if opened from CMS Desk
            if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Design", "Design"))
            {
                RedirectToCMSDeskAccessDenied("CMS.Design", "Design");
            }
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        lblCssStylesheetDisplayName.Text = GetString("CssStylesheet_General.DisplayName");
        lblCssStylesheetName.Text = GetString("CssStylesheet_General.Name");
        lblCssStylesheetText.Text = GetString("CssStylesheet_General.Text");

        if (dialogMode)
        {
            SetDialogMode();
        }
        else
        {
            // Check 'Assign with current web site' check box
            if (CurrentSite != null)
            {
                chkAssign.Text = string.Concat(GetString("General.AssignWithWebSite"), " ", CurrentSite.DisplayName);
                chkAssign.Visible = true;
            }

            btnOk.Text = GetString("general.ok");

            string cssStylesheets = GetString("CssStylesheet.CssStylesheets");
            string currentCssStylesheet = GetString("CssStylesheet.NewCssStylesheet");

            const string cssStylesheetUrl = "~/CMSModules/CssStylesheets/Pages/CssStylesheet_List.aspx";

            // Initializes breadcrumbs
            string[,] pageTitleTabs = new string[2, 3];
            pageTitleTabs[0, 0] = cssStylesheets;
            pageTitleTabs[0, 1] = cssStylesheetUrl;
            pageTitleTabs[1, 0] = currentCssStylesheet;

            CurrentMaster.Title.Breadcrumbs = pageTitleTabs;
        }

        rfvDisplayName.ErrorMessage = GetString("CssStylesheet_New.EmptyDisplayName");
        rfvName.ErrorMessage = GetString("CssStylesheet_New.EmptyName");

        CurrentMaster.Title.TitleText = GetString("CssStylesheet.NewCssStylesheet");
        CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_CSSStylesheet/new.png");
        CurrentMaster.Title.HelpTopicName = "new_sheet";
        CurrentMaster.Title.HelpName = "helpTopic";
    }


    protected void btnOK_Click(object sender, EventArgs e)
    {
        Save(false);
    }


    private bool Save(bool closeOnSave)
    {
        // Finds whether required fields are not empty
        string result = new Validator()
            .NotEmpty(txtDisplayName.Text, GetString("CssStylesheet_New.EmptyDisplayName"))
            .NotEmpty(txtName.Text, GetString("CssStylesheet_New.EmptyName"))
            .IsCodeName(txtName.Text, GetString("general.invalidcodename"))
            .NotEmpty(txtText.Text.Trim(), GetString("CssStylesheet_New.EmptyText"))
            .Result;

        if (result != string.Empty)
        {
            ShowError(result);
            return false;
        }

        try
        {
            int cssStylesheetId = SaveNewCssStylesheet();
            if (cssStylesheetId > 0)
            {
                if (dialogMode)
                {
                    ProcessDialog(cssStylesheetId, closeOnSave);
                }
                else
                {
                    URLHelper.Redirect(string.Format("CssStylesheet_Edit.aspx?cssstylesheetid={0}&saved=1&hash={1}&tabmode=1", cssStylesheetId, QueryHelper.GetHash(string.Empty)));
                }
            }

            return true;
        }
        catch (Exception ex)
        {
            ShowError(ex.Message.Replace("%%name%%", txtName.Text));
            return false;
        }
    }


    private void SetDialogMode()
    {
        // Check if user can edit the stylesheet
        CurrentUserInfo currentUser = CMSContext.CurrentUser;
        string siteName = (CurrentSite != null) ? CurrentSite.SiteName : string.Empty;
        if (!currentUser.IsAuthorizedPerUIElement("CMS.Content", new string[] { "Properties", "Properties.General", "General.Design" }, siteName))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Content", "Properties;Properties.General;General.Design");
        }

        // Hide OK button
        btnOk.Visible = false;

        CurrentMaster.PanelContent.CssClass = "PageContent";
        CurrentMaster.PanelFooter.CssClass = "FloatRight";

        // Add save button
        LocalizedButton btnSave = new LocalizedButton
        {
            ID = "btnSave",
            ResourceString = "general.save",
            EnableViewState = false,
            CssClass = "SubmitButton"
        };
        btnSave.Click += (sender, e) => Save(false);
        CurrentMaster.PanelFooter.Controls.Add(btnSave);

        // Add save & close button
        LocalizedButton btnSaveAndClose = new LocalizedButton
        {
            ID = "btnSaveAndClose",
            ResourceString = "general.saveandclose",
            CssClass = "LongSubmitButton",
            EnableViewState = false
        };
        btnSaveAndClose.Click += (sender, e) => Save(true);
        CurrentMaster.PanelFooter.Controls.Add(btnSaveAndClose);

        // Add close button
        CurrentMaster.PanelFooter.Controls.Add(new LocalizedButton
        {
            ID = "btnCancel",
            ResourceString = "general.close",
            EnableViewState = false,
            OnClientClick = "window.close(); return false;",
            CssClass = "SubmitButton"
        });
    }


    protected int SaveNewCssStylesheet()
    {
        CssStylesheetInfo cs = new CssStylesheetInfo
        {
            StylesheetDisplayName = txtDisplayName.Text,
            StylesheetName = txtName.Text,
            StylesheetText = txtText.Text
        };

        CssStylesheetInfoProvider.SetCssStylesheetInfo(cs);

        // Stylesheet is assigned to site if checkbox is showed and checked or in dialog mode(stylesheet is assigned to specified or current site) 
        if (((chkAssign.Visible && chkAssign.Checked) || dialogMode) && (CurrentSite != null) && (cs.StylesheetID > 0))
        {
            // Add new stylesheet to the actual site
            CssStylesheetSiteInfoProvider.AddCssStylesheetToSite(cs.StylesheetID, CurrentSite.SiteID);
        }

        return cs.StylesheetID;
    }


    private void ProcessDialog(int cssStylesheetId, bool closeOnSave)
    {
        string selector = QueryHelper.GetString("selectorid", string.Empty);
        if (string.IsNullOrEmpty(selector))
        {
            return;
        }

        // Selects newly created container in the UniSelector
        string script =
            string.Format(@"var wopener = window.top.opener ? window.top.opener : window.top.dialogArguments
                    		    if (wopener) {{ wopener.US_SelectNewValue_{0}('{1}'); }}",
                            selector, cssStylesheetId);

        // Redirects to edit window or simply closes the current window
        if (closeOnSave)
        {
            script += "window.top.close()";
        }
        else
        {
            int siteId = (CurrentSite != null) ? CurrentSite.SiteID : 0;
            script += string.Format(@"window.name = {0};
                    	                  window.open('CssStylesheet_General.aspx?cssstylesheetid={1}&saved=1&editonlycode=true&selectorid='+{0}+'&siteid={3}&hash={2}',window.name);",
                                      ScriptHelper.GetString(selector),
                                      cssStylesheetId,
                                      QueryHelper.GetHash("?editonlycode=true"),
                                      siteId);
        }

        ScriptHelper.RegisterStartupScript(this, GetType(), "UpdateSelector", script, true);
    }

    #endregion
}
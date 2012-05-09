using System;
using System.Data;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.ExtendedControls;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSModules_CssStylesheets_Pages_CssStylesheet_General : CMSModalPage
{
    #region "Variables"

    protected int cssStylesheetId = 0;
    private SiteInfo mSite = null;

    #endregion


    #region "Properties"

    private bool DialogMode
    {
        get;
        set;
    }


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
        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Content", new string[] { "Properties", "Properties.General", "General.Design", "Design.EditCSSStylesheets" }, CMSContext.CurrentSiteName))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Content", "Properties;Properties.General;General.Design;Design.EditCSSStylesheets");
        }

        // Page has been opened in CMSDesk and only stylesheet style editing is allowed
        DialogMode = QueryHelper.GetBoolean("editonlycode", false);

        if (DialogMode)
        {
            // Check hash
            if (!QueryHelper.ValidateHash("hash", "saved;cssstylesheetid;selectorid;tabmode;siteid"))
            {
                URLHelper.Redirect(ResolveUrl(string.Format("~/CMSMessages/Error.aspx?title={0}&text={1}", ResHelper.GetString("dialogs.badhashtitle"), ResHelper.GetString("dialogs.badhashtext"))));
            }

            // Check 'Design Web site' permission if opened from CMS Desk
            if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Design", "Design"))
            {
                RedirectToCMSDeskAccessDenied("CMS.Design", "Design");
            }

            MasterPageFile = "~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master";
        }
        else
        {
            // If not dialog mode, user must be global admin
            CheckAccessToSiteManager();
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        lblCssStylesheetDisplayName.Text = GetString("CssStylesheet_General.DisplayName");
        lblCssStylesheetName.Text = GetString("CssStylesheet_General.Name");
        lblCssStylesheetText.Text = GetString("CssStylesheet_General.Text");

        rfvDisplayName.ErrorMessage = GetString("CssStylesheet_General.EmptyDisplayName");
        rfvName.ErrorMessage = GetString("CssStylesheet_General.EmptyName");

        if (QueryHelper.GetString("saved", string.Empty) != string.Empty)
        {
            ShowInformation(GetString("General.ChangesSaved"));

            // Reload header if changes were saved
            if (TabMode || !DialogMode)
            {
                ScriptHelper.RefreshTabHeader(Page, null);
            }
        }

        string stylesheet = QueryHelper.GetString("cssstylesheetid", "0");
        CssStylesheetInfo si = null;

        // If default stylesheet defined and selected, choose it
        if (stylesheet == "default")
        {
            si = CMSContext.CurrentSiteStylesheet;
        }

        // Default stylesheet not selected try to find stylesheet selected
        if (si != null)
        {
            cssStylesheetId = si.StylesheetID;
            SetEditedObject(si, "CssStylesheet_Edit.aspx");
        }
        else
        {
            cssStylesheetId = ValidationHelper.GetInteger(stylesheet, 0);
            if (cssStylesheetId > 0)
            {
                // Get the stylesheet
                si = CssStylesheetInfoProvider.GetCssStylesheetInfo(cssStylesheetId);
                SetEditedObject(si, "CssStylesheet_Edit.aspx");
            }
        }

        if (si != null)
        {
            // Page has been opened in CMSDesk and only stylesheet style editing is allowed
            if (DialogMode)
            {
                SetDialogMode(si);
            }
            else
            {
                // Otherwise the user must be global admin
                CheckGlobalAdministrator();
            }

            if (!RequestHelper.IsPostBack())
            {
                txtCssStylesheetDisplayName.Text = si.StylesheetDisplayName;
                txtCssStylesheetName.Text = si.StylesheetName;
                txtCssStylesheetText.Text = si.StylesheetText;
            }

            if (si.StylesheetCheckedOutByUserID > 0)
            {
                txtCssStylesheetText.ReadOnly = true;
                string username = null;
                UserInfo ui = UserInfoProvider.GetUserInfo(si.StylesheetCheckedOutByUserID);
                if (ui != null)
                {
                    username = HTMLHelper.HTMLEncode(ui.FullName);
                }

                // Checked out by current machine
                if (string.Equals(si.StylesheetCheckedOutMachineName, HTTPHelper.MachineName, StringComparison.OrdinalIgnoreCase))
                {
                    lblCheckOutInfo.Text = string.Format(GetString("CssStylesheet.CheckedOut"), Server.MapPath(si.StylesheetCheckedOutFilename));
                }
                else
                {
                    lblCheckOutInfo.Text = string.Format(GetString("CssStylesheet.CheckedOutOnAnotherMachine"), HTMLHelper.HTMLEncode(si.StylesheetCheckedOutMachineName), username);
                }
            }
            else
            {
                lblCheckOutInfo.Text = string.Format(GetString("CssStylesheet.CheckOutInfo"), Server.MapPath(CssStylesheetInfoProvider.GetVirtualStylesheetUrl(si.StylesheetName, null)));
            }
        }

        InitializeHeaderActions(si);


    }


    /// <summary>
    /// Initializes header action control.
    /// </summary>
    private void InitializeHeaderActions(CssStylesheetInfo si)
    {
        // Header actions
        string[,] actions = new string[4, 11];

        // Save button
        actions[0, 0] = HeaderActions.TYPE_SAVEBUTTON;
        actions[0, 1] = GetString("General.Save");
        actions[0, 5] = GetImageUrl("CMSModules/CMS_Content/EditMenu/save.png");
        actions[0, 6] = "save";
        actions[0, 8] = "true";

        // CheckOut
        actions[1, 0] = HeaderActions.TYPE_SAVEBUTTON;
        actions[1, 1] = GetString("General.CheckOut");
        actions[1, 5] = GetImageUrl("CMSModules/CMS_Content/EditMenu/checkout.png");
        actions[1, 6] = "checkout";
        actions[1, 10] = "false";

        // CheckIn
        actions[2, 0] = HeaderActions.TYPE_SAVEBUTTON;
        actions[2, 1] = GetString("General.CheckIn");
        actions[2, 5] = GetImageUrl("CMSModules/CMS_Content/EditMenu/checkin.png");
        actions[2, 6] = "checkin";
        actions[2, 10] = "false";

        // UndoCheckOut
        actions[3, 0] = HeaderActions.TYPE_SAVEBUTTON;
        actions[3, 1] = GetString("General.UndoCheckOut");
        actions[3, 2] = "return confirm(" + ScriptHelper.GetString(GetString("General.ConfirmUndoCheckOut")) + ");";
        actions[3, 5] = GetImageUrl("CMSModules/CMS_Content/EditMenu/undocheckout.png");
        actions[3, 6] = "undocheckout";
        actions[3, 10] = "false";

        if (si != null)
        {
            if (si.StylesheetCheckedOutByUserID > 0)
            {
                // Checked out by current machine
                if (string.Equals(si.StylesheetCheckedOutMachineName, HTTPHelper.MachineName, StringComparison.OrdinalIgnoreCase))
                {
                    actions[2, 10] = "true";
                }
                if (CMSContext.CurrentUser.UserSiteManagerAdmin)
                {
                    actions[3, 10] = "true";
                }
            }
            else
            {
                actions[1, 10] = "true";
            }
        }

        this.CurrentMaster.HeaderActions.LinkCssClass = "ContentSaveLinkButton";
        this.CurrentMaster.HeaderActions.ActionPerformed += HeaderActions_ActionPerformed;
        this.CurrentMaster.HeaderActions.Actions = actions;
    }


    /// <summary>
    /// Actions handler.
    /// </summary>
    protected void HeaderActions_ActionPerformed(object sender, CommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "save":

                Save();
                break;

            case "checkout":

                // Ensure version before check-out
                using (CMSActionContext context = new CMSActionContext())
                {
                    context.AllowAsyncActions = false;

                    if (!Save())
                    {
                        return;
                    }
                }

                try
                {
                    SiteManagerFunctions.CheckOutStylesheet(cssStylesheetId);
                }
                catch (Exception ex)
                {
                    ShowInformation(string.Concat(GetString("CssStylesheet.ErrorCheckout"), ": ", ex.Message));
                    return;
                }

                string url = URLHelper.AddParameterToUrl(URLHelper.Url.AbsoluteUri, "saved", "1");

                // In case of dialog mode use javascript redirection to ensure script are rendered correctly
                if (DialogMode)
                {
                    ScriptHelper.RegisterStartupScript(this, typeof(string), "PageRedirect", "window.location.replace('" + url + "');", true);
                }
                else
                {
                    URLHelper.Redirect(url);
                }
                break;

            case "checkin":

                try
                {
                    SiteManagerFunctions.CheckInStylesheet(cssStylesheetId);
                }
                catch (Exception ex)
                {
                    ShowError(string.Concat(GetString("CssStylesheet.ErrorCheckin"), ": ", ex.Message));
                    return;
                }

                URLHelper.Redirect(URLHelper.Url.AbsoluteUri);
                break;

            case "undocheckout":

                try
                {
                    SiteManagerFunctions.UndoCheckOutStylesheet(cssStylesheetId);
                }
                catch (Exception ex)
                {
                    ShowError(string.Concat(GetString("CssStylesheet.ErrorUndoCheckout"), ": ", ex.Message));
                    return;
                }

                URLHelper.Redirect(URLHelper.Url.AbsoluteUri);
                break;
        }
    }


    /// <summary>
    /// Saves the stylesheet, returns true if successful.
    /// </summary>
    private bool Save()
    {
        string result = new Validator()
            .NotEmpty(txtCssStylesheetDisplayName.Text, GetString("CssStylesheet_General.EmptyDisplayName"))
            .NotEmpty(txtCssStylesheetName.Text, GetString("CssStylesheet_General.EmptyName"))
            .IsCodeName(txtCssStylesheetName.Text, GetString("general.invalidcodename"))
            .NotEmpty(txtCssStylesheetText.Text.Trim(), GetString("CssStylesheet_General.EmptyText"))
            .Result;

        if (result != string.Empty)
        {
            ShowError(result);
            return false;
        }

        CssStylesheetInfo si = CssStylesheetInfoProvider.GetCssStylesheetInfo(cssStylesheetId);
        if (si != null)
        {
            si.StylesheetDisplayName = txtCssStylesheetDisplayName.Text;
            si.StylesheetName = txtCssStylesheetName.Text;
            si.StylesheetID = cssStylesheetId;
            if (si.StylesheetCheckedOutByUserID <= 0)
            {
                si.StylesheetText = txtCssStylesheetText.Text;
            }
            try
            {
                CssStylesheetInfoProvider.SetCssStylesheetInfo(si);
                ShowInformation(GetString("General.ChangesSaved"));

                // Reload header if changes were saved
                if (TabMode || !DialogMode)
                {
                    ScriptHelper.RefreshTabHeader(Page, null);
                }

                // Ensure that selector has actual values
                if (DialogMode)
                {
                    string selector = QueryHelper.GetString("selectorid", string.Empty);
                    if (!string.IsNullOrEmpty(selector))
                    {
                        // Selects newly created container in the UniSelector
                        string script =
                            string.Format(@"var wopener = window.top.opener ? window.top.opener : window.top.dialogArguments
                    		    if (wopener && wopener.US_SelectNewValue_{0}) {{ wopener.US_SelectNewValue_{0}('{1}'); }}",
                                            selector, QueryHelper.GetInteger("cssstylesheetid", -1));

                        ScriptHelper.RegisterStartupScript(this, GetType(), "UpdateSelector", script, true);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError(ex.Message.Replace("%%name%%", txtCssStylesheetName.Text));
                return false;
            }
            return true;
        }
        return false;
    }


    private void SetDialogMode(CssStylesheetInfo si)
    {
        // Check if stylesheet is under curent site
        int siteId = (CurrentSite != null) ? CurrentSite.SiteID : 0;
        string where = SqlHelperClass.AddWhereCondition("SiteID = " + siteId, "StylesheetID = " + si.StylesheetID);
        DataSet ds = CssStylesheetSiteInfoProvider.GetCssStylesheetSites("StylesheetID", where, null, 1);
        if (DataHelper.DataSourceIsEmpty(ds))
        {
            URLHelper.Redirect(ResolveUrl(string.Format("~/CMSMessages/Error.aspx?title={0}&text={1}", ResHelper.GetString("cssstylesheet.errorediting"), ResHelper.GetString("cssstylesheet.notallowedtoedit"))));
        }

        // Set CSS classes
        CurrentMaster.PanelContent.CssClass = "PageContent";
        CurrentMaster.PanelFooter.CssClass = "FloatRight";

        // Add save and close button        
        LocalizedButton btnSaveAndCancel = new LocalizedButton
        {
            ID = "btnSaveCancel",
            ResourceString = "general.saveandclose",
            EnableViewState = false,
            CssClass = "LongSubmitButton"
        };

        btnSaveAndCancel.Click += (sender, e) =>
        {
            if (Save())
            {
                ScriptHelper.RegisterStartupScript(this, GetType(), "SaveAndClose", "window.top.close();", true);
            }
        };

        CurrentMaster.PanelFooter.Controls.Add(btnSaveAndCancel);

        // Add close button
        CurrentMaster.PanelFooter.Controls.Add(new LocalizedButton
        {
            ID = "btnClose",
            ResourceString = "general.close",
            EnableViewState = false,
            OnClientClick = "window.top.close(); return false;",
            CssClass = "SubmitButton"
        });

        // Set CMS master page title        
        CurrentMaster.Title.TitleText = GetString("CssStylesheet.EditCssStylesheet");
        CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_CSSStylesheet/object.png");

        // Disable display and code name editing
        //txtCssStylesheetDisplayName.ReadOnly = txtCssStylesheetName.ReadOnly = true;
    }

    #endregion
}
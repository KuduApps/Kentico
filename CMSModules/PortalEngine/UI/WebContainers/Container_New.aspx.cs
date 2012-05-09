using System;

using CMS.CMSHelper;
using CMS.ExtendedControls;
using CMS.GlobalHelper;
using CMS.PortalEngine;
using CMS.UIControls;

public partial class CMSModules_PortalEngine_UI_WebContainers_Container_New : CMSModalDesignPage
{
    #region "Variables"

    private bool mDialogMode;

    #endregion


    #region "Methods"

    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);

        RequireSite = false;

        // Page has been opened from CMSDesk
        mDialogMode = QueryHelper.GetBoolean("editonlycode", false);

        // Initialize the master page
        if (mDialogMode)
        {
            // Check for UI permissions
            if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Content", new string[] { "Design", "Design.WebPartProperties", "WebPartProperties.General", "WebPartProperties.NewContainers" }, CMSContext.CurrentSiteName))
            {
                RedirectToCMSDeskUIElementAccessDenied("CMS.Content", "Design.WebPartProperties;WebPartProperties.General;WebPartProperties.NewContainers");
            }

            MasterPageFile = "~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master";
        }
        else
        {
            // Page opened from Site Manager
            CheckAccessToSiteManager();
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (mDialogMode)
        {
            SetDialogMode();
        }
        else
        {
            InitBreadcrumbs();

            CurrentSiteInfo currentSite = CMSContext.CurrentSite;
            if (currentSite != null)
            {
                chkAssign.Text = string.Format("{0} {1}", GetString("General.AssignWithWebSite"), currentSite.DisplayName);
                chkAssign.Visible = true;
            }
        }

        CurrentMaster.Title.HelpName = "helpTopic";
        CurrentMaster.Title.HelpTopicName = "newedit_container";
        CurrentMaster.Title.TitleText = GetString("Container_Edit.NewHeaderCaption");
        CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_WebPartContainer/new.png");

        rfvDisplayName.ErrorMessage = GetString("general.requiresdisplayname");
        rfvCodeName.ErrorMessage = GetString("general.requirescodename");

        this.plcCssLink.Visible = String.IsNullOrEmpty(txtContainerCSS.Text);
    }


    protected void btnOK_Click(object sender, EventArgs e)
    {
        Save(false);
    }


    private bool Save(bool closeOnSave)
    {
        // Validate user input
        string errorMessage = new Validator()
            .NotEmpty(txtContainerDisplayName.Text, rfvDisplayName.ErrorMessage)
            .NotEmpty(txtContainerName.Text, rfvCodeName.ErrorMessage)
            .IsCodeName(txtContainerName.Text, GetString("General.InvalidCodeName"))
            .Result;

        if (!string.IsNullOrEmpty(errorMessage))
        {
            ShowError(errorMessage);
            return false;
        }

        WebPartContainerInfo webPartContainerObj = new WebPartContainerInfo
        {
            ContainerTextBefore = txtContainerTextBefore.Text,
            ContainerTextAfter = txtContainerTextAfter.Text,
            ContainerCSS = txtContainerCSS.Text,
            ContainerName = txtContainerName.Text.Trim(),
            ContainerDisplayName = txtContainerDisplayName.Text.Trim()
        };

        // Check for duplicity
        if (WebPartContainerInfoProvider.GetWebPartContainerInfo(webPartContainerObj.ContainerName) != null)
        {
            ShowError(GetString("Container_Edit.UniqueError"));
            return false;
        }

        WebPartContainerInfoProvider.SetWebPartContainerInfo(webPartContainerObj);

        if (mDialogMode)
        {
            ProcessDialog(webPartContainerObj, closeOnSave);
        }
        else
        {
            ProcessPage(webPartContainerObj);
        }

        return true;
    }


    private void InitBreadcrumbs()
    {
        string[,] breadcrumbs = new string[2, 3];

        breadcrumbs[0, 0] = GetString("Container_Edit.ItemListLink");
        breadcrumbs[0, 1] = ResolveUrl("Container_List.aspx");
        breadcrumbs[1, 0] = GetString("Container_Edit.NewItemCaption");

        CurrentMaster.Title.Breadcrumbs = breadcrumbs;
    }


    private void SetDialogMode()
    {
        // When in modal dialog, the margins have to be increased
        CurrentMaster.PanelContent.CssClass = "PageContent";
        CurrentMaster.PanelFooter.CssClass = "FloatRight";

        btnOk.ResourceString = GetString("general.save");
        btnOk.Parent.Controls.Remove(btnOk);

        // Add save button
        CurrentMaster.PanelFooter.Controls.Add(btnOk);

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

        // When in modal dialog, the window scrolls to bottom, so this hack will scroll it back to top
        string scrollScript = "var scrollerDiv = document.getElementById('divContent'); if (scrollerDiv != null) setTimeout(function() { scrollerDiv.scrollTop = 0; }, 500);";
        ScriptHelper.RegisterStartupScript(this, GetType(), "ScrollTop", scrollScript, true);
    }


    private void ProcessPage(WebPartContainerInfo webPartContainer)
    {
        if (chkAssign.Visible && chkAssign.Checked)
        {
            WebPartContainerSiteInfoProvider.AddContainerToSite(webPartContainer, CMSContext.CurrentSite);
        }

        URLHelper.Redirect(string.Format("Container_Edit_Frameset.aspx?containerid={0}&saved=1&hash={1}",
                                         webPartContainer.ContainerID,
                                         QueryHelper.GetHash(string.Empty)));
    }


    private void ProcessDialog(WebPartContainerInfo webPartContainer, bool closeOnSave)
    {
        WebPartContainerSiteInfoProvider.AddContainerToSite(webPartContainer, CMSContext.CurrentSite);

        string selector = QueryHelper.GetString("selectorid", string.Empty);
        if (string.IsNullOrEmpty(selector))
        {
            return;
        }

        // Selects newly created container in the UniSelector
        string script = string.Format(@"var wopener = window.top.opener ? window.top.opener : window.top.dialogArguments
                                        if (wopener) {{ wopener.US_SelectNewValue_{0}('{1}'); }}",
                                        selector, webPartContainer.ContainerName);

        // Redirects to edit window or simply closes the current window
        if (closeOnSave)
        {
            script += "window.close();";
        }
        else
        {
            script += string.Format(@"window.name = {0};
                                      window.open('Container_Edit_General.aspx?name={1}&saved=1&editonlycode=true&selectorid='+{0}+'&hash={2}',window.name);",
                                      ScriptHelper.GetString(selector),
                                      webPartContainer.ContainerName,
                                      QueryHelper.GetHash("?editonlycode=true"));
        }

        ScriptHelper.RegisterStartupScript(this, GetType(), "UpdateSelector", script, true);
    }

    #endregion
}
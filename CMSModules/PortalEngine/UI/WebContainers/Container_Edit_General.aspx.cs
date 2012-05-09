using System;

using CMS.CMSHelper;
using CMS.ExtendedControls;
using CMS.GlobalHelper;
using CMS.PortalEngine;
using CMS.UIControls;

public partial class CMSModules_PortalEngine_UI_WebContainers_Container_Edit_General : CMSModalDesignPage
{
    #region "Variables"

    protected int containerId;


    WebPartContainerInfo webPartContainerObj;


    private bool mDialogMode;

    #endregion


    #region "Methods"

    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
        this.RequireSite = false;

        // Page has been opened from CMSDesk
        mDialogMode = QueryHelper.GetBoolean("editonlycode", false);
        if (mDialogMode)
        {
            // Check hash
            if (!QueryHelper.ValidateHash("hash", "saved;containerid;name;selectorid;tabmode"))
            {
                URLHelper.Redirect(ResolveUrl(string.Format("~/CMSMessages/Error.aspx?title={0}&text={1}", ResHelper.GetString("dialogs.badhashtitle"), ResHelper.GetString("dialogs.badhashtext"))));
            }

            // Check permissions for web part properties UI        
            if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Content", new string[] { "Design", "Design.WebPartProperties", "WebPartProperties.General", "WebPartProperties.EditContainers" }, CMSContext.CurrentSiteName))
            {
                RedirectToCMSDeskUIElementAccessDenied("CMS.Content", "Design.WebPartProperties;WebPartProperties.General;WebPartProperties.EditContainers");
            }

            MasterPageFile = "~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master";
        }
        else
        {
            // If not in dialog mode, the user must be global admin
            CheckAccessToSiteManager();
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        InitHeaderActions();

        rfvDisplayName.ErrorMessage = GetString("general.requiresdisplayname");
        rfvCodeName.ErrorMessage = GetString("general.requirescodename");

        if (mDialogMode)
        {
            SetDialogMode();
        }
        else
        {
            // Get by container ID
            containerId = QueryHelper.GetInteger("containerid", 0);
            if (containerId > 0)
            {
                webPartContainerObj = WebPartContainerInfoProvider.GetWebPartContainerInfo(containerId);
                SetEditedObject(webPartContainerObj, null);
            }
        }

        if (!RequestHelper.IsPostBack())
        {
            LoadData(webPartContainerObj);

            // Show changes saved if specified from querystring
            if (QueryHelper.GetInteger("saved", 0) == 1)
            {
                ShowInformation(GetString("General.ChangesSaved"));
            }
        }

        this.plcCssLink.Visible = String.IsNullOrEmpty(txtContainerCSS.Text);
    }


    /// <summary>
    /// Load data of editing webPartContainer.
    /// </summary>
    /// <param name="webPartContainer">WebPartContainer object</param>
    protected void LoadData(WebPartContainerInfo webPartContainer)
    {
        if (webPartContainer != null)
        {
            // Load fields
            txtContainerTextBefore.Text = webPartContainer.ContainerTextBefore;
            txtContainerTextAfter.Text = webPartContainer.ContainerTextAfter;
            txtContainerCSS.Text = webPartContainer.ContainerCSS;
            txtContainerName.Text = webPartContainer.ContainerName;
            txtContainerDisplayName.Text = webPartContainer.ContainerDisplayName;
        }
    }


    /// <summary>
    /// Saves the web part container.
    /// </summary>
    /// <returns>true if web part container was saved successfully, otherwise false.</returns>
    private bool Save()
    {
        string errorMessage = new Validator()
            .NotEmpty(txtContainerDisplayName.Text, rfvDisplayName.ErrorMessage)
            .NotEmpty(txtContainerName.Text, rfvCodeName.ErrorMessage)
            .IsCodeName(txtContainerName.Text, GetString("general.invalidcodename"))
            .Result;

        if (errorMessage != string.Empty)
        {
            ShowError(errorMessage);
            return false;
        }

        // If webPartContainer doesnt already exist, create new one
        if (webPartContainerObj == null)
        {
            webPartContainerObj = new WebPartContainerInfo();
        }

        webPartContainerObj.ContainerTextBefore = txtContainerTextBefore.Text;
        webPartContainerObj.ContainerTextAfter = txtContainerTextAfter.Text;
        webPartContainerObj.ContainerCSS = txtContainerCSS.Text;
        webPartContainerObj.ContainerName = txtContainerName.Text.Trim();
        webPartContainerObj.ContainerDisplayName = txtContainerDisplayName.Text.Trim();

        // Check existing name
        if (!mDialogMode)
        {
            WebPartContainerInfo wPcI = WebPartContainerInfoProvider.GetWebPartContainerInfo(webPartContainerObj.ContainerName);
            if ((wPcI != null) && (wPcI.ContainerID != webPartContainerObj.ContainerID))
            {
                ShowError(GetString("Container_Edit.UniqueError"));
                return false;
            }
        }

        // Save the container
        WebPartContainerInfoProvider.SetWebPartContainerInfo(webPartContainerObj);
        ShowInformation(GetString("General.ChangesSaved"));

        // Reload header if changes were saved
        if (TabMode)
        {
            ScriptHelper.RefreshTabHeader(Page, null);
        }

        return true;
    }


    private void InitHeaderActions()
    {
        string[,] actions = new string[1, 12];

        actions[0, 0] = HeaderActions.TYPE_SAVEBUTTON;
        actions[0, 1] = GetString("General.Save");
        actions[0, 5] = GetImageUrl("CMSModules/CMS_Content/EditMenu/save.png");
        actions[0, 6] = "save";
        actions[0, 8] = "true";

        this.CurrentMaster.HeaderActions.LinkCssClass = "ContentSaveLinkButton";
        this.CurrentMaster.HeaderActions.ActionPerformed += (sender, e) =>
        {
            if (e.CommandName == "save")
            {
                Save();
            }
        };
        this.CurrentMaster.HeaderActions.Actions = actions;
    }


    private void SetDialogMode()
    {
        CurrentMaster.PanelContent.CssClass = "PageContent";

        string containerName = QueryHelper.GetString("name", string.Empty);
        if (!string.IsNullOrEmpty(containerName))
        {
            // Set page title
            CurrentMaster.Title.TitleText = GetString("Container_Edit.EditContainer");
            CurrentMaster.Title.TitleImage = GetObjectIconUrl(PortalObjectType.WEBPARTCONTAINER, null);
            CurrentMaster.Title.HelpTopicName = "newedit_container";

            // Get container data
            webPartContainerObj = WebPartContainerInfoProvider.GetWebPartContainerInfo(containerName);
            SetEditedObject(webPartContainerObj, "Container_Edit_Frameset.aspx");
        }

        AddDialogButtons();

        // Disable display and code name editing
        txtContainerDisplayName.ReadOnly = txtContainerName.ReadOnly = true;

        // When in modal dialog, the window scrolls to bottom, so this hack will scroll it back to top
        string scrollScript = "var scrollerDiv = document.getElementById('divContent'); if (scrollerDiv != null) setTimeout(function() { scrollerDiv.scrollTop = 0; }, 500);";
        ScriptHelper.RegisterStartupScript(this, GetType(), "ScrollTop", scrollScript, true);
    }


    private void AddDialogButtons()
    {
        CurrentMaster.PanelFooter.CssClass = "FloatRight";

        // Add save & close button        
        LocalizedButton btnSaveAnClose = new LocalizedButton
        {
            ID = "btnSaveCancel",
            ResourceString = "general.saveandclose",
            EnableViewState = false,
            CssClass = "LongSubmitButton"
        };

        btnSaveAnClose.Click += (sender, e) =>
        {
            if (Save())
            {
                ScriptHelper.RegisterStartupScript(this, GetType(), "SaveAndClose", "window.top.close();", true);
            }
        };

        CurrentMaster.PanelFooter.Controls.Add(btnSaveAnClose);

        // Add close button
        CurrentMaster.PanelFooter.Controls.Add(new LocalizedButton
        {
            ID = "btnCancel",
            ResourceString = "general.close",
            EnableViewState = false,
            OnClientClick = "window.top.close(); return false;",
            CssClass = "SubmitButton"
        });
    }

    #endregion
}
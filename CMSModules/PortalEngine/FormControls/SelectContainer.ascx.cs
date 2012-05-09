using System;

using CMS.CMSHelper;
using CMS.FormControls;
using CMS.GlobalHelper;

public partial class CMSModules_PortalEngine_FormControls_SelectContainer : FormEngineUserControl
{
    #region "Properties"

    /// <summary>
    /// Gets or sets the enabled state of the control.
    /// </summary>
    public override bool Enabled
    {
        get
        {
            return base.Enabled;
        }
        set
        {
            base.Enabled = value;
            this.selectContainer.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets field value.
    /// </summary>
    public override object Value
    {
        get
        {
            return selectContainer.Value;
        }
        set
        {
            EnsureChildControls();
            selectContainer.Value = value;
        }
    }


    /// <summary>
    /// Gets ClientID of the dropdownlist with containers.
    /// </summary>
    public override string ValueElementID
    {
        get
        {
            return selectContainer.ClientID;
        }
    }


    /// <summary>
    /// Is live site.
    /// </summary>
    public override bool IsLiveSite
    {
        get
        {
            return selectContainer.IsLiveSite;
        }
        set
        {
            selectContainer.IsLiveSite = value;
        }
    }

    #endregion


    #region "Methods"

    protected override void CreateChildControls()
    {
        base.CreateChildControls();

        selectContainer.DropDownSingleSelect.CssClass = "SelectorDropDown";

        selectContainer.WhereCondition = string.Format("ContainerID IN (SELECT ContainerID FROM CMS_WebPartContainerSite WHERE SiteID = {0})", CMSContext.CurrentSiteID);

        // Add none value
        string[,] noneValue = new string[1, 2];
        noneValue[0, 0] = GetString("general.empty");
        noneValue[0, 1] = string.Empty;
        selectContainer.SpecialFields = noneValue;

        CurrentUserInfo currentUser = CMSContext.CurrentUser;
        string siteName = CMSContext.CurrentSiteName;

        // Check user permissions
        bool design = currentUser.IsAuthorizedPerResource("CMS.Design", "Design");
        bool deskAuthorized = currentUser.IsAuthorizedPerUIElement("CMS.Desk", "Content");
        bool contentAuthorized = currentUser.IsAuthorizedPerUIElement("CMS.Content", new string[] { "Design", "Design.WebPartProperties", "WebPartProperties.General" }, siteName);

        if (!IsLiveSite && design && deskAuthorized && contentAuthorized)
        {
            // Check UI permissions for editing/creating container
            bool editAuthorized = currentUser.IsAuthorizedPerUIElement("CMS.Content", new string[] { "WebPartProperties.EditContainers" }, siteName);
            bool createAuthorized = currentUser.IsAuthorizedPerUIElement("CMS.Content", new string[] { "WebPartProperties.NewContainers" }, siteName);
            // Initialize selector
            SetDialog(editAuthorized, createAuthorized);
        }
    }


    private void SetDialog(bool allowEdit, bool allowNew)
    {
        if (allowEdit)
        {
            // Set edit web part container URL
            string editUrl = "~/CMSModules/PortalEngine/UI/WebContainers/Container_Edit_General.aspx?editonlycode=true";
            editUrl = URLHelper.AddParameterToUrl(editUrl, "hash", QueryHelper.GetHash("?editonlycode=true"));
            editUrl = URLHelper.AddParameterToUrl(editUrl, "name", "##ITEMID##");
            selectContainer.EditItemPageUrl = editUrl;
        }

        if (allowNew)
        {
            // Set new web part container URL
            selectContainer.NewItemPageUrl = "~/CMSModules/PortalEngine/UI/WebContainers/Container_New.aspx?editonlycode=true";            
        }
    }

    #endregion
}
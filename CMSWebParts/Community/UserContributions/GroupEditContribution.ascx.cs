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

using CMS.PortalControls;
using CMS.DataEngine;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.TreeEngine;
using CMS.SettingsProvider;
using CMS.URLRewritingEngine;
using CMS.UIControls;
using CMS.Community;
using CMS.FormControls;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSWebParts_Community_UserContributions_GroupEditContribution : CMSAbstractWebPart
{
    #region "Public properties"

    /// <summary>
    /// Gets or sets the value that indicates whether deleting document is allowed.
    /// </summary>
    public bool AllowDelete
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("AllowDelete"), this.editForm.AllowDelete);
        }
        set
        {
            this.SetValue("AllowDelete", value);
            this.editForm.AllowDelete = value;
        }
    }


    /// <summary>
    /// Gets or sets group of users which can work with the documents.
    /// </summary>
    public UserContributionAllowUserEnum AllowUsers
    {
        get
        {
            return (UserContributionAllowUserEnum)(ValidationHelper.GetInteger(this.GetValue("AllowUsers"), 2));
        }
        set
        {
            this.SetValue("AllowUsers", Convert.ToInt32(value));
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether the permissions are checked.
    /// </summary>
    public bool CheckPermissions
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("CheckPermissions"), true);            
        }
        set
        {
            this.SetValue("CheckPermissions", value);
            editForm.CheckPermissions = value;
        }
    }


    /// <summary>
    /// Indicates whether group permissions should be checked.
    /// </summary>
    public bool CheckGroupPermissions
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("CheckGroupPermissions"), true);
        }
        set
        {
            this.SetValue("CheckGroupPermissions", value);            
        }
    }


    /// <summary>
    /// Gets or sets alternative form name.
    /// </summary>
    public string AlternativeFormName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("AlternativeFormName"), this.editForm.AlternativeFormName);
        }
        set
        {
            this.SetValue("AlternativeFormName", value);
        }
    }


    /// <summary>
    /// Gets or sets the message which is displayed after validation failed.
    /// </summary>
    public string ValidationErrorMessage
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("ValidationErrorMessage"), this.editForm.ValidationErrorMessage);
        }
        set
        {
            this.SetValue("ValidationErrorMessage", value);
        }
    }


    /// <summary>
    /// Gets or sets edit button label.
    /// </summary>
    public string EditButtonText
    {
        get
        {
            return DataHelper.GetNotEmpty(this.GetValue("EditButtonText"), "general.edit");
        }
        set
        {
            this.SetValue("EditButtonText", value);
            this.btnEdit.ResourceString = value;
        }
    }


    /// <summary>
    /// Gets or sets delete button label.
    /// </summary>
    public string DeleteButtonText
    {
        get
        {
            return DataHelper.GetNotEmpty(this.GetValue("DeleteButtonText"), "general.delete");
        }
        set
        {
            this.SetValue("DeleteButtonText", value);
            this.btnDelete.ResourceString = value;
        }
    }


    /// <summary>
    /// Gets or sets close edit mode button label.
    /// </summary>
    public string CloseEditModeButtonText
    {
        get
        {
            return DataHelper.GetNotEmpty(this.GetValue("CloseEditModeButtonText"), "EditContribution.CloseButton");
        }
        set
        {
            this.SetValue("CloseEditModeButtonText", value);
        }
    }


    /// <summary>
    /// Gets or sets value that indicates whether logging activity is performed.
    /// </summary>
    public bool LogActivity
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("LogActivity"), false);
        }
        set
        {
            this.SetValue("LogActivity", value);
            this.editForm.LogActivity = value;
        }
    }

    #endregion


    #region "Document properties"

    /// <summary>
    /// Gets or sets the culture version of the displayed content.
    /// </summary>
    public string CultureCode
    {
        get
        {
            return DataHelper.GetNotEmpty(ValidationHelper.GetString(this.GetValue("CultureCode"), ""), CMSContext.CurrentUser.PreferredCultureCode);
        }
        set
        {
            this.SetValue("CultureCode", value);
        }
    }


    /// <summary>
    /// Gets or sets the path to the document.
    /// </summary>
    public string Path
    {
        get
        {
            return DataHelper.GetNotEmpty(ValidationHelper.GetString(this.GetValue("Path"), ""), CMSContext.CurrentAliasPath);
        }
        set
        {
            this.SetValue("Path", value);
        }
    }


    /// <summary>
    /// Gets or sets the codename of the site from which you want to display the content.
    /// </summary>
    public string SiteName
    {
        get
        {
            return DataHelper.GetNotEmpty(ValidationHelper.GetString(this.GetValue("SiteName"), ""), CMSContext.CurrentSiteName);
        }
        set
        {
            this.SetValue("SiteName", value);
        }
    }

    #endregion


    /// <summary>
    /// Content loaded event handler.
    /// </summary>
    public override void OnContentLoaded()
    {
        base.OnContentLoaded();
        SetupControl();
    }


    /// <summary>
    /// Initializes the control properties.
    /// </summary>
    protected void SetupControl()
    {
        if (this.StopProcessing)
        {
            this.editForm.StopProcessing = true;
            this.editForm.Visible = false;
        }
        else
        {
            this.pnlEdit.Visible = false;

            CurrentUserInfo currentUser = CMSContext.CurrentUser;

            // Get the document
            TreeNode node = TreeHelper.GetDocument(this.SiteName, CMSContext.ResolveCurrentPath(this.Path), this.CultureCode, false, null, false, this.CheckPermissions, currentUser);
            if (node != null)
            {
                bool authorized = false;

                // Check allowed users
                switch (this.AllowUsers)
                {
                    case UserContributionAllowUserEnum.All:
                        authorized = true;
                        break;

                    case UserContributionAllowUserEnum.Authenticated:
                        authorized = currentUser.IsAuthenticated();
                        break;

                    case UserContributionAllowUserEnum.DocumentOwner:
                        authorized = (node.NodeOwner == currentUser.UserID);
                        break;
                }

                bool authorizedDelete = authorized;

                // Check control access permission
                if (authorized && this.CheckPermissions)
                {
                    // Node owner has always permission
                    if (node.NodeOwner != currentUser.UserID)
                    {
                        authorized &= (currentUser.IsAuthorizedPerDocument(node, new NodePermissionsEnum[] { NodePermissionsEnum.Read, NodePermissionsEnum.Modify }) == AuthorizationResultEnum.Allowed);
                        authorizedDelete &= (currentUser.IsAuthorizedPerDocument(node, new NodePermissionsEnum[] { NodePermissionsEnum.Read, NodePermissionsEnum.Delete }) == AuthorizationResultEnum.Allowed);
                    }
                }

                // Check group permissions
                authorized &= CheckGroupPermission("editpages");
                authorizedDelete &= CheckGroupPermission("deletepages");
                // Global admin has always permission
                authorized |= currentUser.IsGlobalAdministrator;
                authorizedDelete |= currentUser.IsGlobalAdministrator;

                // Do not allow edit for virtual user
                if (currentUser.IsVirtual)
                {
                    authorized = false;
                    authorizedDelete = false;
                }

                // Display form if authorized
                if (authorized || authorizedDelete)
                {
                    this.pnlEdit.Visible = true;

                    // Set visibility of edit and delete buttons
                    this.btnEdit.Visible = this.btnEdit.Visible && authorized;
                    this.btnDelete.Visible = this.btnDelete.Visible && this.AllowDelete && authorizedDelete;

                    if ((!RequestHelper.IsPostBack()) && ((this.btnEdit.Text.Trim() == string.Empty) || (this.btnDelete.Text.Trim() == string.Empty)))
                    {
                        // Initialize labels and css classes
                        this.btnEdit.ResourceString = this.EditButtonText;
                        this.btnEdit.CssClass = "EditContributionEdit";
                        this.btnDelete.ResourceString = this.DeleteButtonText;
                        this.btnDelete.CssClass = "EditContributionDelete";
                    }

                    if (this.pnlForm.Visible)
                    {
                        this.editForm.StopProcessing = false;
                        this.editForm.FunctionsPrefix = this.ID + "_";
                        this.editForm.AllowDelete = this.AllowDelete && CheckGroupPermission("deletepages");
                        this.editForm.CheckPermissions = this.CheckPermissions;
                        this.editForm.NodeID = node.NodeID;
                        this.editForm.SiteName = this.SiteName;
                        this.editForm.CultureCode = this.CultureCode;
                        this.editForm.AlternativeFormName = this.AlternativeFormName;
                        this.editForm.ValidationErrorMessage = this.ValidationErrorMessage;
                        this.editForm.CMSForm.IsLiveSite = true;

                        this.editForm.OnAfterApprove += editForm_OnAfterChange;
                        this.editForm.OnAfterReject += editForm_OnAfterChange;
                        this.editForm.OnAfterDelete += editForm_OnAfterChange;
                        this.editForm.CMSForm.OnAfterSave += CMSForm_OnAfterSave;
                        this.editForm.LogActivity = this.LogActivity;

                        // Reload data
                        this.editForm.ReloadData(false);
                    }
                }
            }
        }
    }


    /// <summary>
    /// Reloads the data.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();
        this.SetupControl();
    }


    /// <summary>
    /// On btnEdit click event handler.
    /// </summary>
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        // Close edit form
        if (this.pnlForm.Visible)
        {
            this.pnlForm.Visible = false;
            this.btnDelete.Visible = true;

            // Refresh current page
            URLHelper.Redirect(URLRewriter.RawUrl);
        }
        // Show edit form
        else
        {
            this.editForm.Action = "edit";
            this.pnlForm.Visible = true;
            this.btnDelete.Visible = false;

            this.btnEdit.ResourceString = this.CloseEditModeButtonText;
            this.btnEdit.CssClass = "EditContributionClose";
        }

        this.ReloadData();
    }


    /// <summary>
    /// On btnDelete click event handler.
    /// </summary>
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        // Close delete form
        if (this.pnlForm.Visible)
        {
            this.pnlForm.Visible = false;
            this.btnEdit.Visible = true;

            this.btnDelete.ResourceString = this.DeleteButtonText;
            this.btnDelete.CssClass = "EditContributionDelete";
            this.btnEdit.ResourceString = this.EditButtonText;
            this.btnEdit.CssClass = "EditContributionEdit";
        }
        // Show delete form
        else
        {
            this.editForm.Action = "delete";
            this.pnlForm.Visible = true;

            this.btnEdit.Visible = false;
            this.btnDelete.ResourceString = this.CloseEditModeButtonText;
            this.btnDelete.CssClass = "EditContributionClose";
        }

        this.ReloadData();
    }


    /// <summary>
    /// Returns true if group permissions should be checked and specified permission is allowed in current group.
    /// Also returns true if group permissions should not be checked.
    /// </summary>
    /// <param name="permissionName">Permission to check (createpages, editpages, deletepages)</param>
    protected bool CheckGroupPermission(string permissionName)
    {
        if (this.CheckGroupPermissions)
        {
            if (CommunityContext.CurrentGroup != null)
            {
                return (GroupInfoProvider.CheckPermission(permissionName, CommunityContext.CurrentGroup.GroupID) || CMSContext.CurrentUser.IsGroupAdministrator(CommunityContext.CurrentGroup.GroupID));
            }

            return false;
        }

        return true;
    }


    /// <summary>
    /// EditForm after change event handler.
    /// </summary>
    void editForm_OnAfterChange(object sender, EventArgs e)
    {
        CMSForm_OnAfterSave(sender, e);
    }


    /// <summary>
    /// CMSForm after save event handler.
    /// </summary>
    void CMSForm_OnAfterSave(object sender, EventArgs e)
    {
        if (!this.StandAlone)
        {
            // Reload data after saving the document
            this.PagePlaceholder.ClearCache();
            this.PagePlaceholder.ReloadData();
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        if (!this.pnlEdit.Visible)
        {
            // Hide control
            this.Visible = false;
        }
        base.OnPreRender(e);
    }
}

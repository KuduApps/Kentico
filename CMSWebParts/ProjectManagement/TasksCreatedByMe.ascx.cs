using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.PortalControls;
using CMS.ProjectManagement;
using CMS.CMSHelper;
using CMS.SettingsProvider;
using CMS.UIControls;

public partial class CMSWebParts_ProjectManagement_TasksCreatedByMe : CMSAbstractWebPart
{
    #region "Properties"

    /// <summary>
    /// Gets or sets the sitenam.
    /// </summary>
    public string SiteName
    {
        get
        {
            string siteName = ValidationHelper.GetString(this.GetValue("SiteName"), String.Empty);
            if (String.Compare(siteName, "#currentsite#", true) == 0)
            {
                siteName = CMSContext.CurrentSiteName;
                this.SetValue("SiteName", siteName);
            }
            return siteName;
        }
        set
        {
            this.SetValue("SiteName", value);
            ucTasks.SiteName = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether paging should be used.
    /// </summary>
    public bool EnablePaging
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("EnablePaging"), false);
        }
        set
        {
            this.SetValue("EnablePaging", value);
            ucTasks.EnablePaging = value;
        }
    }


    /// <summary>
    /// Gets or sets the number of items per page.
    /// </summary>
    public int PageSize
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("PageSize"), 10);
        }
        set
        {
            this.SetValue("PageSize", value);
            ucTasks.PageSize = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether task actions should be enabled.
    /// </summary>
    public bool AllowTaskActions
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("AllowTaskActions"), false);
        }
        set
        {
            this.SetValue("AllowTaskActions", value);
            ucTasks.AllowTaskActions = value;
        }
    }


    /// <summary>
    /// Status display type.
    /// </summary>
    public StatusDisplayTypeEnum StatusDisplayType
    {
        get
        {
            return (StatusDisplayTypeEnum)ValidationHelper.GetInteger(GetValue("ShowStatusAs"), 0);
        }
        set
        {
            SetValue("ShowStatusAs", value);
            ucTasks.StatusDisplayType = value;
        }
    }


    /// <summary>
    /// Show overdue tasks.
    /// </summary>
    public bool ShowOverdueTasks
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("ShowOverdueTasks"), ucTasks.ShowOverdueTasks);
        }
        set
        {
            SetValue("ShowOverdueTasks", value);
            ucTasks.ShowOverdueTasks = value;
        }
    }


    /// <summary>
    /// Show overdue tasks.
    /// </summary>
    public bool ShowOnTimeTasks
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("ShowOnTimeTasks"), ucTasks.ShowOnTimeTasks);
        }
        set
        {
            SetValue("ShowOnTimeTasks", value);
            ucTasks.ShowOnTimeTasks = value;
        }
    }


    /// <summary>
    /// Show overdue tasks.
    /// </summary>
    public bool ShowPrivateTasks
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("ShowPrivateTasks"), ucTasks.ShowPrivateTasks);
        }
        set
        {
            SetValue("ShowPrivateTasks", value);
            ucTasks.ShowPrivateTasks = value;
        }
    }


    /// <summary>
    /// Show overdue tasks.
    /// </summary>
    public bool ShowFinishedTasks
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("ShowFinishedTasks"), ucTasks.ShowFinishedTasks);
        }
        set
        {
            SetValue("ShowFinishedTasks", value);
            ucTasks.ShowFinishedTasks = value;
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// On content loaded.
    /// </summary>
    public override void OnContentLoaded()
    {
        base.OnContentLoaded();
        SetupControl();
    }


    /// <summary>
    /// Setup control.
    /// </summary>
    protected void SetupControl()
    {
        if (this.StopProcessing)
        {
            return;
        }

        // Set display peoperties 
        ucTasks.ShowFinishedTasks = this.ShowFinishedTasks;
        ucTasks.ShowOnTimeTasks = this.ShowOnTimeTasks;
        ucTasks.ShowOverdueTasks = this.ShowOverdueTasks;
        ucTasks.ShowPrivateTasks = this.ShowPrivateTasks;
        ucTasks.StatusDisplayType = this.StatusDisplayType;
        ucTasks.TasksDisplayType = TasksDisplayTypeEnum.TasksOwnedByMe;
        ucTasks.AllowTaskActions = this.AllowTaskActions;
        ucTasks.EnablePaging = this.EnablePaging;
        ucTasks.PageSize = this.PageSize;
        ucTasks.SiteName = this.SiteName;

        // Register security handler
        ucTasks.OnCheckPermissionsExtended += new CMS.UIControls.CMSAdminControl.CheckPermissionsExtendedEventHandler(ucTasks_OnCheckPermissionsExtended);
    }


    /// <summary>
    /// Check permissions event handler.
    /// </summary>
    void ucTasks_OnCheckPermissionsExtended(string permissionType, string modulePermissionType, CMSAdminControl sender)
    {
        // No permissions by default
        sender.StopProcessing = true;
        // Current item ID
        int taskId = 0;

        // Check permission for delete task
        if (permissionType == ProjectManagementPermissionType.DELETE)
        {
            // Get list object
            CMSAdminListControl listControl = sender as CMSAdminListControl;
            // Check whether list object is defined
            if (listControl != null)
            {
                taskId = listControl.SelectedItemID;
            }
        }
        // Check permision for task modify
        else if (permissionType == ProjectManagementPermissionType.MODIFY)
        {
            // Get edit object 
            CMSAdminEditControl editControl = sender as CMSAdminEditControl;
            // Check whether edit control is defined
            if (editControl != null)
            {
                taskId = editControl.ItemID;
            }
        }

        // Check permissions only for existing tasks
        if (taskId > 0)
        {
            // If user has no permission for current action, display error message
            if (ProjectTaskInfoProvider.IsAuthorizedPerTask(taskId, permissionType, CMSContext.CurrentUser, CMSContext.CurrentSiteID))
            {
                sender.StopProcessing = false;
            }
            else
            {
                messageElem.Visible = true;
                messageElem.ErrorMessage = ResHelper.GetString("pm.project.permission");
            }
        }
    }


    /// <summary>
    /// Reloads the control data.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();
        SetupControl();
    }

    #endregion
}

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.PortalControls;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.ProjectManagement;
using CMS.SettingsProvider;

public partial class CMSWebParts_ProjectManagement_ProjectList : CMSAbstractWebPart
{
    #region "Variables"

    bool displayErrorMessage = false;

    #endregion 


    #region "Properties"

    /// <summary>
    /// Gets or sets the permission for creating new project.
    /// </summary>
    public string ProjectAccess
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("ProjectAccess"), ucProjectList.ProjectAccess);
        }
        set
        {
            this.SetValue("ProjectAccess", value);
            ucProjectList.ProjectAccess = value;
        }
    }


    /// <summary>
    /// Gest or sets the role names separated by semicolon which are authorized to create new project.
    /// </summary>
    public string AuthorizedRoles
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("AuthorizedRoles"), ucProjectList.AuthorizedRoles);
        }
        set
        {
            this.SetValue("AuthorizedRoles", value);
            ucProjectList.AuthorizedRoles = value;
        }
    }


    /// <summary>
    /// Show finished projects too.
    /// </summary>
    public bool ShowFinishedProjects
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("ShowFinishedProjects"), ucProjectList.ShowFinishedProjects);
        }
        set
        {
            this.SetValue("ShowFinishedProjects", value);
            ucProjectList.ShowFinishedProjects = value;
        }
    }


    /// <summary>
    /// If true records are paged.
    /// </summary>
    public bool EnablePaging
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("EnablePaging"), ucProjectList.EnablePaging);
        }
        set
        {
            this.SetValue("EnablePaging", value);
            ucProjectList.EnablePaging = value;
        }
    }


    /// <summary>
    /// Number of items per page.
    /// </summary>
    public int PageSize
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("PageSize"), ucProjectList.PageSize);
        }
        set
        {
            this.SetValue("PageSize", value);
            ucProjectList.PageSize = value;
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Content loaded event handler.
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
        // Do nothing if stop processing is enabled
        if (this.StopProcessing)
        {
            this.Visible = false;
            return;
        }

        ucProjectList.DisplayMode = ControlDisplayModeEnum.Simple;
        ucProjectList.ShowFinishedProjects = this.ShowFinishedProjects;
        ucProjectList.EnablePaging = this.EnablePaging;
        ucProjectList.PageSize = this.PageSize;
        ucProjectList.IsLiveSite = true;
        ucProjectList.ProjectAccess = this.ProjectAccess;
        ucProjectList.AuthorizedRoles = this.AuthorizedRoles;
    }


    /// <summary>
    /// Reloads the control data.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();
        SetupControl();
    }


    /// <summary>
    /// Render override.
    /// </summary>
    /// <param name="writer">Writer</param>
    protected override void Render(HtmlTextWriter writer)
    {
        if (!displayErrorMessage)
        {
            messageElem.Visible = false;
        }

        base.Render(writer);
    }

    #endregion
}

using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.PortalControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSWebParts_DashBoard_EmailQueue : CMSAbstractWebPart
{
    #region "Properties"

    /// <summary>
    /// Site name (empty string for all sites).
    /// </summary>
    public string SiteName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("SiteName"), "").Replace("##currentsite##", CMSContext.CurrentSiteName);
        }
        set
        {
            this.SetValue("SiteName", value);
        }
    }


    /// <summary>
    /// Order by.
    /// </summary>
    public string OrderBy
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("OrderBy"), "EmailLastSendAttempt");
        }
        set
        {
            this.SetValue("OrderBy", value);
        }
    }


    /// <summary>
    /// Sorting.
    /// </summary>
    public string Sorting
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Sorting"), "desc");
        }
        set
        {
            this.SetValue("Sorting", value);
        }
    }


    /// <summary>
    /// Items per page.
    /// </summary>
    public string ItemsPerPage
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("ItemsPerPage"), "25");
        }
        set
        {
            this.SetValue("ItemsPerPage", value);
        }
    }

    #endregion


    #region "Stop processing"

    /// <summary>
    /// Returns true if the control processing should be stopped.
    /// </summary>
    public override bool StopProcessing
    {
        get
        {
            return base.StopProcessing;
        }
        set
        {
            base.StopProcessing = value;
            emailQueue.StopProcessing = value;
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
    /// Initializes the control properties.
    /// </summary>
    protected void SetupControl()
    {
        if (this.StopProcessing)
        {
            // Do not process
        }
        else
        {
            // Register the dialog script
            ScriptHelper.RegisterDialogScript(this.Page);

            // Register script for modal dialog
            ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "displayModal", ScriptHelper.GetScript(
                "function DisplayRecipients(emailId) { \n" +
                "    if ( emailId != 0 ) { \n" +
                "       modalDialog('" + URLHelper.ResolveUrl("~/CMSModules/EmailQueue/MassEmails_Recipients.aspx") + "?emailid=' + emailId, 'emailrecipients', 920, 700); \n" +
                "    } } \n "));

            if ((!RequestHelper.IsPostBack()) && (!string.IsNullOrEmpty(ItemsPerPage)))
            {
                emailQueue.EmailGrid.Pager.DefaultPageSize = ValidationHelper.GetInteger(ItemsPerPage, -1);
            }

            emailQueue.EmailGrid.OrderBy = OrderBy + " " + Sorting;
            emailQueue.EmailGrid.WhereCondition = GenerateWhereCondition();
            emailQueue.OnCheckPermissions += new CMSAdminControl.CheckPermissionsEventHandler(emailQueue_OnCheckPermissions);
        }
    }


    /// <summary>
    /// OnLoad handler.
    /// </summary>
    protected override void OnLoad(EventArgs e)
    {
        emailQueue.ReloadData();
        base.OnLoad(e);
    }


    /// <summary>
    /// Reloads the control data.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();
        SetupControl();
        emailQueue.ReloadData();
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        // Remove selection checkboxes
        emailQueue.EmailGrid.GridView.Columns[0].Visible = false;
    }


    /// <summary>
    /// Check permission.
    /// </summary>
    /// <param name="permissionType">Permission type</param>
    /// <param name="sender">Sender</param>
    private void emailQueue_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        if ((CMSContext.CurrentUser == null) || !CMSContext.CurrentUser.UserSiteManagerAdmin)
        {
            sender.StopProcessing = true;
            emailQueue.Visible = false;
            messageElem.Visible = true;
            messageElem.ErrorMessage = GetString("general.nopermission");
        }
    }


    /// <summary>
    /// Generates complete filter where condition.
    /// </summary>    
    private string GenerateWhereCondition()
    {
        string whereCond = "";

        // Append site condition if siteid given
        SiteInfo siteObj = SiteInfoProvider.GetSiteInfo(SiteName);
        int siteId = -1;

        if (siteObj != null)
        {
            siteId = siteObj.SiteID;
        }
        // create where condition for SiteID
        if (siteId > 0)
        {
            whereCond += " (EmailSiteID=" + siteId + ")";
        }

        return whereCond;
    }

    #endregion
}




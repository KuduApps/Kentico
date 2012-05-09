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

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.MediaLibrary;
using CMS.CMSHelper;
using CMS.EventLog;

public partial class CMSModules_MediaLibrary_Controls_UI_MediaLibraryList : CMSAdminListControl
{
    #region "Private variables"

    private int mGroupId = 0;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets group ID.
    /// </summary>
    public int GroupID
    {
        get
        {
            return mGroupId;
        }
        set
        {
            mGroupId = value;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        RaiseOnCheckPermissions(CMSAdminControl.PERMISSION_READ, this);
        gridElem.IsLiveSite = this.IsLiveSite;
        gridElem.OnAction += new OnActionEventHandler(gridElem_OnAction);
        gridElem.WhereCondition = GetWhereCondition();
        gridElem.GroupObject = (GroupID > 0);
        gridElem.ZeroRowsText = GetString("general.nodatafound");
    }


    void gridElem_OnAction(string actionName, object actionArgument)
    {
        switch (actionName.ToLower())
        {
            case "edit":
                this.SelectedItemID = ValidationHelper.GetInteger(actionArgument, 0);
                this.RaiseOnEdit();
                break;

            case "delete":
                MediaLibraryInfo mli = MediaLibraryInfoProvider.GetMediaLibraryInfo(ValidationHelper.GetInteger(actionArgument, 0));
                // Check 'Manage' permission
                if (!MediaLibraryInfoProvider.IsUserAuthorizedPerLibrary(mli, CMSAdminControl.PERMISSION_MANAGE))
                {
                    this.lblError.Text = MediaLibraryHelper.GetAccessDeniedMessage(CMSAdminControl.PERMISSION_MANAGE);
                    this.lblError.Visible = true;
                    return;
                }
                try
                {
                    MediaLibraryInfoProvider.DeleteMediaLibraryInfo(ValidationHelper.GetInteger(actionArgument, 0));
                }
                catch (Exception ex)
                {
                    EventLogProvider eventLog = new EventLogProvider();
                    eventLog.LogEvent("Media library", "DELETEOBJ", ex, CMSContext.CurrentSiteID);

                    this.lblError.Text = ex.Message;
                    this.lblError.ToolTip = EventLogProvider.GetExceptionLogMessage(ex);
                    this.lblError.Visible = true;
                }
                break;
        }

        this.RaiseOnAction(actionName, actionArgument);
    }


    /// <summary>
    /// Returns proper where condition.
    /// </summary>
    private string GetWhereCondition()
    {
        // Filter by current site
        string whereCond = null;
        if (CMSContext.CurrentSite != null)
        {
            whereCond = "LibrarySiteID=" + CMSContext.CurrentSite.SiteID;
        }

        // Filter by group id if specified
        if (this.GroupID != 0)
        {
            if (whereCond != null)
            {
                whereCond += " AND ";
            }
            else
            {
                whereCond = String.Empty;
            }

            whereCond += "LibraryGroupID=" + this.GroupID;
        }
        else
        {
            whereCond += " AND (LibraryGroupID IS NULL OR LibraryGroupID = 0)";
        }
        return whereCond;
    }

    #endregion
}

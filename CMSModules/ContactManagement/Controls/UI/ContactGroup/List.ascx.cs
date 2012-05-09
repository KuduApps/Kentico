using System;
using System.Data;
using System.Collections;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.OnlineMarketing;
using CMS.SettingsProvider;
using CMS.SiteProvider;

public partial class CMSModules_ContactManagement_Controls_UI_ContactGroup_List : CMSAdminListControl
{
    #region "Variables"

    private int mSiteId = -1;
    private string mWhereCondition = null;
    private bool? mModifyGroupPermission;

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets permission for modifying a group.
    /// </summary>
    protected bool ModifyGroupPermission
    {
        get
        {
            return (bool)(mModifyGroupPermission ?? 
                (mModifyGroupPermission = ContactGroupHelper.AuthorizedModifyContactGroup(SiteID, false)));
        }
    }


    /// <summary>
    /// Inner grid.
    /// </summary>
    public UniGrid Grid
    {
        get
        {
            return this.gridElem;
        }
    }


    /// <summary>
    /// Indicates if the control should perform the operations.
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
            this.gridElem.StopProcessing = value;
        }
    }


    /// <summary>
    /// Indicates if the control is used on the live site.
    /// </summary>
    public override bool IsLiveSite
    {
        get
        {
            return base.IsLiveSite;
        }
        set
        {
            base.IsLiveSite = value;
            gridElem.IsLiveSite = value;
        }
    }


    /// <summary>
    /// Gets or sets the site id.
    /// </summary>
    public int SiteID
    {
        get
        {
            return mSiteId;
        }
        set
        {
            mSiteId = value;
        }
    }


    /// <summary>
    /// Additional WHERE condition to filter data.
    /// </summary>
    public string WhereCondition
    {
        get
        {
            return this.mWhereCondition;
        }
        set
        {
            this.mWhereCondition = value;
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Setup unigrid
        gridElem.OnAction += new OnActionEventHandler(gridElem_OnAction);
        gridElem.WhereCondition = GetWhereCondition();
        gridElem.WhereCondition = SqlHelperClass.AddWhereCondition(gridElem.WhereCondition, this.WhereCondition);
        gridElem.EditActionUrl = "Frameset.aspx?groupId={0}&siteId=" + this.SiteID;
        gridElem.ZeroRowsText = GetString("om.contactgroup.notfound");
        gridElem.OnBeforeDataReload += new OnBeforeDataReload(gridElem_OnBeforeDataReload);
        gridElem.OnExternalDataBound += new OnExternalDataBoundEventHandler(gridElem_OnExternalDataBound);

        if (ContactHelper.IsSiteManager)
        {
            gridElem.EditActionUrl = URLHelper.AddParameterToUrl(gridElem.EditActionUrl, "issitemanager", "1");
        }
    }


    object gridElem_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        ImageButton btn = null;

        switch (sourceName.ToLower())
        {
            case "delete":
                if (!ModifyGroupPermission)
                {
                    btn = (ImageButton)sender;
                    btn.Enabled = false;
                    btn.Attributes.Add("src", GetImageUrl("Design/Controls/UniGrid/Actions/DeleteDisabled.png"));
                }
                break;
        }

        return CMControlsHelper.UniGridOnExternalDataBound(sender, sourceName, parameter);
    }

    #endregion


    #region "Events"

    /// <summary>
    /// Unigrid OnBeforeDataReload event handler.
    /// </summary>
    void gridElem_OnBeforeDataReload()
    {
        gridElem.NamedColumns["sitename"].Visible = (SiteID == UniSelector.US_GLOBAL_OR_SITE_RECORD) || (SiteID == UniSelector.US_ALL_RECORDS);
    }


    /// <summary>
    /// Unigrid button clicked.
    /// </summary>
    protected void gridElem_OnAction(string actionName, object actionArgument)
    {
        if (actionName == "delete")
        {
            int groupId = ValidationHelper.GetInteger(actionArgument, 0);
            ContactGroupInfo cgi = ContactGroupInfoProvider.GetContactGroupInfo(groupId);

            // Check permission
            if ((cgi != null) && ContactGroupHelper.AuthorizedModifyContactGroup(cgi.ContactGroupSiteID, true))
            {
                // Delete contact group
                ContactGroupInfoProvider.DeleteContactGroupInfo(groupId);
            }
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Returns where condition for unigrid.
    /// </summary>
    protected string GetWhereCondition()
    {
        string result = null;

        // Filter site objects
        if (SiteID > 0)
        {
            result = "(ContactGroupSiteID = " + SiteID.ToString() + ")";
        }
        // Filter only global objects
        else if (SiteID == UniSelector.US_GLOBAL_RECORD)
        {
            result = "(ContactGroupSiteID IS NULL)";
        }
        else if (SiteID == UniSelector.US_GLOBAL_OR_SITE_RECORD)
        {
            result = "(ContactGroupSiteID IS NULL) OR (ContactGroupSiteID = " + CMSContext.CurrentSiteID + ")";
        }
        return result;
    }

    #endregion
}
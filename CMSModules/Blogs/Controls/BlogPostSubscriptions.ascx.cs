using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.Blogs;
using CMS.UIControls;
using CMS.SiteProvider;
using CMS.TreeEngine;
using CMS.URLRewritingEngine;
using CMS.DataEngine;
using CMS.SettingsProvider;

public partial class CMSModules_Blogs_Controls_BlogPostSubscriptions : CMSAdminControl
{
    #region "Variables"

    private int mUserId = 0;
    private int mSiteId = 0;
    private int mDisplayNameLength = 50;
    private string mSiteName;

    #endregion


    #region "Properties"

    /// <summary>
    /// Maximum length of the displayname (whole display name is displayed in tooltip).
    /// </summary>
    public int DisplayNameLength
    {
        get
        {
            return this.mDisplayNameLength;
        }
        set
        {
            this.mDisplayNameLength = value;
        }
    }


    /// <summary>
    /// Gets or sets user ID.
    /// </summary>
    public int UserID
    {
        get
        {
            return this.mUserId;
        }
        set
        {
            this.mUserId = value;
        }
    }


    /// <summary>
    /// Gets or sets site ID.
    /// </summary>
    public int SiteID
    {
        get
        {
            return this.mSiteId;
        }
        set
        {
            this.mSiteId = value;
        }
    }


    /// <summary>
    /// If true, control does not process the data.
    /// </summary>
    public override bool StopProcessing
    {
        get
        {
            return ValidationHelper.GetBoolean(ViewState["StopProcessing"], false);
        }
        set
        {
            ViewState["StopProcessing"] = value;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        SetupControl();
    }


    protected void SetupControl()
    {
        // In design mode is pocessing of control stoped
        if (this.StopProcessing)
        {
            // Do nothing
        }
        else
        {
            if (CMSContext.CurrentUser.UserID == this.UserID)
            {
                this.gridElem.ZeroRowsText = GetString("blogsubscripitons.userhasnosubscriptions");
            }
            else
            {
                this.gridElem.ZeroRowsText = GetString("blogsubscripitons.NoDataUser");
            }
            this.gridElem.IsLiveSite = this.IsLiveSite;
            this.gridElem.OnAction += new OnActionEventHandler(gridElem_OnAction);
            this.gridElem.OnExternalDataBound += new OnExternalDataBoundEventHandler(gridElem_OnExternalDataBound);
            this.gridElem.OnDataReload += new OnDataReloadEventHandler(gridElem_OnDataReload);
            gridElem.ShowActionsMenu = true;
            gridElem.Columns = "SubscriptionID, SubscriptionEmail, DocumentName, NodeAliasPath, DocumentCulture";

            // Get all possible columns to retrieve
            IDataClass nodeClass = DataClassFactory.NewDataClass("CMS.Tree");
            DocumentInfo di = new DocumentInfo();
            BlogPostSubscriptionInfo bpsi = new BlogPostSubscriptionInfo();
            gridElem.AllColumns = SqlHelperClass.MergeColumns(SqlHelperClass.MergeColumns(SqlHelperClass.MergeColumns(bpsi.ColumnNames.ToArray()), SqlHelperClass.MergeColumns(di.ColumnNames.ToArray())), SqlHelperClass.MergeColumns(nodeClass.ColumnNames.ToArray()));

            mSiteName = SiteInfoProvider.GetSiteName(SiteID);
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        if (DataHelper.DataSourceIsEmpty(this.gridElem.GridView.DataSource))
        {
            this.lblMessage.Visible = false;
        }
    }


    /// <summary>
    /// Overriden SetValue - because of subscriptions control.
    /// </summary>
    /// <param name="propertyName">Name of the property to set</param>
    /// <param name="value">Value to set</param>
    public override void SetValue(string propertyName, object value)
    {
        base.SetValue(propertyName, value);

        switch (propertyName.ToLower())
        {
            case "userid":
                this.UserID = ValidationHelper.GetInteger(value, 0);
                break;
            case "siteid":
                this.SiteID = ValidationHelper.GetInteger(value, 0);
                break;
            case "islivesite":
                IsLiveSite = ValidationHelper.GetBoolean(value, true);
                break;
        }
    }


    /// <summary>
    /// Reloads data for unigrid.
    /// </summary>
    public override void ReloadData()
    {
        this.gridElem.ReloadData();
    }


    #region "UniGrid events"

    protected DataSet gridElem_OnDataReload(string completeWhere, string currentOrder, int currentTopN, string columns, int currentOffset, int currentPageSize, ref int totalRecords)
    {
        return BlogPostSubscriptionInfoProvider.GetBlogPostSubscriptions(UserID, SiteID, completeWhere, currentTopN, currentOrder, columns, currentOffset, currentPageSize, ref totalRecords);
    }


    /// <summary>
    /// Handles the UniGrid's OnAction event.
    /// </summary>
    /// <param name="actionName">Name of item (button) that threw event</param>
    /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
    protected void gridElem_OnAction(string actionName, object actionArgument)
    {
        if (actionName == "delete")
        {
            if (RaiseOnCheckPermissions(CMSAdminControl.PERMISSION_MANAGE, this))
            {
                if (this.StopProcessing)
                {
                    return;
                }
            }

            try
            {
                // Try to delete notification subscription
                BlogPostSubscriptionInfoProvider.DeleteBlogPostSubscriptionInfo(ValidationHelper.GetInteger(actionArgument, 0));
            }
            catch (Exception ex)
            {
                lblError.Visible = true;
                lblError.Text = ex.Message;
            }
        }
    }


    /// <summary>
    /// Handles the UniGrid's OnAction event.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="sourceName"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    object gridElem_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName.ToLower())
        {
            case "documentname":
                DataRowView dr = (DataRowView)parameter;

                if (CMSContext.CurrentSite != null)
                {
                    string url = ResolveUrl(TreePathUtils.GetUrl(ValidationHelper.GetString(dr["NodeAliasPath"], ""), null, mSiteName));
                    string lang = ValidationHelper.GetString(dr["DocumentCulture"], "");
                    if (!String.IsNullOrEmpty(lang))
                    {
                        url += "?" + URLHelper.LanguageParameterName + "=" + lang;
                    }

                    return "<a target=\"_blank\" href=\"" + url + "\">" + HTMLHelper.HTMLEncode(ValidationHelper.GetString(dr["DocumentName"], "")) + "</a>";
                }
                else
                {
                    return HTMLHelper.HTMLEncode(ValidationHelper.GetString(dr["DocumentName"], ""));
                }
        }

        return parameter;
    }

    #endregion
}

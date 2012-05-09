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

using CMS.UIControls;
using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.MessageBoard;
using CMS.TreeEngine;
using CMS.URLRewritingEngine;
using CMS.SettingsProvider;
using CMS.SiteProvider;

public partial class CMSModules_MessageBoards_Controls_Boards_BoardUserSubscriptions : CMSAdminControl
{
    private int mSiteId = 0;
    private int mUserId = 0;
    private string mSiteName;

    #region "Public properties"

    /// <summary>
    /// Site ID.
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
    /// User ID.
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

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.StopProcessing || !this.Visible)
        {
            this.EnableViewState = false;
            this.boardSubscriptions.StopProcessing = true;
            return;
        }

        // If control should be hidden save view state memory
        if (!this.Visible)
        {
            this.EnableViewState = false;
        }

        // Initialize controls
        SetupControls();
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        if (DataHelper.DataSourceIsEmpty(this.boardSubscriptions.GridView.DataSource))
        {
            this.lblMessage.Visible = false;
        }
    }


    #region "Private methods"

    /// <summary>
    /// Initializes controls on the page.
    /// </summary>
    private void SetupControls()
    {
        if (this.UserID > 0)
        {
            this.boardSubscriptions.Visible = true;

            if (CMSContext.CurrentUser.UserID == this.UserID)
            {
                this.boardSubscriptions.ZeroRowsText = GetString("boardsubscripitons.userhasnosubscriptions");
            }
            else
            {
                this.boardSubscriptions.ZeroRowsText = GetString("boardsubscripitons.NoDataUser");
            }

            // Setup UniGrid control     
            this.boardSubscriptions.IsLiveSite = this.IsLiveSite;
            this.boardSubscriptions.Pager.DefaultPageSize = 10;
            this.boardSubscriptions.OnAction += new OnActionEventHandler(boardSubscriptions_OnAction);
            this.boardSubscriptions.OnExternalDataBound += new OnExternalDataBoundEventHandler(boardSubscriptions_OnExternalDataBound);
            boardSubscriptions.OnDataReload += new OnDataReloadEventHandler(boardSubscriptions_OnDataReload);
            boardSubscriptions.ShowActionsMenu = true;
            BoardSubscriptionInfo bsi = new BoardSubscriptionInfo();
            boardSubscriptions.AllColumns = SqlHelperClass.MergeColumns(SqlHelperClass.MergeColumns(bsi.ColumnNames.ToArray()), "BoardID, BoardDisplayName, BoardSiteID, NodeAliasPath, DocumentCulture");

            mSiteName = SiteInfoProvider.GetSiteName(SiteID);
        }
        else
        {
            this.boardSubscriptions.Visible = false;
        }
    }


    #endregion


    #region "UniGrid events handling"

    protected DataSet boardSubscriptions_OnDataReload(string completeWhere, string currentOrder, int currentTopN, string columns, int currentOffset, int currentPageSize, ref int totalRecords)
    {
        DataSet ds = BoardSubscriptionInfoProvider.GetSubscriptions(this.UserID, this.SiteID, currentTopN);
        totalRecords = DataHelper.GetItemsCount(ds);
        return ds;
    }


    /// <summary>
    /// On action event handling.
    /// </summary>
    /// <param name="actionName"></param>
    /// <param name="actionArgument"></param>
    protected void boardSubscriptions_OnAction(string actionName, object actionArgument)
    {
        switch (actionName.ToLower())
        {
            case "delete":

                if (RaiseOnCheckPermissions(CMSAdminControl.PERMISSION_MANAGE, this))
                {
                    if (this.StopProcessing)
                    {
                        return;
                    }
                }

                try
                {
                    BoardSubscriptionInfoProvider.DeleteBoardSubscriptionInfo(ValidationHelper.GetInteger(actionArgument, 0));
                }
                catch (Exception ex)
                {
                    lblError.Visible = true;
                    lblError.Text = ex.Message;
                }

                break;

            default:
                break;
        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="sourceName"></param>
    /// <param name="parameter"></param>
    object boardSubscriptions_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName.ToLower())
        {
            case "displayname":
                DataRowView dr = (DataRowView)parameter;
                string url = ResolveUrl(TreePathUtils.GetUrl(ValidationHelper.GetString(dr["NodeAliasPath"], ""), null, mSiteName));
                string lang = ValidationHelper.GetString(dr["DocumentCulture"], "");
                if (!String.IsNullOrEmpty(lang))
                {
                    url += "?" + URLHelper.LanguageParameterName + "=" + lang;
                }

                return "<a target=\"_blank\" href=\"" + url + "\">" + HTMLHelper.HTMLEncode(ValidationHelper.GetString(dr["BoardDisplayName"], "")) + "</a>";
        }

        return parameter;
    }


    public override void SetValue(string propertyName, object value)
    {
        base.SetValue(propertyName, value);

        if (propertyName != null)
        {
            switch (propertyName.ToLower())
            {
                case "siteid":
                    this.SiteID = ValidationHelper.GetInteger(value, 0);
                    break;
                case "userid":
                    this.UserID = ValidationHelper.GetInteger(value, 0);
                    break;
                case "islivesite":
                    IsLiveSite = ValidationHelper.GetBoolean(value, true);
                    break;
            }
        }
    }

    #endregion
}

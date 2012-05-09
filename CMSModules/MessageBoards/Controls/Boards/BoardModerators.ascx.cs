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
using CMS.MessageBoard;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.ExtendedControls;
using CMS.PortalEngine;
using CMS.SettingsProvider;

public partial class CMSModules_MessageBoards_Controls_Boards_BoardModerators : CMSAdminEditControl
{
    #region "Variables"

    protected int mBoardID = 0;
    protected BoardInfo mBoard = null;
    private string currentValues = String.Empty;
    private bool canModify = false;

    private bool mShouldReloadData = false;

    #endregion


    #region "Properites"

    /// <summary>
    /// ID of the current message board.
    /// </summary>
    public int BoardID
    {
        get
        {
            if (mBoard != null)
            {
                return mBoard.BoardID;
            }

            return mBoardID;
        }
        set
        {
            mBoardID = value;

            mBoard = null;
        }
    }


    /// <summary>
    /// Current message board info object.
    /// </summary>
    public BoardInfo Board
    {
        get
        {
            return mBoard ?? (mBoard = BoardInfoProvider.GetBoardInfo(BoardID));
        }
        set
        {
            mBoard = value;

            mBoardID = 0;
        }
    }

    #endregion


    #region "Private properties"

    /// <summary>
    /// Indicates whether the data should be reloaded on PreRender.
    /// </summary>
    private bool ShouldReloadData
    {
        get
        {
            return this.mShouldReloadData;
        }
        set
        {
            this.mShouldReloadData = value;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        // Register script for pendingCallbacks repair
        ScriptHelper.FixPendingCallbacks(this.Page);

        // Initializes the controls
        SetupControls();
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        // Reload data if necessary
        if (this.ShouldReloadData || (!URLHelper.IsPostback() && !this.IsLiveSite))
        {
            this.currentValues = "";
            userSelector.CurrentValues = GetModerators();

            ReloadData();
        }

        if (Board != null)
        {
            this.userSelector.Enabled = Board.BoardModerated && canModify;
            this.chkBoardModerated.Enabled = canModify;
        }
    }


    private void SetupControls()
    {
        // Get resource strings
        this.lblModerators.Text = GetString("board.moderators.title") + ResHelper.Colon;
        this.chkBoardModerated.Text = GetString("board.moderators.ismoderated");
        this.userSelector.CurrentSelector.OnSelectionChanged += new EventHandler(CurrentSelector_OnSelectionChanged);

        if (BoardID > 0)
        {
            EditedObject = Board;
        }

        if (Board != null)
        {
            // Check permissions
            if (Board.BoardGroupID > 0)
            {
                canModify = CMSContext.CurrentUser.IsAuthorizedPerResource("cms.groups", CMSAdminControl.PERMISSION_MANAGE);
            }
            else
            {
                canModify = CMSContext.CurrentUser.IsAuthorizedPerResource("cms.messageboards", CMSAdminControl.PERMISSION_MODIFY);
            }

            userSelector.BoardID = this.BoardID;
            userSelector.GroupID = Board.BoardGroupID;
            userSelector.CurrentSelector.SelectionMode = SelectionModeEnum.Multiple;
            userSelector.ShowSiteFilter = false;
            userSelector.SiteID = CMSContext.CurrentSiteID;
            userSelector.CurrentValues = GetModerators();
            userSelector.IsLiveSite = this.IsLiveSite;
        }
    }


    /// <summary>
    /// Reloads form data.
    /// </summary>
    public override void ReloadData()
    {
        ReloadData(true);
    }

    /// <summary>
    /// Reloads form data.
    /// </summary>
    public override void ReloadData(bool forceReload)
    {
        base.ReloadData(forceReload);

        // Get board info
        if (Board != null)
        {
            chkBoardModerated.Checked = Board.BoardModerated;
            if (forceReload)
            {
                if (!String.IsNullOrEmpty(currentValues))
                {
                    string where = SqlHelperClass.AddWhereCondition(this.userSelector.CurrentSelector.WhereCondition, "UserID IN (" + this.currentValues.Replace(';', ',') + ")", "OR");
                    this.userSelector.CurrentSelector.WhereCondition = where;
                }

                userSelector.CurrentSelector.Value = GetModerators();
                userSelector.ReloadData();
            }
        }
    }


    /// <summary>
    /// Returns ID of users who are moderators to this board.
    /// </summary>
    protected string GetModerators()
    {
        if (String.IsNullOrEmpty(currentValues))
        {
            // Get all message board moderators
            DataSet ds = BoardModeratorInfoProvider.GetBoardModerators(this.BoardID, "UserID", null, null, 0);
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                currentValues = TextHelper.Join(";", SqlHelperClass.GetStringValues(ds.Tables[0], "UserID"));
            }
        }

        return currentValues;
    }


    /// <summary>
    /// Board moderated checkbox change.
    /// </summary>
    protected void chkBoardModerated_CheckedChanged(object sender, EventArgs e)
    {
        if (!canModify)
        {
            return;
        }

        if (Board != null)
        {
            Board.BoardModerated = chkBoardModerated.Checked;
            BoardInfoProvider.SetBoardInfo(Board);

            lblInfo.Visible = true;
            lblInfo.Text = GetString("General.Changessaved");

            this.ShouldReloadData = true;
        }
    }


    void CurrentSelector_OnSelectionChanged(object sender, EventArgs e)
    {
        this.ShouldReloadData = true;
    }
}

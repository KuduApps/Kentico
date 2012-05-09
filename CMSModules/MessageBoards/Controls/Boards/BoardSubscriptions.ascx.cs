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

public partial class CMSModules_MessageBoards_Controls_Boards_BoardSubscriptions : CMSAdminListControl
{
    // Current board ID
    private int mBoardId = 0;
    private BoardInfo mBoard = null;
    private int mGroupId = 0;
    private bool mChangeMaster = false;

    #region "Public properties"

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

            return mBoardId;
        }
        set
        {
            mBoardId = value;

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

            mBoardId = 0;
        }
    }


    /// <summary>
    /// ID of the current group.
    /// </summary>
    public int GroupID
    {
        get
        {
            return this.mGroupId;
        }
        set
        {
            this.mGroupId = value;
        }
    }


    /// <summary>
    /// Gets or sets to change master according to tab level.
    /// </summary>
    public bool ChangeMaster
    {
        get
        {
            return this.mChangeMaster;
        }
        set
        {
            this.mChangeMaster = value;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        // Initialize controls
        SetupControl();
    }


    /// <summary>
    /// Reloads the control content.
    /// </summary>
    public override void ReloadData()
    {
        this.boardSubscriptions.ReloadData();
    }


    #region "Private methods"

    /// <summary>
    /// Initializes controls on the page.
    /// </summary>
    private void SetupControl()
    {
        this.boardSubscriptions.OnAction += new OnActionEventHandler(boardSubscriptions_OnAction);
        this.boardSubscriptions.OnExternalDataBound += new OnExternalDataBoundEventHandler(boardSubscriptions_OnExternalDataBound);
        this.boardSubscriptions.IsLiveSite = this.IsLiveSite;
        this.boardSubscriptions.ZeroRowsText = GetString("general.nodatafound");

        // Get current board ID
        if (this.BoardID > 0)
        {
            this.boardSubscriptions.WhereCondition = "SubscriptionBoardID = " + this.BoardID;
            this.boardSubscriptions.Visible = true;
        }
        else
        {
            this.boardSubscriptions.Visible = false;
        }
    }

    #endregion


    #region "UniGrid events handling"

    protected object boardSubscriptions_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        // If parameter for binding supplied
        switch (sourceName.ToLower())
        {
            case "formattedusername":
                string userName = ValidationHelper.GetString(parameter, null);
                if (userName == null)
                {
                    return "-";
                }
                else
                {
                    return HTMLHelper.HTMLEncode(Functions.GetFormattedUserName(userName, this.IsLiveSite));
                }
        }

        return HTMLHelper.HTMLEncode(Convert.ToString(parameter));
    }


    protected void boardSubscriptions_OnAction(string actionName, object actionArgument)
    {
        // Get currently processed subscription ID
        int subscriptionId = ValidationHelper.GetInteger(actionArgument, 0);
        if (subscriptionId > 0)
        {
            switch (actionName.ToLower())
            {
                case "delete":
                    if (!CheckPermissions("cms.messageboards", CMSAdminControl.PERMISSION_MODIFY))
                    {
                        return;
                    }

                    // Remove subscription according current ID
                    BoardSubscriptionInfoProvider.DeleteBoardSubscriptionInfo(subscriptionId);
                    break;

                default:
                    break;
            }

            this.RaiseOnAction(actionName, actionArgument);
        }
    }

    #endregion
}

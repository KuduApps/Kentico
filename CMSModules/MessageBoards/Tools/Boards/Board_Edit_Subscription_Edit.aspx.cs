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
using CMS.CMSHelper;
using CMS.MessageBoard;
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSModules_MessageBoards_Tools_Boards_Board_Edit_Subscription_Edit : CMSMessageBoardBoardsPage
{
    private int mSubscriptionId = 0;
    private int mBoardId = 0;
    
    private bool changeMaster = false;

    BoardSubscriptionInfo mCurrentSubscription = null;


    protected override void OnPreInit(EventArgs e)
    {
        // External call
        changeMaster = QueryHelper.GetBoolean("changemaster", false);

        base.OnPreInit(e);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Initialize the controls
        SetupControl();
    }


    /// <summary>
    /// Initializes the controls on the page.
    /// </summary>
    private void SetupControl() 
    {
        // Get current subscription ID
        mSubscriptionId = QueryHelper.GetInteger("subscriptionid", 0);
        if (mSubscriptionId > 0)
        {
            mCurrentSubscription = BoardSubscriptionInfoProvider.GetBoardSubscriptionInfo(mSubscriptionId);
            EditedObject = mCurrentSubscription;
        }

        // Get current board ID
        mBoardId = QueryHelper.GetInteger("boardid", 0);

        this.boardSubscription.IsLiveSite = false;
        this.boardSubscription.BoardID = mBoardId;
        this.boardSubscription.SubscriptionID = mSubscriptionId;
        this.boardSubscription.OnSaved += new EventHandler(boardSubscription_OnSaved);

        InitializeBreadcrumbs();
    }


    protected void boardSubscription_OnSaved(object sender, EventArgs e)
    {
        URLHelper.Redirect("~/CMSModules/MessageBoards/Tools/Boards/Board_Edit_Subscription_Edit.aspx?subscriptionid=" + this.boardSubscription.SubscriptionID + "&boardid=" + this.mBoardId.ToString() + "&saved=1" + "&changemaster=" + this.changeMaster);
    }


    /// <summary>
    /// Initializes the breadcrumbs on the page.
    /// </summary>
    private void InitializeBreadcrumbs()
    {
        string[,] breadcrumbs = new string[2, 3];

        breadcrumbs[0, 0] = GetString("board.subscription.subscriptions");
        breadcrumbs[0, 1] = "~/CMSModules/MessageBoards/Tools/Boards/Board_Edit_Subscriptions.aspx?boardid=" + mBoardId.ToString() + "&changemaster=" + this.changeMaster;
        breadcrumbs[0, 2] = "_self";

        // Display current subscription e-mail
        if (mCurrentSubscription != null)
        {
            breadcrumbs[1, 0] = HTMLHelper.HTMLEncode(mCurrentSubscription.SubscriptionEmail);
        }
        else 
        {
            breadcrumbs[1, 0] = GetString("board.subscriptions.newitem");
        }
        breadcrumbs[1, 1] = "";
        breadcrumbs[1, 2] = "";

        this.CurrentMaster.Title.Breadcrumbs = breadcrumbs;
    }
}

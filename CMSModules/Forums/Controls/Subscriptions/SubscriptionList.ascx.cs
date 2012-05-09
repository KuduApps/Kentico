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
using CMS.LicenseProvider;
using CMS.Forums;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSModules_Forums_Controls_Subscriptions_SubscriptionList : CMSAdminListControl
{
    #region "Variables"

    private int mForumId;
    private bool process = true;
    private bool reloadData = false;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets the ID of the forum to edit.
    /// </summary>
    public int ForumID
    {
        get
        {
            return this.mForumId;
        }
        set
        {
            this.mForumId = value;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        process = true;
        if (!this.Visible || StopProcessing)
        {
            this.EnableViewState = false;
            process = false;
        }

        gridElem.IsLiveSite = this.IsLiveSite;
        gridElem.OnAction += new OnActionEventHandler(gridElem_OnAction);
        gridElem.OnBeforeSorting += new OnBeforeSorting(gridElem_OnBeforeSorting);
        gridElem.ZeroRowsText = GetString("general.nodatafound");
        SetupUniGrid();
    }


    /// <summary>
    /// Reloads the grid data.
    /// </summary>
    public override void ReloadData()
    {
        reloadData = true;
    }


    /// <summary>
    /// Handles the UniGrid's OnAction event.
    /// </summary>
    /// <param name="actionName">Name of item (button) that throws event</param>
    /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
    protected void gridElem_OnAction(string actionName, object actionArgument)
    {
        switch (actionName.ToLower())
        {
            case "delete":
                if (!CheckPermissions("cms.forums", CMSAdminControl.PERMISSION_MODIFY))
                {
                    return;
                }

                // Delete ForumSubscriptionInfo object from database
                ForumSubscriptionInfoProvider.DeleteForumSubscriptionInfo(Convert.ToInt32(actionArgument), chkSendConfirmationEmail.Checked);

                break;
        }

        this.RaiseOnAction(actionName, actionArgument);
    }


    protected void gridElem_OnBeforeSorting(object sender, EventArgs e)
    {
        SetupUniGrid();
        gridElem.ReloadData();
    }


    /// <summary>
    /// Pre render.
    /// </summary>    
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if ((!this.IsLiveSite && !RequestHelper.IsPostBack() && process) || reloadData)
        {
            if (this.ForumID > 0)
            {
                this.gridElem.ReloadData();
            }

            reloadData = false;
        }

        // If data grid
        if (gridElem.GridView.Rows.Count <= 0)
        {
            pnlSendConfirmationEmail.Visible = false;
        }
    }

    private void SetupUniGrid()
    {
        if (this.ForumID > 0)
        {
            gridElem.ObjectType = "forums.forumsubscriptionwithpost";
        }

        QueryDataParameters parameters = new QueryDataParameters();
        parameters.Add("@ForumID", this.ForumID);

        gridElem.QueryParameters = parameters;
        gridElem.OrderBy = "SubscriptionEmail";
    }
}

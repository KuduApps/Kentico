using System;

using CMS.GlobalHelper;
using CMS.Polls;
using CMS.UIControls;

public partial class CMSModules_Polls_Controls_AnswerList : CMSAdminListControl
{
    #region "Properties"

    /// <summary>
    /// Gets and sets Poll ID.
    /// </summary>
    public int PollId
    {
        get
        {
            return ValidationHelper.GetInteger(ViewState[this.ClientID + "PollID"], 0);
        }
        set
        {
            ViewState[this.ClientID + "PollID"] = value;
        }
    }


    /// <summary>
    /// Indicates if DelayedReload for Unigrid should be used.
    /// </summary>
    public bool DelayedReload
    {
        get
        {
            return uniGrid.DelayedReload;
        }
        set
        {
            uniGrid.DelayedReload = value;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check if parent object exists
        if ((PollId > 0) && !IsLiveSite)
        {
            CMSPage.EditedObject = PollInfoProvider.GetPollInfo(PollId);
        }

        uniGrid.IsLiveSite = this.IsLiveSite;
        uniGrid.OnAction += new OnActionEventHandler(uniGrid_OnAction);
        uniGrid.GridView.AllowSorting = false;
        uniGrid.WhereCondition = "AnswerPollID=" + this.PollId;
        uniGrid.Columns = "AnswerID, AnswerText, AnswerCount, AnswerEnabled";
        uniGrid.ZeroRowsText = GetString("general.nodatafound");
        uniGrid.OnExternalDataBound += new OnExternalDataBoundEventHandler(UniGrid_OnExternalDataBound);
    }


    object UniGrid_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName.ToLower())
        {
            case "answerenabled":
                return UniGridFunctions.ColoredSpanYesNo(parameter);
        }
        return parameter;
    }


    /// <summary>
    /// Handles the UniGrid's OnAction event.
    /// </summary>
    /// <param name="actionName">Name of item (button) that throws event</param>
    /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
    protected void uniGrid_OnAction(string actionName, object actionArgument)
    {
        if (actionName == "edit")
        {
            this.SelectedItemID = Convert.ToInt32(actionArgument);
            this.RaiseOnEdit();
        }
        else if (actionName == "delete")
        {
            if (!CheckModifyPermission())
            {
                return;
            }

            // Delete PollAnswerInfo object from database
            PollAnswerInfoProvider.DeletePollAnswerInfo(Convert.ToInt32(actionArgument));
            this.ReloadData(true);
        }
        else if (actionName == "moveup")
        {
            if (!CheckModifyPermission())
            {
                return;
            }

            // Move the answer up in order
            PollAnswerInfoProvider.MoveAnswerUp(this.PollId, Convert.ToInt32(actionArgument));
            this.ReloadData(true);
        }
        else if (actionName == "movedown")
        {
            if (!CheckModifyPermission())
            {
                return;
            }

            // Move the answer down in order
            PollAnswerInfoProvider.MoveAnswerDown(this.PollId, Convert.ToInt32(actionArgument));
            this.ReloadData(true);
        }
    }


    /// <summary>
    /// Forces unigrid to reload data.
    /// </summary>
    public override void ReloadData(bool forceReload)
    {
        uniGrid.Query = "polls.pollanswer.selectall";
        uniGrid.WhereCondition = "AnswerPollID=" + this.PollId;

        if (forceReload)
        {
            uniGrid.WhereClause = null;
            uniGrid.ResetFilter();
        }

        uniGrid.ReloadData();
    }


    /// <summary>
    /// Checks modify permission. Returns false if checking failed.
    /// </summary>
    private bool CheckModifyPermission()
    {
        PollInfo pi = PollInfoProvider.GetPollInfo(this.PollId);
        if (pi != null)
        {
            return (pi.PollSiteID > 0) && CheckPermissions("cms.polls", CMSAdminControl.PERMISSION_MODIFY) ||
                (pi.PollSiteID <= 0) && CheckPermissions("cms.polls", CMSAdminControl.PERMISSION_GLOBALMODIFY);
        }
        return false;
    }
    #endregion
}

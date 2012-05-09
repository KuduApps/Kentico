using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.OnlineMarketing;
using CMS.SettingsProvider;
using CMS.UIControls;

public partial class CMSModules_Scoring_Controls_UI_Score_List : CMSAdminListControl
{
    #region "Properties"

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

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check read permission
        if (!CheckPermissions("cms.scoring", "Read"))
        {
            return;
        }

        // Setup unigrid
        gridElem.OnAction += new OnActionEventHandler(gridElem_OnAction);
        gridElem.WhereCondition = SqlHelperClass.AddWhereCondition(gridElem.WhereCondition, "ScoreSiteID=" + CMSContext.CurrentSiteID);
        gridElem.OnExternalDataBound += new OnExternalDataBoundEventHandler(gridElem_OnExternalDataBound);
        gridElem.ZeroRowsText = GetString("om.score.notfound");
    }

    #endregion


    #region "Events"

    /// <summary>
    /// Unigrid on external data bound event handler.
    /// </summary>
    protected object gridElem_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName.ToLower())
        {
            case "enabled":
                return UniGridFunctions.ColoredSpanYesNo(parameter);
        }
        return parameter;
    }


    /// <summary>
    /// Unigrid button clicked.
    /// </summary>
    protected void gridElem_OnAction(string actionName, object actionArgument)
    {
        if (actionName == "delete")
        {
            // Check modify permission
            if (!CheckPermissions("cms.scoring", "Modify"))
            {
                return;
            }

            int scoreId = ValidationHelper.GetInteger(actionArgument, 0);
            // Delete score
            ScoreInfoProvider.DeleteScoreInfo(scoreId);
        }
    }

    #endregion
}
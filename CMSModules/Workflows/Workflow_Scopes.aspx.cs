using System;
using System.Data;
using System.Web.UI.WebControls;

using CMS.LicenseProvider;
using CMS.SettingsProvider;
using CMS.WorkflowEngine;
using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSModules_Workflows_Workflow_Scopes : SiteManagerPage
{
    #region "Protected vairables"

    protected int workflowId = 0;
    protected int siteId = 0;

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        UniGridWorkflowScopes.OnAction += UniGridRoles_OnAction;
        UniGridWorkflowScopes.OnExternalDataBound += UniGridWorkflowScopes_OnExternalDataBound;
        UniGridWorkflowScopes.ZeroRowsText = GetString("general.nodatafound");

        // Initialize the master page elements
        InitializeMasterPage();

        // Set site selector
        siteSelector.DropDownSingleSelect.AutoPostBack = true;
        siteSelector.AllowAll = false;
        siteSelector.OnlyRunningSites = false;
        siteSelector.UniSelector.OnSelectionChanged += UniSelector_OnSelectionChanged;

        // Set sitedi from query
        siteId = QueryHelper.GetInteger("siteid", 0);

        if (!RequestHelper.IsPostBack())
        {
            if (siteId <= 0)
            {
                // Preselect current site
                siteId = CMSContext.CurrentSiteID;
            }
            siteSelector.Value = siteId;
        }
        else
        {
            siteId = ValidationHelper.GetInteger(siteSelector.Value, 0);
        }

        workflowId = QueryHelper.GetInteger("workflowid", 0);

        // Set unigrid where condition
        UniGridWorkflowScopes.WhereCondition = GetUniGridWhereCondition();

        // Set default sorting
        if (!RequestHelper.IsPostBack())
        {
            UniGridWorkflowScopes.OrderBy = "ScopeStartingPath";
        }

        // Register script for new item
        ScriptHelper.RegisterClientScriptBlock(Page, typeof(string), "AddNewItem", ScriptHelper.GetScript(
            "function AddNewItem() { var drpElem = document.getElementById('" + siteSelector.DropDownSingleSelect.ClientID + "'); this.window.location = '" + ResolveUrl("~/CMSModules/Workflows/Workflow_Scope_Edit.aspx?workflowid=" + workflowId) + "&siteid='+drpElem.value;} "));
    }


    /// <summary>
    /// Handles site selection change event.
    /// </summary>
    protected void UniSelector_OnSelectionChanged(object sender, EventArgs e)
    {
        pnlUpdate.Update();
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        // If ID of site is still not set
        if (siteId <= 0)
        {
            // Force reload site selector
            siteSelector.Reload(true);
            // Gain site id
            siteId = ValidationHelper.GetInteger(siteSelector.DropDownSingleSelect.SelectedValue, 0);
            // Build where condition and reload unigrid
            UniGridWorkflowScopes.WhereCondition = GetUniGridWhereCondition();
            UniGridWorkflowScopes.ReloadData();
        }
        // Hide culture column due to license restrictions
        if ((LicenseHelper.CurrentEdition != ProductEditionEnum.Enterprise) && (LicenseHelper.CurrentEdition != ProductEditionEnum.Ultimate) && (LicenseHelper.CurrentEdition != ProductEditionEnum.EnterpriseMarketingSolution))
        {
            if (UniGridWorkflowScopes.GridView.Columns.Count >= 2)
            {
                DataControlField cultureColumn = UniGridWorkflowScopes.GridView.Columns[UniGridWorkflowScopes.GridView.Columns.Count - 2];
                cultureColumn.Visible = false;
            }
        }
    }


    protected object UniGridWorkflowScopes_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName.ToLower())
        {
            case "classdisplayname":
                string docType = ValidationHelper.GetString(parameter, "");
                if (docType == "")
                {
                    return GetString("general.selectall");
                }
                return docType;

            case "scopecultureid":
                int cultureId = ValidationHelper.GetInteger(parameter, 0);
                if (cultureId > 0)
                {
                    return CultureInfoProvider.GetCultureInfo(cultureId).CultureName;
                }
                else
                {
                    return GetString("general.selectall");
                }

            default:
                return parameter;
        }
    }


    /// <summary>
    /// Initializes the master page elements.
    /// </summary>
    private void InitializeMasterPage()
    {
        // Control initialization
        string[,] actions = new string[1, 8];
        actions[0, 0] = "HyperLink";
        actions[0, 1] = GetString("Development-Workflow_Scopes.NewScope");
        actions[0, 3] = "javascript: AddNewItem();";
        actions[0, 5] = GetImageUrl("Objects/CMS_WorkflowScope/add.png");

        CurrentMaster.HeaderActions.Actions = actions;
        CurrentMaster.DisplaySiteSelectorPanel = true;
    }


    /// <summary>
    /// Handles the UniGrid's OnAction event.
    /// </summary>
    /// <param name="actionName">Name of item (button) that threw event</param>
    /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
    protected void UniGridRoles_OnAction(string actionName, object actionArgument)
    {
        int workflowScopeId = Convert.ToInt32(actionArgument);
        if (actionName == "edit")
        {
            URLHelper.Redirect("Workflow_Scope_Edit.aspx?scopeid=" + workflowScopeId);
        }

        else if (actionName == "delete")
        {
            WorkflowScopeInfoProvider.DeleteWorkflowScopeInfo(workflowScopeId);
        }
    }


    protected string GetUniGridWhereCondition()
    {
        string where = SqlHelperClass.AddWhereCondition(string.Empty, "ScopeWorkflowID = " + workflowId);

        if (siteId > 0)
        {
            where = SqlHelperClass.AddWhereCondition(where, "ScopeSiteID = " + siteId);
        }
        return where;
    }
}
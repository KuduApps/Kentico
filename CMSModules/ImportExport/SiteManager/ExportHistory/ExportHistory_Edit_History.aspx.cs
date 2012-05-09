using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.CMSImportExport;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSModules_ImportExport_SiteManager_ExportHistory_ExportHistory_Edit_History : CMSImportExportPage
{
    #region "Page & control events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Set unigrid properties
        UniGrid.OnAction += uniGrid_OnAction;
        UniGrid.OnExternalDataBound += UniGrid_OnExternalDataBound;
        UniGrid.OnBeforeDataReload += new OnBeforeDataReload(UniGrid_OnBeforeDataReload);
        UniGrid.OrderBy = "ExportDateTime DESC";

        // Set master page properties
        CurrentMaster.Title.TitleText = GetString("ExportHistory.History");
        CurrentMaster.Title.HelpName = "helpTopic";
        CurrentMaster.Title.HelpTopicName = "exporthistory_history_tab";
        CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Export_History/object.png");
        CurrentMaster.DisplaySiteSelectorPanel = true;

        // Initialize javascripts
        ScriptHelper.RegisterDialogScript(this);
        lnkDeleteAll.Attributes.Add("onclick", "return confirm(" + ScriptHelper.GetString(GetString("exporthistory.deleteallhistoryconfirm")) + ");");

        // Load sites list
        LoadSites();
    }


    protected override void OnPreRender(EventArgs e)
    {        
        // Set visibility of delete button
        lnkDeleteAll.Visible = !DataHelper.DataSourceIsEmpty((DataSet)UniGrid.DataSource);
        base.OnPreRender(e);
    }


    protected void lnkDeleteAll_Click(object sender, EventArgs e)
    {
        int siteId = ValidationHelper.GetInteger(siteSelector.Value, 0);
        ExportHistoryInfoProvider.DeleteExportHistories(siteId);
        UniGrid.ReloadData();
        pnlUpdate.Update();
    }


    /// <summary>
    /// Handles site selection change event.
    /// </summary>
    protected void UniSelector_OnSelectionChanged(object sender, EventArgs e)
    {
        pnlUpdate.Update();
    }

    #endregion


    #region "UniGrid events"

    protected void UniGrid_OnBeforeDataReload()
    {
        // Where condition
        string where = null;

        // Site dropdownlist
        int siteId = ValidationHelper.GetInteger(siteSelector.Value, 0);
        if (siteId != 0)
        {
            where = "ExportSiteID = " + siteId;
        }
        else
        {
            where = "ExportSiteID IS NULL";
        }

        UniGrid.WhereCondition = where;
        
    }


    /// <summary>
    /// Handles the UniGrid's OnAction event.
    /// </summary>
    /// <param name="actionName">Name of item (button) that throws event</param>
    /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
    protected void uniGrid_OnAction(string actionName, object actionArgument)
    {
        if (actionName == "delete")
        {
            // delete ExportHistoryInfo object from database
            ExportHistoryInfoProvider.DeleteExportHistoryInfo(Convert.ToInt32(actionArgument));
        }
    }


    /// <summary>
    /// External data binding handler.
    /// </summary>
    protected object UniGrid_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName.ToLower())
        {
            case "userid":
                int userid = ValidationHelper.GetInteger(parameter, 0);
                if (userid > 0)
                {
                    return HTMLHelper.HTMLEncode(Functions.GetFormattedUserName(UserInfoProvider.GetUserNameById(userid)));
                }
                else
                {
                    return parameter;
                }
        }
        return parameter;
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Load list of sites.
    /// </summary>
    private void LoadSites()
    {
        int siteId = QueryHelper.GetInteger("siteid", 0);

        // Set site selector
        siteSelector.DropDownSingleSelect.AutoPostBack = true;
        siteSelector.AllowAll = false;
        siteSelector.UniSelector.SpecialFields = new string[1, 2] { { GetString("ExportConfiguration.NoSite"), "0" } };
        siteSelector.UniSelector.OnSelectionChanged += UniSelector_OnSelectionChanged;

        if (!RequestHelper.IsPostBack())
        {
            if (siteId != 0)
            {
                siteSelector.Value = siteId;
                siteSelector.Enabled = false;
            }
            else
            {
                siteSelector.Value = "0";
            }
        }
    }

    #endregion
}

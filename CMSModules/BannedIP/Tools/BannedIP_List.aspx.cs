using System;

using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.UIControls;

// Title
[Title("Objects/CMS_BannedIP/object.png", "banip.listHeaderCaption", "banip_list")]
// Actions
[Actions(1)]
[Action(0, "Objects/CMS_BannedIP/add.png", "banip.NewItemCaption", null, Javascript = "AddNewItem();")]
public partial class CMSModules_BannedIP_Tools_BannedIP_List : CMSBannedIPsPage
{
    #region "Page events"

    /// <summary>
    /// Page load.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (SiteID > 0)
        {
            // Hide site selector if accessing page from CMSDesk
            plcSites.Visible = false;
        }
        else
        {
            // Show contentplaceholder where site selector can be shown
            CurrentMaster.DisplaySiteSelectorPanel = true;

            // Set site selector
            siteSelector.DropDownSingleSelect.AutoPostBack = true;
            siteSelector.UniSelector.OnSelectionChanged += UniSelector_OnSelectionChanged;

            if (!RequestHelper.IsPostBack())
            {
                // Sites
                siteSelector.Value = SelectedSiteID;
            }
            else
            {
                SelectedSiteID = ValidationHelper.GetInteger(siteSelector.Value, 0);
            }
        }

        UniGrid.EditActionUrl = "BannedIP_Edit.aspx?itemid={0}&selectedsiteid=" + SelectedSiteID + "&siteId=" + SiteID;
        UniGrid.WhereCondition = GenerateWhereCondition();

        UniGrid.OnAction += uniGrid_OnAction;
        UniGrid.OnExternalDataBound += UniGrid_OnExternalDataBound;
        UniGrid.OnBeforeDataReload += UniGrid_OnBeforeDataReload;
    }


    /// <summary>
    /// Page pre render.
    /// </summary>
    protected void Page_PreRender(object sender, EventArgs e)
    {

        // Display information about disabled Banned IP module
        int currentSiteID = ((SiteID > 0) ? SiteID : SelectedSiteID);
        string siteName = SiteInfoProvider.GetSiteName(currentSiteID);

        ShowInformation("");

        if (!BannedIPInfoProvider.IsBannedIPEnabled(siteName))
        {
            ShowInformation(GetString((currentSiteID > 0) ? "banip.moduleDisabled" : "banip.moduleDisabledglobal"));
        }

        // Register correct script for new item
        ScriptHelper.RegisterClientScriptBlock(Page, typeof(string), "AddNewItem", ScriptHelper.GetScript(
            "function AddNewItem() { window.location = '" + ResolveUrl("BannedIP_Edit.aspx" + "?selectedsiteid=" + SelectedSiteID + "&siteId=" + SiteID) + "'} "));
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Generates where condition.
    /// </summary>    
    private string GenerateWhereCondition()
    {
        if (SiteID > 0)
        {
            return "IPAddressSiteID = " + SiteID;
        }
        else if (SelectedSiteID > 0)
        {
            return "IPAddressSiteID = " + SelectedSiteID;
        }
        else if (siteSelector.Value.ToString() == "0")
        {
            return "IPAddressSiteID IS NULL";
        }

        // (all)
        return String.Empty;
    }

    #endregion


    #region "UniSelector events"

    /// <summary>
    /// Handles site selection change event.
    /// </summary>
    protected void UniSelector_OnSelectionChanged(object sender, EventArgs e)
    {
        pnlUpdate.Update();
    }

    #endregion


    #region "UniGrid events"

    /// <summary>
    /// Handles unigrid external fields binding.
    /// </summary>
    protected object UniGrid_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName.ToLower())
        {
            case "bantype":
                // Ban type
                BanControlEnum banControl = BannedIPInfoProvider.GetBanControlEnum((string)parameter);
                return GetString("banip.bantype" + banControl.ToString());
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
        if (actionName == "delete")
        {
            // check 'Modify' permission
            CheckPermissions("Modify");

            // delete BannedIPInfo object from database
            BannedIPInfoProvider.DeleteBannedIPInfo(Convert.ToInt32(actionArgument));
        }
    }


    protected void UniGrid_OnBeforeDataReload()
    {
        // If page is viewed from CMSDesk hide Sitename column
        if ((SiteID > 0) || (SelectedSiteID > 0))
        {
            UniGrid.GridView.Columns[5].Visible = false;
        }
    }

    #endregion
}
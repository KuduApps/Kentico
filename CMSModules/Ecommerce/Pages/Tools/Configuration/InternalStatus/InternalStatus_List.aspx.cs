using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;

using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.Ecommerce;
using CMS.CMSHelper;
using CMS.UIControls;

public partial class CMSModules_Ecommerce_Pages_Tools_Configuration_InternalStatus_InternalStatus_List : CMSInternalStatusesPage
{
    #region "Page Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Pagetitle
        this.CurrentMaster.Title.TitleText = GetString("InternalStatus_List.HeaderCaption");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Ecommerce_InternalStatus/object.png");
        this.CurrentMaster.Title.HelpTopicName = "internal_status_list";
        this.CurrentMaster.Title.HelpName = "helpTopic";

        // New item link
        string[,] actions = new string[2, 9];
        actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
        actions[0, 1] = GetString("InternalStatus_List.NewItemCaption");
        actions[0, 2] = null;
        actions[0, 3] = ResolveUrl("InternalStatus_Edit.aspx?siteId=" + SiteID);
        actions[0, 4] = null;
        actions[0, 5] = GetImageUrl("Objects/Ecommerce_InternalStatus/add.png");

        // Show copy from global link when not configuring global statuses.
        if (ConfiguredSiteID != 0)
        {
            // Show "Copy from global" link only if there is at least one global status
            DataSet ds = InternalStatusInfoProvider.GetInternalStatuses("InternalStatusSiteID IS NULL", null, 1, "InternalStatusSiteID");
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                actions[1, 0] = HeaderActions.TYPE_SAVEBUTTON;
                actions[1, 1] = GetString("general.copyfromglobal");
                actions[1, 2] = "return ConfirmCopyFromGlobal();";
                actions[1, 3] = null;
                actions[1, 4] = null;
                actions[1, 5] = GetImageUrl("Objects/Ecommerce_InternalStatus/fromglobal.png");
                actions[1, 6] = "copyFromGlobal";
                actions[1, 7] = String.Empty;
                actions[1, 8] = true.ToString();

                // Register javascript to confirm generate 
                string script = "function ConfirmCopyFromGlobal() {return confirm(" + ScriptHelper.GetString(GetString("com.ConfirmInternalStatusFromGlobal")) + ");}";
                ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "ConfirmCopyFromGlobal", ScriptHelper.GetScript(script));
            }
        }

        this.CurrentMaster.HeaderActions.Actions = actions;
        this.CurrentMaster.HeaderActions.ActionPerformed += new CommandEventHandler(HeaderActions_ActionPerformed);

        gridElem.OnAction += new OnActionEventHandler(gridElem_OnAction);
        gridElem.OnExternalDataBound += new OnExternalDataBoundEventHandler(gridElem_OnExternalDataBound);
        gridElem.ZeroRowsText = GetString("general.nodatafound");

        // Configuring global records
        if (ConfiguredSiteID == 0)
        {
            // Select only global records
            gridElem.WhereCondition = "InternalStatusSiteID IS NULL";
            // Show "using global settings" info message only if showing global store settings
            if (SiteID != 0)
            {
                lblGlobalInfo.Visible = true;
                lblGlobalInfo.Text = GetString("com.UsingGlobalSettings");
            }
        }
        else
        {
            // Select only site-specific records
            gridElem.WhereCondition = "InternalStatusSiteID = " + ConfiguredSiteID;
        }
    }

    #endregion


    #region "Event Handlers"

    protected void HeaderActions_ActionPerformed(object sender, CommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "copyfromglobal":
                CopyFromGlobal();
                gridElem.ReloadData();
                break;
        }
    }


    /// <summary>
    /// Handles the UniGrid's OnAction event.
    /// </summary>
    /// <param name="actionName">Name of item (button) that throws event</param>
    /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
    protected void gridElem_OnAction(string actionName, object actionArgument)
    {
        if (actionName == "edit")
        {
            URLHelper.Redirect("InternalStatus_Edit.aspx?statusid=" + Convert.ToString(actionArgument) + "&siteId=" + SiteID);
        }
        else if (actionName == "delete")
        {
            CheckConfigurationModification();

            if (InternalStatusInfoProvider.CheckDependencies(ValidationHelper.GetInteger(actionArgument, 0)))
            {
                lblError.Visible = true;
                lblError.Text = GetString("Ecommerce.DeleteDisabled");
                return;
            }

            // delete InternalStatusInfo object from database
            InternalStatusInfoProvider.DeleteInternalStatusInfo(ValidationHelper.GetInteger(actionArgument, 0));
        }
    }


    protected object gridElem_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName.ToLower())
        {
            case "internalstatusenabled":
                return UniGridFunctions.ColoredSpanYesNo(parameter);
        }

        return parameter;
    }

    #endregion


    #region "Methods"

    /// <summary>
    ///  Copies site-specific status options from global status list.
    /// </summary>
    protected void CopyFromGlobal()
    {
        CheckConfigurationModification();
        InternalStatusInfoProvider.CopyFromGlobal(ConfiguredSiteID);
    }

    #endregion
}

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

public partial class CMSModules_Ecommerce_Pages_Tools_Configuration_PublicStatus_PublicStatus_List : CMSPublicStatusesPage
{
    #region "Page Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Pagetitle
        this.CurrentMaster.Title.TitleText = GetString("PublicStatus_List.HeaderCaption");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Ecommerce_PublicStatus/object.png");
        this.CurrentMaster.Title.HelpTopicName = "public_status_list";
        this.CurrentMaster.Title.HelpName = "helpTopic";

        // New item link
        string[,] actions = new string[2, 9];
        actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
        actions[0, 1] = GetString("PublicStatus_List.NewItemCaption");
        actions[0, 2] = null;
        actions[0, 3] = ResolveUrl("PublicStatus_Edit.aspx?siteId="+SiteID);
        actions[0, 4] = null;
        actions[0, 5] = GetImageUrl("Objects/Ecommerce_PublicStatus/add.png");

        // Show copy from global link when not configuring global statuses.
        if (ConfiguredSiteID != 0)
        {
            // Show "Copy from global" link only if there is at least one global status
            DataSet ds = PublicStatusInfoProvider.GetPublicStatuses(0, false);
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                actions[1, 0] = HeaderActions.TYPE_SAVEBUTTON;
                actions[1, 1] = GetString("general.copyfromglobal");
                actions[1, 2] = "return ConfirmCopyFromGlobal();";
                actions[1, 3] = null;
                actions[1, 4] = null;
                actions[1, 5] = GetImageUrl("Objects/Ecommerce_PublicStatus/fromglobal.png");
                actions[1, 6] = "copyFromGlobal";
                actions[1, 7] = String.Empty;
                actions[1, 8] = true.ToString();

                // Register javascript to confirm generate 
                string script = "function ConfirmCopyFromGlobal() {return confirm(" + ScriptHelper.GetString(GetString("com.ConfirmPublicStatusFromGlobal")) + ");}";
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
            gridElem.WhereCondition = "PublicStatusSiteID IS NULL";
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
            gridElem.WhereCondition = "PublicStatusSiteID = " + ConfiguredSiteID;
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
            URLHelper.Redirect("PublicStatus_Edit.aspx?publicStatusId=" + ValidationHelper.GetInteger(actionArgument, 0) + "&siteId=" + SiteID);
        }
        else if (actionName == "delete")
        {
            CheckConfigurationModification();

            if (PublicStatusInfoProvider.CheckDependencies(ValidationHelper.GetInteger(actionArgument, 0)))
            {
                lblError.Visible = true;
                lblError.Text = GetString("Ecommerce.DeleteDisabled");
                return;
            }

            // Delete PublicStatusInfo object from database
            PublicStatusInfoProvider.DeletePublicStatusInfo(ValidationHelper.GetInteger(actionArgument, 0));
        }
    }


    object gridElem_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName.ToLower())
        {
            case "publicstatusenabled":
                return UniGridFunctions.ColoredSpanYesNo(parameter);

            case "publicstatusname":
                return HTMLHelper.HTMLEncode(Convert.ToString(parameter));
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
        PublicStatusInfoProvider.CopyFromGlobal(ConfiguredSiteID);
    }

    #endregion
}

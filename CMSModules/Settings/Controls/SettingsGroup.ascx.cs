using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSModules_Settings_Controls_SettingsGroup : SettingsGroup
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Unigrid
        gridElem.HideControlForZeroRows = false;
        gridElem.OrderBy = "KeyOrder";
        gridElem.OnAction += new OnActionEventHandler(KeyAction);
        gridElem.OnExternalDataBound += new OnExternalDataBoundEventHandler(gridElem_OnExternalDataBound);
        gridElem.ZeroRowsText = GetString("settings.group.nokeysfound");

        if (this.Category != null)
        {
                // Header actions
                string[,] actions = new string[4, 13];
                string catIdStr = this.Category.CategoryID.ToString();

                // Edit button
                actions[0, 0] = HeaderActions.TYPE_LINKBUTTON;
                actions[0, 1] = "";
                actions[0, 4] = GetString("general.edit");
                actions[0, 5] = GetImageUrl("Objects/CMS_CustomSettings/edit.png");
                actions[0, 6] = "edit";
                actions[0, 7] = catIdStr;
                actions[0, 8] = "true";
                actions[0, 12] = "true";

                // Delete button
                actions[1, 0] = HeaderActions.TYPE_LINKBUTTON;
                actions[1, 1] = "";
                actions[1, 2] = " if (confirm(" + ScriptHelper.GetString(GetString("Development.CustomSettings.GroupDeleteConfirmation")) +
                        ")) { return true; } return false;";
                actions[1, 4] = GetString("general.delete");
                actions[1, 5] = GetImageUrl("Objects/CMS_CustomSettings/delete.png");
                actions[1, 6] = "delete";
                actions[1, 7] = catIdStr;
                actions[1, 8] = "true";
                actions[1, 12] = "true";

                // Up button
                actions[2, 0] = HeaderActions.TYPE_LINKBUTTON;
                actions[2, 1] = "";
                actions[2, 4] = GetString("general.moveup");
                actions[2, 5] = GetImageUrl("Objects/CMS_CustomSettings/up.png");
                actions[2, 6] = "moveup";
                actions[2, 7] = catIdStr;
                actions[2, 8] = "true";
                actions[2, 12] = "true";

                // Down button
                actions[3, 0] = HeaderActions.TYPE_LINKBUTTON;
                actions[3, 1] = "";
                actions[3, 4] = GetString("general.movedown");
                actions[3, 5] = GetImageUrl("Objects/CMS_CustomSettings/down.png");
                actions[3, 6] = "movedown";
                actions[3, 7] = catIdStr;
                actions[3, 8] = "true";
                actions[3, 12] = "true";

                cpCategory.HeaderActions.Actions = actions;
                cpCategory.HeaderActions.ActionPerformed += new CommandEventHandler(CategoryActionPerformed);

                // Panel title for group
                cpCategory.Text = HTMLHelper.HTMLEncode(ResHelper.LocalizeString(this.Category.CategoryDisplayName));

                // Filter out only records for this group
                gridElem.WhereClause = "KeyCategoryID = " + this.Category.CategoryID;

                // Setup "Add key" link
                lnkNewKey.Text = ResHelper.GetString("Development.CustomSettings.NewKey");
                lnkNewKey.Click += new EventHandler(CreateNewKey);
                imgNewKey.ImageUrl = GetImageUrl("Objects/CMS_CustomSettings/list.png");
        }

        // Apply site filter if required.
        if (!string.IsNullOrEmpty(gridElem.WhereClause))
        {
            gridElem.WhereClause += " AND ";
        }

        if (mSiteId > 0)
        {
            gridElem.WhereClause += string.Format("SiteID = {0}", mSiteId);
        }
        else
        {
            gridElem.WhereClause += "SiteID IS NULL";
        }
    }


    /// <summary>
    /// OnExternal databoud event handling (because of macro resolving).
    /// </summary>
    private object gridElem_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        if (sourceName.ToLower() == "keydisplayname")
        {
            return HTMLHelper.HTMLEncode(ResHelper.LocalizeString(ValidationHelper.GetString(parameter, "")));
        }
        else if (sourceName.ToLower() == "ishidden")
        {
            ImageButton ib = ((ImageButton)sender);
            ib.Visible = ValidationHelper.GetBoolean(((DataRowView)((GridViewRow)parameter).DataItem).Row["KeyIsHidden"], false);
            ib.Style["cursor"] = "default";
            ib.Attributes["onclick"] = "javascript:return false;";
        }

        return parameter;
    }
}
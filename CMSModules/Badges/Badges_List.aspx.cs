using System;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.SiteProvider;

public partial class CMSModules_Badges_Badges_List : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Set the page title
        this.CurrentMaster.Title.TitleText = GetString("badge.title");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_Badge/object.png");
        this.CurrentMaster.Title.HelpTopicName = "badge";

        // New item link
        string[,] actions = new string[1, 6];
        actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
        actions[0, 1] = GetString("badge.newbadge");
        actions[0, 2] = null;
        actions[0, 3] = ResolveUrl("Badges_Edit.aspx");
        actions[0, 4] = null;
        actions[0, 5] = GetImageUrl("Objects/CMS_Badge/add.png");
        this.CurrentMaster.HeaderActions.Actions = actions;

        UniGrid.OnAction += new OnActionEventHandler(UniGrid_OnAction);
        UniGrid.OnExternalDataBound += new OnExternalDataBoundEventHandler(UniGrid_OnExternalDataBound);
        UniGrid.ZeroRowsText = GetString("general.nodatafound");
    }


    /// <summary>
    /// Unigrid external bind event handler.
    /// </summary>
    object UniGrid_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName.ToLower())
        {
            case "imageurl":
                string url = ValidationHelper.GetString(parameter, "");
                if (!String.IsNullOrEmpty(url))
                {
                    url = "<img alt=\"Badge image\" src=\"" + ResolveUrl(url) + "\" style=\"max-width:50px; max-height: 50px;\"  />";
                    return url;
                }
                return "";
            case "isautomatic":
                if (ValidationHelper.GetBoolean(parameter, false))
                {
                    return GetString("general.yes");
                }
                else
                {
                    return GetString("general.no");
                }
        }
        return null;
    }


    /// <summary>
    /// Unigrid on action event handler.
    /// </summary>
    void UniGrid_OnAction(string actionName, object actionArgument)
    {
        // Edit action
        if (DataHelper.GetNotEmpty(actionName, String.Empty) == "edit")
        {
            URLHelper.Redirect("~/CMSModules/Badges/Badges_Edit.aspx?badgeid=" + ValidationHelper.GetString(actionArgument, "0"));
        }
        // Delete action
        else if (DataHelper.GetNotEmpty(actionName, String.Empty) == "delete")
        {
            int badgeId = ValidationHelper.GetInteger(actionArgument, 0);
            if (badgeId > 0)
            {
                BadgeInfoProvider.DeleteBadgeInfo(badgeId);
            }
        }
    }
}

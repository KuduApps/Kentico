using System;
using System.Data;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSModules_Avatars_Avatar_List : SiteManagerPage
{
    private CurrentUserInfo currentUser = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        currentUser = CMSContext.CurrentUser;

        if (currentUser == null)
        {
            return;
        }

        // Page title
        CurrentMaster.Title.TitleText = GetString("avat.title");
        CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_Avatar/object.png");
        CurrentMaster.Title.HelpTopicName = "avatars_list";
        CurrentMaster.Title.HelpName = "helpTopic";

        // New item link
        string[,] actions = new string[1, 6];
        actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
        actions[0, 1] = GetString("avat.newavatar");
        actions[0, 2] = null;
        actions[0, 3] = ResolveUrl("Avatar_Edit.aspx");
        actions[0, 4] = null;
        actions[0, 5] = GetImageUrl("Objects/CMS_Avatar/add.png");
        CurrentMaster.HeaderActions.Actions = actions;

        // Set up unigrid options
        unigridAvatarList.OrderBy = "AvatarName";
        unigridAvatarList.OnExternalDataBound += unigridAvatarList_OnExternalDataBound;
        unigridAvatarList.OnAction += unigridAvatarList_OnAction;
        unigridAvatarList.GridView.PageSize = 10;
        unigridAvatarList.ZeroRowsText = GetString("general.nodatafound");
        unigridAvatarList.OnBeforeDataReload += new OnBeforeDataReload(unigridAvatarList_OnBeforeDataReload);
    }


    protected void unigridAvatarList_OnBeforeDataReload()
    {
        unigridAvatarList.WhereCondition = filterAvatars.WhereCondition;
    }


    protected object unigridAvatarList_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName.ToLower())
        {
            case "avatartype":
                return GetString("avat.type" + (string)parameter);
            case "imagepreview":
                return "<img src=\"" + URLHelper.EncodeQueryString(ResolveUrl("~/CMSModules/Avatars/CMSPages/GetAvatar.aspx?avatarguid=" + parameter + "&maxsidesize=50")) + "\" alt=\"\" /> ";
            case "avatarname":
                DataRowView dr = (DataRowView)parameter;
                string avatarName = HTMLHelper.HTMLEncode(dr.Row["AvatarName"].ToString());
                if (string.IsNullOrEmpty((avatarName)))
                {
                    string name = HTMLHelper.HTMLEncode(dr.Row["AvatarFilename"].ToString());
                    name = name.Substring(0, name.LastIndexOf("."));
                    return name;
                }
                else
                {
                    return avatarName;
                }
            default:
                return "";
        }
    }


    protected void unigridAvatarList_OnAction(string actionName, object actionArgument)
    {
        // Edit action
        if (DataHelper.GetNotEmpty(actionName, String.Empty) == "edit")
        {
            URLHelper.Redirect("Avatar_Edit.aspx?avatarid=" + (string)actionArgument);
        }
        // Delete action
        else if (DataHelper.GetNotEmpty(actionName, String.Empty) == "delete")
        {
            int avatarId = ValidationHelper.GetInteger(actionArgument, 0);
            if (avatarId > 0)
            {
                AvatarInfoProvider.DeleteAvatarInfo(avatarId);
            }
        }
    }    
}

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using CMS.GlobalHelper;
using CMS.ExtendedControls;
using CMS.CMSHelper;
using CMS.SettingsProvider;
using CMS.URLRewritingEngine;
using CMS.SiteProvider;

public partial class CMSAdminControls_ContextMenus_UserContextMenu : CMSContextMenuControl, IPostBackEventHandler
{
    #region "Variables"

    private CurrentUserInfo currentUser = null;
    protected bool isInIgnoreList = false;
    protected bool isInContactList = false;
    protected int requestedUserId = 0;
    #endregion


    #region "Properties"

    /// <summary>
    /// Indicates if the community module is loaded.
    /// </summary>
    public bool CommunityPresent
    {
        get
        {
            if (!RequestStockHelper.Contains("commPresent"))
            {
                RequestStockHelper.Add("commPresent", ModuleEntry.IsModuleLoaded(ModuleEntry.COMMUNITY));
            }
            return ValidationHelper.GetBoolean(RequestStockHelper.GetItem("commPresent"), false);
        }
    }


    /// <summary>
    /// Indicates if the messaging module is loaded.
    /// </summary>
    public bool MessagingPresent
    {
        get
        {
            if (!RequestStockHelper.Contains("messagingPresent"))
            {
                RequestStockHelper.Add("messagingPresent", ModuleEntry.IsModuleLoaded(ModuleEntry.MESSAGING));
            }

            return ValidationHelper.GetBoolean(RequestStockHelper.GetItem("messagingPresent"), false);
        }
    }

    #endregion


    #region "Events handling"

    /// <summary>
    /// OnLoad event.
    /// </summary>
    /// <param name="e">Event arguments</param>
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        repItem.ItemDataBound += repItem_ItemDataBound;

        currentUser = CMSContext.CurrentUser;
        string script = "";

        // Friendship request
        script += "function ContextFriendshipRequest(id) { \n" +
        "modalDialog('" + CMSContext.ResolveDialogUrl("~/CMSModules/Friends/CMSPages/Friends_Request.aspx") + "?userid=" + currentUser.UserID + "&requestid=' + id,'requestFriend', 480, 350); \n" +
        " } \n";

        // Friendship rejection
        script += "function ContextFriendshipReject(id) { \n" +
                "modalDialog('" + CMSContext.ResolveDialogUrl("~/CMSModules/Friends/CMSPages/Friends_Reject.aspx") + "?userid=" + currentUser.UserID + "&requestid=' + id , 'rejectFriend', 410, 270); \n" +
                " } \n";

        // Send private message
        script += "function ContextPrivateMessage(id) { \n" +
                "modalDialog('" + CMSContext.ResolveDialogUrl("~/CMSModules/Messaging/CMSPages/SendMessage.aspx") + "?userid=" + currentUser.UserID + "&requestid=' + id , 'sendMessage', 390, 390); \n" +
                " } \n";

        // Add to contact list
        script += "function ContextAddToContactList(usertoadd) { \n" +
                "if(confirm(" + ScriptHelper.GetString(ResHelper.GetString("messaging.contactlist.addconfirmation")) + "))" +
                "{" +
                "selectedIdElem = document.getElementById('" + hdnSelectedId.ClientID + "'); \n" +
                "if (selectedIdElem != null) { selectedIdElem.value = usertoadd;}" +
                Page.ClientScript.GetPostBackEventReference(this, "addtocontactlist", false) +
                "} } \n";

        // Add to ignore list
        script += "function ContextAddToIgnoretList(usertoadd) { \n" +
                "if(confirm(" + ScriptHelper.GetString(ResHelper.GetString("messaging.ignorelist.addconfirmation")) + "))" +
                "{" +
                "selectedIdElem = document.getElementById('" + hdnSelectedId.ClientID + "'); \n" +
                "if (selectedIdElem != null) { selectedIdElem.value = usertoadd;}" +
                Page.ClientScript.GetPostBackEventReference(this, "addtoignorelist", false) +
                "} } \n";

        // Group invitation
        script += "function ContextGroupInvitation(id) { \nmodalDialog('" + CMSContext.ResolveDialogUrl("~/CMSModules/Groups/CMSPages/InviteToGroup.aspx") + "?invitedid=' + id , 'inviteToGroup', 500, 300); \n } \n";

        // Redirect to sign in URL
        string signInUrl = CMSContext.CurrentResolver.ResolveMacros(SettingsKeyProvider.GetStringValue(CMSContext.CurrentSiteName + ".CMSSecuredAreasLogonPage"));
        if (signInUrl != "")
        {
            signInUrl = "window.location.replace('" + URLHelper.AddParameterToUrl(ResolveUrl(signInUrl), "ReturnURL", Server.UrlEncode(URLRewriter.CurrentURL)) + "');";
        }

        script += "function ContextRedirectToSignInUrl() { \n" + signInUrl + "} \n";

        // Register menu management scripts
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "UserContextMenuManagement", ScriptHelper.GetScript(script));

        // Register the dialog script
        ScriptHelper.RegisterDialogScript(this.Page);
    }


    /// <summary>
    /// Bounding event.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Event arguments</param>
    protected void repItem_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Panel pnlItem = (Panel)e.Item.FindControl("pnlItem");
        if (pnlItem != null)
        {
            int count = ValidationHelper.GetInteger(((DataRowView)e.Item.DataItem)["Count"], 0) - 1;
            if (e.Item.ItemIndex == count)
            {
                pnlItem.CssClass = "ItemLast";
            }

            string action = (string)((DataRowView)e.Item.DataItem)["ActionScript"];
            pnlItem.Attributes.Add("onclick", action + ";");
        }
    }


    /// <summary>
    /// Postback handling.
    /// </summary>
    /// <param name="eventArgument">Argument of postback event</param>
    public void RaisePostBackEvent(string eventArgument)
    {
        if ((eventArgument == null))
        {
            return;
        }

        // Get ID of user
        int selectedId = ValidationHelper.GetInteger(hdnSelectedId.Value, 0);

        // Add only if messaging is present
        if (MessagingPresent)
        {
            // Add to contact or ignore list
            switch (eventArgument)
            {
                case "addtoignorelist":
                    ModuleCommands.MessagingAddToIgnoreList(currentUser.UserID, selectedId);
                    break;

                case "addtocontactlist":
                    ModuleCommands.MessagingAddToContactList(currentUser.UserID, selectedId);
                    break;
            }
        }
    }

    #endregion


    #region "Public methods"

    /// <summary>
    /// Reload data.
    /// </summary>
    public override void ReloadData()
    {
        requestedUserId = ValidationHelper.GetInteger(ContextMenu.Parameter, 0);

        DataTable table = new DataTable();
        table.Columns.Add("ActionIcon");
        table.Columns.Add("ActionDisplayName");
        table.Columns.Add("ActionScript");

        // Get resource strings prefix
        string resourcePrefix = ContextMenu.ResourcePrefix;

        // Add only if community is present
        if (CommunityPresent)
        {
            // Friendship request
            if ((requestedUserId != currentUser.UserID) && UIHelper.IsFriendsModuleEnabled(CMSContext.CurrentSiteName))
            {
                FriendshipStatusEnum status = currentUser.HasFriend(requestedUserId);
                // If friendship exists add reject action or request friendship
                if (status == FriendshipStatusEnum.Approved)
                {
                    table.Rows.Add(new object[] { "friendshipreject.png", ResHelper.GetString(resourcePrefix + ".rejectfriendship|friends.rejectfriendship"), currentUser.IsAuthenticated() ? "ContextFriendshipReject(GetContextMenuParameter('" + ContextMenu.MenuID + "'))" : "ContextRedirectToSignInUrl()" });
                }
                else if ((status == FriendshipStatusEnum.None) || currentUser.IsPublic())
                {
                    table.Rows.Add(new object[] { "friendshiprequest.png", ResHelper.GetString(resourcePrefix + ".requestfriendship|friends.requestfriendship"), currentUser.IsAuthenticated() ? "ContextFriendshipRequest(GetContextMenuParameter('" + ContextMenu.MenuID + "'))" : "ContextRedirectToSignInUrl()" });
                }
                // Group invitation
                table.Rows.Add(new object[] { "invitetogroup.png", ResHelper.GetString(resourcePrefix + ".invite|groupinvitation.invite"), currentUser.IsAuthenticated() ? "ContextGroupInvitation(GetContextMenuParameter('" + ContextMenu.MenuID + "'))" : "ContextRedirectToSignInUrl()" });
            }
        }

        // Add only if messaging is present
        if (MessagingPresent)
        {
            // Check if user is in ignore list
            isInIgnoreList = ModuleCommands.MessagingIsInIgnoreList(currentUser.UserID, requestedUserId);

            // Check if user is in contact list
            isInContactList = ModuleCommands.MessagingIsInContactList(currentUser.UserID, requestedUserId);

            table.Rows.Add(new object[] { "sendmessage.png", ResHelper.GetString(resourcePrefix + ".sendmessage|sendmessage.sendmessage"), currentUser.IsAuthenticated() ? "ContextPrivateMessage(GetContextMenuParameter('" + ContextMenu.MenuID + "'))" : "ContextRedirectToSignInUrl()" });

            // Not for the same user
            if (requestedUserId != currentUser.UserID)
            {
                // Add to ignore list or add to contact list actions
                if (!isInIgnoreList)
                {
                    table.Rows.Add(new object[] { "addtoignorelist.png", ResHelper.GetString(resourcePrefix + ".addtoignorelist|messsaging.addtoignorelist"), currentUser.IsAuthenticated() ? "ContextAddToIgnoretList(GetContextMenuParameter('" + ContextMenu.MenuID + "'))" : "ContextRedirectToSignInUrl()" });
                }

                if (!isInContactList)
                {
                    table.Rows.Add(new object[] { "addtocontactlist.png", ResHelper.GetString(resourcePrefix + ".addtocontactlist|messsaging.addtocontactlist"), currentUser.IsAuthenticated() ? "ContextAddToContactList(GetContextMenuParameter('" + ContextMenu.MenuID + "'))" : "ContextRedirectToSignInUrl()" });
                }
            }
        }

        // Add count column
        DataColumn countColumn = new DataColumn();
        countColumn.ColumnName = "Count";
        countColumn.DefaultValue = table.Rows.Count;

        table.Columns.Add(countColumn);
        repItem.DataSource = table;
        repItem.DataBind();
    }

    #endregion
}

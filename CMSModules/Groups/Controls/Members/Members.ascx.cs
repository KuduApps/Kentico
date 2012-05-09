using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.Community;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.SiteProvider;
using CMS.CMSHelper;

public partial class CMSModules_Groups_Controls_Members_Members : CMSAdminControl
{
    #region "Variables"

    private bool mHideWhenGroupIsNotSupplied = false;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Determines whether to hide the content of the control when GroupID is not supplied.
    /// </summary>
    public bool HideWhenGroupIsNotSupplied
    {
        get
        {
            return mHideWhenGroupIsNotSupplied;
        }
        set
        {
            mHideWhenGroupIsNotSupplied = value;
        }
    }


    /// <summary>
    /// Gets or sets the group ID for which the members should be displayed.
    /// </summary>
    public int GroupID
    {
        get
        {
            if (memberListElem.GroupID <= 0)
            {
                memberListElem.GroupID = ValidationHelper.GetInteger(GetValue("GroupID"), 0);
            }

            return memberListElem.GroupID;
        }
        set
        {
            memberListElem.GroupID = value;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        #region "Security"

        memberListElem.OnCheckPermissions += new CheckPermissionsEventHandler(memberListElem_OnCheckPermissions);
        memberEditElem.OnCheckPermissions += memberEditElem_OnCheckPermissions;

        #endregion

        if (!Visible)
        {
            EnableViewState = false;
        }

        if (StopProcessing)
        {
            actionsElem.StopProcessing = true;
            memberListElem.StopProcessing = true;
            memberEditElem.StopProcessing = true;
        }
        else
        {
            if ((GroupID == 0) && HideWhenGroupIsNotSupplied)
            {
                Visible = false;
                return;
            }

            memberListElem.OnAction += new CommandEventHandler(memberListElem_GridOnAction);

            lnkEditBack.Click += lnkEditBack_Click;
            lnkEditBack.Text = GetString("group.members");

            // Initialize the invite customer element
            string[,] actions = new string[1, 7];
            actions[0, 0] = HeaderActions.TYPE_LINKBUTTON;
            actions[0, 1] = GetString("groupinvitation.invite");
            actions[0, 2] = "OpenInvite(); return false;";
            actions[0, 5] = GetImageUrl("CMSModules/CMS_Groups/invitemember.png");
            actions[0, 6] = "invitemember";
            actionsElem.Actions = actions;

            string script = "function OpenInvite() {\n" +
                    "modalDialog('" + CMSContext.ResolveDialogUrl("~/CMSModules/Groups/CMSPages/InviteToGroup.aspx") + "?groupid=" + GroupID + "','inviteToGroup', 500, 310); \n" +
                    " } \n";

            // Register menu management scripts
            ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "Members", ScriptHelper.GetScript(script));

            // Register the dialog script
            ScriptHelper.RegisterDialogScript(this.Page);
        }
    }

    #region "Security handlers"

    protected void memberEditElem_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        RaiseOnCheckPermissions(permissionType, sender);
    }

    protected void memberListElem_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        RaiseOnCheckPermissions(permissionType, sender);
    }

    #endregion


    protected void lnkEditBack_Click(object sender, EventArgs e)
    {
        lblInfo.Visible = true;
        plcList.Visible = true;
        plcEdit.Visible = false;
        memberListElem.ReloadGrid();
    }


    protected void memberListElem_GridOnAction(object sender, CommandEventArgs args)
    {
        switch (args.CommandName.ToLower())
        {
            case "approve":
                lblInfo.Text = GetString("group.member.userhasbeenapproved");
                lblInfo.Visible = true;
                break;

            case "reject":
                lblInfo.Text = GetString("group.member.userhasbeenrejected");
                lblInfo.Visible = true;
                break;

            case "edit":
                int memberId = ValidationHelper.GetInteger(args.CommandArgument, 0);
                memberEditElem.MemberID = memberId;
                memberEditElem.GroupID = GroupID;
                plcList.Visible = false;
                plcEdit.Visible = true;
                memberEditElem.Visible = true;
                memberEditElem.ReloadData();

                GroupMemberInfo gmi = GroupMemberInfoProvider.GetGroupMemberInfo(memberId);
                if (gmi != null)
                {
                    UserInfo ui = UserInfoProvider.GetUserInfo(gmi.MemberUserID);
                    if (ui != null)
                    {
                        lblEditBack.Text = " <span class=\"TitleBreadCrumbSeparator\">&nbsp;</span> " + HTMLHelper.HTMLEncode(ui.FullName);
                    }
                }
                break;
        }
    }
}

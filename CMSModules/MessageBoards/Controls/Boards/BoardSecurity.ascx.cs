using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.MessageBoard;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.ExtendedControls;
using CMS.PortalEngine;
using CMS.SettingsProvider;

public partial class CMSModules_MessageBoards_Controls_Boards_BoardSecurity : CMSAdminEditControl
{
    #region "Variables"

    protected int mBoardId = 0;
    private BoardInfo mBoard = null;
    private int mGroupId = 0;

    #endregion


    #region "Public properties"

    /// <summary>
    /// ID of the current message board.
    /// </summary>
    public int BoardID
    {
        get
        {
            if (mBoard != null)
            {
                return mBoard.BoardID;
            }

            return mBoardId;
        }
        set
        {
            mBoardId = value;

            mBoard = null;
        }
    }


    /// <summary>
    /// Current message board info object.
    /// </summary>
    public BoardInfo Board
    {
        get
        {
            return mBoard ?? (mBoard = BoardInfoProvider.GetBoardInfo(BoardID));
        }
        set
        {
            mBoard = value;

            mBoardId = 0;
        }
    }


    /// <summary>
    /// ID of the current group.
    /// </summary>
    public int GroupID
    {
        get
        {
            return this.mGroupId;
        }
        set
        {
            this.mGroupId = value;
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Register script for pendingCallbacks repair
        ScriptHelper.FixPendingCallbacks(this.Page);

        // Initializes the controls
        SetupControls();

        if (!RequestHelper.IsPostBack() && !this.IsLiveSite)
        {
            ReloadData();
        }
        else
        {
            this.addRoles.CurrentValues = GetRoles();
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.messageboards", CMSAdminControl.PERMISSION_MODIFY))
        {
            btnRemoveRole.Enabled = false;
            addRoles.CurrentSelector.Enabled = false;
            lstRoles.Enabled = false;
        }
    }


    protected void radOnlyOwner_CheckedChanged(object sender, EventArgs e)
    {
        if (radOnlyOwner.Checked)
        {
            addRoles.CurrentSelector.Enabled = false;
            btnRemoveRole.Enabled = false;
            lstRoles.Enabled = false;
        }
    }


    protected void radGroupMembers_CheckedChanged(object sender, EventArgs e)
    {
        if (radGroupMembers.Checked)
        {
            addRoles.CurrentSelector.Enabled = false;
            btnRemoveRole.Enabled = false;
            lstRoles.Enabled = false;
        }
    }


    protected void radOnlyGroupAdmin_CheckedChanged(object sender, EventArgs e)
    {
        if (radOnlyGroupAdmin.Checked)
        {
            addRoles.CurrentSelector.Enabled = false;
            btnRemoveRole.Enabled = false;
            lstRoles.Enabled = false;
        }
    }


    protected void radAllUsers_CheckedChanged(object sender, EventArgs e)
    {
        if (radAllUsers.Checked)
        {
            addRoles.CurrentSelector.Enabled = false;
            btnRemoveRole.Enabled = false;
            lstRoles.Enabled = false;
        }
    }


    protected void radOnlyUsers_CheckedChanged(object sender, EventArgs e)
    {
        if (radOnlyUsers.Checked)
        {
            btnRemoveRole.Enabled = false;
            addRoles.CurrentSelector.Enabled = false;
            lstRoles.Enabled = false;
        }
    }


    protected void radOnlyRoles_CheckedChanged(object sender, EventArgs e)
    {
        if (radOnlyRoles.Checked)
        {
            addRoles.CurrentSelector.Enabled = true;
            btnRemoveRole.Enabled = true;
            lstRoles.Enabled = true;
        }
    }


    protected void btnRemoveRole_Click(object sender, EventArgs e)
    {
        if (!CheckPermissions("cms.messageboards", CMSAdminControl.PERMISSION_MODIFY))
        {
            return;
        }

        foreach (ListItem item in lstRoles.Items)
        {
            // Delete selected item
            if (item.Selected)
            {
                int roleId = Convert.ToInt32(item.Value);
                BoardRoleInfoProvider.RemoveRoleFromBoard(roleId, this.BoardID);
            }
        }

        ReloadRoles();
    }


    protected void addRoles_Changed(object sender, EventArgs e)
    {
        ReloadRoles();
        pnlUpdate.Update();
    }


    protected void btnOk_Click(object sender, EventArgs e)
    {
        if (!CheckPermissions("cms.messageboards", CMSAdminControl.PERMISSION_MODIFY))
        {
            return;
        }

        if (Board != null)
        {
            if (radOnlyUsers.Checked)
            {
                Board.BoardAccess = SecurityAccessEnum.AuthenticatedUsers;
            }

            if (radAllUsers.Checked)
            {
                Board.BoardAccess = SecurityAccessEnum.AllUsers;
            }

            if (radOnlyRoles.Checked)
            {
                Board.BoardAccess = SecurityAccessEnum.AuthorizedRoles;
            }

            if (radOnlyGroupAdmin.Visible && radOnlyGroupAdmin.Checked)
            {
                Board.BoardAccess = SecurityAccessEnum.GroupAdmin;
            }

            if (radGroupMembers.Visible && radGroupMembers.Checked)
            {
                Board.BoardAccess = SecurityAccessEnum.GroupMembers;
            }

            if (radOnlyOwner.Visible && radOnlyOwner.Checked)
            {
                Board.BoardAccess = SecurityAccessEnum.Owner;
            }

            Board.BoardUseCaptcha = chkUseCaptcha.Checked;

            BoardInfoProvider.SetBoardInfo(Board);

            lblInfo.Visible = true;
            lblInfo.Text = GetString("General.Changessaved");
        }
    }

    #endregion


    #region "General methods"

    /// <summary>
    /// Initializes the controls.
    /// </summary>
    private void SetupControls()
    {
        this.radAllUsers.Text = GetString("security.allusers");
        this.radOnlyRoles.Text = GetString("board.security.onlyroles");
        this.radOnlyUsers.Text = GetString("board.security.onlyusers");
        this.radGroupMembers.Text = GetString("board.security.onlygroupmembers");
        this.radOnlyGroupAdmin.Text = GetString("board.security.groupadmin");
        this.radOnlyOwner.Text = GetString("board.security.owner");
        this.btnRemoveRole.Text = GetString("general.remove");
        this.lblTitleComments.Text = GetString("board.security.commentstitle");
        this.lblTitleGeneral.Text = GetString("general.general");
        this.chkUseCaptcha.Text = GetString("board.security.usecaptcha");
        this.btnOk.Text = GetString("general.ok");

        this.addRoles.BoardID = this.BoardID;
        this.addRoles.CurrentSelector.IsLiveSite = this.IsLiveSite;
        this.addRoles.Changed += addRoles_Changed;
        this.addRoles.GroupID = this.GroupID;
        this.addRoles.IsLiveSite = this.IsLiveSite;
        this.addRoles.ShowSiteFilter = false;
    }


    /// <summary>
    /// Reloads the listbox with roles.
    /// </summary>
    private void ReloadRoles()
    {
        // Load the roles into the ListBox
        DataSet roles = BoardRoleInfoProvider.GetBoardRoles(this.BoardID, "RoleID,RoleDisplayName,SiteID");
        this.lstRoles.Items.Clear();

        foreach (DataRow dr in roles.Tables[0].Rows)
        {
            string name = Convert.ToString(dr["RoleDisplayName"]);
            if (ValidationHelper.GetInteger(dr["SiteID"], 0) == 0)
            {
                name += " " + GetString("general.global");
            }
            lstRoles.Items.Add(new ListItem(name, Convert.ToString(dr["RoleID"])));
        }

        this.addRoles.CurrentSelector.Value = TextHelper.Join(";", SqlHelperClass.GetStringValues(roles.Tables[0], "RoleID"));
    }


    /// <summary>
    /// Returns board roles separated by semicolon.
    /// </summary>    
    private string GetRoles()
    {
        // Load the roles into the ListBox
        DataSet roles = BoardRoleInfoProvider.GetBoardRoles(this.BoardID, "RoleID,RoleDisplayName");

        return TextHelper.Join(";", SqlHelperClass.GetStringValues(roles.Tables[0], "RoleID"));
    }


    /// <summary>
    /// Reloads data.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();

        ReloadRoles();

        EditedObject = Board;

        if (Board != null)
        {
            this.plcOnlyGroupAdmin.Visible = (this.GroupID > 0);
            this.plcGroupMembers.Visible = (this.GroupID > 0);
            this.plcOnlyOwner.Visible = (Board.BoardUserID > 0);

            switch (Board.BoardAccess)
            {
                // All users
                case SecurityAccessEnum.AllUsers:
                    radAllUsers.Checked = true;
                    break;

                // Authenticated users
                case SecurityAccessEnum.AuthenticatedUsers:
                    radOnlyUsers.Checked = true;
                    break;

                //Selected roles
                case SecurityAccessEnum.AuthorizedRoles:
                    radOnlyRoles.Checked = true;
                    radOnlyRoles_CheckedChanged(null, null);
                    break;

                // Group members
                case SecurityAccessEnum.GroupMembers:
                    radGroupMembers.Checked = true;
                    radGroupMembers_CheckedChanged(null, null);
                    break;

                case SecurityAccessEnum.GroupAdmin:
                    radOnlyGroupAdmin.Checked = true;
                    radOnlyGroupAdmin_CheckedChanged(null, null);
                    break;

                case SecurityAccessEnum.Owner:
                    radOnlyOwner.Checked = true;
                    radOnlyOwner_CheckedChanged(null, null);
                    break;

                default:
                    radAllUsers.Checked = true;
                    break;
            }

            this.lstRoles.Enabled = radOnlyRoles.Checked;
            this.btnRemoveRole.Enabled = radOnlyRoles.Checked;
            this.addRoles.CurrentSelector.Enabled = radOnlyRoles.Checked;
            this.chkUseCaptcha.Checked = Board.BoardUseCaptcha;

            this.addRoles.ReloadData();
            this.addRoles.DataBind();
        }

    }

    #endregion
}

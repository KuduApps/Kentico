using System;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.FormControls;

public partial class CMSModules_Messaging_FormControls_MessageUserSelector : FormEngineUserControl
{
    #region "Private variables"

    private bool? mVisisble = null;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets the enabled state of the control.
    /// </summary>
    public override bool Enabled
    {
        get
        {
            return base.Enabled;
        }
        set
        {
            base.Enabled = value;
            txtUser.Enabled = value;
            btnSelect.Enabled = value;
        }
    }


    /// <summary>
    /// Name of the selected user.
    /// </summary>
    public string SelectedUserName
    {
        get
        {
            UserInfo ui = GetUser();
            return ui != null ? ui.UserName : string.Empty;
        }
        set
        {
            UserInfo ui = UserInfoProvider.GetUserInfo(value);
            if (ui != null)
            {
                txtUser.Text = ui.UserName;
                hiddenField.Value = ui.UserName;
            }
        }
    }


    /// <summary>
    /// ID of the selected user.
    /// </summary>
    public int SelectedUserID
    {
        get
        {
            UserInfo ui = GetUser();
            return ui != null ? ui.UserID : 0;
        }
        set
        {
            UserInfo ui = UserInfoProvider.GetUserInfo(value);
            if (ui != null)
            {
                txtUser.Text = ui.UserName;
                hiddenField.Value = ui.UserName;
            }
            else
            {
                txtUser.Text = string.Empty;
                hiddenField.Value = string.Empty;
            }
        }
    }


    /// <summary>
    /// Visibility of control.
    /// </summary>
    public override bool Visible
    {
        get
        {
            if (mVisisble.HasValue)
            {
                return mVisisble.Value;
            }
            else
            {
                return base.Visible;
            }
        }
        set
        {
            mVisisble = value;
            base.Visible = value;
        }
    }


    /// <summary>
    /// Gets textbox with user name.
    /// </summary>
    public TextBox UserNameTextBox
    {
        get
        {
            return txtUser;
        }
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Returns UserInfo object from hidden field value (because readonly textbox).
    /// </summary>
    private UserInfo GetUser()
    {
        // Try to find by username
        string userName = txtUser.Text.Trim();
        if (!string.IsNullOrEmpty(userName))
        {
            return UserInfoProvider.GetUserInfo(txtUser.Text.Trim());
        }

        int userId = ValidationHelper.GetInteger(hiddenField.Value, 0);
        if (userId > 0)
        {
            return UserInfoProvider.GetUserInfo(userId);
        }

        return null;
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (StopProcessing)
        {
            // Do nothing
        }
        else
        {
            btnSelect.Text = GetString("General.Select");

            ScriptHelper.RegisterDialogScript(Page);
            ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "fillScript", ScriptHelper.GetScript("function FillUserName(userId, mText, mId, mId2) {document.getElementById(mId).value = mText;document.getElementById(mId2).value = userId;}"));

            string showTab = CMSContext.CurrentUser.IsPublic() ? "Search" : "ContactList";
            string url = CMSContext.ResolveDialogUrl("~/CMSModules/Messaging/Dialogs/MessageUserSelector_Frameset.aspx");
            if (IsLiveSite)
            {
                if (!CMSContext.CurrentUser.IsPublic())
                {
                    url = CMSContext.ResolveDialogUrl("~/CMSModules/Messaging/CMSPages/MessageUserSelector_Frameset.aspx");
                }
                else
                {
                    url = CMSContext.ResolveDialogUrl("~/CMSModules/Messaging/CMSPages/PublicMessageUserSelector.aspx");
                }
            }

            btnSelect.OnClientClick = "modalDialog('" + url + "?refresh=false&showtab=" + showTab + "&hidid=" +
                                      hiddenField.ClientID + "&mid=" + txtUser.ClientID +
                                      "','MessageUserSelector',600, 510); return false;";

            if (!RequestHelper.IsPostBack())
            {
                txtUser.Text = SelectedUserName;
            }
        }
    }

    #endregion
}

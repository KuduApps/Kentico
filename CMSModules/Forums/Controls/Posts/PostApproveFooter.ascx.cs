using System;
using System.Data;

using CMS.GlobalHelper;
using CMS.Forums;
using CMS.UIControls;
using CMS.CMSHelper;
using CMS.URLRewritingEngine;

public partial class CMSModules_Forums_Controls_Posts_PostApproveFooter : CMSAdminEditControl
{
    #region "Variables"

    // Current PostID
    private int mPostID = 0;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets the post ID.
    /// </summary>
    public int PostID
    {
        get
        {
            return mPostID;
        }
        set
        {
            mPostID = value;
        }
    }

    #endregion


    #region "Page events"

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        // Button titles
        btnApprove.Text = GetString("general.approve");
        btnDelete.Text = GetString("general.delete");
        btnCancel.Text = GetString("general.cancel");

        // Button actions
        btnCancel.OnClientClick = "window.close(); return false;";
        btnDelete.OnClientClick = "return confirm('" + GetString("forummanage.deleteconfirm") + "');";
    }


    /// <summary>
    /// Handles the Click event of the btnApprove control.
    /// </summary>
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        // Check permissions
        if (!CheckPermissions("cms.forums", CMSAdminControl.PERMISSION_MODIFY))
        {
            return;
        }

        // Approve the post
        ForumPostInfo fpi = ForumPostInfoProvider.GetForumPostInfo(ValidationHelper.GetInteger(PostID, 0));
        if (fpi != null)
        {
            fpi.PostApprovedByUserID = CMSContext.CurrentUser.UserID;
            fpi.PostApproved = true;
            ForumPostInfoProvider.SetForumPostInfo(fpi);
        }

        // Reload the parent window
        RefreshParentWindow();
    }


    /// <summary>
    /// Handles the Click event of the btnDelete control.
    /// </summary>
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        // Check permissions
        if (!CheckPermissions("cms.forums", CMSAdminControl.PERMISSION_MODIFY))
        {
            return;
        }

        // Delete the post
        ForumPostInfoProvider.DeleteForumPostInfo(ValidationHelper.GetInteger(PostID, 0));

        // Reload the parent window
        RefreshParentWindow();
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Closes this dialog and reloads the parent window.
    /// </summary>
    private void RefreshParentWindow()
    {
        string script = @"
            function RefreshParentWindow()
            {
                if (wopener.RefreshPage) {
                    wopener.RefreshPage();
                }
                window.close();
            }

            window.onload = RefreshParentWindow;";

        ltrScript.Text = ScriptHelper.GetScript(script);
    }

    #endregion
}

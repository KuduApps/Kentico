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

using CMS.GlobalHelper;
using CMS.Forums;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.ExtendedControls;

public partial class CMSModules_Forums_Controls_LiveControls_Posts : CMSAdminItemsControl, IPostBackEventHandler
{

    #region "Variables"

    private int mForumId = 0;
    private int postId = 0;
    private ForumPostInfo post = null;
    private const string breadCrumbsSeparator = " <span class=\"TitleBreadCrumbSeparator\">&nbsp;</span> ";

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets the Forum ID.
    /// </summary>
    public int ForumID
    {
        get
        {
            return this.mForumId;
        }
        set
        {
            this.mForumId = value;
        }
    }

    #endregion


    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        #region "Security"

        postEdit.OnCheckPermissions += new CheckPermissionsEventHandler(postEdit_OnCheckPermissions);
        postNew.OnCheckPermissions += new CheckPermissionsEventHandler(postNew_OnCheckPermissions);

        #endregion

        this.postNew.OnInsertPost += new EventHandler(postNew_OnInsertPost);
        this.postNew.OnPreview += new EventHandler(postNew_OnPreview);
        this.postEdit.OnPreview += new EventHandler(postEdit_OnPreview);
        this.postEdit.OnCancelClick += new EventHandler(postEdit_OnCancelClick);
        this.postEdit.OnSaved += new EventHandler(postEdit_OnSaved);

        // Set forum
        this.treeElem.ForumID = this.mForumId;
        this.postNew.ForumID = this.mForumId;
        this.postEdit.ForumID = this.mForumId;

        // Get post ID
        postId = ValidationHelper.GetInteger(this.hdnPost.Value, 0);
        if (postId > 0)
        {
            this.postEdit.EditPostID = postId;
            post = ForumPostInfoProvider.GetForumPostInfo(postId);
        }

        // Unigrid settings
        UniGrid.Visible = false;
        UniGrid.Query = "";
        UniGrid.OnAction += new OnActionEventHandler(UniGrid_OnAction);
        UniGrid.OnExternalDataBound += new OnExternalDataBoundEventHandler(UniGrid_OnExternalDataBound);

        // Initialize page elements
        this.titleViewElem.TitleText = GetString("ForumPost_View.PostTitleText");
        this.titleViewElem.TitleImage = GetImageUrl("Objects/Forums_ForumPost/object.png");
        this.titleEditElem.TitleText = GetString("ForumPost_Edit.HeaderCaption");
        this.titleEditElem.TitleImage = GetImageUrl("Objects/Forums_ForumPost/object.png");
        this.lnkEditBack.Text = GetString("general.view");
        this.lnkEditBack.Click += new EventHandler(lnkEditBack_Click);

        if (post != null)
        {
            lblEditBack.Text = breadCrumbsSeparator + HTMLHelper.HTMLEncode(post.PostSubject);
            InitializeMenu();
        }
        
        // Add handlers
        treeElem.OnGetPostIconUrl += new CMSModules_Forums_Controls_PostTree.GetIconEventHandler(treeElem_OnGetPostIconUrl);

        this.actionsElem.ActionPerformed += new CommandEventHandler(actionsElem_ActionPerformed);
    }


    #region "Security handlers"

    void postNew_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        RaiseOnCheckPermissions(permissionType, sender);
    }


    void postEdit_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        RaiseOnCheckPermissions(permissionType, sender);
    }

    #endregion


    /// <summary>
    /// Unigrid External bound event handler.
    /// </summary>
    object UniGrid_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        // Display link instead of title
        if (sourceName.ToLower() == "title")
        {
            if (parameter != DBNull.Value)
            {
                DataRowView row = (DataRowView)parameter;

                // Get info
                Guid attachmentGuid = ValidationHelper.GetGuid(row["AttachmentGUID"], Guid.Empty);

                if (attachmentGuid != Guid.Empty)
                {
                    string url = URLHelper.GetAbsoluteUrl("~/CMSModules/Forums/CMSPages/GetForumAttachment.aspx?fileguid=" + attachmentGuid);
                    string title = ValidationHelper.GetString(row["AttachmentFileName"], "");

                    // Create link to post attachment
                    HyperLink link = new HyperLink();
                    link.NavigateUrl = url;
                    link.Target = "_blank";
                    link.Text = HTMLHelper.HTMLEncode(title);
                    link.ToolTip = url;
                    return link;
                }
            }
        }

        return parameter.ToString();
    }


    /// <summary>
    /// Unigrid Action event handler.
    /// </summary>
    void UniGrid_OnAction(string actionName, object actionArgument)
    {
        if (actionName == "delete")
        {
            if (!CheckPermissions("cms.forums", CMSAdminControl.PERMISSION_MODIFY))
            {
                return;
            }

            ForumAttachmentInfoProvider.DeleteForumAttachmentInfo(ValidationHelper.GetInteger(actionArgument, 0));
        }
    }


    protected void postNew_OnPreview(object sender, EventArgs e)
    {
        // Display new preview
        DisplayControl("newpreview");
    }


    void postEdit_OnCancelClick(object sender, EventArgs e)
    {
        // Display view
        DisplayControl("view");
    }


    protected void postEdit_OnPreview(object sender, EventArgs e)
    {
        // Display edit preview
        DisplayControl("editpreview");
    }


    protected void postEdit_OnSaved(object sender, EventArgs e)
    {
        // Display new view
        postView.PostData = null;
        DisplayControl("view");
    }


    protected void postNew_OnInsertPost(object sender, EventArgs e)
    {
        // Set properties
        postId = this.postNew.EditPostID;
        this.treeElem.Selected = postId;
        this.postEdit.EditPostID = postId;
        this.postView.PostID = postId;
        this.postNew.ReplyToPostID = 0;

        // Save post to database
        post = ForumPostInfoProvider.GetForumPostInfo(postId);
        this.hdnPost.Value = postId.ToString();
        DisplayControl("view");
    }


    /// <summary>
    /// Reloads the form data.
    /// </summary>
    public override void ReloadData()
    {
        hdnPost.Value = null;
        postId = 0;
        post = null;
        DisplayControl("new");
    }


    /// <summary>
    /// Initializes the menu.
    /// </summary>
    protected void InitializeMenu()
    {

        string[,] actions = new string[10, 11];

        int i = 0;

        // Edit
        actions[i, 0] = HeaderActions.TYPE_LINKBUTTON;
        actions[i, 1] = GetString("general.edit");
        actions[i, 4] = GetString("ForumPost_View.EditToolTip");
        actions[i, 5] = GetImageUrl("CMSModules/CMS_Forums/edit.png");
        actions[i, 6] = "edit";
        i++;

        // Delete
        actions[i, 0] = HeaderActions.TYPE_LINKBUTTON;
        actions[i, 1] = GetString("general.delete");
        actions[i, 2] = "if (!confirm(" + ScriptHelper.GetString(GetString("ForumPost_View.DeleteConfirmation")) + ")){return false;}";
        actions[i, 4] = GetString("ForumPost_View.DeleteToolTip");
        actions[i, 5] = GetImageUrl("CMSModules/CMS_Forums/delete.png");
        actions[i, 6] = "delete";
        i++;

        // Reply
        actions[i, 0] = HeaderActions.TYPE_LINKBUTTON;
        actions[i, 1] = GetString("ForumPost_View.IconReply");
        actions[i, 4] = GetString("ForumPost_View.ReplyToolTip");
        actions[i, 5] = GetImageUrl("CMSModules/CMS_Forums/reply.png");
        actions[i, 6] = "reply";
        i++;

        if (post.PostLevel == 0)
        {

            // Lock
            actions[i, 0] = HeaderActions.TYPE_LINKBUTTON;
            actions[i, 1] = GetString("ForumPost_View.IconLock");
            actions[i, 4] = GetString("ForumPost_View.LockToolTip");
            actions[i, 5] = GetImageUrl("CMSModules/CMS_Forums/lock.png");
            actions[i, 6] = "lockunlock";
            actions[i, 10] = Convert.ToString((post != null) && !post.PostIsLocked);
            i++;

            // UnLock
            actions[i, 0] = HeaderActions.TYPE_LINKBUTTON;
            actions[i, 1] = GetString("ForumPost_View.IconUnLock");
            actions[i, 4] = GetString("ForumPost_View.UnLockToolTip");
            actions[i, 5] = GetImageUrl("CMSModules/CMS_Forums/unlock.png");
            actions[i, 6] = "lockunlock";
            actions[i, 10] = Convert.ToString((post != null) && post.PostIsLocked);
            i++;

            // Stick
            actions[i, 0] = HeaderActions.TYPE_LINKBUTTON;
            actions[i, 1] = GetString("ForumPost_View.IconStick");
            actions[i, 4] = GetString("ForumPost_View.StickToolTip");
            actions[i, 5] = GetImageUrl("CMSModules/CMS_Forums/stick.png");
            actions[i, 6] = "stick";
            actions[i, 10] = Convert.ToString((post != null) && (post.PostStickOrder <= 0));
            i++;

            // UnStick
            actions[i, 0] = HeaderActions.TYPE_LINKBUTTON;
            actions[i, 1] = GetString("ForumPost_View.IconUnStick");
            actions[i, 4] = GetString("ForumPost_View.UnStickToolTip");
            actions[i, 5] = GetImageUrl("CMSModules/CMS_Forums/unstick.png");
            actions[i, 6] = "unstick";
            actions[i, 10] = Convert.ToString((post != null) && (post.PostStickOrder > 0));
            i++;

        }
        else
        {
            // Split
            actions[i, 0] = HeaderActions.TYPE_LINKBUTTON;
            actions[i, 1] = GetString("ForumPost_View.IconSplit");
            actions[i, 4] = GetString("ForumPost_View.SplitToolTip");
            actions[i, 5] = GetImageUrl("CMSModules/CMS_Forums/split.png");
            actions[i, 6] = "split";
            i++;
        }

        // Approve
        actions[i, 0] = HeaderActions.TYPE_LINKBUTTON;

        if (!post.PostApproved)
        {
            actions[i, 1] = GetString("general.approve");
            actions[i, 4] = GetString("ForumPost_View.ApproveToolTip");
            actions[i, 5] = GetImageUrl("CMSModules/CMS_Forums/approve.png");
            actions[i, 6] = "approve";

        }
        else
        {
            // Reject post
            actions[i, 1] = GetString("general.reject");
            actions[i, 4] = GetString("ForumPost_View.RejectToolTip");
            actions[i, 5] = GetImageUrl("CMSModules/CMS_Forums/reject.png");
            actions[i, 6] = "reject";
        }
        i++;

        // Approve subtree
        if (!post.PostApproved)
        {
            actions[i, 0] = HeaderActions.TYPE_LINKBUTTON;
            actions[i, 1] = GetString("ForumPost_View.IconApproveSubTree");
            actions[i, 4] = GetString("ForumPost_View.ApproveSubTreeToolTip");
            actions[i, 5] = GetImageUrl("CMSModules/CMS_Forums/approve.png");
            actions[i, 6] = "approvesubtree";
        }
        else
        {
            // Reject subtree
            actions[i, 0] = HeaderActions.TYPE_LINKBUTTON;
            actions[i, 1] = GetString("ForumPost_View.IconRejectSubTree");
            actions[i, 4] = GetString("ForumPost_View.RejectSubTreeToolTip");
            actions[i, 5] = GetImageUrl("CMSModules/CMS_Forums/reject.png");
            actions[i, 6] = "rejectsubtree";
        }

        i++;

        this.actionsElem.Actions = actions;
        this.actionsElem.SeparatorWidth = 19;
    }


    /// <summary>
    /// Handle actions.
    /// </summary>
    protected void actionsElem_ActionPerformed(object sender, CommandEventArgs e)
    {
        if (!CheckPermissions("cms.forums", CMSAdminControl.PERMISSION_MODIFY))
        {
            return;
        }

        switch (e.CommandName.ToLower())
        {
            case "stick":

                ForumPostInfoProvider.StickThread(post);

                // Get the post object with updated info
                post = ForumPostInfoProvider.GetForumPostInfo(post.PostId);
                DisplayControl("view");
                break;

            case "unstick":

                ForumPostInfoProvider.UnstickThread(post);

                // Get the post object with updated info
                post = ForumPostInfoProvider.GetForumPostInfo(post.PostId);
                DisplayControl("view");
                break;

            case "split":

                ForumPostInfoProvider.SplitThread(post);

                // Get the post object with updated info
                post = ForumPostInfoProvider.GetForumPostInfo(post.PostId);
                DisplayControl("view");
                break;

            case "lockunlock":
                // Lock or unlock post
                post.PostIsLocked = !post.PostIsLocked;
                ForumPostInfoProvider.SetForumPostInfo(post);
                DisplayControl("view");
                break;

            case "edit":
                // Edit
                DisplayControl("edit");
                break;

            case "delete":
                // Delete post
                ForumPostInfoProvider.DeleteForumPostInfo(postId);
                postNew.ClearForm();
                DisplayControl("new");
                break;

            case "reply":
                // Reply
                DisplayControl("reply");
                break;

            case "approve":
                // Approve action
                if (CMSContext.CurrentUser != null)
                {
                    post.PostApprovedByUserID = CMSContext.CurrentUser.UserID;
                    post.PostApproved = true;
                    ForumPostInfoProvider.SetForumPostInfo(post);
                }

                DisplayControl("view");
                break;

            case "reject":
                // Reject action
                post.PostApprovedByUserID = 0;
                post.PostApproved = false;
                ForumPostInfoProvider.SetForumPostInfo(post);

                DisplayControl("view");
                break;

            case "approvesubtree":
                // Approve subtree
                if ((post != null) && (CMSContext.CurrentUser != null))
                {
                    post.PostApprovedByUserID = CMSContext.CurrentUser.UserID;
                    post.PostApproved = true;
                    ForumPostInfoProvider.SetForumPostInfo(post);

                    DataSet ds = ForumPostInfoProvider.GetChildPosts(post.PostId);

                    if (!DataHelper.DataSourceIsEmpty(ds))
                    {
                        // All posts under current post
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            ForumPostInfo mfpi = new ForumPostInfo(dr);
                            if ((mfpi != null) && (!mfpi.PostApproved))
                            {
                                mfpi.PostApprovedByUserID = CMSContext.CurrentUser.UserID;
                                mfpi.PostApproved = true;
                                ForumPostInfoProvider.SetForumPostInfo(mfpi);
                            }
                        }
                    }

                    DisplayControl("view");
                }

                break;

            case "rejectsubtree":
                // Reject subtree
                if (post != null)
                {
                    post.PostApprovedByUserID = 0;
                    post.PostApproved = false;
                    ForumPostInfoProvider.SetForumPostInfo(post);

                    DataSet ds = ForumPostInfoProvider.GetChildPosts(post.PostId);

                    if (!DataHelper.DataSourceIsEmpty(ds))
                    {
                        // All posts under current post
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            ForumPostInfo mfpi = new ForumPostInfo(dr);
                            if ((mfpi != null) && (mfpi.PostApproved))
                            {
                                mfpi.PostApprovedByUserID = 0;
                                mfpi.PostApproved = false;
                                ForumPostInfoProvider.SetForumPostInfo(mfpi);
                            }
                        }
                    }
                    DisplayControl("view");
                }

                break;
        }

        this.hdnPost.Value = postId.ToString();

    }


    protected void lnkEditBack_Click(object sender, EventArgs e)
    {
        // Display view control
        DisplayControl("view");
    }


    protected string treeElem_OnGetPostIconUrl(CMS.Forums.ForumPostTreeNode node)
    {
        string imageUrl = "";

        if (node != null)
        {
            // Set image url
            imageUrl = GetImageUrl("CMSModules/CMS_Forums/post16.png");
            if (!ValidationHelper.GetBoolean(((DataRow)node.ItemData)["PostApproved"], false))
            {
                imageUrl = GetImageUrl("CMSModules/CMS_Forums/rejected16.png");
            }
        }

        return imageUrl;
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (ControlsHelper.IsInUpdatePanel(this))
        {
            ControlsHelper.UpdateCurrentPanel(this);
        }

        // Register script for show post
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "ForumPostPostback_value", ScriptHelper.GetScript(
        "function ShowPost(showId){ \n" +
        "    var hidden = document.getElementById('" + this.hdnPost.ClientID + "'); \n" +
        "    if (hidden != null) { \n" +
        "    hidden.value = showId; \n" +
        "    } \n" + CMS.ExtendedControls.ControlsHelper.GetPostBackEventReference(this, "showid") +
        "} \n"));

    }


    /// <summary>
    /// Displays only specified control. Other controls hides.
    /// </summary>
    /// <param name="selectedControl">Specified control</param>
    private void DisplayControl(string selectedControl)
    {
        // Tree element
        this.treeElem.ForumID = this.mForumId;
        this.treeElem.SelectOnlyApproved = false;
        this.treeElem.Selected = this.postId;

        // Hide elements
        this.plcPostEdit.Visible = false;
        this.plcPostView.Visible = false;
        this.plcPostNew.Visible = false;

        switch (selectedControl.ToLower())
        {
            case "view":
                // View mode
                if ((post != null) && (post.PostAttachmentCount > 0))
                {
                    string where = "(AttachmentPostID = " + postId + ")";

                    // Load unigrid
                    UniGrid.WhereCondition = where;
                    UniGrid.Query = "Forums.ForumAttachment.selectall";
                    UniGrid.Columns = "AttachmentID,AttachmentFileName,AttachmentFileSize,AttachmentGUID";
                    UniGrid.Visible = true;
                    UniGrid.ReloadData();
                }

                if (post != null)
                {
                    InitializeMenu();
                    actionsElem.ReloadData();
                }

                postNew.ForumID = this.mForumId;
                postView.PostID = postId;
                postView.PostData = null;
                plcPostView.Visible = true;
                postView.ReloadData();
                break;

            case "edit":
                // Edit mode
                postEdit.ForumID = this.mForumId;
                postEdit.EditPostID = postId;
                postEdit.ReloadData();
                plcPostEdit.Visible = true;
                break;

            case "reply":
                // Reply mode
                plcPostNew.Visible = true;
                postNew.ReplyToPostID = postId;
                postNew.ReloadData();
                break;

            case "newpreview":
                // New preview mode
                this.plcPostEdit.Visible = false;
                this.plcPostView.Visible = false;
                this.plcPostNew.Visible = true;
                break;

            case "editpreview":
                // Edit preview mode
                this.plcPostEdit.Visible = true;
                this.plcPostView.Visible = false;
                this.plcPostNew.Visible = false;
                break;

            default:
                // Default view mode
                postNew.ClearForm();
                if (postId > 0)
                {
                    postNew.ReplyToPostID = postId;
                }
                else
                {
                    postNew.ReplyToPostID = 0;
                }

                plcPostNew.Visible = true;
                postNew.ReloadData();

                break;

        }

    }


    #region IPostBackEventHandler Members

    public void RaisePostBackEvent(string eventArgument)
    {
        switch (eventArgument.ToLower())
        {
            case "showid":
                if (postId <= 0)
                {
                    ReloadData();
                }
                else
                {
                    DisplayControl("view");
                }
                break;
        }

    }

    #endregion
}

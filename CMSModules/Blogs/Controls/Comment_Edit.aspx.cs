using System;

using CMS.GlobalHelper;
using CMS.Blogs;
using CMS.CMSHelper;
using CMS.UIControls;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_Blogs_Controls_Comment_Edit : CMSModalPage
{
    protected int commentId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        commentId = QueryHelper.GetInteger("commentID", 0);

        // Get comment info
        BlogCommentInfo commentObj = BlogCommentInfoProvider.GetBlogCommentInfo(commentId);
        EditedObject = commentObj;

        if (commentObj != null)
        {
            // Get parent blog            
            TreeNode blogNode = BlogHelper.GetParentBlog(commentObj.CommentPostDocumentID, false);

            // Check site ID of edited blog
            if ((blogNode != null) && (blogNode.NodeSiteID != CMSContext.CurrentSiteID))
            {
                EditedObject = null;
            }

            bool isAuthorized = BlogHelper.IsUserAuthorizedToManageComments(blogNode);

            // Check "manage" permission
            if (!isAuthorized)
            {
                RedirectToAccessDenied("cms.blog", "Manage");
            }

            ctrlCommentEdit.CommentId = commentId;
        }

        btnOk.Click += btnOk_Click;
        btnOk.Text = GetString("General.OK");
        btnOk.ValidationGroup = ctrlCommentEdit.ValidationGroup;

        ctrlCommentEdit.IsLiveSite = false;
        ctrlCommentEdit.OnAfterCommentSaved += new OnAfterCommentSavedEventHandler(ctrlCommentEdit_OnAfterCommentSaved);

        CurrentMaster.Title.TitleText = GetString("Blog.CommentEdit.Title");
        CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Blog_Comment/object.png");
    }


    protected void btnOk_Click(object sender, EventArgs e)
    {
        ctrlCommentEdit.PerformAction();
    }


    protected void ctrlCommentEdit_OnAfterCommentSaved(BlogCommentInfo commentObj)
    {
        // Get filter parameters
        string filterParams = "?user=" + QueryHelper.GetText("user", "") + "&comment=" + QueryHelper.GetText("comment", "") +
                    "&approved=" + QueryHelper.GetText("approved", "") + "&isspam=" + QueryHelper.GetText("isspam", "");

        ltlScript.Text = ScriptHelper.GetScript("wopener.RefreshBlogCommentPage(" + ScriptHelper.GetString(filterParams) + ",'" + QueryHelper.GetBoolean("usepostback", false) + "');window.close();");
    }
}

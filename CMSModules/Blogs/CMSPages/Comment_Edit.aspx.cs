using System;

using CMS.GlobalHelper;
using CMS.Blogs;
using CMS.CMSHelper;
using CMS.UIControls;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_Blogs_CMSPages_Comment_Edit : CMSLiveModalPage
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
            TreeNode blogNode = BlogHelper.GetParentBlog(commentObj.CommentPostDocumentID, true);

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

        ctrlCommentEdit.OnAfterCommentSaved += new OnAfterCommentSavedEventHandler(ctrlCommentEdit_OnAfterCommentSaved);

        btnOk.Click += btnOk_Click;
        btnOk.Text = GetString("General.OK");
        btnOk.ValidationGroup = ctrlCommentEdit.ValidationGroup;

        titleElem.TitleText = GetString("Blog.CommentEdit.Title");
        titleElem.TitleImage = GetImageUrl("Objects/Blog_Comment/object.png");
    }


    protected void btnOk_Click(object sender, EventArgs e)
    {
        ctrlCommentEdit.PerformAction();
    }


    protected void ctrlCommentEdit_OnAfterCommentSaved(BlogCommentInfo commentObj)
    {
        // Get filter parameters
        string filterParams = "?user=" + QueryHelper.GetString("user", "") + "&comment=" + QueryHelper.GetString("comment", "") +
                    "&approved=" + QueryHelper.GetString("approved", "") + "&isspam=" + QueryHelper.GetString("isspam", "");

        ltlScript.Text = ScriptHelper.GetScript("wopener.RefreshPage(" + ScriptHelper.GetString(filterParams) + ");window.close();");
    }
}

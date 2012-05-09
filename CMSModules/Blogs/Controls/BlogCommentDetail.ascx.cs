using System;

using CMS.Blogs;
using CMS.CMSHelper;
using CMS.GlobalHelper;


public partial class CMSModules_Blogs_Controls_BlogCommentDetail : BlogCommentDetail
{ 

    protected void Page_Load(object sender, EventArgs e)
    {
        // Controls initialization
        lnkApprove.Text = GetString("general.approve");
        lnkReject.Text = GetString("general.reject");
        lnkEdit.Text = GetString("general.edit");
        lnkDelete.Text = GetString("general.delete");

        lnkEdit.Visible = this.ShowEditButton;
        lnkDelete.Visible = this.ShowDeleteButton;
        lnkApprove.Visible = this.ShowApproveButton;        
        lnkReject.Visible = this.ShowRejectButton;

        LoadData();

        ScriptHelper.RegisterDialogScript(this.Page);
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "DeleteCommentConfirmation",  ScriptHelper.GetScript("function ConfirmDelete(){ return confirm(" + ScriptHelper.GetString(GetString("BlogCommentDetail.DeleteConfirmation")) + ");}"));
    }


    /// <summary>
    /// Reload data.
    /// </summary>
    public void LoadData()
    {
        if (this.mCommentsDataRow != null)
        {
            // Load comment data
            BlogCommentInfo bci = new BlogCommentInfo(this.mCommentsDataRow);
            if (bci != null)
            {
                this.CommentID = bci.CommentID;
                
                // Set user picture
                if (this.BlogpPoperties.EnableUserPictures)
                {
                    userPict.UserID = bci.CommentUserID;
                    userPict.Width = this.BlogpPoperties.UserPictureMaxWidth;
                    userPict.Height = this.BlogpPoperties.UserPictureMaxHeight;
                    userPict.Visible = true;
                    userPict.RenderOuterDiv = true;
                    userPict.OuterDivCSSClass = "CommentUserPicture";
                }
                else
                {
                    userPict.Visible = false;                    
                }

                if (!String.IsNullOrEmpty(bci.CommentUrl))
                {
                    lnkName.Text = HTMLHelper.HTMLEncode(bci.CommentUserName);
                    lnkName.NavigateUrl = bci.CommentUrl;
                    lblName.Visible = false;
                }
                else
                {
                    lblName.Text = HTMLHelper.HTMLEncode(bci.CommentUserName);
                    lnkName.Visible = false;
                }

                lblText.Text = GetHTMLEncodedCommentText(bci);
                lblDate.Text = CMSContext.ConvertDateTime(bci.CommentDate, this).ToString();

                string url = "~/CMSModules/Blogs/Controls/Comment_Edit.aspx";
                if (this.IsLiveSite)
                {
                    url = "~/CMSModules/Blogs/CMSPages/Comment_Edit.aspx";
                }

                lnkEdit.OnClientClick = "EditComment('" + ResolveUrl(url) + "?commentID=" + this.CommentID + "');return false;";

                // Initialize report abuse
                ucInlineAbuseReport.ReportTitle = CMS.GlobalHelper.ResHelper.GetString("BlogCommentDetail.AbuseReport", CMS.SettingsProvider.SettingsKeyProvider.GetStringValue(CMSContext.CurrentSiteName + ".CMSDefaultCulture")) + bci.CommentText;
                ucInlineAbuseReport.ReportObjectID = this.CommentID;
                ucInlineAbuseReport.CMSPanel.Roles = AbuseReportRoles;
                ucInlineAbuseReport.CMSPanel.SecurityAccess = AbuseReportSecurityAccess;
                ucInlineAbuseReport.CMSPanel.OwnerID = AbuseReportOwnerID;
            }
        }
    }

    /// <summary>
    /// Returns HTML encoded comment text.
    /// </summary>
    private static string GetHTMLEncodedCommentText(BlogCommentInfo bci)
    {
        if (bci != null)
        {
            string comment = HTMLHelper.HTMLEncodeLineBreaks(bci.CommentText);

            // Trackback comment
            if (bci.CommentIsTrackback)
            {
                string from = "";
                if (string.IsNullOrEmpty(bci.CommentUserName))
                {
                    // Use blog post URL
                    from = bci.CommentUrl;
                }
                else
                {
                    // Use user name
                    from = bci.CommentUserName;
                }
                return HTMLHelper.HTMLEncode(string.Format(ResHelper.GetString("blog.comments.pingbackfrom"), from)) + "<br />" + comment;
            }
            // Normal comment
            else
            {
                return comment;
            }
        }
        return "";
    }


    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        FireOnCommentAction("delete", CommentID);
    }


    protected void lnkApprove_Click(object sender, EventArgs e)
    {
        FireOnCommentAction("approve", CommentID);
    }


    protected void lnkReject_Click(object sender, EventArgs e)
    {
        FireOnCommentAction("reject", CommentID);
    }
}

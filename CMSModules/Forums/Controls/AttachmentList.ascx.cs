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

using CMS.Forums;
using CMS.GlobalHelper;
using CMS.ExtendedControls;
using CMS.CMSHelper;

public partial class CMSModules_Forums_Controls_AttachmentList : ForumViewer
{
    /// <summary>
    /// Page load.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "DeleteConfirmation", ScriptHelper.GetScript(
            "function DeleteConfirm() { return confirm(" + ScriptHelper.GetString(GetString("forums.attachment.deleteconfirmation")) + "); } "));

        if ((ForumContext.CurrentPost != null) && (ForumContext.CurrentPost.PostId > 0))
        {
            btnUpload.Text = GetString("general.upload");
            btnBack.Text = GetString("general.back");

            if (ForumContext.CurrentForum != null)
            {
                if (ForumContext.CurrentForum.ForumAttachmentMaxFileSize > 0)
                {
                    lblInfo.Text = GetString("ForumAttachment.MaxFileSizeInfo").Replace("##SIZE##", ForumContext.CurrentForum.ForumAttachmentMaxFileSize.ToString());
                    lblInfo.Visible = true;
                }
            }

            if (ControlsHelper.IsInUpdatePanel(this))
            {
                ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.btnUpload);
            }

            if (!RequestHelper.IsPostBack())
            {
                // Get post attachments
                DataSet attachments = ForumAttachmentInfoProvider.GetForumAttachments(ForumContext.CurrentPost.PostId, false);
                if (!DataHelper.DataSourceIsEmpty(attachments))
                {
                    listAttachment.DataSource = attachments;
                    listAttachment.DataBind();
                }
                else
                {
                    plcListHeader.Visible = false;
                }
            }
        }
    }


    /// <summary>
    /// Handles delete button action - deletes user favorite.
    /// </summary>
    protected void btnDelete_OnCommand(object sender, CommandEventArgs e)
    {

        // Check permissions
        if (!this.IsAvailable(ForumContext.CurrentForum, ForumActionType.Attachment))
        {
            lblError.Visible = true;
            lblError.Text = GetString("ForumNewPost.PermissionDenied");
            return;
        }

        if (e.CommandName == "delete")
        {
            int attachmentId = ValidationHelper.GetInteger(e.CommandArgument, 0);

            // Get forum attachment info
            ForumAttachmentInfo fai = ForumAttachmentInfoProvider.GetForumAttachmentInfo(attachmentId);
            if (fai != null)
            {
                // Delete attachment
                ForumAttachmentInfoProvider.DeleteForumAttachmentInfo(fai);
            }

            //Reload page
            URLHelper.Redirect(URLHelper.CurrentURL);
        }
    }


    /// <summary>
    /// Handles file upload.
    /// </summary>
    protected void btnUpload_OnClick(object sender, EventArgs e)
    {
        if (ForumContext.CurrentForum == null)
        {
            return;
        }

        // Check permissions
        if (!this.IsAvailable(ForumContext.CurrentForum, ForumActionType.Attachment))
        {
            lblError.Visible = true;
            lblError.Text = GetString("ForumNewPost.PermissionDenied");
            return;
        }

        if (fileUpload.HasFile)
        {
            // Check max attachment size
            if ((ForumContext.CurrentForum.ForumAttachmentMaxFileSize > 0) && ((fileUpload.PostedFile.InputStream.Length / 1024) >= ForumContext.CurrentForum.ForumAttachmentMaxFileSize))
            {
                lblError.Visible = true;
                lblError.Text = GetString("ForumAttachment.AttachmentIsTooLarge");
                return;
            }

            // Check attachment extension
            if (!ForumAttachmentInfoProvider.IsExtensionAllowed(fileUpload.FileName, this.SiteName))
            {
                lblError.Visible = true;
                lblError.Text = GetString("ForumAttachment.AttachmentIsNotAllowed");
                return;
            }

            ForumAttachmentInfo attachmentInfo = new ForumAttachmentInfo(fileUpload.PostedFile, 0, 0, ForumContext.CurrentForum.ForumImageMaxSideSize);
            attachmentInfo.AttachmentPostID = ForumContext.CurrentPost.PostId;
            ForumGroupInfo fgi = ForumGroupInfoProvider.GetForumGroupInfo(ForumContext.CurrentForum.ForumGroupID);
            if (fgi != null)
            {
                attachmentInfo.AttachmentSiteID = fgi.GroupSiteID;

                //Save to DB
                ForumAttachmentInfoProvider.SetForumAttachmentInfo(attachmentInfo);
                DataSet ds = ForumAttachmentInfoProvider.GetForumAttachments(ForumContext.CurrentPost.PostId, false);
                if (!DataHelper.DataSourceIsEmpty(ds))
                {
                    listAttachment.DataSource = ds;
                    listAttachment.DataBind();
                    plcListHeader.Visible = true;
                }
            }
        }
    }


    /// <summary>
    /// Returns the url of attachment file.
    /// </summary>
    /// <param name="attachmeentGuid">Guid of attachment</param>    
    protected string GetAttachmentUrl(object attachmeentGuid)
    {
        Guid guid = ValidationHelper.GetGuid(attachmeentGuid, Guid.Empty);

        // Guid is ok
        if (guid != Guid.Empty)
        {
            // Return attachment url
            return ResolveUrl("~/CMSModules/Forums/CMSPages/GetForumAttachment.aspx?fileguid=" + guid);
        }
        else
        {
            return "#";
        }
    }


    /// <summary>
    /// Handles Back button click.
    /// </summary>
    protected void btnBack_OnClick(object sender, EventArgs e)
    {
        URLHelper.Redirect(ClearURL());
    }
}

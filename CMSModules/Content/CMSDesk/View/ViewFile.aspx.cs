using System;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.TreeEngine;
using CMS.WorkflowEngine;
using CMS.FileManager;
using CMS.UIControls;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_Content_CMSDesk_View_ViewFile : CMSContentPage
{
    #region "Variables"

    int nodeId = 0;

    #endregion


    #region "Page events"

    protected override void OnPreInit(EventArgs e)
    {
        ((Panel)CurrentMaster.PanelBody.FindControl("pnlContent")).CssClass = "";
        base.OnPreInit(e);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        lblFileSize.Text = GetString("ViewFile.FileSize");

        // Gets current nodeID
        nodeId = QueryHelper.GetInteger("nodeid", 0);

        // Get the node
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

        // Get current node
        TreeNode node = DocumentHelper.GetDocument(nodeId, CMSContext.PreferredCultureCode, tree);

        if (node != null)
        {
            // Get guid
            Guid guid = ValidationHelper.GetGuid(node.GetValue("FileAttachment"), Guid.Empty);

            //Get latest version
            if (guid != Guid.Empty)
            {
                AttachmentInfo atInfo = DocumentHelper.GetAttachment(node, guid, tree, false);

                // if file exist, check filetype and create texts and links
                if (atInfo != null)
                {
                    lblFileSizeText.Text = atInfo.AttachmentSize.ToString();
                    lblFileNameText.Text = atInfo.AttachmentName;

                    // Get attachment URL
                    string attUrl = null;
                    if (node.NodeClassName.ToLower() == "cms.file")
                    {
                        attUrl = "~/CMSPages/GetFile.aspx?nodeguid=" + node.NodeGUID;  //DocumentHelper.GetAttachmentUrl(atInfo, versionHistoryId);
                    }
                    else
                    {
                        int versionHistoryId = node.DocumentCheckedOutVersionHistoryID;
                        attUrl = DocumentHelper.GetAttachmentUrl(atInfo, versionHistoryId);
                    }

                    // Setup the display information
                    if (ImageHelper.IsImage(atInfo.AttachmentExtension))
                    {
                        if ((atInfo.AttachmentImageWidth != 0) && (atInfo.AttachmentImageHeight != 0))
                        {
                            // Image, show preview
                            plcSize.Visible = true;
                            lblSize.Text = GetString("ViewFile.Size");
                            lblSizeText.Text = atInfo.AttachmentImageWidth + "x" + atInfo.AttachmentImageHeight;
                        }

                        plcImage.Visible = true;
                        imgPreview.ImageUrl = attUrl + "&maxsidesize=600";

                        lnkView.NavigateUrl = attUrl;
                        lnkView.Text = GetString("ViewFile.OpenInFull");
                    }
                    else
                    {
                        // Document, open link
                        lnkView.Text = GetString("ViewFile.Open");
                        lnkView.NavigateUrl = attUrl;
                    }

                    // Register js synchronization script for split mode
                    if (CMSContext.DisplaySplitMode)
                    {
                        RegisterSplitModeSync(true, false);
                    }
                }
            }
        }
    }

    #endregion
}

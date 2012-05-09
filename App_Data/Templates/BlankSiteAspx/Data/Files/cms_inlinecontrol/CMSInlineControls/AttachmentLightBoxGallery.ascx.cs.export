using System;

using CMS.CMSHelper;
using CMS.ExtendedControls;
using CMS.SettingsProvider;
using CMS.UIControls;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSInlineControls_AttachmentLightBoxGallery : InlineUserControl
{
    #region "Variables"

    CMSUserControl ucAttachments = null;

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        ucAttachments = this.Page.LoadControl("~/CMSModules/Content/Controls/Attachments/AttachmentLightboxGallery.ascx") as CMSUserControl;
        ucAttachments.ID = "ctrlAttachments";
        plcAttachmentLightBox.Controls.Add(ucAttachments);

        TreeNode currentDocument = CMSContext.CurrentDocument;
        if (currentDocument != null)
        {
            // Get document type transformation
            string transformationName = currentDocument.NodeClassName + ".AttachmentLightbox";
            string selectedTransformationName = currentDocument.NodeClassName + ".AttachmentLightboxDetail";
            TransformationInfo ti = TransformationInfoProvider.GetTransformation(transformationName);

            // If transformation not present, use default from the Root document type
            if (ti == null)
            {
                transformationName = "cms.root.AttachmentLightbox";
                ti = TransformationInfoProvider.GetTransformation(transformationName);
            }
            if (ti == null)
            {
                throw new Exception("[DocumentAttachments]: Default transformation '" + transformationName + "' doesn't exist!");
            }
            ti = TransformationInfoProvider.GetTransformation(selectedTransformationName);

            // If transformation not present, use default from the Root document type
            if (ti == null)
            {
                selectedTransformationName = "cms.root.AttachmentLightboxDetail";
                ti = TransformationInfoProvider.GetTransformation(selectedTransformationName);
            }
            if (ti == null)
            {
                throw new Exception("[DocumentAttachments]: Default transformation '" + selectedTransformationName + "' doesn't exist!");
            }

            ucAttachments.SetValue("TransformationName", transformationName);
            ucAttachments.SetValue("SelectedItemTransformationName", selectedTransformationName);
            ucAttachments.SetValue("SiteName", CMSContext.CurrentSiteName);
            ucAttachments.SetValue("Path", currentDocument.NodeAliasPath);
            ucAttachments.SetValue("CultureCode", currentDocument.DocumentCulture);
            ucAttachments.SetValue("OrderBy", "AttachmentOrder, AttachmentName");
            ucAttachments.SetValue("PageSize", 0);
            ucAttachments.SetValue("GetBinary", false);
            ucAttachments.SetValue("CacheMinutes", SettingsKeyProvider.GetIntValue(CMSContext.CurrentSite + ".CMSCacheMinutes"));
        }
    }

    #endregion
}


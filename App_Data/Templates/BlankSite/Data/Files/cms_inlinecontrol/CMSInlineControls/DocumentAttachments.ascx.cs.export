using System;

using CMS.ExtendedControls;
using CMS.CMSHelper;
using CMS.SettingsProvider;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSInlineControls_DocumentAttachments : InlineUserControl
{
    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        TreeNode currentDocument = CMSContext.CurrentDocument;
        if (currentDocument != null)
        {
            // Get document type transformation
            string transformationName = currentDocument.NodeClassName + ".attachment";
            TransformationInfo ti = TransformationInfoProvider.GetTransformation(transformationName);
            // If transformation not present, use default from the Root document type
            if (ti == null)
            {
                transformationName = "cms.root.attachment";
                ti = TransformationInfoProvider.GetTransformation(transformationName);
            }

            if (ti == null)
            {
                throw new Exception("[DocumentAttachments]: Default transformation '" + transformationName + "' doesn't exist!");
            }

            ucAttachments.TransformationName = transformationName;
            ucAttachments.SiteName = CMSContext.CurrentSiteName;
            ucAttachments.Path = currentDocument.NodeAliasPath;
            ucAttachments.CultureCode = currentDocument.DocumentCulture;
            ucAttachments.OrderBy = "AttachmentOrder, AttachmentName";
            ucAttachments.PageSize = 0;
            ucAttachments.GetBinary = false;
            ucAttachments.CacheMinutes = SettingsKeyProvider.GetIntValue(CMSContext.CurrentSite + ".CMSCacheMinutes");
        }
    }

    #endregion
}


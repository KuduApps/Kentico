using System;
using System.Data;
using System.Web;
using System.Web.UI;

using CMS.SiteProvider;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.ExtendedControls;
using CMS.IO;
using CMS.PortalEngine;

public partial class CMSModules_DocumentTypes_Pages_Development_DocumentType_Edit_Transformation_Theme : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Setup the filesystem browser
        int transformationId = QueryHelper.GetInteger("transformationid", 0);
        if (transformationId > 0)
        {
            TransformationInfo ti = TransformationInfoProvider.GetTransformation(transformationId);
            EditedObject = ti;

            DataClassInfo dci = DataClassInfoProvider.GetDataClass(ti.TransformationClassID);

            if (dci != null)
            {
                // Ensure the theme folder
                themeElem.Path = "~/App_Themes/Components/Transformations/" + ValidationHelper.GetSafeFileName(dci.ClassName) + "/" + ValidationHelper.GetSafeFileName(ti.TransformationName);
            }
        }
        else
        {
            EditedObject = null;
        }
    }
}

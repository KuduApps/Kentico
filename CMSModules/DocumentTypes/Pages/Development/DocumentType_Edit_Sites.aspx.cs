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
using CMS.SettingsProvider;
using CMS.DataEngine;
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSModules_DocumentTypes_Pages_Development_DocumentType_Edit_Sites : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // gets classID from querystring
        int classId = QueryHelper.GetInteger("documenttypeid", 0);
        if (classId > 0)
        {
            classSites.TitleString = GetString("DocumentType_Edit_Sites.Info");
            classSites.ClassId = classId;
            classSites.CheckLicense = false;
        }
    }
}

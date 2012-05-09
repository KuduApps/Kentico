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
using CMS.UIControls;

public partial class CMSModules_DocumentTypes_Pages_AlternativeForms_AlternativeForms_Frameset : SiteManagerPage
{
    #region "Variables"

    protected int classId = 0;
    protected int altFormId = 0;
    protected int saved = 0;

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        classId = QueryHelper.GetInteger("classid", 0);
        altFormId = QueryHelper.GetInteger("altformid", 0);
        saved = QueryHelper.GetInteger("saved", 0);
    }
}

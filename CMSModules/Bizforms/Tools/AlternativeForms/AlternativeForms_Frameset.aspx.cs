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

public partial class CMSModules_BizForms_Tools_AlternativeForms_AlternativeForms_Frameset : CMSBizFormPage
{
    #region "Variables"

    protected int altformId = 0;
    protected int formId = 0;
    protected int saved = 0;

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        altformId = QueryHelper.GetInteger("altformid", 0);
        formId = QueryHelper.GetInteger("formid", 0);
        saved = QueryHelper.GetInteger("saved", 0);
    }
}

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

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSModules_BizForms_Tools_BizForm_Frameset : CMSBizFormPage
{
    protected string defaultTab = String.Empty;
    protected int formId = 0;
    protected bool newForm = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        // Get form ID from url
        formId = QueryHelper.GetInteger("formid", 0);
        // Learn if new form was created
        newForm = QueryHelper.GetBoolean("newform", false);

        if (formId > 0)
        {
            // Check 'ReadData' permission
            if (CMSContext.CurrentUser.IsAuthorizedPerResource("cms.form", "ReadData") && !newForm)
            {
                defaultTab = "BizForm_Edit_Data.aspx?formid=" + formId.ToString();
            }
            // Check 'ReadForm' permission
            else if (CMSContext.CurrentUser.IsAuthorizedPerResource("cms.form", "ReadForm"))
            {
                defaultTab = "BizForm_Edit_General.aspx?formid=" + formId.ToString();
            }
        }
    }
}

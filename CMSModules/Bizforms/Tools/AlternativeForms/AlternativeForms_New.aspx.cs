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
using CMS.CMSHelper;
using CMS.FormEngine;
using CMS.Synchronization;
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSModules_BizForms_Tools_AlternativeForms_AlternativeForms_New : CMSBizFormPage
{
    #region "Private variables"

    private BizFormInfo bfi = null;
    private int classId = 0;
    private int formId = 0;

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        // Check 'ReadForm' permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.form", "ReadForm"))
        {
            RedirectToCMSDeskAccessDenied("cms.form", "ReadForm");
        }

        formId = QueryHelper.GetInteger("formid", 0);
        bfi = BizFormInfoProvider.GetBizFormInfo(formId);
        EditedObject = bfi;

        if (bfi == null)
        {
            lblError.Visible = true;
            lblError.Text = GetString("general.invalidid");
            return;
        }

        classId = bfi.FormClassID;

        // Init breadcrumbs
        string[,] breadcrumbs = new string[2, 3];

        // Return to list item in breadcrumbs
        breadcrumbs[0, 0] = GetString("altforms.listlink");
        breadcrumbs[0, 1] = "~/CMSModules/BizForms/Tools/AlternativeForms/AlternativeForms_List.aspx?formid=" + formId;
        breadcrumbs[0, 2] = "";
        breadcrumbs[1, 0] = GetString("altform.newbread");
        breadcrumbs[1, 1] = "";
        breadcrumbs[1, 2] = "";

        this.CurrentMaster.Title.Breadcrumbs = breadcrumbs;

        nameElem.ShowSubmitButton = true;
        nameElem.Click += new EventHandler(nameElem_Click);
    }


    void nameElem_Click(object sender, EventArgs e)
    {
        // Check 'EditForm' permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.form", "EditForm"))
        {
            RedirectToCMSDeskAccessDenied("cms.form", "EditForm");
        }

        // Code name validation
        string err = new Validator().IsIdentificator(nameElem.CodeName, GetString("general.erroridentificatorformat")).Result;
        if (err != String.Empty)
        {
            lblError.Visible = true;
            lblError.Text = err;
            return;
        }

        // Checking for duplicate items
        DataSet ds = AlternativeFormInfoProvider.GetAlternativeForms("FormName='" + nameElem.CodeName +
            "' AND FormClassID=" + classId, null);

        if (!DataHelper.DataSourceIsEmpty(ds))
        {
            lblError.Visible = true;
            lblError.Text = GetString("general.codenameexists");
            return;
        }

        // Create new info object
        AlternativeFormInfo afi = new AlternativeFormInfo();
        afi.FormID = 0;
        afi.FormGUID = Guid.NewGuid();
        afi.FormClassID = classId;
        afi.FormName = nameElem.CodeName;
        afi.FormDisplayName = nameElem.DisplayName;

        AlternativeFormInfoProvider.SetAlternativeFormInfo(afi);

        // Required to log staging task, alternative form is not binded to bizform as child
        using (CMSActionContext context = new CMSActionContext())
        {
            context.CreateVersion = false;

            // Log synchronization
            SynchronizationHelper.LogObjectChange(bfi, TaskTypeEnum.UpdateObject);
        }

        URLHelper.Redirect("AlternativeForms_Frameset.aspx?formid=" + formId.ToString() +
            "&altformid=" + afi.FormID + "&saved=1");
    }
}

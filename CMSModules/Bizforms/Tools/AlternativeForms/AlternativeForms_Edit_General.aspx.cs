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
using CMS.FormEngine;
using CMS.CMSHelper;
using CMS.Synchronization;
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSModules_BizForms_Tools_AlternativeForms_AlternativeForms_Edit_General : CMSBizFormPage
{
    #region "Private variables

    protected int altFormId = 0;
    private int formId = 0;

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        // Check 'ReadForm' permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.form", "ReadForm"))
        {
            RedirectToCMSDeskAccessDenied("cms.form", "ReadForm");
        }

        altFormId = QueryHelper.GetInteger("altformid", 0);
        formId = QueryHelper.GetInteger("formid", 0);

        // Validate
        AlternativeFormInfo afi = AlternativeFormInfoProvider.GetAlternativeFormInfo(altFormId);
        EditedObject = afi;

        if (afi == null)
        {
            return;
        }

        // Init values
        if (!RequestHelper.IsPostBack())
        {
            nameElem.DisplayName = afi.FormDisplayName;
            nameElem.CodeName = afi.FormName;
        }
        nameElem.ShowSubmitButton = true;
        nameElem.Click += new EventHandler(nameElem_Click);

        if (QueryHelper.GetInteger("saved", 0) == 1)
        {
            lblInfo.Visible = true;
            lblInfo.Text = GetString("general.changessaved");

            // Reload header if changes were saved
            ScriptHelper.RefreshTabHeader(Page, null);
        }
    }


    /// <summary>
    /// Click event - updates new values.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Params</param>
    protected void nameElem_Click(object sender, EventArgs e)
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
            lblInfo.Visible = false;
            return;
        }

        // Validate form id
        AlternativeFormInfo afi = AlternativeFormInfoProvider.GetAlternativeFormInfo(altFormId);
        EditedObject = afi;
        if (afi == null)
        {
            return;
        }

        // Checking for duplicate items
        DataSet ds = AlternativeFormInfoProvider.GetAlternativeForms("FormName='" + SqlHelperClass.GetSafeQueryString(nameElem.CodeName, false) +
            "' AND FormClassID=" + afi.FormClassID, null);

        if (!DataHelper.DataSourceIsEmpty(ds))
        {
            if (!((ds.Tables.Count == 1) && (ds.Tables[0].Rows.Count == 1) && (
                ValidationHelper.GetInteger(ds.Tables[0].Rows[0]["FormID"], 0) == altFormId)))
            {
                lblError.Visible = true;
                lblError.Text = GetString("general.codenameexists");
                lblInfo.Visible = false;
                return;
            }
        }

        afi.FormDisplayName = nameElem.DisplayName;
        afi.FormName = nameElem.CodeName;
        AlternativeFormInfoProvider.SetAlternativeFormInfo(afi);

        // Required to log staging task, alternative form is not binded to bizform as child
        using (CMSActionContext context = new CMSActionContext())
        {
            context.CreateVersion = false;

            // Log synchronization
            BizFormInfo bfi = BizFormInfoProvider.GetBizFormInfo(formId);
            SynchronizationHelper.LogObjectChange(bfi, TaskTypeEnum.UpdateObject);
        }

        URLHelper.Redirect("AlternativeForms_Edit_General.aspx?altformid=" + altFormId + "&formid=" + formId + "&saved=1");
    }
}

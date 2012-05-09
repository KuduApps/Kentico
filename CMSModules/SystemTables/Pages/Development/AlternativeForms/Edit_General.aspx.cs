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
using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.SiteProvider;

public partial class CMSModules_SystemTables_Pages_Development_AlternativeForms_Edit_General : SiteManagerPage
{
    #region "Private variables"

    protected int altFormId = 0;

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        altFormId = QueryHelper.GetInteger("altformid", 0);

        // Validate
        AlternativeFormInfo afi = AlternativeFormInfoProvider.GetAlternativeFormInfo(altFormId);
        EditedObject = afi;

        // Initialize controls
        btnOk.Text = GetString("general.ok");
        rfvCodeName.ErrorMessage = GetString("general.requirescodename");
        rfvDisplayName.ErrorMessage = GetString("general.requiresdisplayname");

        if (!RequestHelper.IsPostBack())
        {
            // Init values
            this.txtCodeName.Text = afi.FormName;
            this.txtDisplayName.Text = afi.FormDisplayName;

            string className = DataClassInfoProvider.GetClassName(afi.FormClassID);
            if (className.ToLower().Trim() == SiteObjectType.USER.ToLower())
            {
                this.pnlCombineUserSettings.Visible = true;
                this.chkCombineUserSettings.Checked = (afi.FormCoupledClassID > 0);
            }
        }

        btnOk.Click += new EventHandler(btnOK_Click);

        if (QueryHelper.GetInteger("saved", 0) == 1)
        {
            lblInfo.Visible = true;
            lblInfo.Text = GetString("general.changessaved");
        }
    }
    

    /// <summary>
    /// Click event - updates new values.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Params</param>
    void btnOK_Click(object sender, EventArgs e)
    {
        // Code name validation
        string err = new Validator().IsIdentificator(txtCodeName.Text, GetString("general.erroridentificatorformat")).Result;
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
        DataSet ds = AlternativeFormInfoProvider.GetAlternativeForms("FormName='" + SqlHelperClass.GetSafeQueryString(txtCodeName.Text, false) +
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

        afi.FormDisplayName = txtDisplayName.Text;
        afi.FormName = txtCodeName.Text;
        AlternativeFormInfoProvider.SetAlternativeFormInfo(afi);

        lblInfo.Visible = true;
        lblInfo.Text = GetString("general.changessaved");

        // Reload header if changes were saved
        ScriptHelper.RefreshTabHeader(Page, null);
    }
}

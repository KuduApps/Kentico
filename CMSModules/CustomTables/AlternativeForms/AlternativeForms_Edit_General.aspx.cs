using System;
using System.Data;

using CMS.GlobalHelper;
using CMS.FormEngine;
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSModules_CustomTables_AlternativeForms_AlternativeForms_Edit_General : CMSCustomTablesPage
{
    #region "Protected variables"

    protected int altFormId = 0;
    protected int classId = 0;

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        altFormId = QueryHelper.GetInteger("altformid", 0);

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
        nameElem.Click += nameElem_Click;

        if (QueryHelper.GetInteger("saved", 0) == 1)
        {
            lblInfo.Visible = true;
            lblInfo.Text = GetString("general.changessaved");

            // Reload header if changes were saved
            ScriptHelper.RefreshTabHeader(Page, null);
        }
    }

    #endregion


    #region "Other events and methods"

    /// <summary>
    /// Click event - updates new values.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Params</param>
    protected void nameElem_Click(object sender, EventArgs e)
    {
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

        lblInfo.Visible = true;
        lblInfo.Text = GetString("general.changessaved");

        // Reload header if changes were saved
        ScriptHelper.RefreshTabHeader(Page, null);
    }

    #endregion
}

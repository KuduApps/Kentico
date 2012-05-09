using System;
using System.Data;

using CMS.GlobalHelper;
using CMS.FormEngine;
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSModules_CustomTables_AlternativeForms_AlternativeForms_New : CMSCustomTablesPage
{
    #region "Private variables"

    private int classId = 0;

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        classId = QueryHelper.GetInteger("classid", 0);

        // Init breadcrumbs
        string[,] breadcrumbs = new string[2, 3];

        // Return to list item in breadcrumbs
        breadcrumbs[0, 0] = GetString("altforms.listlink");
        breadcrumbs[0, 1] = "~/CMSModules/Customtables/AlternativeForms/AlternativeForms_List.aspx?classid=" + classId;
        breadcrumbs[0, 2] = string.Empty;
        breadcrumbs[1, 0] = GetString("altform.newbread");
        breadcrumbs[1, 1] = string.Empty;
        breadcrumbs[1, 2] = string.Empty;

        CurrentMaster.Title.Breadcrumbs = breadcrumbs;

        nameElem.ShowSubmitButton = true;
        nameElem.Click += nameElem_Click;

        // Load data
        lblError.Visible = false;
    }

    #endregion


    protected void nameElem_Click(object sender, EventArgs e)
    {
        // Code name validation
        string err = new Validator().IsIdentificator(nameElem.CodeName, GetString("general.erroridentificatorformat")).Result;
        if (err != String.Empty)
        {
            lblError.Visible = true;
            lblError.Text = err;
            return;
        }

        // Checking for duplicate items
        DataSet ds = AlternativeFormInfoProvider.GetAlternativeForms("FormName='" + SqlHelperClass.GetSafeQueryString(nameElem.CodeName, false) +
            "' AND FormClassID=" + classId, null);

        if (!DataHelper.DataSourceIsEmpty(ds))
        {
            lblError.Visible = true;
            lblError.Text = GetString("general.codenameexists");
            return;
        }

        // Create new info object
        AlternativeFormInfo alternativeFormInfo = new AlternativeFormInfo();
        alternativeFormInfo.FormID = 0;
        alternativeFormInfo.FormGUID = Guid.NewGuid();
        alternativeFormInfo.FormClassID = classId;
        alternativeFormInfo.FormName = nameElem.CodeName;
        alternativeFormInfo.FormDisplayName = nameElem.DisplayName;

        try
        {
            AlternativeFormInfoProvider.SetAlternativeFormInfo(alternativeFormInfo);
            URLHelper.Redirect("AlternativeForms_Frameset.aspx?classid=" + classId + "&altformid=" + alternativeFormInfo.FormID + "&saved=1");
        }
        catch (Exception ex)
        {
            lblError.Visible = true;
            lblError.Text = ex.Message;
        }
    }
}

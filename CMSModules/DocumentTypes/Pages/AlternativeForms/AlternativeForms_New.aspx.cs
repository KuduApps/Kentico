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

public partial class CMSModules_DocumentTypes_Pages_AlternativeForms_AlternativeForms_New : SiteManagerPage
{
    private int classId = 0;
    private AlternativeFormInfo afi = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        classId = QueryHelper.GetInteger("classid", 0);

        // Init breadcrumbs
        string[,] breadcrumbs = new string[2, 3];

        // Return to list item in breadcrumbs
        breadcrumbs[0, 0] = GetString("altforms.listlink");
        breadcrumbs[0, 1] = "~/CMSModules/DocumentTypes/Pages/AlternativeForms/AlternativeForms_List.aspx?classid=" + classId;
        breadcrumbs[0, 2] = "";
        breadcrumbs[1, 0] = GetString("altform.newbread");
        breadcrumbs[1, 1] = "";
        breadcrumbs[1, 2] = "";

        this.CurrentMaster.Title.Breadcrumbs = breadcrumbs;

        nameElem.ShowSubmitButton = true;
        nameElem.Click += new EventHandler(nameElem_Click);

        // Load data
        if (afi != null)
        {
            nameElem.CodeName = afi.FormName;
            nameElem.DisplayName = afi.FormDisplayName;
        }
        
        lblError.Visible = false;
    }

    
    void nameElem_Click(object sender, EventArgs e)
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
        AlternativeFormInfo afi = new AlternativeFormInfo();
        afi.FormID = 0;
        afi.FormGUID = Guid.NewGuid();
        afi.FormClassID = classId;
        afi.FormName = nameElem.CodeName;
        afi.FormDisplayName = nameElem.DisplayName;

        AlternativeFormInfoProvider.SetAlternativeFormInfo(afi);

        URLHelper.Redirect("AlternativeForms_Frameset.aspx?classid=" + classId + "&altformid=" + afi.FormID + "&saved=1");
    }
}

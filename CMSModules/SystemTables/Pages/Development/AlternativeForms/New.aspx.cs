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

public partial class CMSModules_SystemTables_Pages_Development_AlternativeForms_New : SiteManagerPage
{
    private int classId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        classId = QueryHelper.GetInteger("classid", 0);

        // Init breadcrumbs
        string[,] breadcrumbs = new string[2, 3];

        // Return to list item in breadcrumbs
        breadcrumbs[0, 0] = GetString("altforms.listlink");
        breadcrumbs[0, 1] = "~/CMSModules/SystemTables/Pages/Development/AlternativeForms/List.aspx?classid=" + classId;
        breadcrumbs[0, 2] = "";
        breadcrumbs[1, 0] = GetString("altform.newbread");
        breadcrumbs[1, 1] = "";
        breadcrumbs[1, 2] = "";

        this.CurrentMaster.Title.Breadcrumbs = breadcrumbs;

        // Initialize controls                
        btnOk.Text = GetString("general.ok");
        rfvCodeName.ErrorMessage = GetString("general.requirescodename");
        rfvDisplayName.ErrorMessage = GetString("general.requiresdisplayname");

        this.btnOk.Click += new EventHandler(btnOK_Click);

        // Check if the 'Combine With User Settings' feature should be available
        if (classId > 0)
        {
            string className = DataClassInfoProvider.GetClassName(classId);
            if (className != null && (className.ToLower().Trim() == SiteObjectType.USER.ToLower()))
            {
                this.pnlCombineUserSettings.Visible = true;
            }
        }

        lblError.Visible = false;
    }


    void btnOK_Click(object sender, EventArgs e)
    {
        // Code name validation
        string err = new Validator().IsIdentificator(this.txtCodeName.Text, GetString("general.erroridentificatorformat")).Result;
        if (err != String.Empty)
        {
            lblError.Visible = true;
            lblError.Text = err;
            return;
        }

        // Checking for duplicate items
        DataSet ds = AlternativeFormInfoProvider.GetAlternativeForms("FormName='" + SqlHelperClass.GetSafeQueryString(this.txtCodeName.Text, false) +
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
        afi.FormName = this.txtCodeName.Text;
        afi.FormDisplayName = this.txtDisplayName.Text;

        DataClassInfo dci = DataClassInfoProvider.GetDataClass(SiteObjectType.USERSETTINGS);
        if (dci != null)
        {
            afi.FormCoupledClassID = (this.chkCombineUserSettings.Checked) ? dci.ClassID : 0;
        }

        AlternativeFormInfoProvider.SetAlternativeFormInfo(afi);

        URLHelper.Redirect("Frameset.aspx?classid=" + classId + "&altformid=" + afi.FormID + "&saved=1");
    }
}

using System;
using System.Web.UI;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.ExtendedControls;

public partial class CMSFormControls_Selectors_SelectFileOrFolder_Content : CMSModalPage
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        this.CheckGlobalAdministrator();
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
                
        if (QueryHelper.ValidateHash("hash"))
        {
            ScriptHelper.RegisterJQuery(this.Page);
            CMSDialogHelper.RegisterDialogHelper(this.Page);
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "InitResizers", "$j(InitResizers());", true);

            this.fileSystem.InitFromQueryString();

        }
        else
        {
            string url = ResolveUrl("~/CMSMessages/Error.aspx?title=" + GetString("dialogs.badhashtitle") + "&text=" + GetString("dialogs.badhashtext") + "&cancel=1");
            this.ltlScript.Text = ScriptHelper.GetScript("if (window.parent != null) { window.parent.location = '" + url + "' }");
        }
    }
}

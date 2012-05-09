using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.CMSHelper;
using CMS.GlobalHelper;

public partial class CMSModules_FileImport_Tools_ImportFromComputer : CMSFileImportPage
{
    #region "Page methods"

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        // Check UI personalization
        CurrentUserInfo user = CMSContext.CurrentUser;
        if (!user.IsAuthorizedPerUIElement("CMS.FileImport", "ImportFromComputer"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.FileImport", "ImportFromComputer");
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentMaster.HeaderActionsPlaceHolder.Visible = false;

        // Initialize selectors
        cultureSelector.SiteID = CMSContext.CurrentSiteID;
        pathElem.SiteID = CMSContext.CurrentSiteID;

        pathElem.Changed += pathElem_Changed;

        cultureSelector.DropDownCultures.AutoPostBack = true;
        cultureSelector.DropDownCultures.SelectedIndexChanged += cultureSelector_Changed;

        chkIncludeExtension.AutoPostBack = true;
        chkIncludeExtension.CheckedChanged += chkIncludeExtension_CheckedChanged;
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        InitializeUploader();
    }

    #endregion


    #region "Selectors methods"

    private void chkIncludeExtension_CheckedChanged(object sender, EventArgs e)
    {
        InitializeUploader();
    }


    private void cultureSelector_Changed(object sender, EventArgs e)
    {
        InitializeUploader();
    }


    private void pathElem_Changed(object sender, EventArgs e)
    {
        InitializeUploader();
    }


    private void InitializeUploader()
    {
        uploaderElem.NodeID = pathElem.NodeId;
        uploaderElem.DocumentCulture = cultureSelector.Value.ToString();
        uploaderElem.IncludeExtension = chkIncludeExtension.Checked;

        string script = String.Format(@"
var hdnUploaderOptions = document.getElementById('{0}_hdnUploaderOptions');
if (hdnUploaderOptions) {{
    hdnUploaderOptions.value = 'NodeID|{1}|DocumentCulture|{2}|IncludeExtension|{3}|NodeGroupID|0';
}}", uploaderElem.ClientID, pathElem.NodeId, cultureSelector.Value, chkIncludeExtension.Checked);

        ScriptHelper.RegisterStartupScript(Page, typeof(string), "UploaderInit", script, true);
    }

    #endregion
}

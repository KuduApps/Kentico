using System;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.ExtendedControls;
using CMS.CMSHelper;

public partial class CMSFormControls_Selectors_InsertImageOrMedia_Tabs_WebLink : CMSModalPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Check UIProfile
        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.WYSIWYGEditor", "InsertLink"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.WYSIWYGEditor", "InsertLink");
        }
        else if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.MediaDialog", "WebTab"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.MediaDialog", "WebTab");
        }

        if (QueryHelper.ValidateHash("hash"))
        {
            ScriptHelper.RegisterJQuery(Page);
            CMSDialogHelper.RegisterDialogHelper(Page);
        }
        else
        {
            string url = ResolveUrl("~/CMSMessages/Error.aspx?title=" + GetString("dialogs.badhashtitle") + "&text=" + GetString("dialogs.badhashtext") + "&cancel=1");
            ltlScript.Text = ScriptHelper.GetScript("if (window.parent != null) { window.parent.location = '" + url + "' }");
        }
    }
}

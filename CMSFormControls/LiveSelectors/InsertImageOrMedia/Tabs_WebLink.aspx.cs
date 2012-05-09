using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.ExtendedControls;
using CMS.CMSHelper;
using CMS.SettingsProvider;

public partial class CMSFormControls_LiveSelectors_InsertImageOrMedia_Tabs_WebLink : CMSLiveModalPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bool checkUI = ValidationHelper.GetBoolean(SettingsHelper.AppSettings["CKEditor:PersonalizeToolbarOnLiveSite"], false);
        if (checkUI)
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
        }

        if (QueryHelper.ValidateHash("hash"))
        {
            ScriptHelper.RegisterJQuery(this.Page);
            CMSDialogHelper.RegisterDialogHelper(this.Page);
        }
        else
        {
            string url = ResolveUrl("~/CMSMessages/Error.aspx?title=" + GetString("dialogs.badhashtitle") + "&text=" + GetString("dialogs.badhashtext") + "&cancel=1");
            this.ltlScript.Text = ScriptHelper.GetScript("if (window.parent != null) { window.parent.location = '" + url + "' }");
        }
    }
}

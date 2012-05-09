using System;

using CMS.UIControls;
using CMS.GlobalHelper;

public partial class CMSFormControls_Selectors_InsertImageOrMedia_Header : CMSModalPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (QueryHelper.ValidateHash("hash"))
        {
            header.CurrentMaster = CurrentMaster;
            header.InitFromQueryString();
        }
        else
        {
            string url = ResolveUrl("~/CMSMessages/Error.aspx?title=" + GetString("dialogs.badhashtitle") + "&text=" + GetString("dialogs.badhashtext") + "&cancel=1");
            ltlScript.Text = ScriptHelper.GetScript("if (window.parent != null) { window.parent.location = '" + url + "' }");
        }
    }
}

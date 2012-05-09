using System;

using CMS.UIControls;
using CMS.GlobalHelper;

public partial class CMSFormControls_Selectors_SelectFileOrFolder_Header : CMSModalPage
{    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (QueryHelper.ValidateHash("hash"))
        {
            if (QueryHelper.GetBoolean("show_folders", false))
            {
                this.CurrentMaster.Title.TitleText = GetString("dialogs.header.title.selectfolder");
            }
            else
            {
                this.CurrentMaster.Title.TitleText = GetString("dialogs.header.title.selectfiles");
            }
            this.CurrentMaster.Title.TitleImage = URLHelper.ImagesDirectory + "/CMSModules/CMS_Content/Dialogs/titlefilesystem.png";
        }
        else
        {
            string url = ResolveUrl("~/CMSMessages/Error.aspx?title=" + GetString("dialogs.badhashtitle") + "&text=" + GetString("dialogs.badhashtext") + "&cancel=1");
            this.ltlScript.Text = ScriptHelper.GetScript("if (window.parent != null) { window.parent.location = '" + url + "' }");
        }
    }


}

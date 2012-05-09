using System;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSModules_Messaging_CMSPages_PublicMessageUserSelector : CMSLiveModalPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentMaster.Title.TitleText = GetString("Messaging.MessageUserSelector.HeaderCaption");
        CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_User/object.png");
        // Hide footer
        ((Panel)CurrentMaster.PanelFooter.Parent).Style.Add(HtmlTextWriterStyle.Display, "none");
    }
}

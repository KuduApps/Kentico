using System;

using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSAdminControls_UI_Dialogs_ChangeGroup : CMSModalPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        CurrentMaster.Title.TitleText = GetString("community.group.changedocumentowner");
        CurrentPage.Title = CurrentMaster.Title.TitleText;
        CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_groups/module.png");
    }


    protected void btnOk_Click(object sender, EventArgs e)
    {
        selectDocumentGroupElem.ProcessAction();
    }
}

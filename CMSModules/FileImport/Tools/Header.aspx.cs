using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;

[Tabs("CMS.FileImport", "", "fileImportContent")]
public partial class CMSModules_FileImport_Tools_Header : CMSFileImportPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.CurrentMaster.Title.TitleText = GetString("Tools.FileImport.FileImport");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_FileImport/module.png");
        this.CurrentMaster.Title.HelpName = "helpTopic";
        this.CurrentMaster.Title.HelpTopicName = "CMS_FileImport_ImportFromComputer";
    }
}

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
using CMS.SettingsProvider;
using CMS.FormEngine;
using CMS.SiteProvider;
using CMS.TreeEngine;
using CMS.CMSHelper;
using CMS.LicenseProvider;
using CMS.DataEngine;
using CMS.WorkflowEngine;
using CMS.UIControls;

public partial class CMSModules_DocumentTypes_Pages_Development_DocumentType_New : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Set the inner control theme property
        this.newDocWizard.Theme = this.Theme;

        // for all steps
        string[,] pageTitleTabs = new string[2, 3];
        pageTitleTabs[0, 0] = GetString("DocumentType_New.DocumentTypes");
        pageTitleTabs[0, 1] = "~/CMSModules/DocumentTypes/Pages/Development/DocumentType_List.aspx";
        pageTitleTabs[0, 2] = "";
        pageTitleTabs[1, 0] = GetString("DocumentType_New.CurrentDocumentType");
        pageTitleTabs[1, 1] = "";
        pageTitleTabs[1, 2] = "";

        this.CurrentMaster.Title.Breadcrumbs = pageTitleTabs;
        this.CurrentMaster.Title.TitleText = GetString("DocumentType_New.Title");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_DocumentType/new.png");
        this.CurrentMaster.Title.HelpTopicName = "new_document_type";
        this.CurrentMaster.Title.HelpName = "helpTopic";
        this.CurrentMaster.BodyClass += " FieldEditorWizardBody";
    }
}

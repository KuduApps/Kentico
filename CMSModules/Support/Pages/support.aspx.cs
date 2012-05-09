using System;
using System.Collections;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.CMSHelper;

public partial class CMSModules_Support_Pages_support : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Initialize page

        // Setup page title text and image
        this.CurrentMaster.Title.TitleText = GetString("Header.Support");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_Support/Module.png");

        ArrayList parametersRow = new ArrayList();
        object[] row;

        // Start filling arraylists
        row = new object[5];
        row[0] = GetImageUrl("CMSModules/CMS_Support/DevnetLarge.png");
        row[1] = GetString("Support-LeftMenu.DevNet");
        row[2] = "http://devnet.kentico.com";
        row[3] = GetString("Support-LeftMenu.DevNetDescription");
        row[4] = "DevNet";
        parametersRow.Add(row);

        row = new object[5];
        row[0] = GetImageUrl("CMSModules/CMS_Support/SubmitIssueLarge.png");
        row[1] = GetString("Support-LeftMenu.SubmitIssue");
        row[2] = "SubmitIssue.aspx?siteid=" + Request.QueryString["siteid"];
        row[3] = GetString("Support-LeftMenu.SubmitIssueDescription");
        row[4] = "SubmitIssue";
        parametersRow.Add(row);

        row = new object[5];
        row[0] = GetImageUrl("General/HelpLarge.png");
        row[1] = GetString("General.Documentation");
        row[2] = "http://devnet.kentico.com/Documentation/" + CMSContext.SYSTEM_VERSION.Replace(".", "_") + ".aspx";
        row[3] = GetString("Support-LeftMenu.DocumentationDescription");
        row[4] = "Help";
        parametersRow.Add(row);

        try
        {
            if (CMS.IO.File.Exists(Server.MapPath("~/CMSAPIExamples/Default.aspx")))
            {
                row = new object[6];
                row[0] = GetImageUrl("CMSModules/CMS_Support/CodeLarge.png");
                row[1] = GetString("Support-LeftMenu.ApiExamples");
                row[2] = ResolveUrl("~/CMSAPIExamples/Default.aspx");
                row[3] = GetString("Support-LeftMenu.ApiExamplesDescription");
                row[5] = "true";
                parametersRow.Add(row);
            }
        }
        catch { }
        

        // Initialize guide
        Guide.Columns = 2;
        Guide.Parameters = parametersRow;
    }
}

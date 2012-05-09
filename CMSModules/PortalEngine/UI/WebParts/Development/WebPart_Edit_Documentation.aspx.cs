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
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.PortalEngine;
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSModules_PortalEngine_UI_WebParts_Development_WebPart_Edit_Documentation : SiteManagerPage
{
    private int webpartId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        webpartId = ValidationHelper.GetInteger(Request.QueryString["webpartid"], 0);

        this.Title = "Web part documentation";

        // Resource string
        btnOk.Text = GetString("General.Ok");

        string[,] actions = new string[2, 12];

        WebPartInfo wpi = WebPartInfoProvider.GetWebPartInfo(webpartId);
        if (wpi != null)
        {
            // Generate doucmentation action                
            actions[0, 0] = "HyperLink";
            actions[0, 1] = GetString("webparteditdocumentation.view");
            actions[0, 3] = "~/CMSModules/PortalEngine/UI/WebParts/WebPartDocumentationPage.aspx?webpartid=" + wpi.WebPartName;
            actions[0, 5] = GetImageUrl("CMSModules/CMS_WebParts/viewdocumentation.png");
            actions[0, 11] = "_blank";

            if (SettingsKeyProvider.DevelopmentMode)
            {
                // Generate doucmentation action                
                actions[1, 0] = "HyperLink";
                actions[1, 1] = GetString("webparteditdocumentation.generate");
                actions[1, 3] = "~/CMSPages/Dialogs/Documentation.aspx?webpart=" + wpi.WebPartName;
                actions[1, 5] = GetImageUrl("CMSModules/CMS_WebParts/generatedocumentation.png");
                actions[1, 11] = "_blank";
            }
        }
        this.CurrentMaster.HeaderActions.Actions = actions;

        // HTML editor settings        
        htmlText.AutoDetectLanguage = false;
        htmlText.DefaultLanguage = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        htmlText.EditorAreaCSS = "";
        htmlText.ToolbarSet = "SimpleEdit";

        // Load data
        if (!RequestHelper.IsPostBack())
        {
            if (wpi != null)
            {

                htmlText.ResolvedValue = wpi.WebPartDocumentation;
            }
        }
    }


    /// <summary>
    /// OK click handler, save changes.
    /// </summary>
    protected void btnOk_Click(object sender, EventArgs e)
    {
        WebPartInfo wpi = WebPartInfoProvider.GetWebPartInfo(webpartId);
        if (wpi != null)
        {
            wpi.WebPartDocumentation = htmlText.ResolvedValue;
            WebPartInfoProvider.SetWebPartInfo(wpi);

            lblInfo.Visible = true;
            lblInfo.Text = GetString("General.ChangesSaved");
        }
    }
}

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
using CMS.UIControls;
using CMS.SiteProvider;

public partial class CMSModules_UICultures_Pages_Development_UICultures_Header : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Intialize the control
        SetupControl();
    }


    #region "Private methods"

    /// <summary>
    /// Initializes the controls.
    /// </summary>
    private void SetupControl()
    {
        // Set title
        this.CurrentMaster.Title.TitleText = GetString("Development-UICulture_List.Title");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_UICulture/object.png");

        this.CurrentMaster.Title.HelpName = "helpTopic";
        this.CurrentMaster.Title.HelpTopicName = "uicultures_defaultculture";

        InitializeMenu();
    }


    /// <summary>
    /// Initialize the tab control on the master page.
    /// </summary>
    private void InitializeMenu()
    {
        string[,] tabs = new string[2, 4];

        string defaultCulture = CultureHelper.DefaultUICulture;

        // Default UI culture strings
        tabs[0, 0] = GetString("UICultures.Default");
        tabs[0, 1] = "SetHelpTopic('helpTopic', 'uicultures_defaultculture');";
        tabs[0, 2] = "ResourceString/List.aspx?uicultureid=";
        try
        {
            UICultureInfo ci = UICultureInfoProvider.GetUICultureInfo(defaultCulture);
            if (ci != null)
            {
                tabs[0, 2] += ci.UICultureID;
            }
        }
        catch { }

        tabs[1, 0] = GetString("UICultures.Other");
        tabs[1, 1] = "SetHelpTopic('helpTopic', 'uicultures_tab_other');";
        tabs[1, 2] = "UICulture/List.aspx";

        // Set the target iFrame
        this.CurrentMaster.Tabs.UrlTarget = "uiculturesContent";

        // Assign tabs data
        this.CurrentMaster.Tabs.Tabs = tabs;
    }

    #endregion
}

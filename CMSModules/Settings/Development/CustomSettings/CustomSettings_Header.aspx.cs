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

using CMS.SiteProvider;
using CMS.GlobalHelper;
using CMS.UIControls;


public partial class CMSModules_Settings_Development_CustomSettings_CustomSettings_Header : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string treeRoot = QueryHelper.GetString("treeroot", "customsettings");

        // Decide which category to use as root: custom settings or settings
        switch(treeRoot) {
            case("settings"):
                this.CurrentMaster.Title.TitleText = GetString("Development.Settings");
                this.CurrentMaster.Title.HelpTopicName = "settings_main";
                break;
            case("customsettings"):
            default:
                this.CurrentMaster.Title.TitleText = GetString("Development.CustomSettings");
                this.CurrentMaster.Title.HelpTopicName = "custom_settings_main";
                break;
        }
        
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_CustomSettings/object.png");
        this.CurrentMaster.Title.HelpName = "helpTopic";
    }
}


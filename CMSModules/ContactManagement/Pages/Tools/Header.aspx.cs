using System;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.OnlineMarketing;

public partial class CMSModules_ContactManagement_Pages_Tools_Header : CMSContactManagementPage
{
    #region "Variables"

    int selectedTab;

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets SiteID.
    /// </summary>
    public override int SiteID
    {
        get
        {
            if (this.IsSiteManager)
            {
                // Site selector has '0' as 'none' or 'global' record. Return '-4' as 'global' record instead.
                if (siteSelector.SiteID == UniSelector.US_NONE_RECORD)
                {
                    return UniSelector.US_GLOBAL_RECORD;
                }
                return siteSelector.SiteID;
            }
            else
            {
                return base.SiteID;
            }
        }
    }

    #endregion


    #region "Methods"

    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
        this["TabControl"] = BasicTabControlMenu;
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!RequestHelper.IsPostBack())
        {
            selectedTab = QueryHelper.GetInteger("selectedTab", 0);
        }

        // CMSDesk
        if (!this.IsSiteManager)
        {               
            pnlActions.Visible = false;
        }
        // Site manager
        else
        {
            // Set site selector
            siteSelector.OnlyRunningSites = false;
            siteSelector.AllowEmpty = false;
            siteSelector.AllowAll = true;
            siteSelector.AllowGlobal = true;
            siteSelector.DropDownSingleSelect.AutoPostBack = true;
            siteSelector.UniSelector.SpecialFields = new string[1, 2] { { GetString("general.global"), "0" } };
            siteSelector.UniSelector.OnSelectionChanged += UniSelector_OnSelectionChanged;
            siteSelector.Reload(true);
        }

        string append = null;
        int i = 0;
        if (this.IsSiteManager)
        {
            ScriptHelper.RegisterStartupScript(this, GetType(), "selectedSiteId", ScriptHelper.GetScript(string.Format("var selectedSiteId = {0};", SiteID)));
            append = "?siteid=' + selectedSiteId+'&issitemanager=1";
            i++;
        }

        BasicTabControlMenu.Tabs = new string[4 + i, 4];
        if (this.IsSiteManager)
        {
            BasicTabControlMenu.Tabs[0, 0] = GetString("om.activities");
            BasicTabControlMenu.Tabs[0, 1] = "SetHelpTopic('helpTopic', 'onlinemarketing_activities'); return false;";
            BasicTabControlMenu.Tabs[0, 2] = "Activities/Frameset.aspx" + append;
        }
        BasicTabControlMenu.Tabs[0 + i, 0] = GetString("om.contact.list");
        BasicTabControlMenu.Tabs[0 + i, 1] = "SetHelpTopic('helpTopic', 'onlinemarketing_contact_list'); return false;";
        BasicTabControlMenu.Tabs[0 + i, 2] = "Contact/List.aspx" + append;
        BasicTabControlMenu.Tabs[1 + i, 0] = GetString("om.account.list");
        BasicTabControlMenu.Tabs[1 + i, 1] = "SetHelpTopic('helpTopic', 'onlinemarketing_account_list'); return false;";
        BasicTabControlMenu.Tabs[1 + i, 2] = "Account/List.aspx" + append;
        BasicTabControlMenu.Tabs[2 + i, 0] = GetString("om.contactgroup.list");
        BasicTabControlMenu.Tabs[2 + i, 1] = "SetHelpTopic('helpTopic', 'onlinemarketing_contactgroup_list'); return false;";
        BasicTabControlMenu.Tabs[2 + i, 2] = "ContactGroup/List.aspx" + append;
        BasicTabControlMenu.Tabs[3 + i, 0] = GetString("om.configuration");
        BasicTabControlMenu.Tabs[3 + i, 1] = "SetHelpTopic('helpTopic', 'onlinemarketing_configuration'); return false;";
        BasicTabControlMenu.Tabs[3 + i, 2] = "Configuration/Frameset.aspx" + append;
        BasicTabControlMenu.UrlTarget = "contactManagementContent";
        BasicTabControlMenu.SelectedTab = selectedTab;

        titleElem.TitleText = GetString("om.contactmanagement");
        titleElem.TitleImage = GetImageUrl("Objects/OM_ContactManagement/object.png");
        titleElem.HelpTopicName = "contactmanagement_general";
        titleElem.HelpName = "helpTopic";
    }


    /// <summary>
    /// Handles site selection change event.
    /// </summary>
    protected void UniSelector_OnSelectionChanged(object sender, EventArgs e)
    {
        ScriptHelper.RegisterStartupScript(this, GetType(), "OnSelectedIndex", ScriptHelper.GetScript(
@"
function OnSelectedIndex() {
selectedTab = document.getElementById('TabControlSelItemNo');
if ((selectedTab != null) && (typeof(selectedSiteId) != 'undefined')) {" + (this.IsSiteManager ?
   @"if (selectedTab.value == '0') {
     parent.frames['contactManagementContent'].location = 'Activities/Frameset.aspx?siteid=' + selectedSiteId + '&issitemanager=" + this.IsSiteManager + @"';
   }else if (selectedTab.value == '1') {
     parent.frames['contactManagementContent'].location = 'Contact/List.aspx?siteid=' + selectedSiteId + '&issitemanager=" + this.IsSiteManager + @"';
   } else if (selectedTab.value == '2') {
     parent.frames['contactManagementContent'].location = 'Account/List.aspx?siteid=' + selectedSiteId + '&issitemanager=" + this.IsSiteManager + @"';
   } else if (selectedTab.value == '3') {
     parent.frames['contactManagementContent'].location = 'ContactGroup/List.aspx?siteid=' + selectedSiteId + '&issitemanager=" + this.IsSiteManager + @"';
   } else {
     parent.frames['contactManagementContent'].location = 'Configuration/Frameset.aspx?siteid=' + selectedSiteId + '&issitemanager=" + this.IsSiteManager + @"';}"
   : @"if (selectedTab.value == '0') {
     parent.frames['contactManagementContent'].location = 'Contact/List.aspx?siteid=' + selectedSiteId + '&issitemanager=" + this.IsSiteManager + @"';
   } else if (selectedTab.value == '1') {
     parent.frames['contactManagementContent'].location = 'Account/List.aspx?siteid=' + selectedSiteId + '&issitemanager=" + this.IsSiteManager + @"';
   } else if (selectedTab.value == '2') {
     parent.frames['contactManagementContent'].location = 'ContactGroup/List.aspx?siteid=' + selectedSiteId + '&issitemanager=" + this.IsSiteManager + @"';
   } else {
     parent.frames['contactManagementContent'].location = 'Configuration/Frameset.aspx?siteid=' + selectedSiteId + '&issitemanager=" + this.IsSiteManager + @"';}")
   + @"}}
OnSelectedIndex();"));
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        // Set panel menu breadcrumbs
        if (!this.IsSiteManager)
        {
            string[,] breadcrumbs;

            // Create new breadcrumbs
            if (titleElem.Breadcrumbs == null)
            {
                breadcrumbs = new string[2, 4];

                breadcrumbs[0, 0] = "Tools";
                breadcrumbs[0, 1] = "~/CMSDesk/Tools/Menu.aspx";
                breadcrumbs[0, 2] = "toolscontent";

                if (!String.IsNullOrEmpty(titleElem.TitleText))
                {
                    breadcrumbs[1, 0] = titleElem.TitleText;
                    breadcrumbs[1, 1] = "";
                    breadcrumbs[1, 2] = "";
                }

                // Register frame height correction script
                string resizeScript = @"
var frames = window.parent.frames;
var rows = window.parent.document.body.rows.replace(' ', '').split(',');
for (var i = 0; i < frames.length; i++) {
    if ((frames[i].name == window.name) && (rows[i] != '*')) {
        rows[i] = parseInt(rows[i], 10) + 30;
        window.parent.document.body.rows = rows.join(',');
        break;
    }
}";
                ScriptHelper.RegisterClientScriptBlock(this, typeof(String), "FrameHeightCorrection", ScriptHelper.GetScript(resizeScript));
                titleElem.Breadcrumbs = breadcrumbs;
            }
        }
    }

    #endregion
}
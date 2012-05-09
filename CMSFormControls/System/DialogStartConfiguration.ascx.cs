using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Collections;
using System.Data;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.SettingsProvider;
using CMS.FormControls;

/// <summary>
/// This form control needs other blank fields with following names to work properly:
/// dialogs_content_hide
/// dialogs_content_path
/// dialogs_content_site
/// dialogs_libraries_hide
/// dialogs_libraries_site
/// dialogs_libraries_global
/// dialogs_libraries_global_libname
/// dialogs_groups
/// dialogs_groups_name
/// dialogs_libraries_group
/// dialogs_libraries_group_libname
/// dialogs_libraries_path
/// dialogs_attachments_hide
/// dialogs_anchor_hide
/// dialogs_email_hide
/// dialogs_web_hide
/// autoresize
/// autoresize_width
/// autoresize_height
/// autoresize_maxsidesize
/// </summary>
public partial class CMSFormControls_System_DialogStartConfiguration : FormEngineUserControl
{
    #region "Variables"

    protected bool communityLoaded = false;
    protected bool mediaLoaded = false;

    #endregion


    #region "Properties"

    public override object Value
    {
        get
        {
            return true;
        }
        set
        {
            // Do nothing
        }
    }


    /// <summary>
    /// Indicates if the Autoresize settings should be available.
    /// </summary>
    public bool DisplayAutoresize
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("DisplayAutoresize"), true);
        }
        set
        {
            SetValue("DisplayAutoresize", value);
        }
    }


    /// <summary>
    /// Indicates if the E-mal tab settings should be available.
    /// </summary>
    public bool DisplayEmailTabSettings
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("DisplayEmailTabSettings"), true);
        }
        set
        {
            SetValue("DisplayEmailTabSettings", value);
        }
    }


    /// <summary>
    /// Indicates if the Anchor tab settings should be available.
    /// </summary>
    public bool DisplayAnchorTabSettings
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("DisplayAnchorTabSettings"), true);
        }
        set
        {
            SetValue("DisplayAnchorTabSettings", value);
        }
    }


    /// <summary>
    /// Indicates if the Web tab settings should be available.
    /// </summary>
    public bool DisplayWebTabSettings
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("DisplayWebTabSettings"), true);
        }
        set
        {
            SetValue("DisplayWebTabSettings", value);
        }
    }

    #endregion


    #region "Page events"

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        lnkAdvacedFieldSettings.Click += new EventHandler(lnkAdvacedFieldSettings_Click);

        communityLoaded = ModuleEntry.IsModuleLoaded(ModuleEntry.COMMUNITY);
        mediaLoaded = ModuleEntry.IsModuleLoaded(ModuleEntry.MEDIALIBRARY);

        this.plcMedia.Visible = mediaLoaded;
        this.plcGroups.Visible = communityLoaded;

        if (communityLoaded)
        {
            this.drpGroups.SelectedIndexChanged += new EventHandler(drpGroups_SelectedIndexChanged);
        }

        LoadSites();
        LoadSiteLibraries(null);
        LoadSiteGroups(null);
        LoadGroupLibraries(null, null);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        LoadValues();
    }


    /// <summary>
    /// Advanced dialog link event handler.
    /// </summary>
    protected void lnkAdvacedFieldSettings_Click(object sender, EventArgs e)
    {
        plcAdvancedFieldSettings.Visible = !plcAdvancedFieldSettings.Visible;
    }


    /// <summary>
    /// Group drop-down list event handler.
    /// </summary>
    protected void drpGroups_SelectedIndexChanged(object sender, EventArgs e)
    {
        SelectGroup();
    }


    /// <summary>
    /// Handles site selection change event.
    /// </summary>
    protected void UniSelectorMediaSites_OnSelectionChanged(object sender, EventArgs e)
    {
        string selectedSite = ValidationHelper.GetString(siteSelectorMedia.Value, String.Empty);
        SelectSite(selectedSite);
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Processes the data loading after the site is selected.
    /// </summary>
    private void SelectSite(string selectedSite)
    {
        if (communityLoaded)
        {
            LoadSiteGroups(selectedSite);
        }
        LoadSiteLibraries(selectedSite);
        SelectGroup();
    }


    /// <summary>
    /// Processes the data loading after the group is selected.
    /// </summary>
    private void SelectGroup()
    {
        if (drpGroups.SelectedValue == "#none#")
        {
            drpGroupLibraries.Items.Clear();
            drpGroupLibraries.Items.Insert(0, new ListItem(GetString("general.selectnone"), ""));
            drpGroupLibraries.SelectedIndex = 0;
            drpGroupLibraries.Enabled = false;
        }
        else
        {
            drpGroupLibraries.Items.Clear();
            drpGroupLibraries.Enabled = true;

            string selectedMediaSite = ValidationHelper.GetString(siteSelectorMedia.Value, String.Empty);
            LoadGroupLibraries(selectedMediaSite, drpGroups.SelectedValue);
        }
    }


    /// <summary>
    /// Loads the site dropdownlists.
    /// </summary>
    private void LoadSites()
    {
        // Set site selector
        siteSelectorContent.DropDownSingleSelect.AutoPostBack = true;
        siteSelectorContent.AllowAll = false;
        siteSelectorContent.UseCodeNameForSelection = true;
        siteSelectorContent.UniSelector.SpecialFields = new string[2, 2] { { GetString("general.selectall"), "##all##" }, { GetString("dialogs.config.currentsite"), "##current##" } };

        siteSelectorMedia.DropDownSingleSelect.AutoPostBack = true;
        siteSelectorMedia.AllowAll = false;
        siteSelectorMedia.UseCodeNameForSelection = true;
        siteSelectorMedia.UniSelector.SpecialFields = new string[2, 2] { { GetString("general.selectall"), "##all##" }, { GetString("dialogs.config.currentsite"), "##current##" } };

        if (mediaLoaded)
        {
            siteSelectorMedia.UniSelector.OnSelectionChanged += new EventHandler(UniSelectorMediaSites_OnSelectionChanged);
        }
    }


    /// <summary>
    /// Reloads the site groups.
    /// </summary>
    /// <param name="siteName">Name of the site</param>
    private void LoadSiteGroups(string siteName)
    {
        if (communityLoaded && mediaLoaded && !(drpGroups.Items.Count > 0))
        {
            drpGroups.Items.Clear();

            if (siteName != null)
            {
                DataSet dsGroups = ModuleCommands.CommunityGetSiteGroups(siteName);
                if (!DataHelper.DataSourceIsEmpty(dsGroups))
                {
                    dsGroups.Tables[0].DefaultView.Sort = "GroupDisplayName";
                    drpGroups.DataValueField = "GroupName";
                    drpGroups.DataTextField = "GroupDisplayName";
                    drpGroups.DataSource = dsGroups.Tables[0].DefaultView;
                    drpGroups.DataBind();
                }
            }
            drpGroups.Items.Insert(0, new ListItem(GetString("general.selectall"), ""));
            drpGroups.Items.Insert(1, new ListItem(GetString("general.selectnone"), "#none#"));
            drpGroups.Items.Insert(2, new ListItem(GetString("dialogs.config.currentgroup"), "#current#"));
        }
    }


    /// <summary>
    /// Reloads the site media libraries.
    /// </summary>
    /// <param name="siteName">Name of the site</param>
    private void LoadSiteLibraries(string siteName)
    {
        if (mediaLoaded && !(drpSiteLibraries.Items.Count > 0))
        {
            drpSiteLibraries.Items.Clear();

            if (siteName != null)
            {
                DataSet dsLibraries = ModuleCommands.MediaLibraryGetSiteLibraries(siteName);
                if (!DataHelper.DataSourceIsEmpty(dsLibraries))
                {
                    dsLibraries.Tables[0].DefaultView.Sort = "LibraryDisplayName";
                    drpSiteLibraries.DataValueField = "LibraryName";
                    drpSiteLibraries.DataTextField = "LibraryDisplayName";
                    drpSiteLibraries.DataSource = dsLibraries.Tables[0].DefaultView;
                    drpSiteLibraries.DataBind();
                }
            }
            drpSiteLibraries.Items.Insert(0, new ListItem(GetString("general.selectall"), ""));
            drpSiteLibraries.Items.Insert(1, new ListItem(GetString("general.selectnone"), "#none#"));
            drpSiteLibraries.Items.Insert(2, new ListItem(GetString("dialogs.config.currentlibrary"), "#current#"));
        }
    }


    /// <summary>
    /// Reloads the group media libraries.
    /// </summary>
    /// <param name="siteName">Name of the site</param>
    /// <param name="groupName">Name of the group</param>
    private void LoadGroupLibraries(string siteName, string groupName)
    {
        if (mediaLoaded && communityLoaded && !(drpGroupLibraries.Items.Count > 0))
        {
            drpGroupLibraries.Items.Clear();

            if ((siteName != null) && (groupName != null))
            {
                DataSet dsLibraries = ModuleCommands.MediaLibraryGetGroupLibraries(siteName, groupName);
                if (!DataHelper.DataSourceIsEmpty(dsLibraries))
                {
                    dsLibraries.Tables[0].DefaultView.Sort = "LibraryDisplayName";
                    drpGroupLibraries.DataValueField = "LibraryName";
                    drpGroupLibraries.DataTextField = "LibraryDisplayName";
                    drpGroupLibraries.DataSource = dsLibraries.Tables[0].DefaultView;
                    drpGroupLibraries.DataBind();
                }
            }
            drpGroupLibraries.Items.Insert(0, new ListItem(GetString("general.selectall"), ""));
            drpGroupLibraries.Items.Insert(1, new ListItem(GetString("dialogs.config.currentlibrary"), "#current#"));
        }
    }


    /// <summary>
    /// Selects correct item in given DDL.
    /// </summary>
    /// <param name="ddl">Dropdownlist with the data</param>
    /// <param name="origKey">Key in hashtable which determines whether the value is special or specific item</param>
    /// <param name="singleItemKey">Key in hashtable for specified item</param>
    private void SelectInDDL(DropDownList ddl, string origKey, string singleItemKey)
    {
        string item = ValidationHelper.GetString(this.Form.Data.GetValue(origKey), "").ToLower();
        if (item == "#single#")
        {
            item = ValidationHelper.GetString(this.Form.Data.GetValue(singleItemKey), "");
        }

        ListItem li = ddl.Items.FindByValue(item);
        if (li != null)
        {
            ddl.SelectedValue = li.Value;
        }
    }

    #endregion


    #region "Public methods"

    /// <summary>
    /// Sets inner controls according to the parameters and their values included in configuration collection. Parameters collection will be passed from Field editor.
    /// </summary>
    /// <param name="config">Parameters collection</param>
    public void LoadValues()
    {
        if ((this.Form != null) && (this.Form.Data != null))
        {
            // Set settings configuration
            plcDisplayAnchor.Visible = DisplayAnchorTabSettings;
            plcDisplayEmail.Visible = DisplayEmailTabSettings;
            plcDisplayWeb.Visible = DisplayWebTabSettings;
            plcAutoResize.Visible = DisplayAutoresize;


            IDataContainer data = this.Form.Data;
            elemAutoResize.Form = this.Form;
            if (ContainsColumn("autoresize"))
            {
                elemAutoResize.Value = ValidationHelper.GetString(data.GetValue("autoresize"), null);
            }

            // Content tab
            if (ContainsColumn("dialogs_content_hide"))
            {
                this.chkDisplayContentTab.Checked = !ValidationHelper.GetBoolean(data.GetValue("dialogs_content_hide"), false);
            }
            if (ContainsColumn("dialogs_content_path"))
            {
                this.selectPathElem.Value = ValidationHelper.GetString(data.GetValue("dialogs_content_path"), "");
            }

            if (ContainsColumn("dialogs_content_site"))
            {
                siteSelectorContent.Value = ValidationHelper.GetString(data.GetValue("dialogs_content_site"), null);
            }

            // Media tab
            if (mediaLoaded)
            {
                if (ContainsColumn("dialogs_libraries_hide"))
                {
                    this.chkDisplayMediaTab.Checked = !ValidationHelper.GetBoolean(data.GetValue("dialogs_libraries_hide"), false);
                }

                // Site DDL                
                string libSites = null;
                if (ContainsColumn("dialogs_libraries_site"))
                {
                    libSites = ValidationHelper.GetString(data.GetValue("dialogs_libraries_site"), null);
                }
                siteSelectorMedia.Value = libSites;
                SelectSite(libSites);

                // Site libraries DDL
                if (ContainsColumn("dialogs_libraries_global") && ContainsColumn("dialogs_libraries_global_libname"))
                {
                    SelectInDDL(drpSiteLibraries, "dialogs_libraries_global", "dialogs_libraries_global_libname");
                }

                if (communityLoaded)
                {
                    // Groups DDL
                    if (ContainsColumn("dialogs_groups") && ContainsColumn("dialogs_groups_name"))
                    {
                        SelectInDDL(drpGroups, "dialogs_groups", "dialogs_groups_name");
                    }
                    SelectGroup();

                    // Group libraries DDL
                    if (ContainsColumn("dialogs_libraries_group") && ContainsColumn("dialogs_libraries_group_libname"))
                    {
                        SelectInDDL(drpGroupLibraries, "dialogs_libraries_group", "dialogs_libraries_group_libname");
                    }
                }

                // Starting path
                if (ContainsColumn("dialogs_libraries_path"))
                {
                    this.txtMediaStartPath.Text = ValidationHelper.GetString(data.GetValue("dialogs_libraries_path"), "");
                }
            }

            // Other tabs        
            if (ContainsColumn("dialogs_attachments_hide"))
            {
                this.chkDisplayAttachments.Checked = !ValidationHelper.GetBoolean(data.GetValue("dialogs_attachments_hide"), false);
            }
            if (ContainsColumn("dialogs_anchor_hide"))
            {
                this.chkDisplayAnchor.Checked = !ValidationHelper.GetBoolean(data.GetValue("dialogs_anchor_hide"), false);
            }
            if (ContainsColumn("dialogs_email_hide"))
            {
                this.chkDisplayEmail.Checked = !ValidationHelper.GetBoolean(data.GetValue("dialogs_email_hide"), false);
            }
            if (ContainsColumn("dialogs_web_hide"))
            {
                this.chkDisplayWeb.Checked = !ValidationHelper.GetBoolean(data.GetValue("dialogs_web_hide"), false);
            }
        }
    }


    /// <summary>
    /// Returns other values related to this control.
    /// </summary>
    /// <param name="config">Parameters collection</param>
    public override object[,] GetOtherValues()
    {
        object[,] values = new object[20, 2];
        values[0, 0] = "autoresize";
        values[1, 0] = "autoresize_width";
        values[2, 0] = "autoresize_height";
        values[3, 0] = "autoresize_maxsidesize";
        values[4, 0] = "dialogs_content_hide";
        values[5, 0] = "dialogs_content_path";
        values[6, 0] = "dialogs_content_site";
        values[7, 0] = "dialogs_libraries_hide";
        values[8, 0] = "dialogs_libraries_site";
        values[9, 0] = "dialogs_libraries_global";
        values[10, 0] = "dialogs_libraries_global_libname";
        values[11, 0] = "dialogs_groups";
        values[12, 0] = "dialogs_groups_name";
        values[13, 0] = "dialogs_libraries_group";
        values[14, 0] = "dialogs_libraries_group_libname";
        values[15, 0] = "dialogs_libraries_path";
        values[16, 0] = "dialogs_attachments_hide";
        values[17, 0] = "dialogs_anchor_hide";
        values[18, 0] = "dialogs_email_hide";
        values[19, 0] = "dialogs_web_hide";

        // Resize control values
        if (plcAutoResize.Visible)
        {
            values[0, 1] = elemAutoResize.Value;

            object[,] resizeValues = elemAutoResize.GetOtherValues();
            if ((resizeValues != null) && (resizeValues.Length > 3))
            {
                values[1, 1] = resizeValues[0, 1];
                values[2, 1] = resizeValues[1, 1];
                values[3, 1] = resizeValues[2, 1];
            }
        }

        // Content tab
        if (!this.chkDisplayContentTab.Checked)
        {
            values[4, 1] = true;
        }
        else
        {
            values[4, 1] = false;
        }

        if ((string)this.selectPathElem.Value != "")
        {
            values[5, 1] = this.selectPathElem.Value;
        }

        string selectedSite = ValidationHelper.GetString(siteSelectorContent.Value, String.Empty);
        if (selectedSite != String.Empty)
        {
            values[6, 1] = selectedSite;
        }

        // Media tab
        if (mediaLoaded)
        {
            if (!this.chkDisplayMediaTab.Checked)
            {
                values[7, 1] = true;
            }

            selectedSite = ValidationHelper.GetString(siteSelectorMedia.Value, String.Empty);
            if (selectedSite != String.Empty)
            {
                values[8, 1] = selectedSite;
            }

            // Site libraries DDL
            string value = this.drpSiteLibraries.SelectedValue;
            if ((value == "#none#") || (value == "#current#"))
            {
                values[9, 1] = value;
            }
            else if (value != "")
            {
                values[9, 1] = "#single#";
                values[10, 1] = this.drpSiteLibraries.SelectedValue;
            }

            if (communityLoaded)
            {
                // Groups DDL
                value = this.drpGroups.SelectedValue;
                if ((value == "#none#") || (value == "#current#"))
                {
                    values[11, 1] = value;
                }
                else if (value != "")
                {
                    values[11, 1] = "#single#";
                    values[12, 1] = this.drpGroups.SelectedValue;
                }

                // Group libraries DDL
                value = this.drpGroupLibraries.SelectedValue;
                if ((value == "#none#") || (value == "#current#"))
                {
                    values[13, 1] = value;
                }
                else if (value != "")
                {
                    values[13, 1] = "#single#";
                    values[14, 1] = this.drpGroupLibraries.SelectedValue;
                }
            }

            // Starting path
            value = this.txtMediaStartPath.Text.Trim();
            if (value != "")
            {
                values[15, 1] = value;
            }
        }

        // Other tabs
        if (!this.chkDisplayAttachments.Checked)
        {
            values[16, 1] = true;
        }
        if (!this.chkDisplayAnchor.Checked)
        {
            values[17, 1] = true;
        }
        if (!this.chkDisplayEmail.Checked)
        {
            values[18, 1] = true;
        }
        if (!this.chkDisplayWeb.Checked)
        {
            values[19, 1] = true;
        }

        return values;
    }


    /// <summary>
    /// Validation of form control.
    /// </summary>
    public override bool IsValid()
    {
        bool isValid = true;

        // Check validity of autoresize element.
        if (plcAutoResize.Visible)
        {
            isValid = elemAutoResize.IsValid();
        }

        if (!ContainsColumn("dialogs_content_hide"))
        {
            this.ValidationError += String.Format(GetString("formcontrol.missingcolumn"), "dialogs_content_hide", GetString("templatedesigner.fieldtypes.boolean"));
            isValid = false;
        }
        if (!ContainsColumn("dialogs_content_path"))
        {
            this.ValidationError += String.Format(GetString("formcontrol.missingcolumn"), "dialogs_content_path", GetString("templatedesigner.fieldtypes.boolean"));
            isValid = false;
        }
        if (!ContainsColumn("dialogs_content_site"))
        {
            this.ValidationError += String.Format(GetString("formcontrol.missingcolumn"), "dialogs_content_site", GetString("general.text"));
            isValid = false;
        }
        if (!ContainsColumn("dialogs_libraries_hide"))
        {
            this.ValidationError += String.Format(GetString("formcontrol.missingcolumn"), "dialogs_libraries_hide", GetString("templatedesigner.fieldtypes.boolean"));
            isValid = false;
        }
        if (!ContainsColumn("dialogs_libraries_site"))
        {
            this.ValidationError += String.Format(GetString("formcontrol.missingcolumn"), "dialogs_libraries_site", GetString("general.text"));
            isValid = false;
        }
        if (!ContainsColumn("dialogs_libraries_global"))
        {
            this.ValidationError += String.Format(GetString("formcontrol.missingcolumn"), "dialogs_libraries_global", GetString("general.text"));
            isValid = false;
        }
        if (!ContainsColumn("dialogs_libraries_global_libname"))
        {
            this.ValidationError += String.Format(GetString("formcontrol.missingcolumn"), "dialogs_libraries_global_libname", GetString("general.text"));
            isValid = false;
        }
        if (!ContainsColumn("dialogs_groups"))
        {
            this.ValidationError += String.Format(GetString("formcontrol.missingcolumn"), "dialogs_groups", GetString("general.text"));
            isValid = false;
        }
        if (!ContainsColumn("dialogs_groups_name"))
        {
            this.ValidationError += String.Format(GetString("formcontrol.missingcolumn"), "dialogs_groups_name", GetString("general.text"));
            isValid = false;
        }
        if (!ContainsColumn("dialogs_libraries_group"))
        {
            this.ValidationError += String.Format(GetString("formcontrol.missingcolumn"), "dialogs_libraries_group", GetString("general.text"));
            isValid = false;
        }
        if (!ContainsColumn("dialogs_libraries_group_libname"))
        {
            this.ValidationError += String.Format(GetString("formcontrol.missingcolumn"), "dialogs_libraries_group_libname", GetString("general.text"));
            isValid = false;
        }
        if (!ContainsColumn("dialogs_libraries_path"))
        {
            this.ValidationError += String.Format(GetString("formcontrol.missingcolumn"), "dialogs_libraries_path", GetString("general.text"));
            isValid = false;
        }
        if (!ContainsColumn("dialogs_attachments_hide"))
        {
            this.ValidationError += String.Format(GetString("formcontrol.missingcolumn"), "dialogs_attachments_hide", GetString("templatedesigner.fieldtypes.boolean"));
            isValid = false;
        }
        if (!ContainsColumn("dialogs_anchor_hide"))
        {
            this.ValidationError += String.Format(GetString("formcontrol.missingcolumn"), "dialogs_anchor_hide", GetString("templatedesigner.fieldtypes.boolean"));
            isValid = false;
        }
        if (!ContainsColumn("dialogs_email_hide"))
        {
            this.ValidationError += String.Format(GetString("formcontrol.missingcolumn"), "dialogs_email_hide", GetString("templatedesigner.fieldtypes.boolean"));
            isValid = false;
        }
        if (!ContainsColumn("dialogs_web_hide"))
        {
            this.ValidationError += String.Format(GetString("formcontrol.missingcolumn"), "dialogs_web_hide", GetString("templatedesigner.fieldtypes.boolean"));
            isValid = false;
        }
        if (!ContainsColumn("autoresize"))
        {
            this.ValidationError += String.Format(GetString("formcontrol.missingcolumn"), "autoresize", GetString("general.text"));
            isValid = false;
        }
        if (!ContainsColumn("autoresize_width"))
        {
            this.ValidationError += String.Format(GetString("formcontrol.missingcolumn"), "autoresize_width", GetString("templatedesigner.fieldtypes.integer"));
            isValid = false;
        }
        if (!ContainsColumn("autoresize_height"))
        {
            this.ValidationError += String.Format(GetString("formcontrol.missingcolumn"), "autoresize_height", GetString("templatedesigner.fieldtypes.integer"));
            isValid = false;
        }
        if (!ContainsColumn("autoresize_maxsidesize"))
        {
            this.ValidationError += String.Format(GetString("formcontrol.missingcolumn"), "autoresize_maxsidesize", GetString("templatedesigner.fieldtypes.integer"));
            isValid = false;
        }

        return isValid;
    }

    #endregion
}
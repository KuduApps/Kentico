using System;
using System.Web;

using CMS.CMSHelper;
using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.ExtendedControls;
using CMS.SiteProvider;

public partial class CMSModules_Content_Controls_Dialogs_General_DialogHeader : CMSUserControl
{
    #region "Variables"

    private CurrentUserInfo currentUser = null;
    private ICMSMasterPage mCurrentMaster = null;
    private string mSelectedTab = "";
    private int mSelectedTabIndex = 0;

    private string mCustomOutputFormat = "";

    OutputFormatEnum mOutputFormat = OutputFormatEnum.HTMLMedia;
    SelectableContentEnum mSelectableContent = SelectableContentEnum.AllContent;

    private bool mHideAttachments = false;
    private bool mHideContent = false;
    private bool mHideMediaLibraries = false;
    private bool mHideWeb = false;
    private bool mHideAnchor = false;
    private bool mHideEmail = false;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Tabs header master.
    /// </summary>
    public ICMSMasterPage CurrentMaster
    {
        get
        {
            return mCurrentMaster;
        }
        set
        {
            mCurrentMaster = value;
        }
    }


    /// <summary>
    /// Selected tab: attachments, content, libraries, web, anchor, email.
    /// </summary>
    public string SelectedTab
    {
        get
        {
            return mSelectedTab;
        }
        set
        {
            if (value != null)
            {
                mSelectedTab = value.ToLower();
            }
        }
    }


    /// <summary>
    /// Custom output format (used when OuptupFormat is set to Custom).
    /// </summary>
    public string CustomOutputFormat
    {
        get
        {
            return mCustomOutputFormat;
        }
        set
        {
            mCustomOutputFormat = value;
        }
    }


    /// <summary>
    /// CMS dialog output format which determines dialog title, image and visible tabs.
    /// </summary>
    public OutputFormatEnum OutputFormat
    {
        get
        {
            return mOutputFormat;
        }
        set
        {
            mOutputFormat = value;
        }
    }


    /// <summary>
    /// Type of content which could be selected from the dialog.
    /// </summary>
    public SelectableContentEnum SelectableContent
    {
        get
        {
            return mSelectableContent;
        }
        set
        {
            mSelectableContent = value;
        }
    }


    /// <summary>
    /// Indicates if 'Attachments' tab should be hidden.
    /// </summary>
    public bool HideAttachments
    {
        get
        {
            return mHideAttachments;
        }
        set
        {
            mHideAttachments = value;
        }
    }


    /// <summary>
    /// Indicates if 'Content' tab should be hidden.
    /// </summary>
    public bool HideContent
    {
        get
        {
            return mHideContent;
        }
        set
        {
            mHideContent = value;
        }
    }


    /// <summary>
    /// Indicates if 'Media libraries' tab should be hidden.
    /// </summary>
    public bool HideMediaLibraries
    {
        get
        {
            return mHideMediaLibraries;
        }
        set
        {
            mHideMediaLibraries = value;
        }
    }


    /// <summary>
    /// Indicates if 'Web' tab should be hidden.
    /// </summary>
    public bool HideWeb
    {
        get
        {
            return mHideWeb;
        }
        set
        {
            mHideWeb = value;
        }
    }


    /// <summary>
    /// Indicates if 'Anchor' tab should be hidden.
    /// </summary>
    public bool HideAnchor
    {
        get
        {
            return mHideAnchor;
        }
        set
        {
            mHideAnchor = value;
        }
    }


    /// <summary>
    /// Indicates if 'E-mail' tab should be hidden.
    /// </summary>
    public bool HideEmail
    {
        get
        {
            return mHideEmail;
        }
        set
        {
            mHideEmail = value;
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (StopProcessing)
        {
            Visible = false;
        }
        else
        {
            currentUser = CMSContext.CurrentUser;

            ReloadHeader();
        }
    }

    #endregion


    #region "Public methods"

    public void InitFromQueryString()
    {
        // Get format definition from URL        
        string output = QueryHelper.GetString("output", "html");
        bool link = QueryHelper.GetBoolean("link", false);
        OutputFormat = CMSDialogHelper.GetOutputFormat(output, link);
        if (OutputFormat == OutputFormatEnum.Custom)
        {
            CustomOutputFormat = output;
        }

        // Get selectable content
        string content = QueryHelper.GetString("content", "all");
        SelectableContent = CMSDialogHelper.GetSelectableContent(content);

        // Get user dialog configuration
        XmlData userConfig = CMSContext.CurrentUser.UserSettings.UserDialogsConfiguration;

        // Get selected tab from URL
        SelectedTab = QueryHelper.GetString("tab", (string)userConfig["selectedtab"]);

        // Get hidden tabs from URL        
        bool hasFormGuid = (QueryHelper.GetGuid("formguid", Guid.Empty) != Guid.Empty);
        bool hasDocumentId = (QueryHelper.GetInteger("documentid", 0) > 0);
        bool hasParentId = (QueryHelper.GetInteger("parentid", 0) > 0);

        HideAttachments = !QueryHelper.GetBoolean("attachments_hide", false) ? !((hasFormGuid && hasParentId) || hasDocumentId) : true;

        HideContent = QueryHelper.GetBoolean("content_hide", false);
        if (!HideContent)
        {
            // Check site availability
            if (!ResourceSiteInfoProvider.IsResourceOnSite("CMS.Content", CMSContext.CurrentSiteName))
            {
                HideContent = true;
            }
        }

        HideMediaLibraries = QueryHelper.GetBoolean("libraries_hide", false);
        if (!HideMediaLibraries)
        {
            // Check site availability
            if (!ResourceSiteInfoProvider.IsResourceOnSite("CMS.MediaLibrary", CMSContext.CurrentSiteName))
            {
                HideMediaLibraries = true;
            }
        }
        HideWeb = QueryHelper.GetBoolean("web_hide", false);
        HideAnchor = QueryHelper.GetBoolean("anchor_hide", false);
        HideEmail = QueryHelper.GetBoolean("email_hide", false);
    }


    public void ReloadHeader()
    {
        if (CurrentMaster != null)
        {
            //CurrentMaster.PanelTitle.CssClass = "DialogsPageHeader";
            CurrentMaster.Title.TitleText = GetTitleText();
            CurrentMaster.Title.TitleImage = GetTitleIcon();
            CurrentMaster.PanelLeft.CssClass = "FullTabsLeft";

            CurrentMaster.Tabs.Tabs = GetTabs();
            CurrentMaster.Tabs.OpenTabContentAfterLoad = false;
            CurrentMaster.Tabs.SelectedTab = mSelectedTabIndex;
            CurrentMaster.Tabs.UrlTarget = "insertContent";
        }
    }

    #endregion


    #region "Private methods"

    private string GetTitleText()
    {
        string result = "";

        // Insert link
        if ((OutputFormat == OutputFormatEnum.BBLink) ||
            (OutputFormat == OutputFormatEnum.HTMLLink))
        {
            result = GetString("dialogs.header.title.link");
        }

        // Insert image or media
        else if (OutputFormat == OutputFormatEnum.HTMLMedia)
        {
            result = GetString("dialogs.header.title.imagemedia");
        }
        else if (OutputFormat == OutputFormatEnum.URL)
        {
            if (SelectableContent == SelectableContentEnum.OnlyImages)
            {
                result = GetString("dialogs.header.title.selectimage");
            }
            else if (SelectableContent == SelectableContentEnum.OnlyFlash)
            {
                result = GetString("dialogs.header.title.selectflash");
            }
            else if (SelectableContent == SelectableContentEnum.AllFiles)
            {
                result = GetString("dialogs.header.title.selectimagemedia");
            }
            else
            {
                result = GetString("dialogs.header.title.selectlink");
            }
        }
        else if (OutputFormat == OutputFormatEnum.NodeGUID)
        {
            if (SelectableContent == SelectableContentEnum.OnlyImages)
            {
                result = GetString("dialogs.header.title.selectimage");
            }
            else
            {
                result = GetString("dialogs.header.title.selectfiles");
            }
        }

        // Insert image
        else if (OutputFormat == OutputFormatEnum.BBMedia)
        {
            result = GetString("dialogs.header.title.image");
        }

        else if ((OutputFormat == OutputFormatEnum.URL) || (OutputFormat == OutputFormatEnum.NodeGUID))
        {
            switch (SelectableContent)
            {
                case SelectableContentEnum.OnlyImages:
                    result = GetString("dialogs.header.title.image");
                    break;

                case SelectableContentEnum.OnlyMedia:
                    result = GetString("dialogs.header.title.imagemedia");
                    break;

                case SelectableContentEnum.AllContent:
                    result = GetString("dialogs.header.title.link");
                    break;

                case SelectableContentEnum.AllFiles:
                    result = GetString("dialogs.header.title.allfiles");
                    break;

                case SelectableContentEnum.OnlyFlash:
                    result = GetString("dialogs.header.title.flash");
                    break;
            }
        }
        else if (OutputFormat == OutputFormatEnum.Custom)
        {
            switch (CustomOutputFormat.ToLower())
            {
                case "copy":
                    result = GetString("dialogs.header.title.copydoc");
                    break;

                case "move":
                    result = GetString("dialogs.header.title.movedoc");
                    break;

                case "link":
                    result = GetString("dialogs.header.title.linkdoc");
                    break;

                case "linkdoc":
                    result = GetString("dialogs.header.title.linkdoc");
                    break;

                case "relationship":
                    result = GetString("selectlinkdialog.title");
                    break;

                case "selectpath":
                    result = GetString("dialogs.header.title.selectpath");
                    break;
            }

        }

        return result;
    }


    private string GetTitleIcon()
    {
        string result = "";

        // Insert link
        if ((OutputFormat == OutputFormatEnum.BBLink) ||
            (OutputFormat == OutputFormatEnum.HTMLLink))
        {
            result = GetImageUrl("CMSModules/CMS_Content/Dialogs/titlelink.png");
        }

        // Insert image or media
        else if (OutputFormat == OutputFormatEnum.HTMLMedia)
        {
            result = GetImageUrl("CMSModules/CMS_Content/Dialogs/titlemedia.png");
        }
        else if (OutputFormat == OutputFormatEnum.URL)
        {
            if (SelectableContent == SelectableContentEnum.OnlyImages)
            {
                result = GetImageUrl("CMSModules/CMS_Content/Dialogs/titlemedia.png");
            }
            else if (SelectableContent == SelectableContentEnum.OnlyFlash)
            {
                result = GetImageUrl("CMSModules/CMS_Content/Dialogs/titlemedia.png");
            }
            else if (SelectableContent == SelectableContentEnum.AllFiles)
            {
                result = GetImageUrl("CMSModules/CMS_Content/Dialogs/titleselect.png");
            }
            else
            {
                result = GetImageUrl("CMSModules/CMS_Content/Dialogs/titlelink.png");
            }
        }
        else if (OutputFormat == OutputFormatEnum.NodeGUID)
        {
            if (SelectableContent == SelectableContentEnum.OnlyImages)
            {
                result = GetImageUrl("CMSModules/CMS_Content/Dialogs/titlemedia.png");
            }
            else
            {
                result = GetImageUrl("CMSModules/CMS_Content/Dialogs/titleselect.png");
            }
        }

        // Insert image
        else if (OutputFormat == OutputFormatEnum.BBMedia)
        {
            result = GetImageUrl("CMSModules/CMS_Content/Dialogs/titlemedia.png");
        }

        else if ((OutputFormat == OutputFormatEnum.URL) || (OutputFormat == OutputFormatEnum.NodeGUID))
        {
            switch (SelectableContent)
            {
                case SelectableContentEnum.OnlyImages:
                    result = GetImageUrl("CMSModules/CMS_Content/Dialogs/titlemedia.png");
                    break;

                case SelectableContentEnum.OnlyMedia:
                    result = GetImageUrl("CMSModules/CMS_Content/Dialogs/titlemedia.png");
                    break;

                case SelectableContentEnum.AllContent:
                    result = GetImageUrl("CMSModules/CMS_Content/Dialogs/titlelink.png");
                    break;

                case SelectableContentEnum.AllFiles:
                    result = GetImageUrl("CMSModules/CMS_Content/Dialogs/titleselect.png");
                    break;

                case SelectableContentEnum.OnlyFlash:
                    result = GetImageUrl("CMSModules/CMS_Content/Dialogs/titlemedia.png");
                    break;
            }
        }
        else if (OutputFormat == OutputFormatEnum.Custom)
        {
            switch (CustomOutputFormat.ToLower())
            {
                case "copy":
                    result = GetImageUrl("CMSModules/CMS_Content/Dialogs/titlecopy.png");
                    break;

                case "move":
                    result = GetImageUrl("CMSModules/CMS_Content/Dialogs/titlemove.png");
                    break;

                case "link":
                    result = GetImageUrl("CMSModules/CMS_Content/Dialogs/titlelink.png");
                    break;

                case "linkdoc":
                    result = GetImageUrl("CMSModules/CMS_Content/Dialogs/titlelink.png");
                    break;

                case "relationship":
                    result = GetImageUrl("CMSModules/CMS_Content/Dialogs/titleselect.png");
                    break;

                case "selectpath":
                    result = GetImageUrl("CMSModules/CMS_Content/Dialogs/titleselect.png");
                    break;
            }

        }

        return ResolveUrl(result);
    }


    /// <summary>
    /// Returns path to the specified tab page.
    /// </summary>
    /// <param name="fileName">File name of the tab page</param>    
    private string GetFilePath(string fileName)
    {
        return GetFilePath(fileName, null, null);
    }


    private string GetFilePath(string fileName, string parameterName, string parameterValue)
    {
        string path = null;
        if (IsLiveSite)
        {
            if (CMSContext.CurrentUser.IsAuthenticated())
            {
                path = "~/CMS/Dialogs/CMSFormControls/LiveSelectors/InsertImageOrMedia/";
            }
            else
            {
                path = "~/CMSFormControls/LiveSelectors/InsertImageOrMedia/";
            }
        }
        else
        {
            path = "~/CMSFormControls/Selectors/InsertImageOrMedia/";
        }
        if ((!String.IsNullOrEmpty(parameterName)) && (!String.IsNullOrEmpty(parameterValue)))
        {
            string query = URLHelper.RemoveUrlParameter(URLHelper.Url.Query, "hash");
            query = URLHelper.AddUrlParameter(query, parameterName, parameterValue);
            query = URLHelper.AddUrlParameter(query, "hash", QueryHelper.GetHash(query));
            return URLHelper.ResolveUrl(path + fileName) + URLHelper.EncodeQueryString(query).Replace("'", "%27");
        }
        else
        {
            return URLHelper.ResolveUrl(path + fileName) + URLHelper.EncodeQueryString(URLHelper.Url.Query);
        }
    }


    /// <summary>
    /// Returns path to the Media libraries tab page.
    /// </summary>    
    private string GetMediaLibrariesPath()
    {
        string path = null;
        if (IsLiveSite)
        {
            if (CMSContext.CurrentUser.IsAuthenticated())
            {
                path = "~/CMS/Dialogs/CMSModules/MediaLibrary/FormControls/LiveSelectors/InsertImageOrMedia/Tabs_Media.aspx";
            }
            else
            {
                path = "~/CMSModules/MediaLibrary/FormControls/LiveSelectors/InsertImageOrMedia/Tabs_Media.aspx";
            }
        }
        else
        {
            path = "~/CMSModules/MediaLibrary/FormControls/Selectors/InsertImageOrMedia/Tabs_Media.aspx";
        }

        return URLHelper.ResolveUrl(path) + URLHelper.EncodeQueryString(URLHelper.Url.Query);
    }


    /// <summary>
    /// Returns collection of tabs which should be displayed to the user.
    /// </summary>    
    private string[,] GetTabs()
    {
        int currIndex = 0;
        string[,] result = new string[6, 8];

        bool checkUI = true;
        // Disable personalization for none-HTML editors
        if ((CustomOutputFormat == "copy") || (CustomOutputFormat == "move") || (CustomOutputFormat == "link") ||
            (CustomOutputFormat == "relationship") || (CustomOutputFormat == "selectpath"))
        {
            checkUI = false;
        }
        else if (this.IsLiveSite)
        {
            // Ensure personalization of the HTML editor on the live site
            checkUI = ValidationHelper.GetBoolean(SettingsHelper.AppSettings["CKEditor:PersonalizeToolbarOnLiveSite"], false);
        }

        if (checkUI)
        {
            if ((OutputFormat == OutputFormatEnum.HTMLMedia) && !CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.WYSIWYGEditor", "InsertImageOrMedia"))
            {
                ScriptHelper.RegisterStartupScript(this, typeof(string), "frameLoad", ScriptHelper.GetScript("if (window.parent.frames['insertContent']) { window.parent.frames['insertContent'].location= '" + URLHelper.ResolveUrl("~/CMSDesk/accessdenied.aspx?resource=CMS.WYSIWYGEditor&uielement=InsertImageOrMedia") + "';} "));
                return result;
            }
            if ((OutputFormat == OutputFormatEnum.HTMLLink) && !CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.WYSIWYGEditor", "InsertLink"))
            {
                ScriptHelper.RegisterStartupScript(this, typeof(string), "frameLoad", ScriptHelper.GetScript("if (window.parent.frames['insertContent']) { window.parent.frames['insertContent'].location= '" + URLHelper.ResolveUrl("~/CMSDesk/accessdenied.aspx?resource=CMS.WYSIWYGEditor&uielement=InsertLink") + "';} "));
                return result;
            }
            if ((CustomOutputFormat == "linkdoc") && !(CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Content", "New.LinkExistingDocument") && CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Content", "New")))
            {
                ScriptHelper.RegisterStartupScript(this, typeof(string), "frameLoad", ScriptHelper.GetScript("if (window.parent.frames['insertContent']) { window.parent.frames['insertContent'].location= '" + URLHelper.ResolveUrl("~/CMSDesk/accessdenied.aspx?resource=CMS.Content&uielement=New.LinkExistingDocument") + "';} "));
                return result;
            }
        }

        // Attachments
        if ((CustomOutputFormat == "") && !HideAttachments &&
            (OutputFormat != OutputFormatEnum.NodeGUID) && (!checkUI || currentUser.IsAuthorizedPerUIElement("CMS.MediaDialog", "AttachmentsTab")))
        {
            result[currIndex, 0] = GetString("general.attachments");
            result[currIndex, 2] = GetFilePath("Tabs_Media.aspx", "source", CMSDialogHelper.GetMediaSource(MediaSourceEnum.DocumentAttachments));

            if (SelectedTab == "attachments")
            {
                mSelectedTabIndex = currIndex;
            }
            currIndex++;
        }
        else if (SelectedTab == "attachments")
        {
            SelectedTab = "web";
        }

        // Content
        if (!HideContent && (!checkUI || currentUser.IsAuthorizedPerUIElement("CMS.MediaDialog", "ContentTab")))
        {
            result[currIndex, 0] = GetString("general.content");
            result[currIndex, 2] = GetFilePath("Tabs_Media.aspx", "source", CMSDialogHelper.GetMediaSource(MediaSourceEnum.Content));

            if (SelectedTab == "content")
            {
                mSelectedTabIndex = currIndex;
            }
            currIndex++;
        }
        else if (SelectedTab == "content")
        {
            SelectedTab = "web";
        }

        // Media libraries
        if ((CustomOutputFormat == "") && !HideMediaLibraries &&
            (OutputFormat != OutputFormatEnum.NodeGUID) &&
            ModuleEntry.IsModuleLoaded(ModuleEntry.MEDIALIBRARY) &&
            (!checkUI || currentUser.IsAuthorizedPerUIElement("CMS.MediaDialog", "MediaLibrariesTab")))
        {
            result[currIndex, 0] = GetString("dialogs.header.libraries");
            result[currIndex, 2] = GetMediaLibrariesPath();

            if (SelectedTab == "libraries")
            {
                mSelectedTabIndex = currIndex;
            }
            currIndex++;
        }
        else if (SelectedTab == "libraries")
        {
            SelectedTab = "web";
        }

        // Web
        if ((CustomOutputFormat == "") && !HideWeb && (OutputFormat != OutputFormatEnum.NodeGUID) &&
            (!checkUI || currentUser.IsAuthorizedPerUIElement("CMS.MediaDialog", "WebTab")))
        {
            result[currIndex, 0] = GetString("dialogs.header.web");
            if ((OutputFormat == OutputFormatEnum.BBLink) || (OutputFormat == OutputFormatEnum.HTMLLink))
            {
                result[currIndex, 2] = GetFilePath("Tabs_WebLink.aspx");
            }
            else
            {
                result[currIndex, 2] = GetFilePath("Tabs_Web.aspx");
            }

            if (SelectedTab == "web")
            {
                mSelectedTabIndex = currIndex;
            }
            currIndex++;
        }

        // Anchor & E-mail
        if ((CustomOutputFormat == "") && ((OutputFormat == OutputFormatEnum.BBLink) ||
            (OutputFormat == OutputFormatEnum.HTMLLink) ||
            (OutputFormat == OutputFormatEnum.Custom)))
        {
            // Anchor
            if (!HideAnchor && (!checkUI || currentUser.IsAuthorizedPerUIElement("CMS.MediaDialog", "AnchorTab")))
            {
                result[currIndex, 0] = GetString("dialogs.header.anchor");
                result[currIndex, 2] = GetFilePath("Tabs_Anchor.aspx");

                if (SelectedTab == "anchor")
                {
                    mSelectedTabIndex = currIndex;
                }
                currIndex++;
            }
            else if (SelectedTab == "anchor")
            {
                SelectedTab = "web";
            }

            // E-mail
            if (!HideEmail && (!checkUI || currentUser.IsAuthorizedPerUIElement("CMS.MediaDialog", "EmailTab")))
            {
                result[currIndex, 0] = GetString("general.email");
                result[currIndex, 2] = GetFilePath("Tabs_Email.aspx");

                if (SelectedTab == "email")
                {
                    mSelectedTabIndex = currIndex;
                }
                currIndex++;
            }
            else if (SelectedTab == "email")
            {
                SelectedTab = "web";
            }
        }

        string selectedUrl = mSelectedTabIndex > 0 ? result[mSelectedTabIndex, 2] : result[0, 2];
        if (selectedUrl != null)
        {
            ScriptHelper.RegisterStartupScript(this, typeof(string), "frameLoad", ScriptHelper.GetScript("if (window.parent.frames['insertContent']) { window.parent.frames['insertContent'].location= '" + selectedUrl.Replace("&amp;", "&").Replace("'", "%27") + "';} "));
        }

        // No tab is displayed -> load UI Not available
        if (currIndex == 0)
        {
            ScriptHelper.RegisterStartupScript(this, typeof(string), "frameLoad", ScriptHelper.GetScript("if (window.parent.frames['insertContent']) { window.parent.frames['insertContent'].location= '" + URLHelper.ResolveUrl("~/CMSMessages/Information.aspx") + "?message=" + HttpUtility.UrlPathEncode(GetString("uiprofile.uinotavailable")) + "';} "));
        }

        return result;
    }

    #endregion
}

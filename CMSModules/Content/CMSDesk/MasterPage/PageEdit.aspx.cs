using System;
using System.Web;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.ExtendedControls;
using CMS.GlobalHelper;
using CMS.IO;
using CMS.PortalEngine;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.TreeEngine;
using CMS.UIControls;
using CMS.WorkflowEngine;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_Content_CMSDesk_MasterPage_PageEdit : CMSContentPage
{
    #region "Variables"

    protected string mSave = null;
    protected int nodeId = 0;
    protected TreeNode node = null;
    protected TreeProvider tree = null;
    protected CurrentUserInfo user = null;

    protected string mHead = null;
    protected string mBeforeLayout = null;
    protected string mAfterLayout = null;
    protected string mBody = null;

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        CMSContext.ViewMode = ViewModeEnum.MasterPage;

        // Keep current user
        user = CMSContext.CurrentUser;

        // Check UIProfile
        if (!user.IsAuthorizedPerUIElement("CMS.Content", "MasterPage"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Content", "MasterPage");
        }

        // Check "Design" permission
        if (!user.IsAuthorizedPerResource("CMS.Design", "Design"))
        {
            RedirectToAccessDenied("CMS.Design", "Design");
        }

        // Register the scripts
        ScriptHelper.RegisterProgress(this);
        ScriptHelper.RegisterSaveShortcut(btnSave, null, false);

        // Save changes support
        bool confirmChanges = SettingsKeyProvider.GetBoolValue(CMSContext.CurrentSiteName + ".CMSConfirmChanges");
        string script = string.Empty;
        if (confirmChanges)
        {
            script = "var confirmLeave='" + ResHelper.GetString("Content.ConfirmLeave", user.PreferredUICultureCode) + "'; \n";
        }
        else
        {
            script += "confirmChanges = false;";
        }

        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "saveChangesScript", ScriptHelper.GetScript(script));

        nodeId = QueryHelper.GetInteger("NodeId", 0);

        try
        {
            CMSContext.CurrentPageInfo = PageInfoProvider.GetPageInfo(CMSContext.CurrentSiteName, "/", CMSContext.PreferredCultureCode, null, false);

            // Title
            string title = CMSContext.CurrentTitle;
            if (!string.IsNullOrEmpty(title))
            {
                title = "<title>" + title + "</title>";
            }

            // Body class
            string bodyCss = CMSContext.CurrentBodyClass;
            if (bodyCss != null && bodyCss.Trim() != "")
            {
                bodyCss = "class=\"" + bodyCss + "\"";
            }
            else
            {
                bodyCss = "";
            }

            // metadata
            string meta = "<meta http-equiv=\"pragma\" content=\"no-cache\" />";

            string description = CMSContext.CurrentDescription;
            if (description != "")
            {
                meta += "<meta name=\"description\" content=\"" + description + "\" />";
            }

            string keywords = CMSContext.CurrentKeyWords;
            if (keywords != "")
            {
                meta += "<meta name=\"keywords\"  content=\"" + keywords + "\" />";
            }

            // Site style sheet
            string cssSiteSheet = "";

            CssStylesheetInfo cssInfo = null;
            int stylesheetId = CMSContext.CurrentPageInfo.DocumentStylesheetID;

            cssInfo = CssStylesheetInfoProvider.GetCssStylesheetInfo((stylesheetId > 0) ? stylesheetId : CMSContext.CurrentSite.SiteDefaultStylesheetID);

            if (cssInfo != null)
            {
                cssSiteSheet = CSSHelper.GetCSSFileLink(CSSHelper.GetStylesheetUrl(cssInfo.StylesheetName));
            }

            // Theme css files
            string themeCssFiles = "";
            if (cssInfo != null)
            {
                try
                {
                    string directory = URLHelper.GetPhysicalPath(string.Format("~/App_Themes/{0}/", cssInfo.StylesheetName));
                    if (Directory.Exists(directory))
                    {
                        foreach (string file in Directory.GetFiles(directory, "*.css"))
                        {
                            themeCssFiles += CSSHelper.GetCSSFileLink(CSSHelper.GetPhysicalCSSUrl(cssInfo.StylesheetName, Path.GetFileName(file)));
                        }
                    }
                }
                catch
                {
                }
            }

            // Add values to page
            mHead = FormatHTML(HighlightHTML(title + meta + cssSiteSheet + themeCssFiles), 2);
            mBody = bodyCss;
        }
        catch
        {
            lblError.Visible = true;
            lblError.Text = GetString("MasterPage.PageEditErr");
        }

        // Prepare the hints and typw dropdown
        lblType.Text = ResHelper.GetString("PageLayout.Type");

        if (drpType.Items.Count == 0)
        {
            drpType.Items.Add(new ListItem(ResHelper.GetString("TransformationType.Ascx"), TransformationTypeEnum.Ascx.ToString()));
            drpType.Items.Add(new ListItem(ResHelper.GetString("TransformationType.Html"), TransformationTypeEnum.Html.ToString()));
        }

        string lang = ValidationHelper.GetString(SettingsHelper.AppSettings["CMSProgrammingLanguage"], "C#");
        ltlDirectives.Text = "&lt;%@ Control Language=\"" + lang + "\" ClassName=\"Simple\" Inherits=\"CMS.PortalControls.CMSAbstractLayout\" %&gt;<br />&lt;%@ Register Assembly=\"CMS.PortalControls\" Namespace=\"CMS.PortalControls\" TagPrefix=\"cc1\" %&gt;";

        if (!SettingsKeyProvider.UsingVirtualPathProvider)
        {
            lblChecked.Visible = true;
            lblChecked.Text = "<br />" + AddSpaces(2) + GetString("MasterPage.VirtualPathProviderNotRunning");
            txtLayout.ReadOnly = true;
        }

        LoadData();

        // Register synchronization script for split mode
        if (CMSContext.DisplaySplitMode)
        {
            RegisterSplitModeSync(true, false);
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        bool editingEnabled = true;
        string info = null;

        // Setup the information and code type
        bool isAscx = (drpType.SelectedValue.ToLower() == "ascx");
        if (isAscx)
        {
            txtLayout.Language = LanguageEnum.ASPNET;
            info = ResHelper.GetString("Administration-PageLayout_New.Hint");

            // Check the edit code permission
            if (!user.IsAuthorizedPerResource("CMS.Design", "EditCode"))
            {
                editingEnabled = false;
                info = ResHelper.GetString("EditCode.NotAllowed");
            }
        }
        else
        {
            txtLayout.Language = LanguageEnum.HTMLMixed;
            info = ResHelper.GetString("EditLayout.HintHtml");
        }

        if (String.IsNullOrEmpty(lblLayoutInfo.Text))
        {
            lblLayoutInfo.Text = info;
        }
        lblLayoutInfo.Visible = (lblLayoutInfo.Text != "");

        txtLayout.ReadOnly = !editingEnabled;
    }


    public void LoadData()
    {
        if (nodeId > 0)
        {
            // Get the document
            tree = new TreeProvider(user);
            node = tree.SelectSingleNode(nodeId, user.PreferredCultureCode);
            if (node != null)
            {
                PageTemplateInfo pti = PageTemplateInfoProvider.GetPageTemplateInfo(node.DocumentPageTemplateID);
                if (pti != null)
                {
                    // Get shared layout
                    LayoutInfo li = LayoutInfoProvider.GetLayoutInfo(pti.LayoutID);
                    if (li != null)
                    {
                        // Load shared layout
                        if (!RequestHelper.IsPostBack())
                        {
                            txtLayout.Text = li.LayoutCode;
                            drpType.SelectedIndex = (li.LayoutType == LayoutTypeEnum.Html ? 1 : 0);
                        }

                        if (li.LayoutCheckedOutByUserID > 0)
                        {
                            lblChecked.Visible = true;
                            lblChecked.Text = "<br />" + AddSpaces(2) + GetString("MasterPage.LayoutCheckedOut");
                            if (!RequestHelper.IsPostBack())
                            {
                                txtLayout.Text = li.LayoutCode;
                                txtLayout.ReadOnly = true;
                            }
                        }
                    }
                    else
                    {
                        // Load custom layout
                        if (!RequestHelper.IsPostBack())
                        {
                            txtLayout.Text = ValidationHelper.GetString(pti.PageTemplateLayout, "");
                            drpType.SelectedIndex = (pti.PageTemplateLayoutType == LayoutTypeEnum.Html ? 1 : 0);
                        }

                        if (pti.PageTemplateLayoutCheckedOutByUserID > 0)
                        {
                            lblChecked.Visible = true;
                            lblChecked.Text = "<br />" + AddSpaces(2) + GetString("MasterPage.LayoutCheckedOut");
                            if (!RequestHelper.IsPostBack())
                            {
                                txtLayout.ReadOnly = true;
                            }
                        }
                    }
                }
                else
                {
                    txtLayout.ReadOnly = true;
                }

                // Load node data
                if (!RequestHelper.IsPostBack())
                {
                    txtBodyCss.Text = node.NodeBodyElementAttributes;
                    txtDocType.Text = node.NodeDocType;
                    txtHeadTags.Value = node.NodeHeadTags;
                }
            }
        }

        lblAfterDocType.Text = HighlightHTML("<html>") + "<br />" + AddSpaces(1) + HighlightHTML("<head>");
        lblAfterHeadTags.Text = AddSpaces(1) + HighlightHTML("</head>");
        lblAfterLayout.Text = AddSpaces(1) + HighlightHTML("</body>") + "<br />" + HighlightHTML("</html>");
        lblBodyEnd.Text = HighlightHTML(">");
        lblBodyStart.Text = AddSpaces(1) + HighlightHTML("<body " + HttpUtility.HtmlDecode(mBody));
    }


    /// <summary>
    /// Format HTML text.
    /// </summary>
    /// <param name="inputHTML">Input HTML</param>
    /// <param name="level">Indentation level</param>
    public string FormatHTML(string inputHTML, int level)
    {
        return AddSpaces(level) + inputHTML.Replace(HttpUtility.HtmlEncode(">"), HttpUtility.HtmlEncode(">") + "<br />" + AddSpaces(level));
    }


    /// <summary>
    /// Add spaces.
    /// </summary>
    /// <param name="level">Indentation level</param>
    public string AddSpaces(int level)
    {
        string toReturn = "";
        for (int i = 0; i < level * 2; i++)
        {
            toReturn += "&nbsp;";
        }

        return toReturn;
    }


    /// <summary>
    /// Highlight HTML.
    /// </summary>
    /// <param name="inputHtml">Input HTML</param>
    public string HighlightHTML(string inputHtml)
    {
        return HTMLHelper.HighlightHTML(inputHtml);
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        if ((nodeId > 0) && (node != null))
        {
            LayoutTypeEnum layoutType = LayoutInfoProvider.GetLayoutTypeEnum(drpType.SelectedValue);

            // Check the permissions
            if ((layoutType != LayoutTypeEnum.Ascx) || user.IsAuthorizedPerResource("CMS.Design", "EditCode"))
            {
                // Update the layout
                if (node.DocumentPageTemplateID > 0)
                {
                    PageTemplateInfo pti = PageTemplateInfoProvider.GetPageTemplateInfo(node.DocumentPageTemplateID);
                    if (pti != null)
                    {
                        // Get shared layout
                        LayoutInfo li = LayoutInfoProvider.GetLayoutInfo(pti.LayoutID);
                        if (li != null)
                        {
                            // Update shared layout
                            li.LayoutCode = txtLayout.Text;
                            li.LayoutType = layoutType;

                            LayoutInfoProvider.SetLayoutInfo(li);
                        }
                        else if (pti.PageTemplateLayoutCheckedOutByUserID <= 0)
                        {
                            // Update custom layout
                            pti.PageTemplateLayout = txtLayout.Text;
                            pti.PageTemplateLayoutType = layoutType;

                            PageTemplateInfoProvider.SetPageTemplateInfo(pti);
                        }
                    }
                }
            }

            // Update fields
            node.NodeBodyElementAttributes = txtBodyCss.Text;
            node.NodeDocType = txtDocType.Text;
            node.NodeHeadTags = txtHeadTags.Value.ToString();

            // Update the node
            node.Update();

            // Update search index
            if ((node.PublishedVersionExists) && (SearchIndexInfoProvider.SearchEnabled))
            {
                SearchTaskInfoProvider.CreateTask(SearchTaskTypeEnum.Update, PredefinedObjectType.DOCUMENT, SearchHelper.ID_FIELD, node.GetSearchID());
            }

            // Log synchronization
            DocumentSynchronizationHelper.LogDocumentChange(node, TaskTypeEnum.UpdateDocument, tree);

            lblInfo.Visible = true;
            lblInfo.Text = GetString("General.ChangesSaved");

            // Clear cache
            PageInfoProvider.RemoveAllPageInfosFromCache();
        }
    }

    #endregion
}
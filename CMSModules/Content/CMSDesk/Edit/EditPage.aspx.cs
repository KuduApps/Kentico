using System;

using CMS.SettingsProvider;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.TreeEngine;
using CMS.PortalEngine;
using CMS.UIControls;
using CMS.URLRewritingEngine;

public partial class CMSModules_Content_CMSDesk_Edit_EditPage : CMSContentPage
{
    protected string viewpage = "../blank.htm";
    protected TreeNode node = null;
    protected string loadScript = "FocusFrame();";

    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptHelper.RegisterProgress(this);
        ScriptHelper.RegisterScriptFile(this, "cmsedit.js");

        string notAllowedScript = ScriptHelper.GetScript("var notAllowedAction = '" + GetString("editpage.actionnotallowed") + "';");
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "notAllowedAction", notAllowedScript);

        // Get the node ID
        int nodeId = QueryHelper.GetInteger("nodeid", 0);

        viewpage = ResolveUrl("~/CMSModules/Content/CMSDesk/Edit/edit.aspx") + URLHelper.Url.Query;

        string action = QueryHelper.GetString("action", "").ToLower();
        string mode = QueryHelper.GetString("mode", "").ToLower();

        string elementToCheck = null;
        switch (mode)
        {
            case "edit":
                elementToCheck = "Page";
                break;

            case "design":
                elementToCheck = "Design";
                break;

            case "editform":
                elementToCheck = "EditForm";
                break;
        }

        // Check UI elements for tabs only if mode is set (skip UI element check for actions, which don't have own UI element like NEW)
        if (!string.IsNullOrEmpty(elementToCheck))
        {
            if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Content", elementToCheck))
            {
                RedirectToCMSDeskUIElementAccessDenied("CMS.Content", elementToCheck);
            }
        }

        switch (action)
        {
            case "newvariant":
                viewpage = URLHelper.AppendQuery(ResolveUrl("~/CMSModules/OnlineMarketing/Pages/Content/ABTesting/ABVariant/NewPage.aspx"), URLHelper.Url.Query);
                break;

            default:
                // Check if new document desired
                bool newdocument = (action == "new");
                DataClassInfo classInfo = null;
                if (newdocument)
                {
                    // Get the class ID
                    int classId = QueryHelper.GetInteger("classid", 0);
                    classInfo = DataClassInfoProvider.GetDataClass(classId);
                }
                else
                {
                    // Get the document
                    TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);
                    node = tree.SelectSingleNode(nodeId, CMSContext.PreferredCultureCode, false);
                    if (node != null)
                    {
                        classInfo = DataClassInfoProvider.GetDataClass(node.NodeClassName);

                        // Register js synchronization script for split mode
                        if (CMSContext.DisplaySplitMode)
                        {
                            RegisterSplitModeSync(false, false);
                        }
                    }
                }

                // Check the editing page change
                if (classInfo != null)
                {
                    if (CMSContext.ViewMode == ViewModeEnum.EditForm || newdocument)
                    {
                        // If new document, check if new page URL is set
                        if (newdocument)
                        {
                            if (!string.IsNullOrEmpty(classInfo.ClassNewPageURL))
                            {
                                // Use special settings for new page
                                if (classInfo.ClassNewPageURL.ToLower().EndsWith("/cmsmodules/content/cmsdesk/new/newpage.aspx"))
                                {
                                    // Disable onload script
                                    loadScript = String.Empty;
                                }

                                viewpage = URLHelper.AppendQuery(ResolveUrl(classInfo.ClassNewPageURL), URLHelper.Url.Query);
                            }
                        }
                        // If existing document, check if editing page URL is set
                        else if (!string.IsNullOrEmpty(classInfo.ClassEditingPageURL))
                        {
                            viewpage = URLHelper.AppendQuery(ResolveUrl(classInfo.ClassEditingPageURL), URLHelper.Url.Query);
                        }

                        ScriptHelper.RegisterTitleScript(this, GetString("content.ui.form"));
                    }
                    else if (CMSContext.ViewMode == ViewModeEnum.Edit)
                    {
                        // Check if view page URL is set
                        if (!string.IsNullOrEmpty(classInfo.ClassViewPageUrl))
                        {
                            viewpage = URLHelper.AppendQuery(ResolveUrl(classInfo.ClassViewPageUrl), URLHelper.Url.Query);
                        }
                        else
                        {
                            viewpage = URLRewriter.GetEditingUrl(node);
                        }

                        ScriptHelper.RegisterTitleScript(this, GetString("content.ui.page"));
                    }
                    else if (CMSContext.ViewMode == ViewModeEnum.Design)
                    {
                        // Use permanent URL to get proper design mode
                        viewpage = URLRewriter.GetEditingUrl(node);

                        ScriptHelper.RegisterTitleScript(this, GetString("content.ui.design"));
                    }
                    else
                    {
                        // Use standard URL to get other modes
                        viewpage = ResolveUrl(CMSContext.GetUrl(node.NodeAliasPath, node.DocumentUrlPath));
                    }
                }
                break;
        }
    }
}

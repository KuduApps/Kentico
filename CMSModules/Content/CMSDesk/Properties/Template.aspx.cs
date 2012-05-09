using System;
using System.Data;
using System.Web.UI;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.PortalEngine;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.Synchronization;
using CMS.TreeEngine;
using CMS.UIControls;
using CMS.WorkflowEngine;
using CMS.FormControls;
using CMS.LicenseProvider;


[RegisterTitle("content.ui.propertiestemplate")]
public partial class CMSModules_Content_CMSDesk_Properties_Template : CMSPropertiesPage
{
    #region "Variables & constants"

    private const string PORTALENGINE_UI_LAYOUTPATH = "~/CMSModules/PortalEngine/UI/Layout/";

    protected int nodeId = 0;

    protected string mSave = null;
    protected string mInherit = null;
    protected string mClone = null;
    protected string mEditTemplateProperties = null;
    protected string mSaveDoc = null;

    private string btnSaveOnClickScript = "";
    private int siteid = 0;
    private int cloneId = 0;

    protected bool hasModifyPermission = true;

    protected TreeNode node = null;
    protected TreeProvider tree = null;
    protected CurrentUserInfo currentUser = null;

    protected bool hasDesign = false;
    protected bool displaySplitMode = CMSContext.DisplaySplitMode;

    #endregion


    #region "Page events"

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Content", "Properties.Template"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Content", "Properties.Template");
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Register the scripts        
        ScriptHelper.RegisterProgress(this);
        ScriptHelper.RegisterScriptFile(this, "~/CMSModules/Content/CMSDesk/Properties/template.js");
        inheritElem.DocumentSettings = true;
        int documentId = 0;

        UIContext.PropertyTab = PropertyTabEnum.Template;

        currentUser = CMSContext.CurrentUser;

        nodeId = QueryHelper.GetInteger("nodeid", 0);

        tree = new TreeProvider(currentUser);
        node = tree.SelectSingleNode(nodeId, CMSContext.PreferredCultureCode, false);

        // Redirect to page 'New culture version' in split mode. It must be before setting EditedDocument.
        if ((node == null) && displaySplitMode)
        {
            URLHelper.Redirect("~/CMSModules/Content/CMSDesk/New/NewCultureVersion.aspx" + URLHelper.Url.Query);
        }
        // Set edited document
        EditedDocument = node;

        if (node != null)
        {
            siteid = node.NodeSiteID;
            documentId = node.DocumentID;
        }

        imgSaveDoc.ImageUrl = GetImageUrl("CMSModules/CMS_Content/EditMenu/save.png");
        imgSaveDoc.DisabledImageUrl = GetImageUrl("CMSModules/CMS_Content/EditMenu/savedisabled.png");
        mSaveDoc = GetString("general.save");

        pnlInherits.GroupingText = GetString("PageProperties.InheritLevels");

        ltlScript.Text = "";
        string initScript = null;

        hasDesign = currentUser.IsAuthorizedPerResource("CMS.Design", "Design");
        if (hasDesign)
        {
            btnEditTemplateProperties.Attributes.Add("onclick", "modalDialog('" + ResolveUrl("~/CMSModules/PortalEngine/UI/PageTemplates/PageTemplate_Edit.aspx") + "?templateid=' + document.getElementById('SelectedTemplateId').value + '&dialog=1', 'TemplateSelection', 850, 680);return false;");

            bool allowEditShared = currentUser.IsAuthorizedPerUIElement("CMS.Content", "Template.ModifySharedTemplates");

            // Define GetCurrentTemplateId() used for specifing teplateId in the SaveAsNewTemplate onClick handler
            initScript =
@"
var allowEditShared = " + allowEditShared.ToString().ToLower() + @";

function GetCurrentTemplateId() {
    if (document.getElementById('SelectedTemplateId').value > 0) { 
        return document.getElementById('SelectedTemplateId').value;
    } else { 
        return document.getElementById('InheritedTemplateId').value;
    }
};"
            ;

            ltlPreInitScript.Text = ScriptHelper.GetScript(initScript);

            btnSelect.Text = GetString("PageProperties.Select");
            btnSelect.Attributes.Add("onclick", "modalDialog('" + ResolveUrl(PORTALENGINE_UI_LAYOUTPATH + "PageTemplateSelector.aspx") + "?documentid=" + documentId + "', 'PageTemplateSelection', '90%', '85%'); return false;");

            // Register the dialog script
            ScriptHelper.RegisterDialogScript(this);

            ltlElemScript.Text += ScriptHelper.GetScript(
@"
var cloneElem = document.getElementById('" + btnClone.ClientID + @"');
if (cloneElem != null) var cloneElemStyle = (cloneElem.style != null) ? cloneElem.style : cloneElem;
var inheritElem = document.getElementById('" + btnInherit.ClientID + @"');
if (inheritElem != null) var inheritElemStyle = (inheritElem.style != null) ? inheritElem.style : inheritElem;
var saveElem = document.getElementById('" + btnSave.ClientID + @"');
if (saveElem != null) var saveElemStyle = (saveElem.style != null) ? saveElem.style : saveElem;
var editTemplatePropertiesElem = document.getElementById('" + btnEditTemplateProperties.ClientID + @"');
if (editTemplatePropertiesElem != null) var editTemplatePropertiesElemStyle = (editTemplatePropertiesElem.style != null) ? editTemplatePropertiesElem.style : editTemplatePropertiesElem;
"
            );

            txtTemplate.Text = ValidationHelper.GetString(Request.Params["txtTemplate"], "");

            pnlActions.GroupingText = GetString("PageProperties.Template");

            mClone = GetString("PageProperties.Clone");
            mSave = GetString("PageProperties.Save");
            mInherit = GetString("PageProperties.Inherit");
            mEditTemplateProperties = GetString("PageProperties.EditTemplateProperties");

            imgClone.ImageUrl = GetImageUrl("CMSModules/CMS_Content/Template/clone.png");
            imgInherit.ImageUrl = GetImageUrl("CMSModules/CMS_Content/Template/inherit.png");
            imgSave.ImageUrl = GetImageUrl("CMSModules/CMS_Content/Template/save.png");
            imgEditTemplateProperties.ImageUrl = GetImageUrl("CMSModules/CMS_Content/Template/edit.png");
            imgClone.DisabledImageUrl = GetImageUrl("CMSModules/CMS_Content/Template/clonedisabled.png");
            imgInherit.DisabledImageUrl = GetImageUrl("CMSModules/CMS_Content/Template/inheritdisabled.png");
            imgSave.DisabledImageUrl = GetImageUrl("CMSModules/CMS_Content/Template/savedisabled.png");
            imgEditTemplateProperties.DisabledImageUrl = GetImageUrl("CMSModules/CMS_Content/Template/editdisabled.png");
        }
        else
        {
            RedirectToUINotAvailable();
        }

        if (!RequestHelper.IsPostBack())
        {
            ReloadData();

            // Modal dialog for save
            btnSaveOnClickScript = "modalDialog('" + ResolveUrl(PORTALENGINE_UI_LAYOUTPATH + "SaveNewPageTemplate.aspx") + "?templateId=' + GetCurrentTemplateId() + '&siteid=" + siteid + "', 'SaveNewTemplate', 480, 360);return false;";

            if (node != null)
            {
                if (node.NodeAliasPath != "/")
                {
                    inheritElem.Value = node.NodeInheritPageLevels;
                    // Try get info whether exist linked document in path
                    DataSet ds = tree.SelectNodes(CMSContext.CurrentSiteName, "/%", node.DocumentCulture, false, null, "NodeLinkedNodeID IS NOT NULL AND (N'" + SqlHelperClass.GetSafeQueryString(node.NodeAliasPath) + "' LIKE NodeAliasPath + '%')", null, -1, false, 1, "Count(*) AS NumOfDocs");
                    // If node is not link or none of parent documents is not linked document use document name path
                    if (!node.IsLink && ValidationHelper.GetInteger(DataHelper.GetDataRowValue(ds.Tables[0].Rows[0], "NumOfDocs"), 0) == 0)
                    {
                        inheritElem.TreePath = TreePathUtils.GetParentPath("/" + node.DocumentNamePath);
                    }
                    // otherwise use alias path
                    else
                    {
                        inheritElem.TreePath = TreePathUtils.GetParentPath("/" + node.NodeAliasPath);
                    }
                }
                else
                {
                    pnlInherits.Visible = false;
                }
            }
        }
        else if (hasDesign)
        {
            initScript =
                "document.getElementById('SelectedTemplateId').value = " + ValidationHelper.GetInteger(Request.Params["SelectedTemplateId"], 0) + "; \n " +
                "document.getElementById('InheritedTemplateId').value = " + ValidationHelper.GetInteger(Request.Params["InheritedTemplateId"], 0) + "; \n " +
                "document.getElementById('Saved').value = " + ValidationHelper.GetBoolean(Request.Params["Saved"], false).ToString().ToLower() + "; \n" +
                "document.getElementById('TemplateDisplayName').value = '" + ValidationHelper.GetString(Request.Params["TemplateDisplayName"], "") + "'; \n " +
                "document.getElementById('TemplateDescription').value = '" + ValidationHelper.GetString(Request.Params["TemplateDescription"], "") + "'; \n " +
                "document.getElementById('TemplateCategory').value = '" + ValidationHelper.GetString(Request.Params["TemplateCategory"], "") + "'; \n " +
                "document.getElementById('isPortal').value = " + ValidationHelper.GetBoolean(Request.Params["isPortal"], false).ToString().ToLower() + "; \n " +
                "document.getElementById('isReusable').value = " + ValidationHelper.GetBoolean(Request.Params["isReusable"], false).ToString().ToLower() + "; \n " +
                "document.getElementById('isAdHoc').value = " + ValidationHelper.GetBoolean(Request.Params["isAdHoc"], false).ToString().ToLower() + "; \n ";

            string textTemplate = ValidationHelper.GetString(Request.Params["txtTemplate"], "");
            if (textTemplate == "")
            {
                textTemplate = ValidationHelper.GetString(Request.Params["TextTemplate"], "");
            }
            initScript += "document.getElementById('TextTemplate').value = " + ScriptHelper.GetString(textTemplate) + "; \n ";

            ltlInitScript.Text = ScriptHelper.GetScript(initScript);
            ltlScript.Text += ScriptHelper.GetScript("ShowButtons(document.getElementById('isPortal').value, document.getElementById('isReusable').value, document.getElementById('isAdHoc').value); \n");

            ltlScript.Text += ScriptHelper.GetScript("if (document.getElementById('SelectedTemplateId').value == 0) { if (inheritElemStyle != null) inheritElemStyle.display = 'none'; if (editTemplatePropertiesElemStyle != null) editTemplatePropertiesElemStyle.display = 'none'; }");
            txtTemplate.Text = textTemplate;

            btnSaveOnClickScript = "modalDialog('" + ResolveUrl(PORTALENGINE_UI_LAYOUTPATH + "SaveNewPageTemplate.aspx") + "?templateId=' + GetCurrentTemplateId() + '&siteid=" + siteid + "', 'SaveNewTemplate', 480, 360);return false;";
        }

        // Javascript function for updating template name 
        string updateTemplateName = ScriptHelper.GetScript(@"function SetTemplateName(templateName) {
        var txtTemplate = document.getElementById('" + txtTemplate.ClientID + "'); txtTemplate.value = templateName;}");

        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "SetTemplateName", updateTemplateName);
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        btnSave.Attributes.Add("onclick", btnSaveOnClickScript);

        if (hasModifyPermission)
        {
            ScriptHelper.RegisterSaveShortcut(lnkSaveDoc, null, false);
        }

        if (node != null)
        {
            siteid = node.NodeSiteID;

            // If node is root
            if (node.NodeClassName.Equals("CMS.Root", StringComparison.InvariantCultureIgnoreCase))
            {
                mInherit = GetString("PageProperties.Clear");
                imgInherit.ImageUrl = GetImageUrl("CMSModules/CMS_Content/Template/clear.png");
                imgInherit.DisabledImageUrl = GetImageUrl("CMSModules/CMS_Content/Template/cleardisabled.png");
            }

            // Register js synchronization script for split mode
            if (displaySplitMode)
            {
                RegisterSplitModeSync(true, false);
            }
        }
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Disables form editing.
    /// </summary>
    private void DisableFormEditing()
    {
        hasModifyPermission = false;

        //pnlSoftTemplate.Enabled = false;
        txtTemplate.Enabled = false;
        btnSelect.Enabled = false;
        btnInherit.Enabled = false;
        btnClone.Enabled = false;
        pnlInherits.Visible = false;
        btnSave.Enabled = true;
        btnEditTemplateProperties.Enabled = true;

        // Disable 'save button'
        lnkSaveDoc.Enabled = false;
        lnkSaveDoc.CssClass = "MenuItemEditDisabled";

    }


    private void ReloadData()
    {
        if (node != null)
        {
            // Check read permissions
            if (currentUser.IsAuthorizedPerDocument(node, NodePermissionsEnum.Read) == AuthorizationResultEnum.Denied)
            {
                RedirectToAccessDenied(String.Format(GetString("cmsdesk.notauthorizedtoreaddocument"), node.NodeAliasPath));
            }
            // Check modify permissions
            else if (currentUser.IsAuthorizedPerDocument(node, NodePermissionsEnum.Modify) == AuthorizationResultEnum.Denied)
            {
                DisableFormEditing();

                lblInfo.Visible = true;
                lblInfo.Text = String.Format(GetString("cmsdesk.notauthorizedtoeditdocument"), node.NodeAliasPath);
            }

            if (hasDesign)
            {
                // Show template name by template id
                if (node.GetValue("DocumentPageTemplateID") != null)
                {
                    PageTemplateInfo pt = PageTemplateInfoProvider.GetPageTemplateInfo(Convert.ToInt32(node.GetValue("DocumentPageTemplateID")));
                    if (pt != null)
                    {
                        // Prepare the template name
                        string templateName = null;
                        if (pt.IsReusable)
                        {
                            templateName = pt.DisplayName;
                        }
                        else
                        {
                            templateName = "Ad-hoc: " + (!string.IsNullOrEmpty(node.DocumentName) ? node.DocumentName : node.DocumentNamePath);
                        }

                        ltlScript.Text += ScriptHelper.GetScript("SelectTemplate(" + pt.PageTemplateId + ", " + ScriptHelper.GetString(templateName) + ", " + pt.IsPortal.ToString().ToLower() + ", " + pt.IsReusable.ToString().ToLower() + "); RememberTemplate(" + ScriptHelper.GetString(pt.DisplayName) + ");");
                    }
                    else
                    {
                        ltlScript.Text += ScriptHelper.GetScript("NoTemplateSelected();");
                    }
                    btnInherit.Visible = true;
                }
                else
                {
                    btnInherit_Click(null, null);
                }
            }
        }
    }

    #endregion


    #region "Button handling"

    protected void btnInherit_Click(object sender, EventArgs e)
    {
        int pageTemplateId = 0;
        if ((node != null) && (node.NodeParentID > 0))
        {
            // Get inherited page template
            object currentPageTemplateId = node.GetValue("DocumentPageTemplateID");
            node.SetValue("DocumentPageTemplateID", DBNull.Value);
            node.LoadInheritedValues(new string[] { "DocumentPageTemplateID" }, false);
            pageTemplateId = ValidationHelper.GetInteger(node.GetValue("DocumentPageTemplateID"), 0);
            node.SetValue("DocumentPageTemplateID", currentPageTemplateId);
        }

        if (pageTemplateId > 0)
        {
            // Get the page template
            PageTemplateInfo pt = PageTemplateInfoProvider.GetPageTemplateInfo(pageTemplateId);
            if (pt != null)
            {
                txtTemplate.Text = pt.DisplayName + " (inherited)";
                ltlScript.Text += ScriptHelper.GetScript("pressedInherit(" + pageTemplateId + "); ShowButtons(" + pt.IsPortal.ToString().ToLower() + ", " + pt.IsReusable.ToString().ToLower() + ");");
                btnSaveOnClickScript = "modalDialog('" + ResolveUrl(PORTALENGINE_UI_LAYOUTPATH + "SaveNewPageTemplate.aspx") + "?templateid=' + document.getElementById('InheritedTemplateId').value + '&siteid=" + siteid + "', 'SaveNewTemplate', 480, 360);return false;";
            }
        }
        else
        {
            txtTemplate.Text = "";
            ltlScript.Text += ScriptHelper.GetScript("pressedInherit(" + pageTemplateId + "); ShowButtons(false, false); ");
        }

        if (sender != null)
        {
            // Log the synchronization
            DocumentSynchronizationHelper.LogDocumentChange(node, TaskTypeEnum.UpdateDocument, tree);
        }
    }


    protected void btnClone_Click(object sender, EventArgs e)
    {
        int pageTemplateId = ValidationHelper.GetInteger(Request.Params["SelectedTemplateId"], 0);
        if (pageTemplateId == 0)
        {
            pageTemplateId = ValidationHelper.GetInteger(Request.Params["InheritedTemplateId"], 0);
        }

        if (pageTemplateId > 0)
        {
            PageTemplateInfo pt = PageTemplateInfoProvider.GetPageTemplateInfo(pageTemplateId);
            if (pt != null)
            {
                // Clone the info
                string docName = node.DocumentName;
                if (docName == "")
                {
                    docName = "/";
                }

                string displayName = "Ad-hoc: " + docName;

                PageTemplateInfo newInfo = PageTemplateInfoProvider.CloneTemplateAsAdHoc(pt, displayName, CMSContext.CurrentSite.SiteID);

                newInfo.Description = String.Format(GetString("PageTemplate.AdHocDescription"), node.DocumentNamePath);
                PageTemplateInfoProvider.SetPageTemplateInfo(newInfo);

                // Save the MVT/Content personalization variants of this page template
                if (LicenseHelper.CheckFeature(URLHelper.GetCurrentDomain(), FeatureEnum.MVTesting))
                {
                    ModuleCommands.OnlineMarketingCloneTemplateMVTVariants(pageTemplateId, newInfo.PageTemplateId);
                }
                if (LicenseHelper.CheckFeature(URLHelper.GetCurrentDomain(), FeatureEnum.ContentPersonalization))
                {
                    ModuleCommands.OnlineMarketingCloneTemplateContentPersonalizationVariants(pageTemplateId, newInfo.PageTemplateId);
                }

                ltlScript.Text += ScriptHelper.GetScript("pressedClone(" + newInfo.PageTemplateId + "); ShowButtons(" + pt.IsPortal.ToString().ToLower() + ", " + pt.IsReusable.ToString().ToLower() + ", true);");
                btnSaveOnClickScript = "modalDialog('" + ResolveUrl(PORTALENGINE_UI_LAYOUTPATH + "SaveNewPageTemplate.aspx") + "?templateid=' + document.getElementById('SelectedTemplateId').value + '&siteid=" + siteid + "', 'SaveNewTemplate', 480, 360);return false;";
                txtTemplate.Text = newInfo.DisplayName;

                cloneId = newInfo.PageTemplateId;
            }
            btnSave.Visible = true;

            lnkSave_Click(sender, e);
        }
    }


    protected void lnkSave_Click(object sender, EventArgs e)
    {
        if (node != null)
        {
            // Check modify permissions
            if (currentUser.IsAuthorizedPerDocument(node, NodePermissionsEnum.Modify) == AuthorizationResultEnum.Denied)
            {
                DisableFormEditing();

                lblInfo.Visible = true;
                lblInfo.Text = String.Format(GetString("cmsdesk.notauthorizedtoeditdocument"), node.NodeAliasPath);
                return;
            }

            if (hasDesign)
            {
                // Update the data
                int selectedPageTemplateId = ValidationHelper.GetInteger(Request.Params["SelectedTemplateId"], 0);

                // Save just created ad-hoc template
                if (cloneId > 0)
                {
                    selectedPageTemplateId = cloneId;
                }

                if (selectedPageTemplateId != 0)
                {
                    ltlScript.Text += ScriptHelper.GetScript("document.getElementById('SelectedTemplateId').value = " + selectedPageTemplateId);
                    if ((txtTemplate.Text.Length > 5) && (txtTemplate.Text.ToLower().Substring(0, 6) == "ad-hoc"))
                    {
                        // Ad-hoc page is used, set ad-hoc page as DocumentPageTemplateID
                        node.SetValue("DocumentPageTemplateID", selectedPageTemplateId);

                        PageTemplateInfo pt = PageTemplateInfoProvider.GetPageTemplateInfo(selectedPageTemplateId);
                        if (pt != null)
                        {
                            ltlScript.Text += ScriptHelper.GetScript("pressedClone(" + selectedPageTemplateId + ",null); ShowButtons(" + pt.IsPortal.ToString().ToLower() + ", " + pt.IsReusable.ToString().ToLower() + ", true)");
                        }
                    }
                    else
                    {
                        // Template was selected, set SelectedTemplateId as DocumentPageTemplateID
                        PageTemplateInfo pt = PageTemplateInfoProvider.GetPageTemplateInfo(selectedPageTemplateId);
                        if (pt != null)
                        {
                            node.SetValue("DocumentPageTemplateID", ValidationHelper.GetInteger(Request.Params["SelectedTemplateId"], 0));
                            ltlScript.Text += ScriptHelper.GetScript("SelectTemplate(" + selectedPageTemplateId + ", null, " + pt.IsPortal.ToString().ToLower() + ", " + pt.IsReusable.ToString().ToLower() + ")");
                        }
                    }
                }
                else
                {
                    // Template is inherited, set null as DocumentPageTemplateID
                    node.SetValue("DocumentPageTemplateID", null);
                    int inheritedTemplateID = ValidationHelper.GetInteger(Request.Params["InheritedTemplateId"], 0);
                    if (inheritedTemplateID > 0)
                    {
                        PageTemplateInfo pt = PageTemplateInfoProvider.GetPageTemplateInfo(inheritedTemplateID);
                        if (pt != null)
                        {
                            ltlScript.Text += ScriptHelper.GetScript("pressedInherit(" + inheritedTemplateID + "); ShowButtons(" + pt.IsPortal.ToString().ToLower() + ", " + pt.IsReusable.ToString().ToLower() + ", false)");
                        }
                    }
                    else
                    {
                        node.SetValue("DocumentPageTemplateID", null);
                        ltlScript.Text += ScriptHelper.GetScript("pressedInherit(0); ShowButtons(false, false, false); ");
                    }
                }
            }

            node.SetValue("NodeInheritPageLevels", inheritElem.Value);
            node.Update();

            // Ensure default combination if page template changed
            if (SettingsKeyProvider.GetBoolValue(CMSContext.CurrentSiteName + ".CMSMVTEnabled")
                && LicenseHelper.CheckFeature(URLHelper.GetCurrentDomain(), FeatureEnum.MVTesting)
                && (ModuleCommands.OnlineMarketingContainsMVTest(node.NodeAliasPath, node.NodeSiteID, node.DocumentCulture, false)))
            {
                ModuleCommands.OnlineMarketingEnsureDefaultCombination(node.DocumentPageTemplateID);
            }

            // Update search index for node
            if (node.PublishedVersionExists && SearchIndexInfoProvider.SearchEnabled)
            {
                SearchTaskInfoProvider.CreateTask(SearchTaskTypeEnum.Update, PredefinedObjectType.DOCUMENT, SearchHelper.ID_FIELD, node.GetSearchID());
            }

            lblInfo.Visible = true;
            lblInfo.Text = GetString("General.ChangesSaved");
        }

        // Log the synchronization
        DocumentSynchronizationHelper.LogDocumentChange(node, TaskTypeEnum.UpdateDocument, tree);
    }

    #endregion
}
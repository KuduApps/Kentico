using System;
using System.Data;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.TreeEngine;
using CMS.CMSHelper;
using CMS.SettingsProvider;
using CMS.UIControls;
using CMS.SiteProvider;
using CMS.LicenseProvider;
using CMS.ExtendedControls;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_Content_Controls_TreeContextMenu : CMSUserControl, ICallbackEventHandler
{
    #region "Variables"

    private DialogConfiguration mConfig = null;
    private const string separator = "##SEP##";

    #endregion


    #region "Enums"

    protected enum Action
    {
        Move = 0,
        Copy = 1,
        LinkDoc = 2
    }

    #endregion


    #region "Private properties"

    /// <summary>
    /// Gets the configuration for Copy and Move dialog.
    /// </summary>
    private DialogConfiguration Config
    {
        get
        {
            if (mConfig == null)
            {
                mConfig = new DialogConfiguration();
                mConfig.ContentSelectedSite = CMSContext.CurrentSiteName;
                mConfig.OutputFormat = OutputFormatEnum.Custom;
                mConfig.SelectableContent = SelectableContentEnum.AllContent;
                mConfig.HideAttachments = false;
            }
            return mConfig;
        }
    }

    #endregion


    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        // Display LANGUAGES option just in case its available in the current site context
        if (!pnlUILanguages.IsHidden)
        {
            pnlUILanguages.Visible = CultureInfoProvider.IsSiteMultilignual(CMSContext.CurrentSiteName) && CultureInfoProvider.LicenseVersionCheck();
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Prepare scripts for Copy / Move
        string copyRef = Page.ClientScript.GetCallbackEventReference(this, "GetContextMenuParameter('nodeMenu')", "CopyMoveItem", "'" + Action.Copy + "'");
        string moveRef = Page.ClientScript.GetCallbackEventReference(this, "GetContextMenuParameter('nodeMenu')", "CopyMoveItem", "'" + Action.Move + "'");
        string linkRef = Page.ClientScript.GetCallbackEventReference(this, "GetContextMenuParameter('nodeMenu')", "CopyMoveItem", "'" + Action.LinkDoc + "'");
        string script = "function CopyMoveItem(content, context) { \n" +
                              "    var arr = content.split('" + separator + "'); \n" +
                              "    if (context == '" + Action.Copy + "') { \n" +
                              "        modalDialog(arr[0], 'contentselectnode', '90%', '85%'); \n" +
                              "    } else if (context == '" + Action.Move + "') { \n" +
                              "        modalDialog(arr[1], 'contentselectnode', '90%', '85%'); \n" +
                              "    } else if (context == '" + Action.LinkDoc + "') { \n" +
                              "        modalDialog(arr[2], 'contentselectnode', '90%', '85%'); \n" +
                              "    } }";
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "SetCopyMoveUrl", ScriptHelper.GetScript(script));

        menuNew.LoadingContent = "<div class=\"TreeContextMenu TreeNewContextMenu\"><div class=\"ItemPadding\">" + GetString("ContextMenu.Loading") + "</div></div>";

        // Check license for Content personalization
        pnlUICPVariants.Visible = (DataHelper.GetNotEmpty(URLHelper.GetCurrentDomain(), string.Empty) != string.Empty)
            && LicenseHelper.IsFeatureAvailableInUI(FeatureEnum.ContentPersonalization, ModuleEntry.ONLINEMARKETING)
            && ResourceSiteInfoProvider.IsResourceOnSite("CMS.ContentPersonalization", CMSContext.CurrentSiteName);

        // Main menu
        imgNew.ImageUrl = GetImageUrl("CMSModules/CMS_Content/ContextMenu/New.png");
        lblNew.Text = GetString("ContentMenu.ContextIconNew");
        pnlNew.Attributes.Add("onclick", "NewItem(GetContextMenuParameter('nodeMenu'), 0, true);");

        imgDelete.ImageUrl = GetImageUrl("CMSModules/CMS_Content/ContextMenu/Delete.png");
        lblDelete.Text = GetString("general.delete");
        pnlDelete.Attributes.Add("onclick", "DeleteItem(GetContextMenuParameter('nodeMenu'), true);");

        imgCopy.ImageUrl = GetImageUrl("CMSModules/CMS_Content/ContextMenu/Copy.png");
        lblCopy.Text = GetString("ContentMenu.IconCopy");
        pnlCopy.Attributes.Add("onclick", copyRef);

        imgMove.ImageUrl = GetImageUrl("CMSModules/CMS_Content/ContextMenu/Move.png");
        lblMove.Text = GetString("ContentMenu.IconMove");
        pnlMove.Attributes.Add("onclick", moveRef);

        imgUp.ImageUrl = GetImageUrl("CMSModules/CMS_Content/ContextMenu/Up.png");
        lblUp.Text = GetString("ContentMenu.ContextIconMoveUp");
        pnlUp.Attributes.Add("onclick", "MoveUp(GetContextMenuParameter('nodeMenu'));");

        imgDown.ImageUrl = GetImageUrl("CMSModules/CMS_Content/ContextMenu/Down.png");
        lblDown.Text = GetString("ContentMenu.ContextIconMoveDown");
        lblDown.ToolTip = GetString("ContentMenu.MoveDown");
        pnlDown.Attributes.Add("onclick", "MoveDown(GetContextMenuParameter('nodeMenu'));");

        imgSort.ImageUrl = GetImageUrl("CMSModules/CMS_Content/ContextMenu/Sort.png");
        lblSort.Text = GetString("ContentMenu.IconSort");
        imgProperties.ImageUrl = GetImageUrl("CMSModules/CMS_Content/ContextMenu/Properties.png");
        lblProperties.Text = GetString("ContentMenu.IconProperties");
        pnlProperties.Attributes.Add("onclick", "Properties(GetContextMenuParameter('nodeMenu'), 'general');");

        // Refresh subsection
        imgRefresh.ImageUrl = GetImageUrl("CMSModules/CMS_Content/ContextMenu/Refresh.png");
        lblRefresh.Text = GetString("ContentMenu.IconRefresh");
        pnlRefresh.Attributes.Add("onclick", "RefreshTree(GetContextMenuParameter('nodeMenu'),null);");

        // Properties menu
        lblGeneral.Text = GetString("general.general");
        pnlGeneral.Attributes.Add("onclick", "Properties(GetContextMenuParameter('nodeMenu'), 'general');");

        lblUrls.Text = GetString("PropertiesMenu.iconurls");
        pnlUrls.Attributes.Add("onclick", "Properties(GetContextMenuParameter('nodeMenu'), 'urls');");

        lblTemplate.Text = GetString("PropertiesMenu.IconTemplate");
        pnlTemplate.Attributes.Add("onclick", "Properties(GetContextMenuParameter('nodeMenu'), 'template');");

        lblMetadata.Text = GetString("PropertiesMenu.IconMetadata");
        pnlMetadata.Attributes.Add("onclick", "Properties(GetContextMenuParameter('nodeMenu'), 'metadata');");

        lblCategories.Text = GetString("PropertiesMenu.iconcategories");
        pnlCategories.Attributes.Add("onclick", "Properties(GetContextMenuParameter('nodeMenu'), 'categories');");

        lblMenu.Text = GetString("PropertiesMenu.IconMenu");
        pnlMenu.Attributes.Add("onclick", "Properties(GetContextMenuParameter('nodeMenu'), 'menu');");

        lblWorkflow.Text = GetString("PropertiesMenu.IconWorkflow");
        pnlWorkflow.Attributes.Add("onclick", "Properties(GetContextMenuParameter('nodeMenu'), 'workflow');");

        lblVersions.Text = GetString("PropertiesMenu.IconVersions");
        pnlVersions.Attributes.Add("onclick", "Properties(GetContextMenuParameter('nodeMenu'), 'versions');");

        lblRelated.Text = GetString("PropertiesMenu.IconRelated");
        pnlRelated.Attributes.Add("onclick", "Properties(GetContextMenuParameter('nodeMenu'), 'relateddocs');");

        lblLinked.Text = GetString("PropertiesMenu.IconLinked");
        pnlLinked.Attributes.Add("onclick", "Properties(GetContextMenuParameter('nodeMenu'), 'linkeddocs');");

        lblSecurity.Text = GetString("PropertiesMenu.IconSecurity");
        pnlSecurity.Attributes.Add("onclick", "Properties(GetContextMenuParameter('nodeMenu'), 'security');");

        lblAttachments.Text = GetString("PropertiesMenu.IconAttachments");
        pnlAttachments.Attributes.Add("onclick", "Properties(GetContextMenuParameter('nodeMenu'), 'attachments');");

        lblLanguages.Text = GetString("PropertiesMenu.IconLanguages");
        pnlLanguages.Attributes.Add("onclick", "Properties(GetContextMenuParameter('nodeMenu'), 'languages');");

        pnlCPVariants.Attributes.Add("onclick", "Properties(GetContextMenuParameter('nodeMenu'), 'variants');");

        // Sort menu
        lblAlphaAsc.Text = GetString("SortMenu.IconAlphaAsc");
        pnlAlphaAsc.Attributes.Add("onclick", "SortAlphaAsc(GetContextMenuParameter('nodeMenu'));");

        lblAlphaDesc.Text = GetString("SortMenu.IconAlphaDesc");
        pnlAlphaDesc.Attributes.Add("onclick", "SortAlphaDesc(GetContextMenuParameter('nodeMenu'));");

        lblDateAsc.Text = GetString("SortMenu.IconDateAsc");
        pnlDateAsc.Attributes.Add("onclick", "SortDateAsc(GetContextMenuParameter('nodeMenu'));");

        lblDateDesc.Text = GetString("SortMenu.IconDateDesc");
        pnlDateDesc.Attributes.Add("onclick", "SortDateDesc(GetContextMenuParameter('nodeMenu'));");

        menuNew.OnReloadData += menuNew_OnReloadData;
        repNew.ItemDataBound += repNew_ItemDataBound;

        // Up menu
        lblTop.Text = GetString("UpMenu.IconTop");
        pnlTop.Attributes.Add("onclick", "MoveTop(GetContextMenuParameter('nodeMenu'));");

        // Down menu
        lblBottom.Text = GetString("DownMenu.IconBottom");
        pnlBottom.Attributes.Add("onclick", "MoveBottom(GetContextMenuParameter('nodeMenu'));");

        // New menu
        imgNewLinked.ImageUrl = GetImageUrl("CMSModules/CMS_Content/ContextMenu/New/Link.png");
        lblNewLinked.Text = GetString("contentnew.newlink");
        lblNewLinked.Attributes.Add("onclick", linkRef);
    }


    protected void repNew_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Panel pnlItem = (Panel)e.Item.FindControl("pnlItem");
        if (pnlItem != null)
        {
            int count = ValidationHelper.GetInteger(((DataRowView)e.Item.DataItem)["Count"], 0) - 1;
            if (e.Item.ItemIndex == count)
            {
                pnlItem.CssClass = "ItemLast";
            }

            pnlItem.Attributes.Add("onclick", "NewItem(GetContextMenuParameter('nodeMenu'), " + ((DataRowView)e.Item.DataItem)["ClassID"] + ", true);");
        }
    }


    protected void menuNew_OnReloadData(object sender, EventArgs e)
    {
        int nodeId = ValidationHelper.GetInteger(menuNew.Parameter, 0);

        // Get the node
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);
        TreeNode node = tree.SelectSingleNode(nodeId);

        pnlNewVariant.Visible = false;

        if (node != null)
        {
            CurrentUserInfo curUser = CMSContext.CurrentUser;
            if (!curUser.IsAuthorizedPerUIElement("CMS.Content", "New"))
            {
                DisplayErrorMessage(String.Format(GetString("CMSSiteManager.AccessDeniedOnUIElementName"), "New"));
                return;
            }

            if (curUser.IsAuthorizedToCreateNewDocument(node, null))
            {
                // Check user permissions for "Create" permission
                bool hasNodeAllowCreate = (curUser.IsAuthorizedPerTreeNode(node, NodePermissionsEnum.Create) == AuthorizationResultEnum.Allowed);
                bool isAuthorizedToCreateInContent = curUser.IsAuthorizedPerResource("CMS.Content", "Create");

                // AB test variant settings
                if (SettingsKeyProvider.GetBoolValue(CMSContext.CurrentSiteName + ".CMSABTestingEnabled")
                    && curUser.IsAuthorizedPerResource("cms.ABTest", "Read")
                    && ModuleEntry.IsModuleLoaded("cms.onlinemarketing")
                    && (node.NodeAliasPath != "/"))
                {
                    if (isAuthorizedToCreateInContent || curUser.IsAuthorizedPerClassName(node.NodeClassName, "Create") || (curUser.IsAuthorizedPerClassName(node.NodeClassName, "CreateSpecific") && hasNodeAllowCreate))
                    {
                        pnlNewVariant.Visible = true;
                        imgNewVariant.ImageUrl = GetImageUrl("objects/cms_variant/object_small.png");
                        lblNewVariant.Text = GetString("abtesting.abtestvariant");
                        lblNewVariant.Attributes.Add("onclick", "NewVariant(GetContextMenuParameter('nodeMenu'), true);");
                        if (!imgNewLinked.Visible)
                        {
                            pnlNewVariantSeparator.Visible = true;
                        }
                    }
                }

                string where = "ClassID IN (SELECT ChildClassID FROM CMS_AllowedChildClasses WHERE ParentClassID=" + ValidationHelper.GetInteger(node.GetValue("NodeClassID"), 0) + ") " +
                    "AND ClassID IN (SELECT ClassID FROM CMS_ClassSite WHERE SiteID = " + CMSContext.CurrentSiteID + ")";

                // Get the allowed child classes
                DataSet ds = DataClassInfoProvider.GetClasses("ClassID, ClassName, ClassDisplayName, (CASE ClassName WHEN 'CMS.MenuItem' THEN 0 ELSE 1 END) AS MenuItemOrder", where, null, 50);

                DataTable resultTable = null;
                ArrayList rows = new ArrayList();

                if (!DataHelper.DataSourceIsEmpty(ds))
                {
                    DataTable table = ds.Tables[0];
                    table.DefaultView.Sort = "MenuItemOrder, ClassDisplayName";
                    resultTable = table.DefaultView.ToTable();

                    for (int i = 0; i < resultTable.Rows.Count; ++i)
                    {
                        DataRow dr = resultTable.Rows[i];
                        string doc = ValidationHelper.GetString(DataHelper.GetDataRowValue(dr, "ClassName"), "");

                        // Document type is not allowed, remove it from the data set
                        if (!isAuthorizedToCreateInContent && !curUser.IsAuthorizedPerClassName(doc, "Create") && (!curUser.IsAuthorizedPerClassName(doc, "CreateSpecific") || !hasNodeAllowCreate))
                        {
                            rows.Add(dr);
                        }
                    }

                    // Remove the document types
                    foreach (DataRow dr in rows)
                    {
                        resultTable.Rows.Remove(dr);
                    }

                    bool classesRemoved = false;

                    // Leave only first 15 rows
                    while (resultTable.Rows.Count > 15)
                    {
                        resultTable.Rows.RemoveAt(resultTable.Rows.Count - 1);
                        classesRemoved = true;
                    }

                    if (!DataHelper.DataSourceIsEmpty(resultTable))
                    {
                        // Add show more item
                        if (classesRemoved)
                        {
                            DataRow dr = resultTable.NewRow();
                            dr["ClassID"] = 0;
                            dr["ClassName"] = "more";
                            dr["ClassDisplayName"] = GetString("class.showmore");
                            resultTable.Rows.InsertAt(dr, resultTable.Rows.Count);
                        }

                        // Create temp column
                        int rowCount = resultTable.Rows.Count;
                        DataColumn tmpColumn = new DataColumn("Count");
                        tmpColumn.DefaultValue = rowCount;
                        resultTable.Columns.Add(tmpColumn);
                    }
                    else
                    {
                        DisplayErrorMessage("Content.NoPermissions");
                    }
                }
                else
                {
                    pnlNewVariantSeparator.Visible = true;
                    DisplayErrorMessage("NewMenu.NoChildAllowed");
                }

                repNew.DataSource = resultTable;
                repNew.DataBind();

                if (DataHelper.DataSourceIsEmpty(ds))
                {
                    DisplayErrorMessage("NewMenu.NoChildAllowed");
                }
            }
            else
            {
                DisplayErrorMessage("Content.NoPermissions");
            }
        }
    }


    /// <summary>
    /// Displays error message (if any permission is not present)
    /// </summary>
    /// <param name="message">Message to display</param>
    private void DisplayErrorMessage(String message)
    {
        pnlNewLinked.Visible = false;
        pnlSepNewLinked.Visible = false;
        pnlNoChild.Visible = true;
        ltlNoChild.Text = GetString(message);
    }


    #region "Dialog handling"

    /// <summary>
    /// Returns Correct URL of the copy or move dialog.
    /// </summary>
    /// <param name="nodeId">ID Of the node to be copied or moved</param>
    /// <param name="CurrentAction">Action which should be performed</param>
    private string GetDialogUrl(int nodeId, Action CurrentAction)
    {
        Config.CustomFormatCode = CurrentAction.ToString().ToLower();
        string url = CMSDialogHelper.GetDialogUrl(Config, false, false, null, false);

        url = URLHelper.RemoveParameterFromUrl(url, "hash");
        url = URLHelper.AddParameterToUrl(url, "sourcenodeids", nodeId.ToString());
        url = URLHelper.AddParameterToUrl(url, "hash", QueryHelper.GetHash(url));
        url = URLHelper.EncodeQueryString(url);

        return url;
    }

    #endregion


    #region "Callback handling"

    string mCallbackResult = string.Empty;

    /// <summary>
    /// Raises the callback event.
    /// </summary>
    /// <param name="eventArgument">Event argument</param>
    public void RaiseCallbackEvent(string eventArgument)
    {
        int nodeId = ValidationHelper.GetInteger(eventArgument, 0);

        string copyUrl = GetDialogUrl(nodeId, Action.Copy);
        string moveUrl = GetDialogUrl(nodeId, Action.Move);
        string linkUrl = GetDialogUrl(nodeId, Action.LinkDoc);

        mCallbackResult = copyUrl + separator + moveUrl + separator + linkUrl;
    }


    /// <summary>
    /// Returns the result of a callback.
    /// </summary>
    public string GetCallbackResult()
    {
        return mCallbackResult;
    }

    #endregion
}

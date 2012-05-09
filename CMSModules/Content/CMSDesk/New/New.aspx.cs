using System;
using System.Data;
using System.Collections;

using CMS.ExtendedControls;
using CMS.GlobalHelper;
using CMS.TreeEngine;
using CMS.SettingsProvider;
using CMS.CMSHelper;
using CMS.UIControls;

public partial class CMSModules_Content_CMSDesk_New_New : CMSContentPage
{
    #region "Variables"

    private int nodeId = 0;
    private DataSet dsClasses = null;
    private TreeProvider tree = null;
    private TreeNode node = null;
    private DialogConfiguration mConfig = null;

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


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentUserInfo cui = CMSContext.CurrentUser;

        // Check UI element "New"
        if (!cui.IsAuthorizedPerUIElement("CMS.Content", "New"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Content", "New");
        }

        // Register progrees script
        ScriptHelper.RegisterProgress(this);
        ScriptHelper.RegisterDialogScript(this);
        ScriptHelper.RegisterScriptFile(this, @"~/CMSModules/Content/CMSDesk/New/New.js");

        // Current Node ID
        nodeId = QueryHelper.GetInteger("nodeid", 0);

        // Setup unigrid
        gridClasses.GridView.ShowHeader = false;
        gridClasses.GridView.BorderWidth = 0;
        gridClasses.OnExternalDataBound += gridClasses_OnExternalDataBound;
        gridClasses.OnBeforeDataReload += new OnBeforeDataReload(gridClasses_OnBeforeDataReload);

        // Setup page title text and image
        CurrentMaster.Title.TitleText = GetString("Content.NewTitle");
        CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_Content/Menu/New.png");

        lblNewLink.Text = GetString("ContentNew.NewLink");

        lnkNewLink.NavigateUrl = "javascript:modalDialog('" + GetLinkDialogUrl(nodeId) +
                                 "', 'contentselectnode', '90%', '85%')";

        imgNewLink.ImageUrl = GetImageUrl("CMSModules/CMS_Content/Menu/Link.png");

        // Get the node
        tree = new TreeProvider(cui);
        node = tree.SelectSingleNode(nodeId);
        plcNewABTestVariant.Visible = false;

        if (node != null)
        {
            // AB test variant settings
            if (SettingsKeyProvider.GetBoolValue(CMSContext.CurrentSiteName + ".CMSABTestingEnabled")
                && cui.IsAuthorizedPerResource("cms.ABTest", "Read")
                && ModuleEntry.IsModuleLoaded("cms.onlinemarketing")
                && (node.NodeAliasPath != "/"))
            {
                plcNewABTestVariant.Visible = true;
                lblNewVariant.Text = GetString("abtesting.abtestvariant");
                lnkNewVariant.NavigateUrl = "~/CMSModules/Content/CMSDesk/Edit/EditFrameset.aspx?action=newvariant&nodeid=" + nodeId;

                if (pnlFooter.Visible == false)
                {
                    pnlABVariant.CssClass += "PageSeparator";
                }
            }
        }

        imgNewVariant.ImageUrl = GetImageUrl("Objects/CMS_Variant/object_small.png");
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        gridClasses.GridView.Columns[1].Visible = false;
        gridClasses.GridView.GridLines = System.Web.UI.WebControls.GridLines.None;
    }

    protected void gridClasses_OnBeforeDataReload()
    {
        if (node != null)
        {
            CurrentUserInfo curUser = CMSContext.CurrentUser;

            // Check permission to create new document
            if (curUser.IsAuthorizedToCreateNewDocument(node, null))
            {
                // Prepare where condition
                string where = "ClassID IN (SELECT ChildClassID FROM CMS_AllowedChildClasses WHERE ParentClassID=" + ValidationHelper.GetInteger(node.GetValue("NodeClassID"), 0) + ") " +
                    "AND ClassID IN (SELECT ClassID FROM CMS_ClassSite WHERE SiteID = " + CMSContext.CurrentSiteID + ")";

                if (!String.IsNullOrEmpty(gridClasses.CompleteWhereCondition))
                {
                    where += " AND (" + gridClasses.CompleteWhereCondition + ")";
                }

                // Get the allowed child classes
                DataSet ds = DataClassInfoProvider.GetClasses("ClassID, ClassName, ClassDisplayName", where, null, gridClasses.TopN);

                DataRow menuItemRow = null;
                DataTable resultTable = new DataTable();

                // Check user permissions for "Create" permission
                bool hasNodeAllowCreate = (curUser.IsAuthorizedPerTreeNode(node, NodePermissionsEnum.Create) == AuthorizationResultEnum.Allowed);
                bool isAuthorizedToCreateInContent = curUser.IsAuthorizedPerResource("CMS.Content", "Create");

                // If dataSet is not empty
                if (!DataHelper.DataSourceIsEmpty(ds))
                {
                    ArrayList rows = new ArrayList();
                    DataTable table = ds.Tables[0];
                    table.DefaultView.Sort = "ClassDisplayName";
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
                        else
                        {
                            if (doc.ToLower() == "cms.menuitem")
                            {
                                menuItemRow = dr;
                            }
                        }
                    }

                    // Remove the document types
                    foreach (DataRow dr in rows)
                    {
                        resultTable.Rows.Remove(dr);
                    }

                    if (!DataHelper.DataSourceIsEmpty(resultTable))
                    {
                        // Put Menu item to first position
                        if (menuItemRow != null)
                        {
                            DataRow dr = resultTable.NewRow();
                            dr.ItemArray = menuItemRow.ItemArray;

                            resultTable.Rows.Remove(menuItemRow);
                            resultTable.Rows.InsertAt(dr, 0);
                        }
                    }
                    else
                    {
                        // Show error message
                        lblError.Visible = true;
                        lblError.Text = GetString("Content.NoPermissions");
                        lblInfo.Visible = false;
                        pnlFooter.Visible = false;
                        pnlABVariant.Visible = false;
                    }
                }
                else
                {
                    // Show error message
                    lblError.Visible = true;
                    lblError.Text = GetString("Content.NoAllowedChildDocuments");
                    lblInfo.Visible = false;
                    pnlFooter.Visible = false;
                    pnlABVariant.Visible = false;
                }

                dsClasses = new DataSet();
                dsClasses.Tables.Add(resultTable);

                gridClasses.DataSource = dsClasses;
            }
            else
            {
                // Show error message
                lblError.Visible = true;
                lblError.Text = GetString("Content.NoPermissions");
                lblInfo.Visible = false;
                pnlFooter.Visible = false;
                pnlABVariant.Visible = false;
            }

            lblInfo.Text = GetString("Content.NewInfo");
        }
        gridClasses.DataSource = dsClasses;
    }


    protected object gridClasses_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        if (sourceName.ToLower() == "classname")
        {
            DataRowView drv = (DataRowView)parameter;

            // Get properties
            string className = ValidationHelper.GetString(drv["ClassName"], "");
            string classDisplayName = ResHelper.LocalizeString(CMSContext.ResolveMacros(ValidationHelper.GetString(drv["ClassDisplayName"], "")));
            string classId = ValidationHelper.GetString(drv["ClassId"], "");

            // Format items to output
            return "<a class=\"ContentNewClass\" href=\"../Edit/EditFrameset.aspx?action=new&nodeid=" + nodeId + "&classid=" + classId + "\">" +
            "<img style=\"border-right-width: 0px; border-top-width: 0px; border-bottom-width: 0px; border-left-width: 0px;\" " +
            "src=\"" + ResolveUrl(GetDocumentTypeIconUrl(className)) + "\" />" +
            HTMLHelper.HTMLEncode(classDisplayName) + "</a>" + GenerateSpaceAfter(className);
        }
        return HTMLHelper.HTMLEncode(parameter.ToString());
    }


    /// <summary>
    /// Generates empty line after menu item link.
    /// </summary>
    /// <param name="className">Class name</param>
    public string GenerateSpaceAfter(object className)
    {
        string classNameStr = ValidationHelper.GetString(className, "").ToLower();
        if (classNameStr == "cms.menuitem")
        {
            return "<br /><br />";
        }

        return string.Empty;
    }

    #endregion


    #region "Dialog handling"

    /// <summary>
    /// Returns Correct URL of the copy or move dialog.
    /// </summary>
    /// <param name="currentNodeId">ID Of the node to be copied or moved</param>
    private string GetLinkDialogUrl(int currentNodeId)
    {
        Config.CustomFormatCode = "linkdoc";
        string url = CMSDialogHelper.GetDialogUrl(Config, false, false, null, false);

        // Prepare url for link dialog
        url = URLHelper.RemoveParameterFromUrl(url, "hash");
        url = URLHelper.AddParameterToUrl(url, "sourcenodeids", currentNodeId.ToString());
        url = URLHelper.AddParameterToUrl(url, "hash", QueryHelper.GetHash(url));
        url = URLHelper.EncodeQueryString(url);

        return url;
    }

    #endregion
}
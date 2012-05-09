using System;
using System.Collections;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.CMSImportExport;
using CMS.SettingsProvider;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.EventLog;
using CMS.IO;
using System.Data;

public partial class CMSModules_ImportExport_Controls_ImportPanel : CMSUserControl
{
    #region "Variables"

    // Position of the tree scrollbar
    protected string mScrollPosition = "0";

    // Additional settings control
    protected ImportExportControl settingsControl = null;

    private SiteImportSettings mSettings = null;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Import settings.
    /// </summary>
    public SiteImportSettings Settings
    {
        get
        {
            return mSettings;
        }
        set
        {
            mSettings = value;
            gvObjects.Settings = value;
            //gvTasks.Settings = value;
        }
    }


    /// <summary>
    /// Selected  node value.
    /// </summary>
    public string SelectedNodeValue
    {
        get
        {
            return ValidationHelper.GetString(ViewState["SelectedNodeValue"], CMSObjectHelper.GROUP_OBJECTS);
        }
        set
        {
            ViewState["SelectedNodeValue"] = value;
        }
    }


    /// <summary>
    /// If true, node is site node.
    /// </summary>
    public bool SiteNode
    {
        get
        {
            return ValidationHelper.GetBoolean(ViewState["SiteNode"], false);
        }
        set
        {
            ViewState["SiteNode"] = value;
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!StopProcessing)
        {
            if (!RequestHelper.IsCallback())
            {
                objectTree.TreeView.SelectedNodeChanged += treeElem_SelectedNodeChanged;
                objectTree.OnBeforeCreateNode += treeElem_BeforeCreateNode;
                objectTree.TreeView.ExpandImageToolTip = GetString("contenttree.expand");
                objectTree.TreeView.CollapseImageToolTip = GetString("contenttree.collapse");
                objectTree.TreeView.NodeStyle.CssClass = "ContentTreeItem";
                objectTree.TreeView.SelectedNodeStyle.CssClass = "ContentTreeSelectedItem";

                objectTree.NodeTextTemplate = "<span class=\"Name\">##NODENAME##</span>";
                objectTree.SelectedNodeTextTemplate = "<span class=\"Name\">##NODENAME##</span>";
                objectTree.ValueTextTemplate = "##OBJECTTYPE##";

                objectTree.PreselectObjectType = SelectedNodeValue;
                objectTree.IsPreselectedObjectTypeSiteObject = SiteNode;

                if (Settings != null)
                {
                    if (SelectedNodeValue != CMS.TreeEngine.TreeObjectType.DOCUMENT)
                    {
                        // Bind grid view
                        gvObjects.Visible = true;
                        gvObjects.ObjectType = SelectedNodeValue;
                        gvObjects.Settings = Settings;
                        gvObjects.SiteObject = SiteNode;

                        gvTasks.Visible = true;
                        gvTasks.ObjectType = SelectedNodeValue;
                        gvTasks.Settings = Settings;
                        gvTasks.SiteObject = SiteNode;
                    }
                    else
                    {
                        gvObjects.Visible = false;
                        gvTasks.Visible = false;
                    }
                }

                // Reload data
                ReloadData(false);

                // Load settings control
                LoadSettingsControl();

                // Load context help
                LoadContextHelp();
            }
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (!RequestHelper.IsCallback())
        {
            if (Settings != null)
            {
                if (SelectedNodeValue != CMS.TreeEngine.TreeObjectType.DOCUMENT)
                {
                    DataSet ds = ImportProvider.LoadObjects(Settings, SelectedNodeValue, SiteNode, true);

                    // Bind grid view
                    gvObjects.Visible = true;
                    gvObjects.ObjectType = SelectedNodeValue;
                    gvObjects.Settings = Settings;
                    gvObjects.SiteObject = SiteNode;

                    gvObjects.DataSource = ds;
                    gvObjects.Bind();

                    gvTasks.Visible = true;
                    gvTasks.ObjectType = SelectedNodeValue;
                    gvTasks.Settings = Settings;
                    gvTasks.SiteObject = SiteNode;

                    gvTasks.DataSource = ds;
                    gvTasks.Bind();
                }
                else
                {
                    gvObjects.Visible = false;
                    gvTasks.Visible = false;
                }

                // Reload settings control
                if (settingsControl != null)
                {
                    settingsControl.ReloadData();
                }
            }

            if (SelectedNodeValue != "")
            {
                if (SelectedNodeValue.StartsWith("##"))
                {
                    titleElem.TitleText = GetString("ImportTasks." + SelectedNodeValue.Replace(".", "_").Replace("#", "_"));
                }
                else
                {
                    titleElem.TitleText = GetString("ObjectTasks." + SelectedNodeValue.Replace(".", "_").Replace("#", "_"));
                }
                titleElem.TitleImage = GetImageUrl("Objects/" + SelectedNodeValue.Replace(".", "_").Replace("#", "_") + "/object.png");
            }
            else
            {
                titleElem.TitleText = "";
            }

            pnlError.Visible = (lblError.Text != "");

            // Save scrollbar position
            mScrollPosition = ValidationHelper.GetString(Page.Request.Params["hdnDivScrollBar"], "0");
        }
    }

    #endregion


    private void LoadContextHelp()
    {
        titleElem.HelpName = "site_import";
        // Special context help for special control
        if (SelectedNodeValue == CMSObjectHelper.GROUP_OBJECTS)
        {
            titleElem.HelpTopicName = "importGridObjects";
        }
        else if (plcControl.Visible)
        {
            titleElem.HelpTopicName = "importGrid_" + ImportExportHelper.GetSafeObjectTypeName(SelectedNodeValue);
        }
        else
        {
            titleElem.HelpTopicName = "importGrid";
        }
    }


    private void LoadSettingsControl()
    {
        try
        {
            if (Settings != null)
            {
                plcControl.Controls.Clear();
                plcControl.Visible = false;
                settingsControl = null;

                if (!string.IsNullOrEmpty(SelectedNodeValue))
                {
                    string virtualPath = "~/CMSModules/ImportExport/Controls/Import/" + (SiteNode ? "Site/" : "") + ImportExportHelper.GetSafeObjectTypeName(SelectedNodeValue) + ".ascx";
                    string filePath = Server.MapPath(virtualPath);

                    if (File.Exists(filePath))
                    {
                        // Load control
                        settingsControl = (ImportExportControl)Page.LoadControl(virtualPath);
                        settingsControl.EnableViewState = true;
                        settingsControl.ID = "settingControl";
                        settingsControl.Settings = Settings;

                        if (settingsControl.Visible)
                        {
                            plcControl.Controls.Add(settingsControl);
                            plcControl.Visible = true;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblError.Text = "[ImportPanel.LoadSettingsControl]: Error loading settings control for object type '" + SelectedNodeValue + "'. " + EventLogProvider.GetExceptionLogMessage(ex);
        }
    }


    public void ReloadData(bool forceReload)
    {
        if (Settings != null)
        {
            // Genearate items of the tree
            GenerateTreeItems(forceReload);

            if (!objectTree.ContainsObjectType(SelectedNodeValue))
            {
                SelectedNodeValue = "##OBJECTS##";
            }

            if (forceReload)
            {
                gvObjects.Settings = Settings;
                gvObjects.Bind();
                LoadSettingsControl();
            }
        }
    }


    public bool ApplySettings()
    {
        // Save last selection
        SaveSelection();

        // Check if any object is selected
        if (Settings.IsEmptySelection())
        {
            lblError.Text = GetString("ImportPanel.NoObjectSelected");
            return false;
        }

        return true;
    }


    private void SaveSelection()
    {
        // Save additional settings
        if (settingsControl != null)
        {
            settingsControl.SaveSettings();
        }
    }


    // Handle node selection
    protected void treeElem_SelectedNodeChanged(object sender, EventArgs e)
    {
        // Save selected objects
        SaveSelection();

        SelectedNodeValue = objectTree.TreeView.SelectedValue;
        SiteNode = IsSiteNode(objectTree.TreeView.SelectedNode);

        // Load settings control
        LoadSettingsControl();

        // Load context help
        LoadContextHelp();

        // Reset the pagers
        gvObjects.PagerControl.Reset();
        gvTasks.PagerControl.Reset();
    }


    /// <summary>
    /// Checks if the node can be created.
    /// </summary>
    /// <param name="node">Tree node</param>
    protected bool treeElem_BeforeCreateNode(ObjectTypeTreeNode node)
    {
        string objectType = node.ObjectType;
        if (string.IsNullOrEmpty(objectType))
        {
            return true;
        }

        return Settings.IsIncluded(objectType, node.Site);
    }


    /// <summary>
    /// Returns true if the node is site node.
    /// </summary>
    /// <param name="node">Node</param>
    public bool IsSiteNode(TreeNode node)
    {
        if ((node == null) || (node.Parent == null))
        {
            return false;
        }
        else if (node.Value == "##SITE##")
        {
            return true;
        }
        else
        {
            return IsSiteNode(node.Parent);
        }
    }


    /// <summary>
    /// Genearate items of the tree.
    /// </summary>
    /// <param name="forceReload">If true, tree is forced to reload</param>
    private void GenerateTreeItems(bool forceReload)
    {
        if ((Settings == null) || ((objectTree.TreeView.Nodes.Count > 0) && !forceReload))
        {
            return;
        }

        // Display site objects only if site id is set
        if (Settings.TemporaryFilesCreated)
        {
            int siteId = 0;
            if (Settings.SiteIsIncluded)
            {
                siteId = 1;
            }

            objectTree.RootNode = ImportExportHelper.ObjectTree;
            objectTree.SiteID = siteId;
            objectTree.ReloadData();
        }
    }


    /// <summary>
    /// Returns true if any of the types is included in the package.
    /// </summary>
    public bool IsAnyIncluded(string objectTypes, bool siteObjects)
    {
        ArrayList types = CMSObjectHelper.GetTypes(objectTypes);
        foreach (string type in types)
        {
            if (Settings.IsIncluded(type, siteObjects))
            {
                return true;
            }
        }

        return false;
    }
}

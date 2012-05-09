using System;
using System.Data;

using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.PortalEngine;
using CMS.TreeEngine;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.SettingsProvider;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_ImportExport_Controls_SelectMasterTemplate : CMSUserControl
{
    /// <summary>
    /// Template ID.
    /// </summary>
    public int MasterTemplateId
    {
        get
        {
            return ucSelector.SelectedId;
        }
    }


    /// <summary>
    /// Site name.
    /// </summary>
    public string SiteName
    {
        get
        {
            return ValidationHelper.GetString(ViewState["SiteName"], "");
        }
        set
        {
            ViewState["SiteName"] = value;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!StopProcessing)
        {
            ReloadData();
        }
    }


    public void ReloadData()
    {
        // Load the data
        DataSet templates = PageTemplateInfoProvider.GetAllMasterTemplates();

        ucSelector.DataSource = templates;
        ucSelector.IDColumn = "PageTemplateID";
        ucSelector.DisplayNameColumn = "PageTemplateDisplayName";
        ucSelector.DescriptionColumn = "PageTemplateDescription";
        ucSelector.ObjectType = PortalObjectType.PAGETEMPLATE;
        ucSelector.DataBind();

        if (ucSelector.SelectedId == 0)
        {
            if (!DataHelper.DataSourceIsEmpty(templates))
            {
                int firstTemplateId = ValidationHelper.GetInteger(templates.Tables[0].Rows[0]["PageTemplateID"], 0);
                ucSelector.SelectedId = firstTemplateId;
            }
        }
    }


    /// <summary>
    /// Apply control settings.
    /// </summary>
    public bool ApplySettings()
    {
        if (MasterTemplateId <= 0)
        {
            lblError.Text = GetString("TemplateSelection.SelectTemplate");
            return false;
        }
        else
        {
            // Update all culture versions
            TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);
            DataSet ds = tree.SelectNodes(SiteName, "/", TreeProvider.ALL_CULTURES, false, "CMS.Root", null, null, -1, false);
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    // Update the document
                    TreeNode node = TreeNode.New(dr, "CMS.Root", tree);
                    node.DocumentPageTemplateID = MasterTemplateId;
                    node.Update();

                    // Update search index for node
                    if ((node.PublishedVersionExists) && (SearchIndexInfoProvider.SearchEnabled))
                    {
                        SearchTaskInfoProvider.CreateTask(SearchTaskTypeEnum.Update, PredefinedObjectType.DOCUMENT, SearchHelper.ID_FIELD, node.GetSearchID());
                    }
                }
            }
        }

        return true;
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        lblError.Visible = (lblError.Text != "");
    }
}
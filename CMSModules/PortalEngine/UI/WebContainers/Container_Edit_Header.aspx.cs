using System;

using CMS.PortalEngine;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.IO;

// Set edited object
[EditedObject("cms.webpartcontainer", "containerid")]

public partial class CMSModules_PortalEngine_UI_WebContainers_Container_Edit_Header : CMSModalDesignPage
{
    #region "Variables"

    private WebPartContainerInfo mWebPartContainer = null;
    private bool mDialogMode = false;

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets edited WebPartContainer object.
    /// </summary>
    private WebPartContainerInfo WebPartContainer
    {
        get
        {
            if (mWebPartContainer == null)
            {
                int containerId = QueryHelper.GetInteger("containerid", 0);
                if (containerId > 0)
                {
                    mWebPartContainer = WebPartContainerInfoProvider.GetWebPartContainerInfo(containerId);
                }

                if (mWebPartContainer == null)
                {
                    string containerName = QueryHelper.GetString("name", string.Empty);
                    if (!string.IsNullOrEmpty(containerName))
                    {
                        mWebPartContainer = WebPartContainerInfoProvider.GetWebPartContainerInfo(containerName);
                    }
                }
            }
            return mWebPartContainer;
        }
        set
        {
            mWebPartContainer = value;
        }
    }

    #endregion


    #region "Page methods"

    protected override void OnPreInit(EventArgs e)
    {
        mDialogMode = QueryHelper.GetBoolean("editonlycode", false);
        RequireSite = false;

        if (mDialogMode)
        {
            MasterPageFile = "~/CMSMasterPages/UI/Dialogs/TabsHeader.master";
        }
        else
        {
            // Page opened from Site Manager
            CheckAccessToSiteManager();
        }

        base.OnPreInit(e);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Initialize container object
        if (PersistentEditedObject == null)
        {
            PersistentEditedObject = WebPartContainer;
        }

        WebPartContainer = PersistentEditedObject as WebPartContainerInfo;

        // Initialize master page elements
        InitializeMasterPage();
    }


    /// <summary>
    /// Initializes the master page elements.
    /// </summary>
    private void InitializeMasterPage()
    {
        if (mDialogMode)
        {
            SetTitle("Objects/CMS_WebPartContainer/object.png", GetString("Container_Edit.EditContainer"), "newedit_container", "helpTopic");

            string selector = QueryHelper.GetString("selectorid", string.Empty);
            if (!string.IsNullOrEmpty(selector) && RequestHelper.IsPostBack())
            {
                // Add selector refresh
                string script =
                    string.Format(@"var wopener = window.top.opener ? window.top.opener : window.top.dialogArguments;
		                                if (wopener && wopener.US_SelectNewValue_{0}) {{ wopener.US_SelectNewValue_{0}('{1}'); }}",
                                    selector, WebPartContainer.ContainerName);

                ScriptHelper.RegisterStartupScript(this, GetType(), "UpdateSelector", script, true);
            }
        }
        else
        {
            SetTitle("Objects/CMS_WebPartContainer/object.png", GetString("Container_Edit.HeaderCaption"), "newedit_container", "helpTopic");

            // Set breadcrumbs
            InitBreadcrumbs(2);
            SetBreadcrumb(0, GetString("Container_Edit.ItemListLink"), ResolveUrl("Container_List.aspx"), "_parent", null);
            SetBreadcrumb(1, WebPartContainer.ContainerDisplayName, null, null, null);
        }

        int i = 0;

        // Set tabs
        InitTabs(3, "content");
        string url = URLHelper.RemoveParameterFromUrl("Container_Edit_General.aspx" + URLHelper.Url.Query, "saved");
        
        if (mDialogMode)
        {
            url = URLHelper.AddParameterToUrl(url, "name", WebPartContainer.ContainerName);
        }
        else
        {
            url = URLHelper.AddParameterToUrl(url, "containerid", WebPartContainer.ContainerID.ToString());
        }

        SetTab(i++, GetString("General.General"), url, "SetHelpTopic('helpTopic', 'newedit_container');");

        if (!mDialogMode)
        {
            if (!StorageHelper.IsExternalStorage)
            {
                SetTab(i++, GetString("Stylesheet.Theme"), "Container_Edit_Theme.aspx?containerId=" + WebPartContainer.ContainerID, "SetHelpTopic('helpTopic', 'webpartcontainer_theme_tab');");
            }

            SetTab(i++, GetString("General.Sites"), "Container_Edit_Sites.aspx?containerId=" + WebPartContainer.ContainerID, "SetHelpTopic('helpTopic', 'webpartcontainer_sites_tab');");
        }
    }

    #endregion
}

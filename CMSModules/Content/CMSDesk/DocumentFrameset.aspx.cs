using System;
using System.Web;

using CMS.TreeEngine;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.SettingsProvider;
using CMS.UIControls;
using CMS.SiteProvider;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_Content_CMSDesk_DocumentFrameset : CMSContentPage
{
    #region "Constants"

    private const string VALIDATION_EXCLUDED_CLASS_NAMES = ";cms.file;";

    #endregion


    #region "Private & protected variables"

    protected string viewpage = "~/CMSModules/Content/CMSDesk/blank.htm";
    private int nodeId = 0;
    private TreeProvider mTree = null;
    private TreeNode mTreeNode = null;

    #endregion


    #region "Private properties"

    private TreeProvider Tree
    {
        get
        {
            return mTree ?? (mTree = new TreeProvider(CMSContext.CurrentUser));
        }
    }


    private TreeNode TreeNode
    {
        get
        {
            return mTreeNode;
        }
        set
        {
            mTreeNode = value;
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Register script files
        ltlScript.Text += ScriptHelper.GetIncludeScript("~/CMSModules/Content/CMSDesk/ContentEditFrameset.js");

        int classId = 0;
        bool checkCulture = false;
        bool splitViewSupported = false;
        nodeId = QueryHelper.GetInteger("nodeid", 0);
        string action = QueryHelper.GetString("action", "edit").ToLower();

        switch (action)
        {
            // New dialog / new page form
            case "new":
                classId = QueryHelper.GetInteger("classid", 0);
                if (classId <= 0)
                {
                    // Get by class name if specified
                    string className = QueryHelper.GetString("classname", string.Empty);
                    if (className != string.Empty)
                    {
                        DataClassInfo ci = DataClassInfoProvider.GetDataClass(className);
                        if (ci != null)
                        {
                            classId = ci.ClassID;
                        }
                    }
                }

                if (classId > 0)
                {
                    viewpage = "Edit/EditFrameset.aspx";

                    // Check if document type is allowed under parent node
                    if (nodeId > 0)
                    {
                        // Get the node
                        TreeNode = Tree.SelectSingleNode(nodeId, TreeProvider.ALL_CULTURES);
                        if (TreeNode != null)
                        {
                            if (!DataClassInfoProvider.IsChildClassAllowed(ValidationHelper.GetInteger(TreeNode.GetValue("NodeClassID"), 0), classId))
                            {
                                viewpage = "NotAllowed.aspx?action=child";
                            }
                        }
                    }
                }
                else
                {
                    viewpage = "New/new.aspx";
                }
                break;

            case "newvariant":
                viewpage = "Edit/EditFrameset.aspx";
                break;

            case "delete":
                // Delete dialog
                viewpage = "Delete.aspx";
                break;

            case "preview":
                checkCulture = true;

                string previewUrl = null;

                TreeNode = Tree.SelectSingleNode(nodeId, TreeProvider.ALL_CULTURES);
                if (TreeNode != null)
                {
                    classId = ValidationHelper.GetInteger(TreeNode.GetValue("NodeClassID"), 0);

                    DataClassInfo ci = DataClassInfoProvider.GetDataClass(classId);
                    if (ci != null)
                    {
                        if (!string.IsNullOrEmpty(ci.ClassPreviewPageUrl))
                        {
                            previewUrl = ci.ClassPreviewPageUrl;
                        }
                    }
                }

                // Preview mode
                if (CheckValidationVisibility(action, nodeId))
                {
                    if (String.IsNullOrEmpty(previewUrl))
                    {
                        previewUrl = "preview.aspx";
                    }
                    viewpage = URLHelper.AddParameterToUrl("View/ViewValidate.aspx", "viewpage", previewUrl);
                }
                else
                {
                    if (String.IsNullOrEmpty(previewUrl))
                    {
                        previewUrl = "View/preview.aspx";
                    }
                    viewpage = previewUrl;
                    splitViewSupported = true;
                }

                break;

            case "listing":
                // Listing mode
                viewpage = "View/listing.aspx";

                TreeNode = Tree.SelectSingleNode(nodeId, TreeProvider.ALL_CULTURES);
                if (TreeNode != null)
                {
                    classId = ValidationHelper.GetInteger(TreeNode.GetValue("NodeClassID"), 0);

                    DataClassInfo ci = DataClassInfoProvider.GetDataClass(classId);
                    if (ci != null)
                    {
                        if (!string.IsNullOrEmpty(ci.ClassListPageURL))
                        {
                            viewpage = ci.ClassListPageURL;
                        }
                    }
                }
                break;

            case "livesite":
                checkCulture = true;

                // Live site mode
                if (CheckValidationVisibility(action, nodeId))
                {
                    viewpage = URLHelper.AddParameterToUrl("View/ViewValidate.aspx", "viewpage", "livesite.aspx");
                }
                else
                {
                    viewpage = "View/livesite.aspx";
                    splitViewSupported = true;
                }
                break;

            case "newculture":
                // New document culture
                viewpage = "Edit/EditFrameset.aspx";
                break;

            default:
                // Edit mode
                viewpage = "Edit/EditFrameset.aspx";
                checkCulture = true;
                break;
        }

        // If culture version should be checked, check
        if (checkCulture)
        {
            // Check (and ensure) the proper content culture
            CheckPreferredCulture(true);

            // Check split mode 
            bool isSplitMode = CMSContext.DisplaySplitMode;
            bool combineWithDefaultCulture = !isSplitMode && SiteInfoProvider.CombineWithDefaultCulture(CMSContext.CurrentSiteName);

            TreeNode = Tree.SelectSingleNode(nodeId, CMSContext.PreferredCultureCode, combineWithDefaultCulture);
            if (TreeNode == null)
            {
                if ((action == "preview") || (action == "livesite"))
                {
                    viewpage = "~/CMSMessages/PageNotAvailable.aspx?reason=splitviewmissingculture&showlink=false";
                }
                else
                {
                    // Document does not exist -> redirect to new culture version creation dialog
                    viewpage = "Edit/NewCultureVersion.aspx";
                }
            }
        }

        // Apply the additional transformations to the view page URL
        viewpage = URLHelper.AppendQuery(viewpage, URLHelper.Url.Query);
        viewpage = ResolveUrl(viewpage);
        viewpage = URLHelper.AddParameterToUrl(viewpage, "hash", QueryHelper.GetHash(viewpage));

        // Split mode enabled
        if (splitViewSupported && CMSContext.DisplaySplitMode && (TreeNode != null) && (action == "preview" || (TreeNode.IsPublished && action == "livesite")))
        {
            viewpage = GetSplitViewUrl(viewpage);
        }
    }


    private bool CheckValidationVisibility(string mode, int nodeId)
    {
        CurrentUserInfo curUser = CMSContext.CurrentUser;
        string siteName = CMSContext.CurrentSiteName;

        if (curUser.IsAuthorizedPerUIElement("CMS.Content", "Validation", siteName))
        {
            TreeProvider tree = new TreeProvider(curUser);
            bool combineWithDefaultCulture = tree.CombineWithDefaultCulture;

            // Get the document
            TreeNode node = tree.SelectSingleNode(nodeId, CMSContext.PreferredCultureCode, combineWithDefaultCulture);

            if (node != null)
            {
                if (VALIDATION_EXCLUDED_CLASS_NAMES.Contains(";" + node.NodeClassName.ToLower() + ";"))
                {
                    return false;
                }
                // Check the document availability
                else if (!node.DocumentCulture.Equals(CMSContext.PreferredCultureCode, StringComparison.InvariantCultureIgnoreCase) && (!SiteInfoProvider.CombineWithDefaultCulture(siteName) || !node.DocumentCulture.Equals(CultureHelper.GetDefaultCulture(siteName), StringComparison.InvariantCultureIgnoreCase)) || ((mode == "livesite") && !node.IsPublished))
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }

        return true;
    }




    #endregion
}

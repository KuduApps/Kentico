using System;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.PortalEngine;
using CMS.SettingsProvider;
using CMS.TreeEngine;
using CMS.UIControls;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_Content_CMSDesk_Edit_EditFrameset : CMSContentPage
{
    #region "Variables"

    protected string viewpage = null;
    protected string menupage = null;
    protected string menuSize = "43";

    private int nodeId = 0;
    private int classId = 0;
    private string mode = null;

    private TreeProvider tree = null;
    private TreeNode node = null;

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        nodeId = QueryHelper.GetInteger("nodeid", 0);
        mode = QueryHelper.GetString("mode", string.Empty).ToLower();

        // Get current node
        tree = new TreeProvider(CMSContext.CurrentUser);
        switch (mode)
        {
            case "edit":
            case "editform":
                node = tree.SelectSingleNode(nodeId, CMSContext.PreferredCultureCode, false);
                if (node == null)
                {
                    URLHelper.Redirect("~/CMSModules/Content/CMSDesk/New/NewCultureVersion.aspx" + URLHelper.Url.Query);
                }
                break;

            default:
                node = tree.SelectSingleNode(nodeId);
                break;
        }

        if (node != null)
        {
            // Check read permissions
            if (CMSContext.CurrentUser.IsAuthorizedPerDocument(node, NodePermissionsEnum.Read) == AuthorizationResultEnum.Denied)
            {
                RedirectToAccessDenied(String.Format(GetString("cmsdesk.notauthorizedtoreaddocument"), node.NodeAliasPath));
            }
        }

        // Used when creating new document
        classId = QueryHelper.GetInteger("classid", 0);

        if (classId > 0)
        {
            DataClassInfo ci = DataClassInfoProvider.GetDataClass(classId);
            if (ci != null)
            {
                // check permission to create new document
                if (CMSContext.CurrentUser.IsAuthorizedToCreateNewDocument(nodeId, ci.ClassName))
                {
                    InitPage();
                }
                else
                {
                    RedirectToAccessDenied(GetString("cmsdesk.notauthorizedtocreatedocument"));
                }
            }
        }
        else
        {
            InitPage();
        }
    }


    protected void InitPage()
    {
        menupage = "editmenu.aspx" + URLHelper.Url.Query;

        // Register script files
        ScriptHelper.RegisterProgress(Page);
        ScriptHelper.RegisterScriptFile(Page, "~/CMSModules/Content/CMSDesk/Edit/EditFrameset.js");

        string script = "";
        switch (mode)
        {
            case "edit":
                script += ScriptHelper.GetScript("SetTabsContext('page');");
                break;

            case "editform":
                script += ScriptHelper.GetScript("SetTabsContext('edit');");
                break;

            case "design":
                script += ScriptHelper.GetScript("SetTabsContext('design');");
                break;
        }

        // Register script
        ScriptHelper.RegisterStartupScript(Page, typeof(string), "tabs", script, true);

        if (classId <= 0)
        {
            if (node != null)
            {
                // Set view mode to edit if new document
                string action = QueryHelper.GetString("action", string.Empty);
                switch (action.ToLower())
                {
                    case "new":
                    case "newlink":
                    case "newculture":
                    case "newvariant":
                        CMSContext.ViewMode = ViewModeEnum.EditForm;
                        break;
                }

                // Update view mode
                switch (UpdateViewMode(ViewModeEnum.Edit))
                {
                    case ViewModeEnum.Design:
                        menuSize = "0";
                        menupage = "../separator.aspx";
                        break;

                    case ViewModeEnum.Edit:
                        if (node.NodeClassName.ToLower() == "cms.file")
                        {
                            menuSize = "0";
                            menupage = "../blank.htm";
                        }
                        break;
                }
            }
        }

        viewpage = "editpage.aspx" + URLHelper.Url.Query;
    }

    #endregion
}

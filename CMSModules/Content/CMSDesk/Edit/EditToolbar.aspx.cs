using System;

using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.TreeEngine;
using CMS.CMSHelper;
using CMS.PortalEngine;
using CMS.UIControls;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_Content_CMSDesk_Edit_EditToolbar : CMSContentPage
{
    protected string viewpage = null;
    protected TreeNode node = null;
    protected string controlFrame = "editview";

    protected void Page_Load(object sender, EventArgs e)
    {
        // Get the node ID
        int nodeId = QueryHelper.GetInteger("nodeid", 0);

        if (!RequestHelper.IsPostBack())
        {
            try
            {
                viewpage = "edit.aspx" + URLHelper.Url.Query;

                string action = QueryHelper.GetString("action", "").ToLower();

                switch (action)
                {
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
                            node = tree.SelectSingleNode(nodeId);
                            if (node != null)
                            {
                                classInfo = DataClassInfoProvider.GetDataClass(node.NodeClassName);
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
                                        viewpage = ResolveUrl(classInfo.ClassNewPageURL) + URLHelper.Url.Query;
                                    }
                                }
                                // If existing document, check if editing page URL is set
                                else if (!string.IsNullOrEmpty(classInfo.ClassEditingPageURL))
                                {
                                    viewpage = ResolveUrl(classInfo.ClassEditingPageURL) + URLHelper.Url.Query;
                                }
                            }
                            else if (CMSContext.ViewMode == ViewModeEnum.Edit)
                            {
                                // Check if view page URL is set
                                if (!string.IsNullOrEmpty(classInfo.ClassViewPageUrl))
                                {
                                    viewpage = ResolveUrl(classInfo.ClassViewPageUrl) + URLHelper.Url.Query;
                                }
                                else
                                {
                                    viewpage = ResolveUrl(CMSContext.GetUrl(node.NodeAliasPath, node.DocumentUrlPath));
                                }
                            }
                            else
                            {
                                viewpage = ResolveUrl(CMSContext.GetUrl(node.NodeAliasPath, node.DocumentUrlPath));
                            }
                        }
                        break;
                }

                ltlScript.Text += ScriptHelper.GetScript("parent.frames['" + controlFrame + "'].location.replace('" + viewpage + "');");
            }
            catch (Exception ex)
            {
                ltlScript.Text += ScriptHelper.GetAlertScript("[EditToolbar.aspx]: " + ex.Message);
            }
        }

        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "ControlFunctions", ScriptHelper.GetScript(
                "function SaveDocument(nodeId, createAnother) { " + ClientScript.GetPostBackEventReference(btnSave, null) + "; }\n" +
                "function Approve(nodeId) { " + ClientScript.GetPostBackEventReference(btnApprove, null) + "; }\n" +
                "function CheckIn(nodeId) { " + ClientScript.GetPostBackEventReference(btnCheckIn, null) + "; }\n"
            ));
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        ltlScript.Text += ScriptHelper.GetScript("parent.frames['" + controlFrame + "'].SaveDocument();");
    }


    protected void btnApprove_Click(object sender, EventArgs e)
    {
        ltlScript.Text += ScriptHelper.GetScript("parent.frames['" + controlFrame + "'].Approve();");
    }


    protected void btnCheckIn_Click(object sender, EventArgs e)
    {
        ltlScript.Text += ScriptHelper.GetScript("parent.frames['" + controlFrame + "'].CheckIn();");
    }
}

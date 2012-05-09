using System;
using System.Web.UI;

using CMS.CMSHelper;
using CMS.ExtendedControls;
using CMS.GlobalHelper;
using CMS.TreeEngine;
using CMS.UIControls;

public partial class CMSModules_Content_CMSDesk_TemplateSelection : CMSContentPage, IPostBackEventHandler
{
    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        lblChoose.Text = GetString("TemplateSelection.chooseTemplate");
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "SaveDocument", ScriptHelper.GetScript("function SaveDocument(nodeId, createAnother) { " + ControlsHelper.GetPostBackEventReference(this, "#", false).Replace("'#'", "createAnother+''") + "; return false; }"));

        ltlScript.Text = GetSpellCheckDialog();

        int nodeId = QueryHelper.GetInteger("nodeid", 0);

        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);
        // For new node is not document culture important, preffered culture is used
        TreeNode node = tree.SelectSingleNode(nodeId);
        if (node != null)
        {
            selTemplate.DocumentID = node.DocumentID;
        }

        selTemplate.ParentNodeID = nodeId;
    }

    #endregion


    #region "IPostBackEventHandler Members"

    /// <summary>
    /// Processes the postback action.
    /// </summary>
    public void RaisePostBackEvent(string eventArgument)
    {
        string errorMessage = null;
        int templateId = selTemplate.EnsureTemplate(null, ref errorMessage);

        if (!String.IsNullOrEmpty(errorMessage))
        {
            this.lblError.Text = errorMessage;
            this.lblError.Visible = true;
        }
        else
        {
            URLHelper.Redirect("~/CMSModules/Content/CMSDesk/Edit/edit.aspx" + URLHelper.Url.Query + "&templateid=" + templateId);
        }
    }

    #endregion
}

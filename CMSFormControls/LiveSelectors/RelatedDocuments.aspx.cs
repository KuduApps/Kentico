using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.TreeEngine;
using CMS.CMSHelper;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSFormControls_LiveSelectors_RelatedDocuments : CMSLiveModalPage
{
    #region "Variables"

    private int nodeId = 0;
    private TreeProvider mTreeProvider = null;
    private TreeNode node = null;
    private string externalControlID = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// Tree provider.
    /// </summary>
    protected TreeProvider TreeProvider
    {
        get
        {
            if (mTreeProvider == null)
            {
                mTreeProvider = new TreeProvider(CMSContext.CurrentUser);
            }

            return mTreeProvider;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Initialize modal page
        RegisterEscScript();
        ScriptHelper.RegisterWOpenerScript(Page);

        if (QueryHelper.ValidateHash("hash"))
        {
            externalControlID = QueryHelper.GetString("externalControlID", string.Empty);
            string title = GetString("Relationship.AddRelatedDocs");
            Page.Title = title;
            CurrentMaster.Title.TitleText = title;
            CurrentMaster.Title.TitleImage = GetImageUrl("/Objects/CMS_RelationshipName/new.png");

            btnSave.Click += new EventHandler(btnSave_Click);
            btnClose.Attributes.Add("onclick", "window.close(); return false;");

            AddNoCacheTag();

            addRelatedDocument.ShowButtons = false;

            nodeId = QueryHelper.GetInteger("nodeid", 0);
            if (nodeId > 0)
            {
                // Get the node
                node = TreeProvider.SelectSingleNode(nodeId, CMSContext.PreferredCultureCode, TreeProvider.CombineWithDefaultCulture);

                if (node != null)
                {
                    // Check read permissions
                    if (CMSContext.CurrentUser.IsAuthorizedPerDocument(node, NodePermissionsEnum.Read) == AuthorizationResultEnum.Denied)
                    {
                        RedirectToAccessDenied(String.Format(GetString("cmsdesk.notauthorizedtoreaddocument"), node.NodeAliasPath));
                    }
                    else
                    {
                        lblInfo.Visible = false;
                    }

                    // Set tree node
                    addRelatedDocument.TreeNode = node;
                }
                else
                {
                    btnSave.Enabled = false;
                }
            }
        }
        else
        {
            // Hide all controls
            btnSave.Visible = false;
            btnClose.Visible = false;
            addRelatedDocument.Visible = false;
            string url = ResolveUrl("~/CMSMessages/Error.aspx?title=" + GetString("dialogs.badhashtitle") + "&text=" + GetString("dialogs.badhashtext") + "&cancel=1");
            ltlScript.Text = ScriptHelper.GetScript("window.location = '" + url + "';");
        }
    }


    /// <summary>
    /// Save meta data of attachment.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Argument</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (addRelatedDocument.SaveRelationship())
        {
            string script = "if (wopener.RefreshUpdatePanel_" + externalControlID + ") { wopener.RefreshUpdatePanel_" + externalControlID + "(); close(); } ";
            this.ltlScript.Text = ScriptHelper.GetScript(script);
        }
    }

    #endregion
}

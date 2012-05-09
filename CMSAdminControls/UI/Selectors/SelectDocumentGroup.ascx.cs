using System;

using CMS.CMSHelper;
using CMS.FormControls;
using CMS.GlobalHelper;
using CMS.Synchronization;
using CMS.TreeEngine;
using CMS.UIControls;
using CMS.WorkflowEngine;
using CMS.SettingsProvider;

public partial class CMSAdminControls_UI_Selectors_SelectDocumentGroup : CMSUserControl
{
    private int mSiteId = 0;
    private int mNodeId = 0;
    private int mGroupId = 0;

    FormEngineUserControl ctrl = null;

    private string controlPath = "~/CMSModules/Groups/FormControls/CommunityGroupSelector.ascx";


    /// <summary>
    /// Indicates if buttons should be displayed.
    /// </summary>
    public bool DisaplayButtons 
    {
        get
        {
            return plcButtons.Visible;
        }
        set
        {
            plcButtons.Visible = value;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (FileHelper.FileExists(controlPath))
        {
            // Load the group selector
            ctrl = this.LoadControl(controlPath) as FormEngineUserControl ;
            ctrl.ID = "groupSelector"; 
           
            plcGroupSelector.Controls.Add(ctrl);            

            UniSelector usGroups = ctrl.GetValue("CurrentSelector") as UniSelector;
            if (usGroups != null)
            {
                usGroups.ReturnColumnName = "GroupID";
            }
            
            mSiteId = QueryHelper.GetInteger("siteid", 0);
            mNodeId = QueryHelper.GetInteger("nodeid", 0);
            mGroupId = QueryHelper.GetInteger("groupid", 0);
                       
            if (!RequestHelper.IsPostBack())
            {                
                ctrl.Value = mGroupId;                
            }

            ctrl.IsLiveSite = false;
            ctrl.SetValue("DisplayCurrentGroup", false);
            ctrl.SetValue("DisplayNoneValue", true);
            ctrl.SetValue("SiteID", mSiteId);
            ctrl.SetValue("CurrentSelector", usGroups);
        }
    }


    protected void btnOk_Click(object sender, EventArgs e)
    {
        ProcessAction();
    }


    public void ProcessAction()
    {
        if (ctrl != null)
        {
            // Get the node
            TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

            TreeNode node = tree.SelectSingleNode(mNodeId);
            int groupId = ValidationHelper.GetInteger(ctrl.Value, 0);

            // Check inherited documents
            if (chkInherit.Checked)
            {
                tree.ChangeCommunityGroup(node.NodeAliasPath, groupId, mSiteId, true);
            }

            // Update the document node
            node.SetIntegerValue("NodeGroupID", groupId, false);
            node.Update();

            // Log synchronization
            DocumentSynchronizationHelper.LogDocumentChange(node, TaskTypeEnum.UpdateDocument, tree);
        }

        ltlScript.Text = ScriptHelper.GetScript("wopener.ReloadOwner(); window.close();");
    }
}

using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.OnlineMarketing;
using CMS.TreeEngine;

public partial class CMSModules_OnlineMarketing_Pages_Tools_MVTest_List : CMSMVTestPage
{
    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Prepare the actions
        string[,] actions = new string[1, 6];
        actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
        actions[0, 1] = GetString("mvtest.new");        
        actions[0, 3] = ResolveUrl("Edit.aspx");
        actions[0, 5] = GetImageUrl("Objects/OM_MVTest/add.png");

        // Set the actions
        ICMSMasterPage master = this.CurrentMaster;
        master.HeaderActions.Actions = actions;

        // Setup the uniGrid 
        listElem.ShowFilter = true;
    }

    #endregion
}

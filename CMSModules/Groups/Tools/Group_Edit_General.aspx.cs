using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.Community;

public partial class CMSModules_Groups_Tools_Group_Edit_General : CMSGroupPage
{
    protected int groupId = 0;


    protected void Page_Load(object sender, EventArgs e)
    {        
        // Get groupid
        groupId = QueryHelper.GetInteger("groupid", 0);

        CheckGroupPermissions(groupId, CMSAdminControl.PERMISSION_READ); 

        // Initialize editing control
        this.groupEditElem.GroupID = groupId;
        this.groupEditElem.DisplayAdvanceOptions = true;
        this.groupEditElem.IsLiveSite = false;

        if (CMSContext.CurrentSite != null)
        {
            this.groupEditElem.SiteID = CMSContext.CurrentSite.SiteID;
        }        
    }
}

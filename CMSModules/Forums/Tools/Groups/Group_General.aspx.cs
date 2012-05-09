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
using CMS.DataEngine;
using CMS.SiteProvider;
using CMS.Forums;
using CMS.CMSHelper;
using CMS.LicenseProvider;
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSModules_Forums_Tools_Groups_Group_General : CMSForumsPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int groupID = QueryHelper.GetInteger("groupid", 0);
        ForumContext.CheckSite(groupID, 0, 0);

        this.groupEdit.GroupID = groupID;
        this.groupEdit.IsLiveSite = false;
    }
}

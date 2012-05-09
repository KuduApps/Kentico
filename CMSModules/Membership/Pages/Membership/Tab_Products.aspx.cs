using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.SiteProvider;

public partial class CMSModules_Membership_Pages_Membership_Tab_Products : CMSMembershipPage
{
    private int membershipID;

    protected void Page_Load(object sender, EventArgs e)
    {
        membershipID = QueryHelper.GetInteger("MembershipID", 0);
        MembershipInfo mi = MembershipInfoProvider.GetMembershipInfo(membershipID);

        // Test security
        CheckMembershipPermissions(mi);
    }
}


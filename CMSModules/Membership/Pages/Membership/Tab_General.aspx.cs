using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSModules_Membership_Pages_Membership_Tab_General : CMSMembershipPage
{
    #region "Methods"

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        // Get the ID from query string
        this.editElem.MembershipID = QueryHelper.GetInteger("membershipId", 0);

        // Check permissions
        CheckMembershipPermissions(editElem.MembershipObj);

        editElem.OnCheckPermissions += new CMSAdminControl.CheckPermissionsEventHandler(membershipEditElem_OnCheckPermissions);        
    }


    protected void membershipEditElem_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Membership", permissionType))
        {
            RedirectToCMSDeskAccessDenied("CMS.Membership", permissionType);
        }
    }

    #endregion
}
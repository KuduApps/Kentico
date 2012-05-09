using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.SettingsProvider;
using CMS.CMSHelper;


public partial class CMSModules_Membership_Pages_Membership_Tab_Roles : CMSMembershipPage
{
    #region "Variables"

    protected int siteID = 0;
    private string currentValues = String.Empty;
    private int membershipID = 0;

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        int siteID = CMSContext.CurrentSiteID;
        membershipID = QueryHelper.GetInteger("MembershipID", 0);

        // Test security
        MembershipInfo mi = MembershipInfoProvider.GetMembershipInfo(membershipID);

        EditedObject = mi;

        CheckMembershipPermissions(mi);

        // Set SiteID 
        if (mi != null)
        {
            siteID = mi.MembershipSiteID;
        }

        usRoles.OnSelectionChanged += new EventHandler(usRoles_OnSelectionChanged);

        string where = (siteID == 0) ? "SiteID IS NULL" : "SiteID = " + siteID;
        usRoles.WhereCondition = where + " AND RoleGroupID IS NULL";

        DataSet ds = MembershipRoleInfoProvider.GetMembershipRoles("MembershipID = " + membershipID, String.Empty);
        if (!DataHelper.DataSourceIsEmpty(ds))
        {
            currentValues = TextHelper.Join(";", SqlHelperClass.GetStringValues(ds.Tables[0], "RoleID"));
        }

        if (!RequestHelper.IsPostBack())
        {
            // Set values
            usRoles.Value = currentValues;
        }

    }


    protected void usRoles_OnSelectionChanged(object sender, EventArgs ea)
    {
        SaveData();
    }


    /// <summary>
    /// Store selected (unselected) roles.
    /// </summary>
    private void SaveData()
    {
        // Check "modify" permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Membership", "Modify"))
        {
            RedirectToAccessDenied("CMS.Membership", "Modify");
        }

        this.lblInfo.Visible = false;

        // Remove old items
        string newValues = ValidationHelper.GetString(usRoles.Value, null);
        string items = DataHelper.GetNewItemsInList(newValues, currentValues);
        bool saved = false;

        if (!String.IsNullOrEmpty(items))
        {
            string[] newItems = items.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (newItems != null)
            {
                // Add all new items to site
                foreach (string item in newItems)
                {
                    int roleID = ValidationHelper.GetInteger(item, 0);
                    MembershipRoleInfoProvider.RemoveMembershipFromRole(membershipID, roleID);

                    saved = true;
                }
            }
        }

        // Add new items
        items = DataHelper.GetNewItemsInList(currentValues, newValues);
        if (!String.IsNullOrEmpty(items))
        {
            string[] newItems = items.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (newItems != null)
            {
                // Add all new items to site
                foreach (string item in newItems)
                {
                    int roleID = ValidationHelper.GetInteger(item, 0);
                    MembershipRoleInfoProvider.AddMembershipToRole(membershipID, roleID);
                    
                    saved = true;
                }
            }
        }

        if (saved)
        {
            MembershipInfoProvider.InvalidateMembershipUsers(membershipID); 

            lblInfo.Visible = true;
            lblInfo.Text = GetString("General.ChangesSaved");
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        if (RequestHelper.IsPostBack())
        {
            pnlBasic.Update();
        }

        base.OnPreRender(e);
    }

    #endregion
}


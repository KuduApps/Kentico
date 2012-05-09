using System;
using System.Data;
using System.Web;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.OnlineMarketing;
using CMS.SettingsProvider;

/// <summary>
/// Online marketing functions.
/// </summary>
public static class OnlineMarketingFunctions
{
    /// <summary>
    /// Returns contact's last activity of specified activity type.
    /// </summary>
    /// <param name="contact">Contact info object</param>
    /// <param name="activityType">Activity type</param>
    public static ActivityInfo LastActivityOfType(object contact, object activityType)
    {
        ContactInfo ci = (ContactInfo)contact;
        string type = ValidationHelper.GetString(activityType, string.Empty);

        if (ci != null)
        {
            return ActivityInfoProvider.GetContactsLastActivity(ci.ContactID, type);
        }

        return null;
    }


    /// <summary>
    /// Returns contact's first activity of specified activity type.
    /// </summary>
    /// <param name="contact">Contact info object</param>
    /// <param name="activityType">Activity type</param>
    public static ActivityInfo FirstActivityOfType(object contact, object activityType)
    {
        ContactInfo ci = (ContactInfo)contact;
        string type = ValidationHelper.GetString(activityType, string.Empty);

        if (ci != null)
        {
            return ActivityInfoProvider.GetContactsFirstActivity(ci.ContactID, type);
        }

        return null;
    }


    /// <summary>
    /// Returns TRUE if the contact is in specified contact group on current site.
    /// </summary>
    /// <param name="contact">Contact info object</param>
    /// <param name="contactGroupName">Contact group name</param>
    public static bool IsInContactGroup(object contact, object contactGroupName)
    {
        ContactInfo ci = (ContactInfo)contact;
        string groupName = ValidationHelper.GetString(contactGroupName, string.Empty);
        int siteId = CMSContext.CurrentSiteID;

        if ((ci != null) && (siteId > 0) && (!string.IsNullOrEmpty(groupName)))
        {
            // Get contact group ID if the name matches and contact is member of the group
            DataSet ds = ContactGroupInfoProvider.GetContactGroups("ContactGroupName='" + groupName + "' AND ContactGroupSiteID=" + siteId + " AND ContactGroupID IN (SELECT ContactGroupMemberContactGroupID FROM OM_ContactGroupMember WHERE ContactGroupMemberRelatedID=" + ci.ContactID + " AND ContactGroupMemberType=0)", null, 1, "ContactGroupID");

            return !DataHelper.DataSourceIsEmpty(ds);
        }

        return false;
    }


    /// <summary>
    /// Returns contact's points in specified score on current site.
    /// </summary>
    /// <param name="contact">Contact info object</param>
    /// <param name="scoreName">Score name</param>
    public static int GetScore(object contact, object scoreName)
    {
        ContactInfo ci = (ContactInfo)contact;
        string score = ValidationHelper.GetString(scoreName, string.Empty);
        int siteId = CMSContext.CurrentSiteID;

        if ((ci != null) && (siteId > 0) && (!string.IsNullOrEmpty(score)))
        {
            // Prepare DB query
            QueryDataParameters parameters = new QueryDataParameters();
            parameters.Add("@ContactID", ci.ContactID);
            parameters.Add("@ScoreName", score);
            parameters.Add("@SiteID", siteId);

            string where = "(Expiration IS NULL OR Expiration > GetDate()) AND (ContactID=@ContactID) AND (ScoreName=@ScoreName) AND (ScoreSiteID=@SiteID)";

            // Get sum of points of the contact in specified score
            DataSet ds = SqlHelperClass.ExecuteQuery("om.score.selectdatajoined", parameters, "SUM(Value) AS Score", where, null, -1);

            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                return ValidationHelper.GetInteger(ds.Tables[0].Rows[0][0], 0);
            }
        }

        return 0;
    }


    /// <summary>
    /// Returns e-mail domain name.
    /// </summary>
    /// <param name="email">E-mail address</param>
    public static string GetEmailDomain(object email)
    {
        return ContactHelper.GetEmailDomain(ValidationHelper.GetString(email, string.Empty));
    }
}
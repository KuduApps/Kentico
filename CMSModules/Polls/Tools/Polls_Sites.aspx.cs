using System;
using System.Data;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.Polls;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSModules_Polls_Tools_Polls_Sites : CMSPollsPage
{
    #region "Private variables"

    protected int pollId = 0;
    protected string currentValues = String.Empty;

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        lblAvialable.Text = GetString("Poll_Sites.Available");

        // Get poll ID from querystring
        pollId = QueryHelper.GetInteger("pollid", 0);

        PollInfo pi = PollInfoProvider.GetPollInfo(pollId);
        EditedObject = pi;

        // Check global and site read permmision
        this.CheckPollsReadPermission(pi.PollSiteID);

        // Disable page for site polls
        if (pi.PollSiteID > 0)
        {
            EditedObject = pi;
            return;
        }

        plcC.Visible = true;

        if (pollId > 0)
        {
            // Get the active sites
            currentValues = GetPollSites();

            if (!RequestHelper.IsPostBack())
            {
                usSites.Value = currentValues;
            }
        }

        usSites.OnSelectionChanged += usSites_OnSelectionChanged;

        // Non global admin users will see only sites where they are members
        if (!CMSContext.CurrentUser.UserSiteManagerAdmin) 
        {
            // For global admin without access to site manager show only polls with site relationship
            if (CMSContext.CurrentUser.UserIsGlobalAdministrator)
            {
                usSites.WhereCondition = SqlHelperClass.AddWhereCondition(usSites.WhereCondition, "SiteID IN (SELECT SiteID FROM CMS_UserSite");
            }
            else
            {
                usSites.WhereCondition = SqlHelperClass.AddWhereCondition(usSites.WhereCondition, "SiteID IN (SELECT SiteID FROM CMS_UserSite WHERE UserID = " + CMSContext.CurrentUser.UserID + ")");
            }
        }
    }


    /// <summary>
    /// Returns string with poll sites.
    /// </summary>    
    private string GetPollSites()
    {
        //DataSet ds = PollSiteInfoProvider.GetPollSites("SiteID", "PollID = " + pollId, null, 0, null);
        DataSet ds = PollInfoProvider.GetPollSites(pollId, null, null, -1, "CMS_Site.SiteID");
        if (!DataHelper.DataSourceIsEmpty(ds))
        {
            return TextHelper.Join(";", SqlHelperClass.GetStringValues(ds.Tables[0], "SiteID"));
        }

        return String.Empty;
    }


    protected void usSites_OnSelectionChanged(object sender, EventArgs e)
    {
        SaveSites();
    }


    protected void SaveSites()
    {
        // Check 'GlobalModify' permission
        CheckPollsModifyPermission(0);

        // Remove old items
        string newValues = ValidationHelper.GetString(usSites.Value, null);
        string items = DataHelper.GetNewItemsInList(newValues, currentValues);
        bool falseValues = false;

        if (!String.IsNullOrEmpty(items))
        {
            string[] newItems = items.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (newItems != null)
            {
                // Add all new items to site
                foreach (string item in newItems)
                {
                    int siteId = ValidationHelper.GetInteger(item, 0);

                    // Remove poll from site
                    PollInfoProvider.RemovePollFromSite(pollId, siteId);
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
                    int siteId = ValidationHelper.GetInteger(item, 0);
                    SiteInfo si = SiteInfoProvider.GetSiteInfo(siteId);

                    if (si != null)
                    {

                        // Check if site has license permission to assign poll to the site
                        if (!PollInfoProvider.LicenseVersionCheck(si.DomainName, FeatureEnum.Polls, VersionActionEnum.Insert))
                        {
                            lblError.Visible = true;
                            lblError.Text = GetString("LicenseVersion.Polls");
                            falseValues = true;
                            continue;
                        }
                        else
                        {
                            // If poll is not in site, add it to the site
                            PollInfoProvider.AddPollToSite(pollId, si.SiteID);
                        }
                    }
                }
            }
        }

        // If there were some errors, reload uniselector
        if (falseValues)
        {
            usSites.Value = GetPollSites();
            usSites.Reload(true);
        }

        lblInfo.Visible = true;
        lblInfo.Text = GetString("General.ChangesSaved");
    }
}

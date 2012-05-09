using System;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.LicenseProvider;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.UIControls;

/// <summary>
/// Displays a table of subscribers who clicked a link in a specified issue.
/// </summary>
public partial class CMSModules_Newsletters_Tools_Newsletters_Newsletter_Issue_SubscribersClicks : CMSToolsModalPage
{
    #region "Variables"

    private int linkId;

    #endregion


    #region "Methods"

    protected void Page_Init(object sender, EventArgs e)
    {
        // Check the license
        if (!string.IsNullOrEmpty(DataHelper.GetNotEmpty(URLHelper.GetCurrentDomain(), string.Empty)))
        {
            LicenseHelper.CheckFeatureAndRedirect(URLHelper.GetCurrentDomain(), FeatureEnum.Newsletters);
        }

        // Check site availability
        if (!ResourceSiteInfoProvider.IsResourceOnSite("CMS.Newsletter", CMSContext.CurrentSiteName))
        {
            RedirectToResourceNotAvailableOnSite("CMS.Newsletter");
        }

        CurrentUserInfo user = CMSContext.CurrentUser;

        // Check permissions for CMS Desk -> Tools -> Newsletter
        if (!user.IsAuthorizedPerUIElement("CMS.Tools", "Newsletter"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Tools", "Newsletter");
        }

        // Check 'NewsletterRead' permission
        if (!user.IsAuthorizedPerResource("CMS.Newsletter", "Read"))
        {
            RedirectToCMSDeskAccessDenied("CMS.Newsletter", "Read");
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentMaster.Title.TitleText = GetString("newsletter_issue_subscribersclicks.title");
        CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_Newsletter/ViewParticipants.png");

        linkId = QueryHelper.GetInteger("linkid", 0);
        if (linkId == 0)
        {
            RequestHelper.EndResponse();
        }

        // Filter by Link ID (from querystring)
        string whereCondition = SqlHelperClass.GetWhereCondition("LinkID", linkId);

        // Filter by Site ID to prevent accessing links from sites other than current site
        int siteId = CMSContext.CurrentSiteID;
        whereCondition = SqlHelperClass.AddWhereCondition(whereCondition, SqlHelperClass.GetWhereCondition("SiteID", siteId));

        UniGrid.WhereCondition = whereCondition;
        UniGrid.Pager.DefaultPageSize = 15;
        UniGrid.Pager.ShowPageSize = false;
        UniGrid.FilterLimit = 1;
        UniGrid.OnExternalDataBound += UniGrid_OnExternalDataBound;
    }


    protected object UniGrid_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName)
        {
            case "subscribername":
                return TextHelper.LimitLength(HTMLHelper.HTMLEncode(parameter.ToString()), 65);

            case "subscriberemail":
                return TextHelper.LimitLength(parameter.ToString(), 65);

            default:
                return parameter;
        }
    }

    #endregion
}
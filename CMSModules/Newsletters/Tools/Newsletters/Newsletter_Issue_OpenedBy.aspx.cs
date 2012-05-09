using System;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.LicenseProvider;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.UIControls;

/// <summary>
/// Displays a table of issue openers (subscribers who have opened the email with the specified issue).
/// </summary>
public partial class CMSModules_Newsletters_Tools_Newsletters_Newsletter_Issue_OpenedBy : CMSToolsModalPage
{
    #region "Variables"

    private int issueId;

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
        this.CurrentMaster.Title.TitleText = GetString("newsletter_issue_openedby.title");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_Newsletter/module.png");

        issueId = QueryHelper.GetInteger("issueid", 0);
        if (issueId == 0)
        {
            RequestHelper.EndResponse();
        }

        // Filter by Issue ID (from querystring)
        QueryDataParameters parameters = new QueryDataParameters();
        parameters.Add("@IssueID", issueId);

        UniGrid.QueryParameters = parameters;

        // Filter by Site ID to prevent accessing issues from sites other than current site
        string whereCondition = SqlHelperClass.GetWhereCondition("SiteID", CMSContext.CurrentSiteID);

        UniGrid.WhereCondition = SqlHelperClass.AddWhereCondition(whereCondition, fltOpenedBy.WhereCondition);
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
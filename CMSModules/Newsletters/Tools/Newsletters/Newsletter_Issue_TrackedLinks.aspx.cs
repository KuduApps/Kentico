using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.LicenseProvider;
using CMS.Newsletter;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.UIControls;

/// <summary>
/// Displays a table of clicked links.
/// </summary>
public partial class CMSModules_Newsletters_Tools_Newsletters_Newsletter_Issue_TrackedLinks : CMSToolsModalPage
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
        CurrentMaster.Title.TitleText = GetString("newsletter_issue_trackedlinks.title");
        CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_Newsletter/ViewStatistics.png");

        issueId = QueryHelper.GetInteger("issueid", 0);
        if (issueId == 0)
        {
            RequestHelper.EndResponse();
        }

        ScriptHelper.RegisterDialogScript(this);

        string scriptBlock = string.Format(@"
            function OpenTarget(url) {{ window.open(url, 'LinkTarget'); return false; }}
            function ViewClicks(id) {{ modalDialog('{0}?linkid=' + id, 'NewsletterIssueSubscriberClicks', '900px', '700px');  return false; }}",
            ResolveUrl(@"~\CMSModules\Newsletters\Tools\Newsletters\Newsletter_Issue_SubscribersClicks.aspx"));
        ScriptHelper.RegisterClientScriptBlock(this, GetType(), "Actions", scriptBlock, true);

        // Filter by Issue ID (from querystring)
        string whereCondition = SqlHelperClass.GetWhereCondition("IssueID", issueId);

        // Filter by Site ID to prevent accessing issues from sites other than current site
        int siteId = CMSContext.CurrentSiteID;
        whereCondition = SqlHelperClass.AddWhereCondition(whereCondition, SqlHelperClass.GetWhereCondition("SiteID", siteId));

        UniGrid.WhereCondition = whereCondition;
        UniGrid.Pager.DefaultPageSize = 15;
        UniGrid.Pager.ShowPageSize = false;
        UniGrid.FilterLimit = 1;
        UniGrid.OnExternalDataBound += UniGrid_OnExternalDataBound;
        UniGrid.OnAction += UniGrid_OnAction;
    }


    protected object UniGrid_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName)
        {
            case "clickrate":
                return string.Format("{0:F0}", parameter);

            case "linktarget":
                return string.Format(@"<a href=""#"" onclick=""OpenTarget('{0}')"">{1}</a>",
                    parameter,
                    HTMLHelper.HTMLEncode(TextHelper.LimitLength(parameter.ToString(), 50)));

            case "linktargettooltip":
                return HTMLHelper.HTMLEncode(parameter.ToString());

            case "linkdescription":
                return HTMLHelper.HTMLEncode(TextHelper.LimitLength(parameter.ToString(), 25));

            case "linkdescriptiontooltip":
                return HTMLHelper.HTMLEncode(parameter.ToString());

            case "view":
                if (sender is ImageButton)
                {
                    ImageButton imageButton = sender as ImageButton;
                    GridViewRow gvr = parameter as GridViewRow;
                    if (gvr != null)
                    {
                        DataRowView drv = gvr.DataItem as DataRowView;
                        if (drv != null)
                        {
                            int totalClics = ValidationHelper.GetInteger(drv["TotalClicks"], 0);
                            if (totalClics <= 0)
                            {
                                imageButton.Style.Add(HtmlTextWriterStyle.Display, "none");
                            }
                        }
                    }
                }
                return sender;

            case "deleteoutdated":
                if (sender is ImageButton)
                {
                    ImageButton imageButton = sender as ImageButton;
                    GridViewRow gvr = parameter as GridViewRow;
                    if (gvr != null)
                    {
                        DataRowView drv = gvr.DataItem as DataRowView;
                        if (drv != null)
                        {
                            bool isOutdated = ValidationHelper.GetBoolean(drv["LinkOutdated"], false);
                            if (!isOutdated)
                            {
                                imageButton.Style.Add(HtmlTextWriterStyle.Display, "none");
                            }
                        }
                    }
                }
                return sender;

            default:
                return parameter;
        }
    }


    protected void UniGrid_OnAction(string actionName, object actionArgument)
    {
        switch (actionName)
        {
            case "deleteoutdated":
                int linkId = ValidationHelper.GetInteger(actionArgument, 0);
                LinkInfoProvider.DeleteLinkInfo(linkId);
                break;
        }
    }

    #endregion
}
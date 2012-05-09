using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.Newsletter;
using CMS.SettingsProvider;
using CMS.UIControls;

/// <summary>
/// Displays a list of issues for a specified newsletter.
/// </summary>
public partial class CMSModules_Newsletters_Tools_Newsletters_Newsletter_Issue_List : CMSNewsletterNewslettersPage
{
    #region "Variables"

    private int newsletterId;


    private Newsletter newsletter;


    private bool mBounceMonitoringEnabled;


    private bool mOnlineMarketingEnabled;

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        newsletterId = QueryHelper.GetInteger("newsletterid", 0);
        if (newsletterId == 0)
        {
            RequestHelper.EndResponse();
        }

        string siteName = CMSContext.CurrentSiteName;
        mBounceMonitoringEnabled = SettingsKeyProvider.GetBoolValue(siteName + ".CMSMonitorBouncedEmails");
        mOnlineMarketingEnabled = NewsletterProvider.OnlineMarketingEnabled(siteName);

        ScriptHelper.RegisterDialogScript(this);

        string scriptBlock = string.Format(@"
            function RefreshPage() {{ document.location.replace(document.location); }}
            function EditItem(id) {{ modalDialog('{0}?issueid=' + id, 'NewsletterIssueEdit', screen.availWidth - 10, screen.availHeight - 80); }}
            function NewItem(id) {{ modalDialog('{1}?newsletterid=' + id, 'NewsletterNewIssue', screen.availWidth - 10, screen.availHeight - 80); }}
            function ShowOpenedBy(id) {{ modalDialog('{2}?issueid=' + id, 'NewsletterIssueOpenedBy', '900px', '700px');  return false; }}
            function ViewClickedLinks(id) {{ modalDialog('{3}?issueid=' + id, 'NewsletterTrackedLinks', '900px', '700px'); return false; }}",
            ResolveUrl(@"~\CMSModules\Newsletters\Tools\Newsletters\Newsletter_Issue_Frameset.aspx"),
            ResolveUrl(@"~\CMSModules\Newsletters\Tools\Newsletters\Newsletter_Issue_New_Edit.aspx"),
            ResolveUrl(@"~\CMSModules\Newsletters\Tools\Newsletters\Newsletter_Issue_OpenedBy.aspx"),
            ResolveUrl(@"~\CMSModules\Newsletters\Tools\Newsletters\Newsletter_Issue_TrackedLinks.aspx"));
        ScriptHelper.RegisterClientScriptBlock(this, GetType(), "Actions", scriptBlock, true);

        UniGrid.OnAction += uniGrid_OnAction;
        UniGrid.WhereCondition = "IssueNewsletterID = " + newsletterId;
        UniGrid.OnExternalDataBound += UniGrid_OnExternalDataBound;
        UniGrid.ZeroRowsText = GetString("Newsletter_Issue_List.NoIssuesFound");
        UniGrid.OnBeforeDataReload += UniGrid_OnBeforeDataReload;

        // Get newsletter object and check its existence
        EditedObject = newsletter = NewsletterProvider.GetNewsletter(newsletterId);

        InitHeaderActions();
    }


    protected void InitHeaderActions()
    {
        if (newsletter.NewsletterType != NewsletterType.Dynamic)
        {
            string[,] actions = new string[1, 12];

            actions[0, 0] = HeaderActions.TYPE_LINKBUTTON;
            actions[0, 1] = GetString("Newsletter_Issue_List.NewItemCaption");
            actions[0, 2] = string.Format("NewItem({0}); return false;", newsletterId);
            actions[0, 5] = GetImageUrl("Objects/Newsletter_Issue/add.png");

            CurrentMaster.HeaderActions.Actions = actions;
        }
    }


    protected void UniGrid_OnBeforeDataReload()
    {
        // Hide opened emails if online marketing is disabled or no license
        UniGrid.GridView.Columns[4].Visible = mOnlineMarketingEnabled;

        // Hide bounced emails info if monitoring disabled or online marketing not present
        UniGrid.GridView.Columns[6].Visible = mOnlineMarketingEnabled && mBounceMonitoringEnabled;
    }


    /// <summary>
    /// Handles the UniGrid's OnExternalDataBound event.
    /// </summary>
    /// <param name="sender">The sender</param>
    /// <param name="sourceName">Name of the source</param>
    /// <param name="parameter">The data row</param>
    /// <returns>Formatted value to be used in the UniGrid</returns>
    protected object UniGrid_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName.ToLower())
        {
            case "issuesubject":
                return HTMLHelper.HTMLEncode(MacroResolver.RemoveSecurityParameters(parameter.ToString(), true, null));

            case "issueopenedemails":
                return GetOpenedEmails(parameter as DataRowView);

            case "viewclickedlinks":
                if (sender is ImageButton)
                {
                    ImageButton imageButton = sender as ImageButton;
                    GridViewRow gvr = parameter as GridViewRow;
                    if (gvr != null)
                    {
                        DataRowView drv = gvr.DataItem as DataRowView;
                        if (drv != null)
                        {
                            int count = ValidationHelper.GetInteger(drv["LinkCount"], 0);
                            if ((count == 0) || !mOnlineMarketingEnabled)
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


    /// <summary>
    /// Handles the UniGrid's OnAction event.
    /// </summary>
    /// <param name="actionName">Name of item (button) that throws event</param>
    /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
    protected void uniGrid_OnAction(string actionName, object actionArgument)
    {
        switch (actionName)
        {
            case "delete":
                DeleteIssue(ValidationHelper.GetInteger(actionArgument, 0));
                break;
        }
    }


    /// <summary>
    /// Gets a clickable opened emails counter based on the values from datasource.
    /// </summary>
    /// <param name="rowView">A <see cref="DataRowView" /> that represents one row from UniGrid's source</param>
    /// <returns>A link with detailed statistics about opened emails</returns>
    private string GetOpenedEmails(DataRowView rowView)
    {
        // Get issue ID
        int issueId = ValidationHelper.GetInteger(DataHelper.GetDataRowViewValue(rowView, "IssueID"), 0);

        // Get opened emails count from issue record
        int openedEmails = ValidationHelper.GetInteger(DataHelper.GetDataRowViewValue(rowView, "IssueOpenedEmails"), 0);
        if (mOnlineMarketingEnabled)
        {
            // Get number of emails opened by contact group members
            openedEmails += OpenedEmailInfoProvider.GetContactGroupOpens(issueId);
        }

        if (openedEmails > 0)
        {
            return string.Format(@"<a href=""#"" onclick=""ShowOpenedBy({0})"">{1}</a>", issueId, openedEmails);
        }
        else
        {
            return "0";
        }
    }


    /// <summary>
    /// Deletes an issue specified by its ID (if authorized).
    /// </summary>
    /// <param name="issueId">Issue's ID</param>
    private static void DeleteIssue(int issueId)
    {
        // Delete issue from database (if authorized)
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.newsletter", "authorissues"))
        {
            RedirectToCMSDeskAccessDenied("cms.newsletter", "authorissues");
        }

        // Delete issue
        IssueProvider.DeleteIssue(issueId);
    }

    #endregion
}
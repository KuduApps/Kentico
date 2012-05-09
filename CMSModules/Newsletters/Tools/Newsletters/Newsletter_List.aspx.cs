using System;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.Newsletter;
using CMS.Scheduler;
using CMS.SettingsProvider;
using CMS.UIControls;

public partial class CMSModules_Newsletters_Tools_Newsletters_Newsletter_List : CMSNewsletterNewslettersPage
{
    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // New record link
        string[,] actions = new string[1, 6];
        actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
        actions[0, 1] = GetString("Newsletter_List.NewItemCaption");
        actions[0, 2] = null;
        actions[0, 3] = ResolveUrl("Newsletter_New.aspx");
        actions[0, 4] = null;
        actions[0, 5] = GetImageUrl("Objects/Newsletter_Newsletter/add.png");

        // Setup Master page
        this.CurrentMaster.HeaderActions.Actions = actions;

        // Setup UniGrid
        UniGrid.OnAction += new OnActionEventHandler(uniGrid_OnAction);
        UniGrid.WhereCondition = "NewsletterSiteID=" + CMSContext.CurrentSiteID;
        UniGrid.ZeroRowsText = GetString("general.nodatafound");
    }


    /// <summary>
    /// Increment counter at the end of string.
    /// </summary>
    /// <param name="s">String</param>
    /// <param name="lpar">Left parathenses</param>
    /// <param name="rpar">Right parathenses</param>
    string Increment(string s, string lpar, string rpar)
    {
        int i = 1;
        s = s.Trim();
        if ((rpar == String.Empty) || s.EndsWith(rpar))
        {
            int leftpar = s.LastIndexOf(lpar);
            if (lpar == rpar)
            {
                leftpar = s.LastIndexOf(lpar, leftpar - 1);
            }

            if (leftpar >= 0)
            {
                i = ValidationHelper.GetSafeInteger(s.Substring(leftpar + lpar.Length, s.Length - leftpar - lpar.Length - rpar.Length), 0);
                if (i > 0) // Remove parathenses only if parathenses found
                {
                    s = s.Remove(leftpar);
                }
                i++;
            }
        }

        s += lpar + i + rpar;
        return s;
    }


    /// <summary>
    /// Handles the UniGrid's OnAction event.
    /// </summary>
    /// <param name="actionName">Name of item (button) that throws event</param>
    /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
    protected void uniGrid_OnAction(string actionName, object actionArgument)
    {
        switch (actionName.ToLower())
        {
            case "edit":
                URLHelper.Redirect("Newsletter_Frameset.aspx?newsletterid=" + Convert.ToString(actionArgument));
                break;

            case "delete":
                if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.newsletter", "configure"))
                {
                    RedirectToCMSDeskAccessDenied("cms.newsletter", "configure");
                }
                // delete Newsletter object from database
                NewsletterProvider.DeleteNewsletter(ValidationHelper.GetInteger(actionArgument, 0));
                break;

            case "clone":
                if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.newsletter", "configure"))
                {
                    RedirectToCMSDeskAccessDenied("cms.newsletter", "configure");
                }

                Newsletter oldnl = NewsletterProvider.GetNewsletter(ValidationHelper.GetInteger(actionArgument, 0));
                if (oldnl != null)
                {
                    // Create new newsletter
                    Newsletter nl = oldnl.Clone(true);

                    // Find unique newsletter code name
                    string newsletterDisplayName = nl.NewsletterDisplayName;
                    string newsletterName = nl.NewsletterName;

                    while (NewsletterProvider.GetNewsletter(newsletterName, nl.NewsletterSiteID) != null)
                    {
                        newsletterName = Increment(newsletterName, "_", "");
                        newsletterDisplayName = Increment(newsletterDisplayName, "(", ")");
                    }

                    nl.NewsletterName = newsletterName;
                    nl.NewsletterDisplayName = newsletterDisplayName;

                    if (NewsletterProvider.LicenseVersionCheck(CMSContext.CurrentSite.DomainName, FeatureEnum.Newsletters, VersionActionEnum.Insert))
                    {
                        // Clone scheduled task
                        if (oldnl.NewsletterDynamicScheduledTaskID > 0)
                        {
                            TaskInfo oldti = TaskInfoProvider.GetTaskInfo(oldnl.NewsletterDynamicScheduledTaskID);
                            if (oldti != null)
                            {
                                // Create new task
                                TaskInfo ti = oldti.Clone(true);

                                ti.TaskDisplayName = GetString("DynamicNewsletter.TaskName") + nl.NewsletterDisplayName;

                                // Save the task
                                TaskInfoProvider.SetTaskInfo(ti);

                                nl.NewsletterDynamicScheduledTaskID = ti.TaskID;
                            }
                        }

                        NewsletterProvider.SetNewsletter(nl);
                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.Text = GetString("licenseversioncheck.newsletterclone");
                    }
                }
                break;
        }
    }

    #endregion
}
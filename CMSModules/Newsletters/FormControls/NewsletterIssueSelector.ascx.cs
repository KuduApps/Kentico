using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.Newsletter;
using CMS.UIControls;

public partial class CMSModules_Newsletters_FormControls_NewsletterIssueSelector : CMS.FormControls.FormEngineUserControl
{
    #region "Variables"

    private int mValue = 0;

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets or sets the enabled state of the control.
    /// </summary>
    public override bool Enabled
    {
        get
        {
            return base.Enabled;
        }
        set
        {
            this.EnsureChildControls();
            base.Enabled = value;
            this.usNewsletters.Enabled = value;
            this.usIssues.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets field value.
    /// </summary>
    public override object Value
    {
        get
        {
            return (ValidationHelper.GetInteger(usIssues.Value, 0) == 0 ? null : usIssues.Value);
        }
        set
        {
            mValue = ValidationHelper.GetInteger(value, 0);
        }
    }


    #endregion


    #region "Methods"

    protected override void CreateChildControls()
    {
        if (usNewsletters == null)
        {
            pnlUpdate.LoadContainer();
        }
        base.CreateChildControls();
        if (!StopProcessing)
        {
            ReloadData();
        }
    }


    /// <summary>
    /// Reloads dropdown lists.
    /// </summary>
    protected void ReloadData()
    {
        usNewsletters.WhereCondition = "NewsletterSiteID = " + CMSContext.CurrentSiteID;
        usNewsletters.ButtonRemoveSelected.CssClass = "XLongButton";
        usNewsletters.ButtonAddItems.CssClass = "XLongButton";
        usNewsletters.ReturnColumnName = "NewsletterID";
        usNewsletters.DropDownSingleSelect.SelectedIndexChanged += new EventHandler(DropDownSingleSelect_SelectedIndexChanged);
        usNewsletters.DropDownSingleSelect.AutoPostBack = true;

        usIssues.WhereCondition = GetIssueWhereCondition(usNewsletters.Value);
        usIssues.ButtonRemoveSelected.CssClass = "XLongButton";
        usIssues.ButtonAddItems.CssClass = "XLongButton";
        usIssues.ReturnColumnName = "IssueID";

        // Initialize both dropdown lists according to incoming issue ID
        if (!RequestHelper.IsPostBack())
        {
            if (mValue > 0)
            {
                // Retrieve newsletter ID from issue info
                Issue issue = IssueProvider.GetIssue(mValue);
                int issueNewsletterID = 0;
                if (issue != null)
                {
                    issueNewsletterID = issue.IssueNewsletterID;
                }
                usNewsletters.Value = issueNewsletterID;
                usIssues.WhereCondition = GetIssueWhereCondition(issueNewsletterID);
                usIssues.Reload(true);
                usIssues.DropDownSingleSelect.SelectedValue = mValue.ToString();
                usIssues.Value = mValue;
            }
        }
    }


    protected void DropDownSingleSelect_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Reload issues dropdown list
        usIssues.WhereCondition = GetIssueWhereCondition(usNewsletters.Value);
        usIssues.Reload(true);
        mValue = 0;
    }


    /// <summary>
    /// Returns WHERE condition for issue dropdown list
    /// </summary>
    /// <param name="value">Newsletter ID</param>
    private string GetIssueWhereCondition(object value)
    {
        return "IssueNewsletterID=" + ValidationHelper.GetInteger(value, 0);
    }

    #endregion
}

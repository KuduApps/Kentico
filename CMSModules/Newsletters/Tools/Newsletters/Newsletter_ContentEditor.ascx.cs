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
using System.Xml;
using System.Text;

using CMS.Newsletter;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.Controls;
using CMS.CMSHelper;
using CMS.UIControls;

public partial class CMSModules_Newsletters_Tools_Newsletters_Newsletter_ContentEditor : CMSUserControl
{
    #region "Variables"

    private int mIssueID = 0;
    private int mNewsletterID = 0;
    protected string frameSrc = string.Empty;
    private bool mIsNewIssue = false;

    #endregion


    #region "Properties"

    /// <summary>
    /// Issue ID.
    /// </summary>
    public int IssueID
    {
        get
        {
            return mIssueID;
        }
        set
        {
            mIssueID = value;
        }
    }


    /// <summary>
    /// Newsletter ID.
    /// </summary>
    public int NewsletterID
    {
        get
        {
            return mNewsletterID;
        }
        set
        {
            mNewsletterID = value;
        }
    }


    /// <summary>
    /// True indicates that new issue is created otherwise existing one is edited. Default value is false.
    /// </summary>
    public bool IsNewIssue
    {
        get
        {
            return mIsNewIssue;
        }
        set
        {
            mIsNewIssue = value;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        frameSrc = "Newsletter_Iframe_Edit.aspx?newsletterid=" + NewsletterID + "&issueid=" + IssueID;

        if (QueryHelper.GetBoolean("saved", false))
        {
            frameSrc += "&saved=1";
        }
        if (IsNewIssue)
        {
            frameSrc += "&new=1";
        }
    }

    #endregion
}

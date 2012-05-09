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

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.TreeEngine;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.Blogs;
using CMS.SettingsProvider;

public partial class CMSModules_Blogs_Controls_CommentFilter : CMSUserControl
{
    #region "Variables"

    private SiteInfo currentSite = null;
    private CurrentUserInfo currentUser = null;
    private bool mDisplayAllRecord = true;
    private bool mIsInMydesk = false;

    #endregion


    #region "Events"


    /// <summary>
    /// Event which raises when the search button is clicked.
    /// </summary>
    public event EventHandler SearchPerformed;


    /// <summary>
    /// Raises the action performed event.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void RaiseSearchPerformed(Object sender, EventArgs e)
    {
        if (SearchPerformed != null)
        {
            SearchPerformed(sender, e);
        }
    }


    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets the value which determines whether to display (all) record in Blog dropdown.
    /// </summary>
    public bool DisplayAllRecord
    {
        get
        {
            return this.mDisplayAllRecord;
        }
        set
        {
            this.mDisplayAllRecord = value;
        }
    }


    /// <summary>
    /// Gets the Blog part of the WHERE conditon.
    /// </summary>
    public string BlogWhereCondition
    {
        get
        {
            string blogWhere = "";

            string val = ValidationHelper.GetString(this.uniSelector.Value, "");
            if (val == "")
            {
                val = (this.DisplayAllRecord ? "##ALL##" : "##MYBLOGS##");
            }

            // Blogs dropdownlist
            switch (val)
            {
                case "##ALL##":
                    // If current user isn't Global admin or user with 'Manage' permissions for blogs
                    if (!currentUser.IsAuthorizedPerResource("cms.blog", "Manage"))
                    {
                        blogWhere = "(NodeOwner=" + currentUser.UserID +
                            " OR (';' + BlogModerators + ';' LIKE N'%;" + SqlHelperClass.GetSafeQueryString(currentUser.UserName, false) + ";%'))";
                    }
                    break;

                case "##MYBLOGS##":
                    blogWhere = "NodeOwner = " + currentUser.UserID;
                    break;

                default:
                    blogWhere = "BlogID = " + ValidationHelper.GetInteger(this.uniSelector.Value, 0);
                    break;
            }

            return blogWhere;
        }
    }


    /// <summary>
    /// Gets the Comment part of the WHERE conditon.
    /// </summary>
    public string CommentWhereCondition
    {
        get
        {
            string where = "";

            // Approved dropdownlist
            if (drpApproved.SelectedIndex > 0)
            {
                switch (drpApproved.SelectedValue)
                {
                    case "YES":
                        where += " CommentApproved = 1 AND";
                        break;

                    case "NO":
                        where += " (CommentApproved = 0 OR CommentApproved IS NULL ) AND";
                        break;
                }
            }
            // Spam dropdownlist
            if (drpSpam.SelectedIndex > 0)
            {
                switch (drpSpam.SelectedValue)
                {
                    case "YES":
                        where += " CommentIsSpam = 1 AND";
                        break;

                    case "NO":
                        where += " (CommentIsSpam = 0 OR CommentIsSpam IS NULL) AND";
                        break;
                }
            }
            if (txtUserName.Text.Trim() != "")
            {
                where += " CommentUserName LIKE '%" + txtUserName.Text.Trim().Replace("'", "''") + "%' AND";
            }
            if (txtComment.Text.Trim() != "")
            {
                where += " CommentText LIKE '%" + txtComment.Text.Trim().Replace("'", "''") + "%' AND";
            }
            if (where != "")
            {
                where = where.Remove(where.Length - 4); // 4 = " AND".Length
            }

            return where;
        }
    }


    /// <summary>
    /// Gets the filter query string.
    /// </summary>
    public string FilterQueryString
    {
        get
        {
            return "&user=" + HTMLHelper.HTMLEncode(this.txtUserName.Text) +
                   "&comment=" + HTMLHelper.HTMLEncode(this.txtComment.Text) +
                   "&approved=" + this.drpApproved.SelectedItem.Value +
                   "&isspam=" + this.drpSpam.SelectedItem.Value;
        }
    }


    /// <summary>
    /// Indicates if controls is in MyDesk section.
    /// </summary>
    public bool IsInMydesk
    {
        get
        {
            return this.mIsInMydesk;
        }
        set
        {
            this.mIsInMydesk = value;
        }
    }

    #endregion


    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        currentSite = CMSContext.CurrentSite;
        currentUser = CMSContext.CurrentUser;

        bool manageBlogs = false;
        // Check 'Manage' permission
        if (currentUser.IsAuthorizedPerResource("cms.blog", "Manage"))
        {
            manageBlogs = true;
        }

        string where = "(NodeSiteID = " + currentSite.SiteID + ")";
        if (!((currentUser.IsGlobalAdministrator) || (manageBlogs)))
        {
            where += " AND " + BlogHelper.GetBlogsWhere(currentUser.UserID, currentUser.UserName, null);
        }

        this.uniSelector.DisplayNameFormat = "{%BlogName%}";
        this.uniSelector.SelectionMode = SelectionModeEnum.SingleDropDownList;
        this.uniSelector.WhereCondition = where;
        this.uniSelector.ReturnColumnName = "BlogID";
        this.uniSelector.ObjectType = "cms.blog";
        this.uniSelector.ResourcePrefix = "unisiteselector";
        this.uniSelector.AllowEmpty = false;
        this.uniSelector.AllowAll = false;

        if (this.DisplayAllRecord)
        {
            this.uniSelector.SpecialFields = new string[,] { { GetString("general.selectall"), "##ALL##" },
                                                             { GetString("myblogs.comments.selectmyblogs"), "##MYBLOGS##" }};
        }
        else
        {
            this.uniSelector.SpecialFields = new string[,] { { GetString("myblogs.comments.selectmyblogs"), "##MYBLOGS##" } };
        }

        if (IsInMydesk && !RequestHelper.IsPostBack())
        {
            uniSelector.Value = "##MYBLOGS##";
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.StopProcessing)
        {
            this.Visible = false;
            this.uniSelector.StopProcessing = true;
        }
        else
        {
            this.btnFilter.Text = GetString("General.Show");

            if (!RequestHelper.IsPostBack())
            {
                // Fill dropdowns
                HandleDropdowns();

                // Preselect filter data
                PreselectFilter();
            }
        }
    }


    protected void btnFilter_Click(object sender, EventArgs e)
    {
        RaiseSearchPerformed(null, null);
    }


    protected void HandleDropdowns()
    {
        // Filter approved dropdown
        drpApproved.Items.Add(new ListItem(GetString("general.selectall"), "ALL"));
        drpApproved.Items.Add(new ListItem(GetString("general.yes"), "YES"));
        drpApproved.Items.Add(new ListItem(GetString("general.no"), "NO"));

        drpApproved.SelectedValue = QueryHelper.GetString("approved", (IsInMydesk ? "NO" : "ALL"));
        
        // Filter spam dropdown
        drpSpam.Items.Add(new ListItem(GetString("general.selectall"), "ALL"));
        drpSpam.Items.Add(new ListItem(GetString("general.yes"), "YES"));
        drpSpam.Items.Add(new ListItem(GetString("general.no"), "NO"));
    }


    /// <summary>
    /// Gets the information on last selected filter configuration and pre-selects the actual values.
    /// </summary>
    private void PreselectFilter()
    {
        string username = QueryHelper.GetString("user", "");
        string comment = QueryHelper.GetString("comment", "");
        string approved = QueryHelper.GetString("approved", "");
        string isspam = QueryHelper.GetString("isspam", "");

        if (username != "")
        {
            this.txtUserName.Text = username;
        }

        if (comment != "")
        {
            this.txtComment.Text = comment;
        }

        if (approved != "")
        {
            if (this.drpApproved.Items.Count > 0)
            {
                this.drpApproved.SelectedValue = approved;
            }
        }

        if (isspam != "")
        {
            if (this.drpSpam.Items.Count > 0)
            {
                this.drpSpam.SelectedValue = isspam;
            }
        }
    }
}

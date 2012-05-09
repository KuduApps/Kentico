using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using CMS.Forums;
using CMS.GlobalHelper;
using CMS.CMSHelper;

public partial class CMSModules_Forums_Controls_ThreadMove : ForumViewer
{

    #region "Public properties"

    /// <summary>
    /// Gets the current thread id if ForumCOntext.CurrentThread is not available e.g. in tree layout.
    /// </summary>
    public int CurrentThread
    {
        get
        {
            return QueryHelper.GetInteger("moveto", this.SelectedThreadID);
        }
    }


    /// <summary>
    /// Gets or sets the thread id.
    /// </summary>
    public int SelectedThreadID
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("SelectedThreadID"), 0);
        }
        set
        {
            this.SetValue("SelectedThreadID", value);
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether controls is in administration mode.
    /// </summary>
    public bool AdminMode
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("AdminMode"), false);
        }
        set
        {
            this.SetValue("AdminMode", value);
        }
    }

    #endregion


    /// <summary>
    /// Occurs when thread  is moved.
    /// </summary>
    public event EventHandler TopicMoved;


    protected void Page_Load(object sender, EventArgs e)
    {
        if ((ForumContext.CurrentMode == ForumMode.TopicMove) || (this.AdminMode))
        {
            CopyValuesFromParent(this);

            this.btnMove.Click += new EventHandler(btnMove_Click);

            if (!RequestHelper.IsPostBack())
            {
                LoadMoveTopicDropdown();
            }
            else
            {
                // Display message if no forum exists
                if (drpMoveToForum.Items.Count == 0)
                {
                    plcMoveInner.Visible = false;
                    lblMoveInfo.Visible = true;
                    lblMoveInfo.Text = GetString("Forums.NoForumToMoveIn");
                }
            }
        }
    }


    /// <summary>
    /// Topic move action handler.
    /// </summary>
    protected void btnMove_Click(object sender, EventArgs e)
    {
        int forumId = ValidationHelper.GetInteger(this.drpMoveToForum.SelectedValue, 0);
        if (forumId > 0)
        {
            ForumPostInfo fpi = ForumContext.CurrentThread;
            if ((fpi == null) && (this.CurrentThread > 0))
            {
                fpi = ForumPostInfoProvider.GetForumPostInfo(this.CurrentThread);
            }

            if (fpi != null)
            {
                // Move the thread
                ForumPostInfoProvider.MoveThread(fpi, forumId);

                this.plcMoveInner.Visible = false;

                // Generate back button
                this.ltlMoveBack.Text = GetLink(null, GetString("general.back"), "ActionLink", ForumActionType.Thread);

                string targetForumName = this.drpMoveToForum.SelectedItem.Text.TrimStart(' ');
                ForumInfo fi = ForumInfoProvider.GetForumInfo(forumId);
                if (fi != null)
                {
                    targetForumName = HTMLHelper.HTMLEncode(fi.ForumDisplayName);
                }

                // Display info
                this.lblMoveInfo.Text = String.Format(GetString("forum.thread.topicmoved"), targetForumName);
                this.lblMoveInfo.Visible = true;

                this.SetValue("TopicMoved", true);

                if (TopicMoved != null)
                {
                    TopicMoved(this, null);
                }
            }

        }
        else
        {
            this.lblMoveError.Text = GetString("forum.thread.movetopic.selectforum");
            this.lblMoveError.Visible = true;
        }
    }


    /// <summary>
    /// Loads the forums dropdownlist for topic move.
    /// </summary>
    private void LoadMoveTopicDropdown()
    {

        if (this.drpMoveToForum.Items.Count > 0)
        {
            return;
        }

        lblMoveInfo.Visible = false;

        int currentForumId = 0;
        ForumPostInfo fpi = ForumContext.CurrentThread;
        if ((fpi == null) && (this.CurrentThread > 0))
        {
            fpi = ForumPostInfoProvider.GetForumPostInfo(this.CurrentThread);
        }


        if (fpi != null)
        {
            bool isOk = AdminMode ? true : ((ForumContext.CurrentForum != null) && (ForumContext.CurrentForum.ForumID == fpi.PostForumID));
            if (isOk)
            {
                currentForumId = fpi.PostForumID;

                ForumInfo fi = ForumInfoProvider.GetForumInfo(currentForumId);
                if (fi != null)
                {
                    ForumGroupInfo fgi = ForumGroupInfoProvider.GetForumGroupInfo(fi.ForumGroupID);
                    if (fgi != null)
                    {
                        string where = null;
                        DataSet dsGroups = null;
                        if (fgi.GroupGroupID > 0)
                        {
                            where = "GroupName NOT LIKE 'AdHoc%' AND GroupSiteID = " + this.SiteID + " AND GroupGroupID = " + fgi.GroupGroupID;
                        }
                        else
                        {
                            where = "GroupName NOT LIKE 'AdHoc%' AND GroupSiteID = " + this.SiteID + " AND GroupGroupID IS NULL";
                        }
                        dsGroups = ForumGroupInfoProvider.GetGroups(where, "GroupDisplayName", 0, "GroupID, GroupDisplayName");

                        if (!DataHelper.DataSourceIsEmpty(dsGroups))
                        {
                            Hashtable forums = new Hashtable();

                            // Get all forums for selected groups
                            string groupWhere = "AND ForumGroupID IN (SELECT GroupID FROM Forums_ForumGroup WHERE " + where + ") ";
                            DataSet dsForums = ForumInfoProvider.GetForums(" ForumOpen = 1 " + " AND NOT ForumID = " + currentForumId + groupWhere, "ForumDisplayName", 0, "ForumID, ForumDisplayName, ForumGroupID");

                            if (!DataHelper.DataSourceIsEmpty(dsForums))
                            {
                                // Load forums into hash table
                                foreach (DataRow drForum in dsForums.Tables[0].Rows)
                                {
                                    int groupId = Convert.ToInt32(drForum["ForumGroupID"]);
                                    List<string[]> forumNames = forums[groupId] as List<string[]>;
                                    if (forumNames == null)
                                    {
                                        forumNames = new List<string[]>();
                                    }

                                    forumNames.Add(new string[] { Convert.ToString(drForum["ForumDisplayName"]), Convert.ToString(drForum["ForumID"]) });
                                    forums[groupId] = forumNames;
                                }
                            }

                            foreach (DataRow dr in dsGroups.Tables[0].Rows)
                            {
                                int groupId = Convert.ToInt32(dr["GroupId"]);

                                List<string[]> forumNames = forums[groupId] as List<string[]>;
                                if (forumNames != null)
                                {
                                    // Add forum group item if some forum
                                    ListItem li = new ListItem(Convert.ToString(dr["GroupDisplayName"]), "0");
                                    li.Attributes.Add("disabled", "disabled");
                                    this.drpMoveToForum.Items.Add(li);

                                    // Add forum items
                                    foreach (string[] forum in forumNames)
                                    {
                                        // Add forum to DDL
                                        this.drpMoveToForum.Items.Add(new ListItem(" \xA0\xA0\xA0\xA0 " + forum[0], forum[1]));
                                    }
                                }
                            }


                            // Display message if no forum exists
                            if (drpMoveToForum.Items.Count == 0)
                            {
                                plcMoveInner.Visible = false;
                                lblMoveInfo.Visible = true;
                                lblMoveInfo.Text = GetString("Forums.NoForumToMoveIn");
                            }
                        }
                    }
                }
            }
        }
    }
}

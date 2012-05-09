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

using CMS.PortalControls;
using CMS.GlobalHelper;
using CMS.TreeEngine;
using CMS.CMSHelper;
using CMS.Polls;
using CMS.WebAnalytics;
using CMS.Community;

public partial class CMSWebParts_Community_Polls_GroupPoll : CMSAbstractWebPart
{
    #region "Properties"

    /// <summary>
    /// Gets or sets the code name of the poll, which should be displayed.
    /// </summary>
    public string PollCodeName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("PollCodeName"), "");
        }
        set
        {
            this.SetValue("PollCodeName", value);
        }
    }


    /// <summary>
    /// Gets or sets the community group name.
    /// </summary>
    public string GroupName
    {
        get
        {
            string groupName = ValidationHelper.GetString(GetValue("GroupName"), "");
            if ((string.IsNullOrEmpty(groupName) || groupName == GroupInfoProvider.CURRENT_GROUP) && (CommunityContext.CurrentGroup != null))
            {
                return CommunityContext.CurrentGroup.GroupName;
            }
            return groupName;
        }
        set
        {
            SetValue("GroupName", value);
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether the graph of the poll is displayed.
    /// </summary>
    public bool ShowGraph
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("ShowGraph"), this.viewPoll.ShowGraph);
        }
        set
        {
            this.SetValue("ShowGraph", value);
        }
    }


    /// <summary>
    /// Gets or sets the type of the representation of the answers’ count in the graph.
    /// </summary>
    public CountTypeEnum CountType
    {
        get
        {
            int countTypeInt = ValidationHelper.GetInteger(this.GetValue("CountType"), 0);
            if (countTypeInt == 1)
            {
                return CountTypeEnum.Absolute;
            }
            else if (countTypeInt == 2)
            {
                return CountTypeEnum.Percentage;
            }
            else
            {
                return CountTypeEnum.None;
            }
        }
        set
        {
            if (value == CountTypeEnum.Absolute)
            {
                this.SetValue("CountType", 1);
            }
            else if (value == CountTypeEnum.Percentage)
            {
                this.SetValue("CountType", 2);
            }
            else
            {
                this.SetValue("CountType", 0);
            }
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether the graph is displayed after answering the poll.
    /// </summary>
    public bool ShowResultsAfterVote
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("ShowResultsAfterVote"), this.viewPoll.ShowResultsAfterVote);
        }
        set
        {
            this.SetValue("ShowResultsAfterVote", value);
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether check if current user has voted.
    /// </summary>
    public bool CheckVoted
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("CheckVoted"), this.viewPoll.CheckVoted);
        }
        set
        {
            this.SetValue("CheckVoted", value);
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether the permissions are checked.
    /// </summary>
    public bool CheckPermissions
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("CheckPermissions"), this.viewPoll.CheckPermissions);
        }
        set
        {
            this.SetValue("CheckPermissions", value);
            viewPoll.CheckPermissions = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether the control hides when not authorized, 
    /// otherwise the control displays the message and does not allow to vote.
    /// </summary>
    public bool HideWhenNotAuthorized
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("HideWhenNotAuthorized"), this.viewPoll.HideWhenNotAuthorized);
        }
        set
        {
            this.SetValue("HideWhenNotAuthorized", value);
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether the control hides when not opened, 
    /// otherwise the control does not allow to vote.
    /// </summary>
    public bool HideWhenNotOpened
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("HideWhenNotOpened"), this.viewPoll.HideWhenNotOpened);
        }
        set
        {
            this.SetValue("HideWhenNotOpened", value);
        }
    }


    /// <summary>
    /// Gets or sets the text of the vote button.
    /// </summary>
    public string ButtonText
    {
        get
        {
            return DataHelper.GetNotEmpty(this.GetValue("ButtonText"), this.viewPoll.ButtonText);
        }
        set
        {
            this.SetValue("ButtonText", value);
        }
    }


    /// <summary>
    /// Gets or sets the conversion track name used after successful registration.
    /// </summary>
    public string TrackConversionName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("TrackConversionName"), "");
        }
        set
        {
            if (value.Length > 400)
            {
                value = value.Substring(0, 400);
            }
            this.SetValue("TrackConversionName", value);
        }
    }


    /// <summary>
    /// Gets or sets the conversion value used after successful registration.
    /// </summary>
    public double ConversionValue
    {
        get
        {
            return ValidationHelper.GetDouble(this.GetValue("ConversionValue"), 0);
        }
        set
        {
            this.SetValue("ConversionValue", value);
        }
    }

    #endregion


    /// <summary>
    /// Content loaded event handler.
    /// </summary>
    public override void OnContentLoaded()
    {
        base.OnContentLoaded();
        SetupControl();
    }


    /// <summary>
    /// Initializes the control properties.
    /// </summary>
    protected void SetupControl()
    {
        if (this.StopProcessing)
        {
            // Do nothing
            viewPoll.Visible = false;
        }
        else
        {
            GroupInfo gi = GroupInfoProvider.GetGroupInfo(this.GroupName, CMSContext.CurrentSiteName);
            if (gi != null)
            {
                viewPoll.ControlContext = ControlContext;
                viewPoll.PollCodeName = PollCodeName;
                viewPoll.CheckPermissions = CheckPermissions;
                viewPoll.CheckVoted = CheckVoted;
                viewPoll.CountType = CountType;
                viewPoll.CacheMinutes = CacheMinutes;
                viewPoll.HideWhenNotAuthorized = HideWhenNotAuthorized;
                viewPoll.ShowGraph = ShowGraph;
                viewPoll.ShowResultsAfterVote = ShowResultsAfterVote;
                viewPoll.HideWhenNotOpened = HideWhenNotOpened;
                viewPoll.ButtonText = ButtonText;
                viewPoll.OnAfterVoted += viewPoll_OnAfterVoted;
                viewPoll.PollSiteID = CMSContext.CurrentSiteID;
                viewPoll.PollGroupID = gi.GroupID;
            }
            else
            {
                viewPoll.Visible = false;
            }
        }
    }


    /// <summary>
    /// After voted event.
    /// </summary>
    void viewPoll_OnAfterVoted(object sender, EventArgs e)
    {
        // Log track conversion
        if (!String.IsNullOrEmpty(TrackConversionName))
        {
            string siteName = CMSContext.CurrentSiteName;

            if (AnalyticsHelper.AnalyticsEnabled(siteName) && AnalyticsHelper.TrackConversionsEnabled(siteName) && !AnalyticsHelper.IsIPExcluded(siteName, HTTPHelper.UserHostAddress))
            {
                HitLogProvider.LogConversions(siteName, CMSContext.PreferredCultureCode, TrackConversionName, 0, ConversionValue);
            }
        }
    }


    /// <summary>
    /// OnPrerender override (Set visibility).
    /// </summary>
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        this.Visible = this.viewPoll.Visible;
    }


    /// <summary>
    /// Reloads the control data.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();

        SetupControl();
        this.viewPoll.ReloadData(true);
    }
}

using System;

using CMS.ExtendedControls;
using CMS.GlobalHelper;
using CMS.Polls;
using CMS.SettingsProvider;
using CMS.SiteProvider;

public partial class CMSModules_Polls_InlineControls_PollControl : InlineUserControl
{
    /// <summary>
    /// Poll code name.
    /// </summary>
    public string PollName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("PollName"), null);
        }
        set
        {
            this.SetValue("PollName", value);
            this.PollView1.PollCodeName = value;
        }
    }


    /// <summary>
    /// Poll site name.
    /// </summary>
    public string SiteName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("SiteName"), null);
        }
        set
        {
            this.SetValue("SiteName", value);
            this.PollView1.PollSiteID = GetSiteID();
        }
    }


    /// <summary>
    /// Poll group name.
    /// </summary>
    public string GroupName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("GroupName"), null);
        }
        set
        {
            this.SetValue("GroupName", value);
            this.PollView1.PollGroupID = GetGroupID();
        }
    }


    /// <summary>
    /// Type of the representation of the answers count in the graph.
    /// </summary>
    public CountTypeEnum CountType
    {
        get
        {
            string value = ValidationHelper.GetString(this.GetValue("CountType"), "absolute");
            switch (value.ToLower())
            {
                case "none":
                case "0":
                    return CountTypeEnum.None;
                case "percentage":
                case "2":
                    return CountTypeEnum.Percentage;
                default:
                    return CountTypeEnum.Absolute;
            }
        }
        set
        {
            this.SetValue("CountType", value.ToString().ToLower());
            this.PollView1.CountType = value;
        }
    }


    /// <summary>
    /// Control parameter.
    /// </summary>
    public override string Parameter
    {
        get
        {
            return this.PollName;
        }
        set
        {
            this.PollName = value;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        SetupControl();
    }


    /// <summary>
    /// Initializes the control properties.
    /// </summary>
    protected void SetupControl()
    {
        this.PollView1.PollCodeName = this.PollName;
        this.PollView1.PollSiteID = GetSiteID();
        this.PollView1.PollGroupID = GetGroupID();
        this.PollView1.CountType = CountType;
    }


    /// <summary>
    /// Returns site ID according to site name.
    /// </summary>
    protected int GetSiteID()
    {
        if (!string.IsNullOrEmpty(this.SiteName))
        {
            // Get site object
            SiteInfo site = SiteInfoProvider.GetSiteInfo(this.SiteName);
            if (site != null)
            {
                return site.SiteID;
            }
        }

        return 0;
    }


    /// <summary>
    /// Returns group ID according to group name.
    /// </summary>
    protected int GetGroupID()
    {
        if (!string.IsNullOrEmpty(this.GroupName))
        {
            // Get group object
            GeneralizedInfo group = ModuleCommands.CommunityGetGroupInfoByName(this.GroupName, this.SiteName);
            if (group != null)
            {
                // Get ID column value
                return ValidationHelper.GetInteger(group.GetValue(group.IDColumn), 0);
            }
        }

        return 0;
    }
}

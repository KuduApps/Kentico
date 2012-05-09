using System;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.SiteProvider;

public partial class CMSModules_Badges_Badges_Edit : SiteManagerPage
{
    int badgeId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        // Set the page title
        this.CurrentMaster.Title.TitleText = GetString("badge.newbadge");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_Badge/new.png");
        this.CurrentMaster.Title.HelpTopicName = "badge_edit";

        // Initialize breadcrumbs 		
        string[,] pageTitleTabs = new string[2, 3];
        pageTitleTabs[0, 0] = GetString("badge.title");
        pageTitleTabs[0, 1] = "~/CMSModules/Badges/Badges_List.aspx";
        pageTitleTabs[0, 2] = "";
        pageTitleTabs[1, 1] = "";
        pageTitleTabs[1, 2] = "";
        pageTitleTabs[1, 0] = GetString("badge.newbadge");

        // Initialize validators
        rfvName.ErrorMessage = GetString("general.invalidcodename");
        rfvDisplayName.ErrorMessage = GetString("badge.errors.displayname");
        rvtxtTopLimit.ErrorMessage = GetString("badge.errors.toplimit");

        // Get badge ID from query string
        badgeId = QueryHelper.GetInteger("badgeid", 0);
        if (badgeId > 0)
        {
            // Get BadgeInfo
            BadgeInfo bi = BadgeInfoProvider.GetBadgeInfo(badgeId);
            if (bi != null)
            {
                this.CurrentMaster.Title.TitleText = GetString("badge.properties");
                pageTitleTabs[1, 0] = HTMLHelper.HTMLEncode(bi.BadgeDisplayName);
                this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_Badge/object.png");

                // Load data
                if (!RequestHelper.IsPostBack())
                {
                    if (QueryHelper.GetInteger("saved", 0) == 1)
                    {
                        lblSaved.Visible = true;
                    }
                    this.LoadData(bi);
                }
            }
        }

        this.CurrentMaster.Title.Breadcrumbs = pageTitleTabs;
    }


    /// <summary>
    /// Loads data to fields.
    /// </summary>
    /// <param name="badgeId">ID of badge record</param>
    private void LoadData(BadgeInfo bi)
    {
        if (bi != null)
        {
            // Load data to appropriate fields
            txtName.Text = bi.BadgeName;
            txtDisplayName.Text = bi.BadgeDisplayName;
            txtImageURL.Text = bi.BadgeImageURL;
            chkIsAutomatic.Checked = bi.BadgeIsAutomatic;
            txtTopLimit.Text = bi.BadgeTopLimit.ToString();
        }
    }


    /// <summary>
    /// OK button click event handler.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        // Validate entered values
        string error = new Validator().IsCodeName(txtName.Text.Trim(), GetString("general.invalidcodename"))
            .NotEmpty(txtDisplayName.Text.Trim(), GetString("badge.errors.displayname")).Result;

        // Save new values
        if (DataHelper.IsEmpty(error))
        {
            // Check that code name is unique
            BadgeInfo bi = BadgeInfoProvider.GetBadgeInfo(txtName.Text.Trim());
            if ((bi == null) || ((bi != null) && (bi.BadgeID == badgeId)))
            {
                // Update existing record
                if (bi == null)
                {
                    bi = new BadgeInfo();
                }

                // Set properties
                bi.BadgeID = badgeId;
                bi.BadgeName = txtName.Text.Trim();
                bi.BadgeDisplayName = txtDisplayName.Text.Trim();
                bi.BadgeImageURL = txtImageURL.Text.Trim();
                int topLimit = ValidationHelper.GetInteger(txtTopLimit.Text.Trim(), 0);
                if (topLimit >= 0)
                {
                    bi.BadgeTopLimit = topLimit;
                }
                else
                {
                    bi.BadgeTopLimit = 0;
                }
                bi.BadgeIsAutomatic = chkIsAutomatic.Checked;

                // Save BadgeInfo
                BadgeInfoProvider.SetBadgeInfo(bi);
                URLHelper.Redirect("~/CMSModules/Badges/Badges_Edit.aspx?saved=1&badgeid=" + bi.BadgeID);
            }
            else
            {
                lblError.ResourceString = "badge.errors.uniquecodename";
                lblError.Visible = true;
            }
        }
        else
        {
            lblError.ResourceString = error;
            //badge.errors.values
            lblError.Visible = true;
        }
    }
}

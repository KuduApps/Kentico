using System;
using System.Data;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.FormControls;
using CMS.SiteProvider;

public partial class CMSModules_Badges_FormControls_BadgeSelector : FormEngineUserControl
{
    private string mValue = "";


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
            base.Enabled = value;
            this.drpBadges.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets field value.
    /// </summary>
    public override object Value
    {
        get
        {
            return drpBadges.SelectedValue;
        }
        set
        {
            mValue = ValidationHelper.GetString(value, "");
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.drpBadges.Items.Count == 0)
        {
            DataSet ds = BadgeInfoProvider.GetBadges(null, null, 0, "BadgeDisplayName, BadgeIsAutomatic, BadgeID");

            // Check dataset
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    BadgeInfo bi = new BadgeInfo(dr);

                    // Create and add custom table list item
                    string name = ResHelper.LocalizeString(bi.BadgeDisplayName);
                    if (bi.BadgeIsAutomatic)
                    {
                        name += GetString("badge.automatic");
                    }
                    
                    drpBadges.Items.Add(new ListItem(name, bi.BadgeID.ToString()));
                }

                drpBadges.Items.Insert(0, new ListItem(GetString("general.selectnone"), "0"));

                drpBadges.SelectedValue = mValue;
            }
        }
    }
}

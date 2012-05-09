using System;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.FormControls;
using CMS.EventLog;

public partial class CMSFormControls_System_EventLogTypeSelector : FormEngineUserControl
{
    #region "Variables"

    private string mSelectedValue = "";

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets or sets the filed value.
    /// </summary>
    public override object Value
    {
        get
        {
            return this.drpType.SelectedValue;
        }
        set
        {
            this.drpType.SelectedValue = mSelectedValue = (string)value;
        }
    }


    /// <summary>
    /// Gets or sets the enabled.
    /// </summary>
    public override bool Enabled
    {
        get
        {
            return drpType.Enabled;
        }
        set
        {
            drpType.Enabled = value;
        }
    }

    #endregion


    #region "Page events"

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        // Initialize filter dropdownlist
        if (!RequestHelper.IsPostBack())
        {
            if (drpType.Items.Count <= 0)
            {
                drpType.Items.Add(new ListItem(GetString("general.selectall"), ""));
                foreach (string eventType in new string[] { "e", "i", "w" })
                {
                    drpType.Items.Add(new ListItem(EventLogHelper.GetEventTypeText(eventType), eventType));
                }
            }
            this.drpType.SelectedValue = mSelectedValue;
        }
    }

    #endregion
}

using System;

using CMS.FormControls;

public partial class CMSModules_Notifications_FormControls_MultipleNotificationGatewaySelector : FormEngineUserControl
{
    #region "Properties"

    /// <summary>
    /// Gets or sets the field value.
    /// </summary>
    public override object Value
    {
        get
        {
            return uniSelector.Value;
        }
        set
        {
            if (uniSelector == null)
            {
                pnlUpdate.LoadContainer();
            }
            uniSelector.Value = value;
        }
    }


    /// <summary>
    /// Gets or sets state enable.
    /// </summary>
    public override bool Enabled
    {
        get
        {
            return base.Enabled;
        }
        set
        {
            if (uniSelector == null)
            {
                pnlUpdate.LoadContainer();
            }

            base.Enabled = value;
            uniSelector.Enabled = value;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        if (StopProcessing)
        {
            uniSelector.StopProcessing = true;
        }
        else
        {
            ReloadData();
        }
    }


    /// <summary>
    /// Reloads the data in the selector.
    /// </summary>
    public void ReloadData()
    {
        uniSelector.IsLiveSite = IsLiveSite;
        uniSelector.DisplayNameFormat = "{%GatewayDisplayName%}";
        uniSelector.ReturnColumnName = "GatewayName";
        uniSelector.ResourcePrefix = "notificationgatewayselector";
    }
}

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

using CMS.FormControls;
using CMS.Notifications;
using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSModules_Notifications_FormControls_NotificationGatewaySelector : FormEngineUserControl
{
    #region "Variables"

    private bool mUseGatewayNameForSelection = true;
    private bool mAddNoneRecord = false;
    private bool mUseAutoPostBack = false;

    #endregion


    #region "Public properties"

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
            if (this.uniSelector != null)
            {
                this.uniSelector.Enabled = value;
            }
        }
    }


    /// <summary>
    /// Returns ClientID of the dropdown with gateways.
    /// </summary>
    public override string ValueElementID
    {
        get
        {
            return this.uniSelector.DropDownSingleSelect.ClientID;
        }
    }


    /// <summary>
    /// Gets or sets the field value.
    /// </summary>
    public override object Value
    {
        get
        {
            if (this.mUseGatewayNameForSelection)
            {
                return this.GatewayName;
            }
            else
            {
                return this.GatewayID;
            }
        }
        set
        {
            if (this.mUseGatewayNameForSelection)
            {
                this.GatewayName = ValidationHelper.GetString(value, "");
            }
            else
            {
                this.GatewayID = ValidationHelper.GetInteger(value, 0);
            }
        }
    }


    /// <summary>
    /// Gets or sets the Gateway ID.
    /// </summary>
    public int GatewayID
    {
        get
        {
            if (this.mUseGatewayNameForSelection)
            {
                string name = ValidationHelper.GetString(this.uniSelector.Value, "");
                NotificationGatewayInfo ngi = NotificationGatewayInfoProvider.GetNotificationGatewayInfo(name);
                if (ngi != null)
                {
                    return ngi.GatewayID;
                }
                return 0;
            }
            else
            {
                return ValidationHelper.GetInteger(this.uniSelector.Value, 0);
            }
        }
        set
        {
            if (uniSelector == null)
            {
                this.pnlUpdate.LoadContainer();
            }

            if (this.mUseGatewayNameForSelection)
            {
                NotificationGatewayInfo ngi = NotificationGatewayInfoProvider.GetNotificationGatewayInfo(value);
                if (ngi != null)
                {
                    this.uniSelector.Value = ngi.GatewayName;
                }
            }
            else
            {
                this.uniSelector.Value = value;
            }
        }
    }


    /// <summary>
    /// Gets or sets the Gateway code name.
    /// </summary>
    public string GatewayName
    {
        get
        {
            if (this.mUseGatewayNameForSelection)
            {
                return ValidationHelper.GetString(this.uniSelector.Value, "");
            }
            else
            {
                int id = ValidationHelper.GetInteger(this.uniSelector.Value, 0);
                NotificationGatewayInfo ngi = NotificationGatewayInfoProvider.GetNotificationGatewayInfo(id);
                if (ngi != null)
                {
                    return ngi.GatewayName;
                }
                return "";
            }
        }
        set
        {
            if (uniSelector == null)
            {
                this.pnlUpdate.LoadContainer();
            }

            if (this.mUseGatewayNameForSelection)
            {
                this.uniSelector.Value = value;
            }
            else
            {
                NotificationGatewayInfo ngi = NotificationGatewayInfoProvider.GetNotificationGatewayInfo(value);
                if (ngi != null)
                {
                    this.uniSelector.Value = ngi.GatewayID;
                }
            }
        }
    }


    /// <summary>
    ///  If true, selected value is GatewayName, if false, selected value is GatewayID.
    /// </summary>
    public bool UseGatewayNameForSelection
    {
        get
        {
            return mUseGatewayNameForSelection;
        }
        set
        {
            mUseGatewayNameForSelection = value;
            if (this.uniSelector != null)
            {
                this.uniSelector.ReturnColumnName = (value ? "GatewayName" : "GatewayID");
            }
        }
    }


    /// <summary>
    /// Gets or sets the value which determines, whether to add none item record to the dropdownlist.
    /// </summary>
    public bool AddNoneRecord
    {
        get
        {
            return mAddNoneRecord;
        }
        set
        {
            mAddNoneRecord = value;
            if (this.uniSelector != null)
            {
                this.uniSelector.AllowEmpty = value;
            }
        }
    }


    /// <summary>
    /// Gets or sets the value which determines, whether the dropdown should use AutoPostBack.
    /// </summary>
    public bool UseAutoPostBack
    {
        get
        {
            return mUseAutoPostBack;
        }
        set
        {
            mUseAutoPostBack = value;
            if (this.uniSelector != null)
            {
                this.uniSelector.DropDownSingleSelect.AutoPostBack = value;
            }
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.StopProcessing)
        {
            this.uniSelector.StopProcessing = true;
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
        this.uniSelector.IsLiveSite = this.IsLiveSite;
        this.uniSelector.SelectionMode = SelectionModeEnum.SingleDropDownList;
        this.uniSelector.ReturnColumnName = (this.UseGatewayNameForSelection ? "GatewayName" : "GatewayID");
        this.uniSelector.DropDownSingleSelect.AutoPostBack = this.UseAutoPostBack;
        this.uniSelector.AllowEmpty = this.AddNoneRecord;
    }
}

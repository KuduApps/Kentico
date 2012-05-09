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

using CMS.GlobalHelper;
using CMS.Notifications;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.FormControls;

public partial class CMSModules_Notifications_FormControls_NotificationTemplateSelector : FormEngineUserControl
{
    #region "Variables"

    private bool mUseTemplateNameForSelection = true;
    private bool mAddGlobalTemplates = true;
    private bool mAddNoneRecord = true;

    private int mTemplateID = 0;
    private string mTemplateName = "";

    private int mSiteId = 0;

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
    /// Returns ClientID of the dropdown with templates.
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
            if (this.mUseTemplateNameForSelection)
            {
                return this.TemplateName;
            }
            else
            {
                return this.TemplateID;
            }
        }
        set
        {
            if (this.mUseTemplateNameForSelection)
            {
                this.TemplateName = ValidationHelper.GetString(value, "");
            }
            else
            {
                this.TemplateID = ValidationHelper.GetInteger(value, 0);
            }
        }
    }


    /// <summary>
    /// Gets or sets the Template ID.
    /// </summary>
    public int TemplateID
    {
        get
        {
            if (this.mUseTemplateNameForSelection)
            {
                string name = ValidationHelper.GetString(this.uniSelector.Value, "");
                NotificationTemplateInfo nti = NotificationTemplateInfoProvider.GetNotificationTemplateInfo(name, SiteId);
                if (nti != null)
                {
                    return nti.TemplateID;
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

            if (this.mUseTemplateNameForSelection)
            {
                NotificationTemplateInfo nti = NotificationTemplateInfoProvider.GetNotificationTemplateInfo(value);
                if (nti != null)
                {
                    this.mTemplateName = nti.TemplateName;
                    this.mTemplateID = nti.TemplateID;
                }
            }
            else
            {
                this.uniSelector.Value = value;
            }
        }
    }


    /// <summary>
    /// Gets or sets the Template code name.
    /// </summary>
    public string TemplateName
    {
        get
        {
            if (this.mUseTemplateNameForSelection)
            {
                return ValidationHelper.GetString(this.uniSelector.Value, "");
            }
            else
            {
                int id = ValidationHelper.GetInteger(this.uniSelector.Value, 0);
                NotificationTemplateInfo nti = NotificationTemplateInfoProvider.GetNotificationTemplateInfo(id);
                if (nti != null)
                {
                    return nti.TemplateName;
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

            if (this.mUseTemplateNameForSelection)
            {
                this.uniSelector.Value = value;
            }
            else
            {
                NotificationTemplateInfo nti = NotificationTemplateInfoProvider.GetNotificationTemplateInfo(value, SiteId);
                if (nti != null)
                {
                    this.uniSelector.Value = nti.TemplateID;
                }
            }
        }
    }


    /// <summary>
    ///  If true, selected value is TemplateName, if false, selected value is TemplateID.
    /// </summary>
    public bool UseTemplateNameForSelection
    {
        get
        {
            return mUseTemplateNameForSelection;
        }
        set
        {
            mUseTemplateNameForSelection = value;
            if (this.uniSelector != null)
            {
                this.uniSelector.ReturnColumnName = (value ? "TemplateName" : "TemplateID");
            }
        }
    }


    /// <summary>
    /// Gets or sets the value which determines, whether to add (global) record to the dropdownlist.
    /// </summary>
    public bool AddGlobalTemplates
    {
        get
        {
            return mAddGlobalTemplates;
        }
        set
        {
            mAddGlobalTemplates = value;
        }
    }


    /// <summary>
    /// Gets or sets SiteId value.
    /// </summary>
    public int SiteId
    {
        get
        {
            if (mSiteId == 0)
            {
                mSiteId = CMSContext.CurrentSite.SiteID;
            }
            return mSiteId;
        }
        set
        {
            mSiteId = value;
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
        this.uniSelector.ReturnColumnName = (this.UseTemplateNameForSelection ? "TemplateName" : "TemplateID");
        this.uniSelector.AllowEmpty = this.AddNoneRecord;
        if (this.AddGlobalTemplates)
        {
            this.uniSelector.SetValue("FilterMode", "notificationtemplateglobal");
        }
        else
        {
            this.uniSelector.SetValue("FilterMode", "notificationtemplate");
        }

        if (this.UseTemplateNameForSelection)
        {
            this.uniSelector.AllRecordValue = "";
            this.uniSelector.NoneRecordValue = "";
        }
    }
}
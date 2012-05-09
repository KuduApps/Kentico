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
using CMS.ExtendedControls;
using CMS.FormControls;
using CMS.FormEngine;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.SettingsProvider;
using CMS.UIControls;

public partial class CMSModules_BizForms_FormControls_SelectBizForm : FormEngineUserControl
{

    #region "Variables"

    private int mSiteID = 0;

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
    /// Returns ClientID of the textbox with selected bizforms.
    /// </summary>
    public override string ValueElementID
    {
        get
        {
            return this.uniSelector.TextBoxSelect.ClientID;
        }
    }


    /// <summary>
    /// Gets or sets the field value.
    /// </summary>
    public override object Value
    {
        get
        {
            return this.uniSelector.Value;
        }
        set
        {
            if (uniSelector == null)
            {
                this.pnlUpdate.LoadContainer();
            }
            this.uniSelector.Value = value;
        }
    }


    /// <summary>
    /// Gets or sets the ID of the site for which the bizforms should be returned. 0 means current site.
    /// </summary>
    public int SiteID
    {
        get
        {
            return this.mSiteID;
        }
        set
        {
            this.mSiteID = value;
        }
    }


    /// <summary>
    /// Gets or sets if site filter should be shown or not.
    /// </summary>
    public bool ShowSiteFilter
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("ShowSiteFilter"), true);
        }
        set
        {
            this.SetValue("ShowSiteFilter", value);
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.StopProcessing)
        {
            this.uniSelector.StopProcessing = true;
        }
        else
        {
            // If current control context is widget or livesite hide site selector
            if (ControlsHelper.CheckControlContext(this, ControlContext.WIDGET_PROPERTIES) || ControlsHelper.CheckControlContext(this, ControlContext.LIVE_SITE))
            {
                this.ShowSiteFilter = false;
            }

            ReloadData();
        }
    }


    /// <summary>
    /// Reloads the data in the selector.
    /// </summary>
    public void ReloadData()
    {
        this.uniSelector.IsLiveSite = this.IsLiveSite;

        // Return form name or ID according to type of field (if no field specified form name is returned)
        if ((this.FieldInfo != null) && ((this.FieldInfo.DataType == FormFieldDataTypeEnum.Integer) || (this.FieldInfo.DataType == FormFieldDataTypeEnum.LongInteger)))
        {
            // Store old value
            object value = this.uniSelector.Value;
            this.uniSelector.ReturnColumnName = "FormID";
            this.uniSelector.SelectionMode = SelectionModeEnum.SingleDropDownList;
            this.ShowSiteFilter = false;
            this.uniSelector.AllowEmpty = true;
            // Reset previously saved value
            this.uniSelector.Value = value;
        }
        else
        {
            this.uniSelector.ReturnColumnName = "FormName";
        }

        // Add sites filter
        if (ShowSiteFilter)
        {
            this.uniSelector.FilterControl = "~/CMSFormControls/Filters/SiteFilter.ascx";
            this.uniSelector.SetValue("DefaultFilterValue", (this.SiteID > 0) ? this.SiteID : CMSContext.CurrentSiteID);
            this.uniSelector.SetValue("FilterMode", "bizform");
        }
        // Select bizforms depending on a site if not filtered by uniselector site filter
        else
        {
            int siteId = (this.SiteID == 0) ? CMSContext.CurrentSiteID : this.SiteID;
            uniSelector.WhereCondition = SqlHelperClass.AddWhereCondition(uniSelector.WhereCondition, "FormSiteID = " + siteId);
        }
    }


    /// <summary>
    /// Returns WHERE condition for selected form.
    /// </summary>
    public override string GetWhereCondition()
    {
        // Return correct WHERE condition for integer if none value is selected
        if ((this.FieldInfo != null) && ((this.FieldInfo.DataType == FormFieldDataTypeEnum.Integer) || (this.FieldInfo.DataType == FormFieldDataTypeEnum.LongInteger)))
        {
            int id = ValidationHelper.GetInteger(uniSelector.Value, 0);
            if (id > 0)
            {
                return base.GetWhereCondition();
            }
        }
        return null;
    }

    #endregion
}
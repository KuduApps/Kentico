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

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.Newsletter;
using CMS.FormEngine;

public partial class CMSModules_Newsletters_FormControls_NewsletterSelectorSimple : CMS.FormControls.FormEngineUserControl
{
    private string mValue = null;


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
            this.EnsureChildControls();
            base.Enabled = value;
            this.usNewsletters.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets field value.
    /// </summary>
    public override object Value
    {
        get
        {
            return usNewsletters.Value;
        }
        set
        {
            this.EnsureChildControls();
            mValue = ValidationHelper.GetString(value, String.Empty);
            try
            {
                usNewsletters.Value = mValue;
            }
            catch
            {
            }
        }
    }


    /// <summary>
    /// Gets ClientID of the dropdownlist with newsletters.
    /// </summary>
    public override string ValueElementID
    {
        get
        {
            return usNewsletters.ClientID;
        }
    }

    #endregion


    #region "Methods"

    protected override void CreateChildControls()
    {
        if (usNewsletters == null)
        {
            pnlUpdate.LoadContainer();
        }

        base.CreateChildControls();
        if (!StopProcessing)
        {
            ReloadData();
        }
    }


    protected void ReloadData()
    {
        usNewsletters.WhereCondition = "NewsletterSiteID = " + CMSContext.CurrentSiteID;
        usNewsletters.ButtonRemoveSelected.CssClass = "XLongButton";
        usNewsletters.ButtonAddItems.CssClass = "XLongButton";

        // Return newsletter name or newsletter ID according to type of field (if no field specified newsletter name is returned)
        if ((this.FieldInfo != null) && ((this.FieldInfo.DataType == FormFieldDataTypeEnum.Integer) || (this.FieldInfo.DataType == FormFieldDataTypeEnum.LongInteger)))
        {
            usNewsletters.AllowEmpty = true;
            usNewsletters.ReturnColumnName = "NewsletterID";
        }
        else
        {
            usNewsletters.ReturnColumnName = "NewsletterName";
        }

        try
        {
            usNewsletters.Value = mValue;
        }
        catch
        {
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
            int id = ValidationHelper.GetInteger(usNewsletters.Value, 0);
            if (id > 0)
            {
                return base.GetWhereCondition();
            }
        }
        return null;
    }

    #endregion
}

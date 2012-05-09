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

using CMS.FormEngine;
using CMS.FormControls;
using CMS.SiteProvider;
using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSModules_Settings_FormControls_SelectCulture : FormEngineUserControl
{
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
    /// Returns ClientID of the DropDownList with culture.
    /// </summary>
    public override string ValueElementID
    {
        get
        {
            return this.uniSelector.DropDownSingleSelect.ClientID;
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
        this.uniSelector.IsLiveSite = false;
        this.uniSelector.DisplayNameFormat = "{%CultureName%}";
        this.uniSelector.SelectionMode = SelectionModeEnum.SingleDropDownList;
        this.uniSelector.ReturnColumnName = "CultureCode";
        this.uniSelector.OrderBy = "CultureName";
        this.uniSelector.ObjectType = "cms.culture";
        this.uniSelector.ResourcePrefix = "cultureselector";
        this.uniSelector.AllowEmpty = false;
    }
}

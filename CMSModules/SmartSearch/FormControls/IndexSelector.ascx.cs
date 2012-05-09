using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.FormControls;
using CMS.CMSHelper;

public partial class CMSModules_SmartSearch_FormControls_IndexSelector : FormEngineUserControl
{
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
            usIndexes.Enabled = value; 
        }
    }


    /// <summary>
    /// Gets or sets if live iste property.
    /// </summary>
    public override bool IsLiveSite
    {
        get
        {
            EnsureChildControls();
            return base.IsLiveSite;
        }
        set
        {
            EnsureChildControls();
            base.IsLiveSite = value;
            usIndexes.IsLiveSite = value;
        }
    }


    /// <summary>
    /// Gets or sets field value.
    /// </summary>
    public override object Value
    {
        get
        {
            EnsureChildControls();
            return usIndexes.Value;
        }
        set
        {
            EnsureChildControls();
            usIndexes.Value = value;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        usIndexes.WhereCondition = "IndexID IN (SELECT IndexID FROM CMS_SearchIndexSite WHERE IndexSiteID = " + CMSContext.CurrentSiteID + ")";
    }


    /// <summary>
    /// Creates child controls and loads update panle container if it is required.
    /// </summary>
    protected override void CreateChildControls()
    {
        // If selector is not defined load updat panel container
        if (usIndexes == null)
        {
            this.pnlUpdate.LoadContainer();
        }
        // Call base method
        base.CreateChildControls();
    }

    #endregion
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.FormEngine;
using CMS.FormControls;
using CMS.UIControls;
using CMS.CMSHelper;

public partial class CMSModules_Scoring_FormControls_SelectScore : FormEngineUserControl
{
    #region "Properties"

    /// <summary>
    /// Gets or sets the enabled state of the control.
    /// </summary>
    public override bool Enabled
    {
        get
        {
            return uniSelector.Enabled;
        }
        set
        {
            uniSelector.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets field value.
    /// </summary>
    public override object Value
    {
        get
        {
            return uniSelector.Value;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        InitSelector();
    }


    /// <summary>
    /// Initializes uniselector
    /// </summary>
    private void InitSelector()
    {
        uniSelector.WhereCondition = "ScoreEnabled = 1 AND ScoreSiteID = " + CMSContext.CurrentSiteID;
    }

    #endregion
}
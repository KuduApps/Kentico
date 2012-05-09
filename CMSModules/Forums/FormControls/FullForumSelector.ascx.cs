using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.FormControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.SiteProvider;

public partial class CMSModules_Forums_FormControls_FullForumSelector : FormEngineUserControl
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
            forumSelector.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets field value.
    /// </summary>
    public override object Value
    {
        get
        {
            return forumSelector.Value;
        }
        set
        {
            forumSelector.Value = value;

        }
    }

    #endregion


    #region "Event methods"

    protected void Page_Load(object sender, EventArgs e)
    {        
    }

    #endregion
}

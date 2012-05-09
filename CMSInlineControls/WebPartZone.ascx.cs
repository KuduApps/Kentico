using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using CMS.GlobalHelper;
using CMS.ExtendedControls;
using CMS.UIControls;

public partial class CMSInlineControls_WebPartZone : InlineUserControl
{
    #region "Properties"

    /// <summary>
    /// Control parameter.
    /// </summary>
    public override string Parameter
    {
        get
        {
            return this.zoneElem.ID;
        }
        set
        {
            this.zoneElem.ID = value;
        }
    }

    #endregion


    #region "Page events"

    /// <summary>
    /// Method that is called when the control content is loaded.
    /// </summary>
    public override void OnContentLoaded()
    {
        base.OnContentLoaded();

        SetupControl();
    }


    /// <summary>
    /// Load event handler.
    /// </summary>
    protected void SetupControl()
    {
        // Init the zone ID
        string zoneId = ValidationHelper.GetString(mProperties["id"], "");
        if (!String.IsNullOrEmpty(zoneId))
        {
            this.zoneElem.ID = zoneId;
        }
    }

    #endregion
}

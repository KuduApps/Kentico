using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;

public partial class CMSModules_Countries_Controls_State_StateList : CMSAdminEditControl
{
    #region "Properties"

    /// <summary>
    /// UniGrid control used for listing.
    /// </summary>
    public UniGrid UniGridControl
    {
        get
        {
            return this.gridStates;
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #endregion
}

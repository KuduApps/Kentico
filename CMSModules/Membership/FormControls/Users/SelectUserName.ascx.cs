using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.FormControls;


public partial class CMSModules_Membership_FormControls_Users_SelectUserName : FormEngineUserControl
{

    #region "Properties"

    /// <summary>
    /// Given or set value of object.
    /// </summary>
    public override object Value
    {
        get
        {
            return ucSelector.Value; 
        }
        set
        {
            ucSelector.Value = value;
        }
    }


    /// <summary>
    /// Indicates whether the control is enabled or disabled.
    /// </summary>
    public override bool Enabled
    {
        get
        {
            return ucSelector.Enabled;
        }
        set
        {
            ucSelector.Enabled = value;
        }
    }


    /// <summary>
    /// Indicates if control is used on live site.
    /// </summary>
    public override bool IsLiveSite
    {
        get
        {
            return base.IsLiveSite;
        }
        set
        {
            base.IsLiveSite = value;
            ucSelector.IsLiveSite = value;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        ucSelector.UniSelector.ReturnColumnName = "UserName";
    }

    #endregion
}

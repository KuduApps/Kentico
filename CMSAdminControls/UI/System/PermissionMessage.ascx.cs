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

using CMS.UIControls;
using CMS.GlobalHelper;

public partial class CMSAdminControls_UI_System_PermissionMessage : CMSUserControl
{
    #region "Variables"

    private string mResource;
    private string mPermission;
    private string mErrorMessage;
    bool? mDisplayMessage = null;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Resource name.
    /// </summary>
    public string Resource
    {
        get
        {
            return mResource;
        }
        set
        {
            mResource = value;
        }
    }


    /// <summary>
    /// Permission name.
    /// </summary>
    public string Permission
    {
        get
        {
            return mPermission;
        }
        set
        {
            mPermission = value;
        }
    }


    /// <summary>
    /// Error message.
    /// </summary>
    public string ErrorMessage
    {
        get
        {
            return DataHelper.GetNotEmpty(mErrorMessage, GetString("General.PermissionResource"));
        }
        set
        {
            mErrorMessage = value;
        }
    }


    public bool DisplayMessage
    {
        get
        {
            return ValidationHelper.GetBoolean(mDisplayMessage, true);
        }
        set
        {
            mDisplayMessage = value;
        }
    }


    #endregion


    /// <summary>
    /// Display message.
    /// </summary>
    /// <param name="e">Event args</param>
    protected override void OnPreRender(EventArgs e)
    {
        if (mDisplayMessage != null)
        {
            this.Visible = this.DisplayMessage;
        }

        if (!String.IsNullOrEmpty(this.ErrorMessage))
        {
            lblPermission.Text = String.Format(ErrorMessage, Permission, Resource);
        }

        base.OnPreRender(e);
    }

}

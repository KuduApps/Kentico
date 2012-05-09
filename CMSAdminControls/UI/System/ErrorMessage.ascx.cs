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
using CMS.UIControls;

public partial class CMSAdminControls_UI_System_ErrorMessage : ErrorMessageControl
{
    #region "Properties"

    /// <summary>
    /// Error title.
    /// </summary>
    public override string ErrorTitle
    {
        get
        {
        	 return this.ptTitle.TitleText; 
        }
        set
        {
        	 this.ptTitle.TitleText = value; 
        }
    }


    /// <summary>
    /// Error message.
    /// </summary>
    public override string ErrorMessage
    {
        get
        {
        	 return this.lblMessage.Text; 
        }
        set
        {
        	 this.lblMessage.Text = value; 
        }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        this.ptTitle.TitleImage = GetImageUrl("Others/Messages/denied.png");
    }
}

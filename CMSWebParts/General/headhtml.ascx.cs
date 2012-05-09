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

using CMS.PortalControls;
using CMS.Controls;
using CMS.CMSHelper;
using CMS.GlobalHelper;

public partial class CMSWebParts_General_headhtml : CMSAbstractWebPart
{
    #region "Public properties"

    /// <summary>
    /// Gets or sets the head HTML code.
    /// </summary>
    public string HTMLCode
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("HTMLCode"), "");
        }
        set
        {
            this.SetValue("HTMLCode", value);
        }
    }

    #endregion

    /// <summary>
    /// Content loaded event handler.
    /// </summary>
    public override void OnContentLoaded()
    {
        base.OnContentLoaded();
        SetupControl();
    }


    /// <summary>
    /// Initializes the control properties.
    /// </summary>
    protected void SetupControl()
    {
        
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (this.StopProcessing)
        {
            // Do nothing
        }
        else
        {
            this.Page.Header.Controls.Add(new LiteralControl(this.HTMLCode));
        }
    }
}

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

public partial class CMSWebParts_General_cssstylesheet : CMSAbstractWebPart
{
    #region "Public properties"

    /// <summary>
    /// Gets or sets path to the stylesheet file.
    /// </summary>
    public string FilePath
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("FilePath"), "");
        }
        set
        {
            this.SetValue("FilePath", value);
        }
    }


    /// <summary>
    /// Gets or sets the media type.
    /// </summary>
    public string Media
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Media"), "screen");
        }
        set
        {
            this.SetValue("Media", value);
        }
    }

    #endregion


    #region "Methods"

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


    /// <summary>
    /// PreRender event handler.
    /// </summary>
    /// <param name="e">EventArgs</param>
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (!this.StopProcessing)
        {
            // Add link to page header
            string url = CSSHelper.GetPhysicalCSSUrl(this.FilePath);
            string link = CSSHelper.GetCSSFileLink(url, this.Media);

            LiteralControl ltlCss = new LiteralControl(link);
            ltlCss.EnableViewState = false;

            Page.Header.Controls.Add(ltlCss);
        }
    }

    #endregion
}
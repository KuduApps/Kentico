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
using CMS.GlobalHelper;
using CMS.TreeEngine;
using CMS.CMSHelper;
using CMS.ExtendedControls;

public partial class CMSWebParts_Text_staticHTML : CMSAbstractWebPart
{
    /// <summary>
    /// Gets or sets the text (HTML code) to be displayed.
    /// </summary>
    public string Text
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Text"), ltlText.Text);
        }
        set
        {
            this.SetValue("Text", value);
            this.ltlText.Text = value;
            this.ltlText.EnableViewState = (this.ResolveDynamicControls && ControlsHelper.ResolveDynamicControls(this));
        }
    }


    /// <summary>
    /// Enables or disables resolving of inline controls.
    /// </summary>
    public bool ResolveDynamicControls
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("ResolveDynamicControls"), true);
        }
        set
        {
            this.SetValue("ResolveDynamicControls", value);
        }
    }


    /// <summary>
    /// Content loaded event handler.
    /// </summary>
    public override void OnContentLoaded()
    {
        base.OnContentLoaded();
        SetupControl();
    }


    /// <summary>
    /// Reloads the control data.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();
        SetupControl();
    }


    /// <summary>
    /// Initializes the control properties.
    /// </summary>
    protected void SetupControl()
    {
        if (this.StopProcessing)
        {
            // Do not process
        }
        else
        {
            this.ltlText.Text = this.Text;
            this.ltlText.EnableViewState = (this.ResolveDynamicControls && ControlsHelper.ResolveDynamicControls(this));
        }
    }
}

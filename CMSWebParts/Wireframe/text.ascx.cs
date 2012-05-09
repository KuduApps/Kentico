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

public partial class CMSWebParts_Wireframe_Text : CMSAbstractWebPart
{
    /// <summary>
    /// Gets or sets the text to be displayed.
    /// </summary>
    public string Text
    {
        get
        {
            return HTMLHelper.ResolveUrls(ValidationHelper.GetString(this.GetValue("Text"), ltlText.Text), null);
        }
        set
        {
            this.SetValue("Text", value);
            this.ltlText.Text = this.EncodeText ? HTMLHelper.HTMLEncode(value) : value;
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
    /// Enables or disables HTML encoding of text.
    /// </summary>
    public bool EncodeText
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("EncodeText"), false);
        }
        set
        {
            this.SetValue("EncodeText", value);
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
            string text = this.Text;
            this.ltlText.Text = this.EncodeText ? HTMLHelper.HTMLEncode(text) : text;
            this.ltlText.EnableViewState = (this.ResolveDynamicControls && ControlsHelper.ResolveDynamicControls(this));
        }
    }


    /// <summary>
    /// Reloads the control data.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();

        SetupControl();
    }
}




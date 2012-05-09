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
using CMS.Controls;
using CMS.EventLog;
using CMS.IO;

public partial class CMSWebParts_Filters_Filter : CMSAbstractWebPart
{
    private string mFilterControlPath = null;
    private string mFilterName = null;
    private CMSAbstractBaseFilterControl mFilterControl = null;


    /// <summary>
    /// Gets or sets the path of the filter control.
    /// </summary>
    public string FilterControlPath
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("FilterControlPath"), mFilterControlPath);
        }
        set
        {
            this.SetValue("FilterControlPath", value);
            this.mFilterControlPath = value;
        }
    }


    /// <summary>
    /// Gets or sets the name of the filter control.
    /// </summary>
    public string FilterName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("FilterName"), mFilterName);
        }
        set
        {
            this.SetValue("FilterName", value);
            this.mFilterName = value;
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
        // In design mode is pocessing of control stoped
        if (this.StopProcessing)
        {
            // Do nothing
        }
        else
        {
            LoadFilter();
        }
    }

    
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        LoadFilter();
    }


    /// <summary>
    /// Load filter control according filterpath.
    /// </summary>
    private void LoadFilter()
    {
        if (this.mFilterControl == null)
        {
            if (this.FilterControlPath != null)
            {
                try
                {
                    if (File.Exists(Server.MapPath(this.FilterControlPath)))
                    {
                        this.mFilterControl = (this.Page.LoadControl(this.FilterControlPath)) as CMSAbstractBaseFilterControl;
                        if (this.mFilterControl != null)
                        {
                            this.mFilterControl.ID = "filterControl";
                            this.Controls.AddAt(0, this.mFilterControl);
                            this.mFilterControl.FilterName = this.FilterName;
                            if (this.Page != null)
                            {
                                this.mFilterControl.Page = this.Page;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    EventLogProvider log = new EventLogProvider();
                    log.LogEvent("Filter control", "LOADFILTER", ex);
                }
            }
        }
    }
}

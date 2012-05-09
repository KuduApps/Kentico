using System;
using System.Web.UI;

using CMS.PortalControls;
using CMS.GlobalHelper;
using CMS.EventLog;

public partial class CMSWebParts_General_usercontrol : CMSAbstractWebPart
{
    private string mUserControlPath = "";

    /// <summary>
    /// Gets or sets the path of the user control.
    /// </summary>
    public string UserControlPath
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("UserControlPath"), mUserControlPath);
        }
        set
        {
            this.SetValue("UserControlPath", value);
            this.mUserControlPath = value;
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
        }
    }


    /// <summary>
    /// Loads the user control.
    /// </summary>
    protected void LoadControl()
    {
        if (!string.IsNullOrEmpty(UserControlPath))
        {
            try
            {
                Control ctrl = Page.LoadControl(UserControlPath);
                ctrl.ID = "userControlElem";
                Controls.Add(ctrl);
            }
            catch (Exception ex)
            {
                lblError.Text = "[" + this.ID + "] " + GetString("WebPartUserControl.ErrorLoad") + ": " + ex.Message;
                lblError.ToolTip = EventLogProvider.GetExceptionLogMessage(ex);
                lblError.Visible = true;
            }
        }
    }


    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        // The control must load after OnInit to properly load its viewstate
        LoadControl();
    }
}

using System.Text;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.PortalControls;
using CMS.PortalEngine;

public partial class CMSWebParts_Silverlight_SilverlightApplication : CMSAbstractWebPart
{
    #region "Public properties"

    /// <summary>
    /// HTML content which is displayed to user when Silverlight plugin is not installed.
    /// </summary>
    public string AlternateContent
    {
        get
        {
            return ValidationHelper.GetString(GetValue("AlternateContent"), string.Empty);
        }
        set
        {
            SetValue("AlternateContent", value);
            this.silverlightElem.AlternateContent = value;
        }
    }


    /// <summary>
    /// Silverlight application path.
    /// </summary>
    public string ApplicationPath
    {
        get
        {
            return ValidationHelper.GetString(GetValue("ApplicationPath"), string.Empty);
        }
        set
        {
            SetValue("ApplicationPath", value);
            this.silverlightElem.ApplicationPath = value;
        }
    }


    /// <summary>
    /// Minimum version of the Microsoft Silverlight which is required by the current silverlight application.
    /// </summary>
    public string MinimumVersion
    {
        get
        {
            return ValidationHelper.GetString(GetValue("MinimumVersion"), string.Empty);
        }
        set
        {
            SetValue("MinimumVersion", value);
            this.silverlightElem.MinimumVersion = value;
        }
    }


    /// <summary>
    /// Silverlight application parameters.
    /// </summary>
    public string Parameters
    {
        get
        {
            return ValidationHelper.GetString(GetValue("Parameters"), string.Empty);
        }
        set
        {
            SetValue("Parameters", value);
            this.silverlightElem.Parameters = value;
        }
    }


    /// <summary>
    /// Silverlight application container width.
    /// </summary>
    public string ContainerWidth
    {
        get
        {
            return ValidationHelper.GetString(GetValue("ContainerWidth"), string.Empty);
        }
        set
        {
            SetValue("ContainerWidth", value);
            this.silverlightElem.ContainerWidth = value;
        }
    }


    /// <summary>
    /// Silverlight application container height.
    /// </summary>
    public string ContainerHeight
    {
        get
        {
            return ValidationHelper.GetString(GetValue("ContainerHeight"), string.Empty);
        }
        set
        {
            SetValue("ContainerHeight", value);
            this.silverlightElem.ContainerHeight = value;
        }
    }


    /// <summary>
    /// Silverlight application container background.
    /// </summary>
    public string ContainerBackground
    {
        get
        {
            return ValidationHelper.GetString(GetValue("ContainerBackground"), "#FFFFFF");
        }
        set
        {
            SetValue("ContainerBackground", value);
            this.silverlightElem.ContainerBackground = value;
        }
    }


    /// <summary>
    /// Web service endpoint address.
    /// </summary>
    public string EndpointAddress
    {
        get
        {
            return ValidationHelper.GetString(GetValue("EndpointAddress"), string.Empty);
        }
        set
        {
            SetValue("EndpointAddress", value);
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
    /// Reload data.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();
        SetupControl();
    }


    /// <summary>
    /// Initializes control properties.
    /// </summary>
    protected void SetupControl()
    {
        if (StopProcessing)
        {
            // Do nothing
            this.silverlightElem.StopProcessing = true;
        }
        else
        {
            // Due to new design mode (with preview) we need to move the content down
            // for the user to be able to drag and drop the control
            if (CMSContext.ViewMode == ViewModeEnum.Design)
            {
                ltlDesign.Text = "<div style=\"width: 10px; height: 30px;\"></div>";
            }
            else
            {
                ltlDesign.Text = string.Empty;
            }

            this.silverlightElem.AlternateContent = this.AlternateContent;
            this.silverlightElem.ApplicationPath = this.ApplicationPath;
            this.silverlightElem.ContainerBackground = this.ContainerBackground;
            this.silverlightElem.ContainerHeight = this.ContainerHeight;
            this.silverlightElem.ContainerWidth = this.ContainerWidth;
            this.silverlightElem.MinimumVersion = this.MinimumVersion;
            this.silverlightElem.Parameters = GetApplicationParameters();

            ScriptHelper.RegisterClientScriptBlock(
                this,
                typeof(string),
                "_sl_historyFrame",
                "<iframe id=\"_sl_historyFrame\" style=\"visibility:hidden;height:0px;width:0px;border:0px\"></iframe>");
        }
    }


    /// <summary>
    /// Returns application parameters with added web service URL as parameter with key 'webservice'.
    /// </summary>
    private string GetApplicationParameters()
    {

        string endpoint = string.Empty;
        if (EndpointAddress != string.Empty)
        {
            // Add web service URL to application parameters
            endpoint = "endpoint=" + URLHelper.GetAbsoluteUrl(EndpointAddress);
            if (Parameters != string.Empty)
            {
                endpoint += ",";
            }
        }

        return endpoint + Parameters;
    }
}

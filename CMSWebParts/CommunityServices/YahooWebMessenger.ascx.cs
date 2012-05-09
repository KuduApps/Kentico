using System;
using System.Web;
using System.Web.UI;

using CMS.PortalControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.PortalEngine;

public partial class CMSWebParts_CommunityServices_YahooWebMessenger : CMSAbstractWebPart
{
    #region "Public properties"

    /// <summary>
    /// Unique ID from yahoo messenger web.
    /// </summary>
    public string YahooMessengerID
    {
        get
        {
            return ValidationHelper.GetString(GetValue("YahooMessengerID"), "");
        }
        set
        {
            SetValue("YahooMessengerID", value);
        }
    }


    /// <summary>
    /// Width of the IM window.
    /// </summary>
    public int Width
    {
        get
        {
            return ValidationHelper.GetInteger(GetValue("Width"), 300);
        }
        set
        {
            SetValue("Width", value);
        }
    }


    /// <summary>
    /// Height of the IM window.
    /// </summary>
    public int Height
    {
        get
        {
            return ValidationHelper.GetInteger(GetValue("Height"), 300);
        }
        set
        {
            SetValue("Height", value);
        }
    }

    #endregion


    #region "Page events"

    /// <summary>
    /// Loads the web part content.
    /// </summary>
    public override void OnContentLoaded()
    {
        base.OnContentLoaded();
        SetupControl();
    }

    #endregion


    #region "Other methods"

    /// <summary>
    /// Sets up the control.
    /// </summary>
    protected void SetupControl()
    {
        if (StopProcessing)
        {
            // Do nothing
        }
        else
        {
            if ((CMSContext.ViewMode == ViewModeEnum.LiveSite) || (CMSContext.ViewMode == ViewModeEnum.Preview))
            {
                if (YahooMessengerID != "")
                {
                    ltlYahooMessenger.Text = "<object id=\"YahooMessenger_" + InstanceGUID + "\" type=\"application/x-shockwave-flash\" data=\"http://wgweb.msg.yahoo.com/badge/Pingbox.swf\"";
                    ltlYahooMessenger.Text += " width=\"" + Width + "\" height=\"" + Height + "\"><param name=\"movie\" value=\"http://wgweb.msg.yahoo.com/badge/Pingbox.swf\" />";
                    ltlYahooMessenger.Text += "<param name=\"allowScriptAccess\" value=\"always\" /><param name=\"flashvars\" value=\"" + YahooMessengerID + "\" /></object>";
                }
            }
        }
    }

    #endregion
}
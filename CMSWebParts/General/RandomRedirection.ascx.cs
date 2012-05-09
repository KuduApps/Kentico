using System;

using CMS.PortalControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.PortalEngine;

public partial class CMSWebParts_General_RandomRedirection : CMSAbstractWebPart
{   
    #region Webpart properties

    /// <summary>
    /// List of URLs to where webpart could redirect.
    /// </summary>
    public string RedirectionURLs
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("RedirectionURLs"), "");
        }
        set
        {
            this.SetValue("RedirectionURLs", value);
        }
    }

    #endregion


    #region Webpart methods

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
        if (!this.StopProcessing)
        {
            if ((this.RedirectionURLs.Trim().Length > 0) &&
                (CMSContext.ViewMode == ViewModeEnum.LiveSite))
            {
                // Parse URLs string
                string[] URLs = this.RedirectionURLs.Trim().Split(new String[] {"\n"},StringSplitOptions.RemoveEmptyEntries);

                if (URLs.Length > 0)
                {
                    // Generate random integer index to array
                    int rndID = new Random().Next(0, URLs.Length);
                    string newURL = URLHelper.ResolveUrl(URLs[rndID].Trim());
                    if ((URLHelper.CurrentURL != newURL) &&
                        (URLHelper.GetAbsoluteUrl(URLHelper.CurrentURL) != newURL))
                    {
                        URLHelper.ResponseRedirect(newURL);     
                    }                    
                }
            }
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

    #endregion
}

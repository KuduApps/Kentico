using System;

using CMS.PortalControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.PortalEngine;

public partial class CMSWebParts_GoogleServices_GoogleAnalytics : CMSAbstractWebPart
{
    #region "Properties"

    /// <summary>
    /// Gets or set tracking code for google analytics.
    /// </summary>
    public string TrackingCode
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("TrackingCode"), "");
        }
        set
        {
            this.SetValue("TrackingCode", value);
        }
    }


    /// <summary>
    /// Get or set type of analytics tracking
    /// Single domain – value 0
    /// One domain with multiple subdomains – value 1
    /// Multiple top-level domains – value 2
    /// </summary>
    public int TrackingType
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("TrackingType"), 0);
        }
        set
        {
            this.SetValue("TrackingType", value);
        }
    }


    /// <summary>
    /// Indicates if asynchronous version of script should be used.
    /// </summary>
    public bool UseAsyncScript
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("UseAsyncScript"), false);
        }
        set
        {
            SetValue("UseAsyncScript", value);
        }
    }

    #endregion


    #region "Webpart events"

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
            if (CMSContext.ViewMode == ViewModeEnum.LiveSite)
            {
                // Register necessary google scripts
                string googleScript = "(function() {\n" +
                                      "var ga = document.createElement('script'); ga.type = 'text/javascript';\n";
                
                // If async script add async parameter
                if (UseAsyncScript)
                {
                    googleScript += "ga.async = true;";
                }

                googleScript += "\nga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';\n" +
                                "var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);\n" +
                                "})();";

                // Register final script to a page
                ScriptHelper.RegisterStartupScript(this, typeof(string), "GoogleAnalyticsScript", ScriptHelper.GetScript(googleScript));

                // Create custom tracking script
                string trackingScript = "if ( (parent == null) || (!parent.IsCMSDesk) ) {\ntry { \n";
                if (UseAsyncScript)
                {
                    trackingScript += "var _gaq = _gaq || [];\n" +
                                      "_gaq.push(['_setAccount', '" + this.TrackingCode + "']);\n";
                }
                else
                {
                    trackingScript += "var pageTracker = _gat._getTracker('" + this.TrackingCode + "');\n";
                }

                switch (this.TrackingType)
                {
                    // One domain with multiple subdomains
                    case 1:
                        if (UseAsyncScript)
                        {
                            trackingScript += "_gaq.push(['_setDomainName', '" + GetMainDomain() + "']);\n";
                        }
                        else
                        {
                            trackingScript += "pageTracker._setDomainName('" + GetMainDomain() + "');\n";
                        }
                        break;

                    // Multiple top-level domains 
                    case 2:
                        if (UseAsyncScript)
                        {
                            trackingScript += "_gaq.push(['_setDomainName', 'none']);\n" +
                                              "_gaq.push(['_setAllowLinker', true]);\n";
                        }
                        else
                        {
                            trackingScript += "pageTracker._setDomainName('none');\n" +
                                              "pageTracker._setAllowLinker(true);\n";
                        }
                        break;

                    // Single domain
                    default:
                        break;
                }

                if (UseAsyncScript)
                {
                    trackingScript += "_gaq.push(['_trackPageview']);\n";
                }
                else
                {
                    trackingScript += "pageTracker._trackPageview();\n";                            
                }

                trackingScript += "} catch(err) {}\n}";

                // Register final script to a page
                ScriptHelper.RegisterStartupScript(this, typeof(string), "GoogleAnalyticsTracking" + "_" + this.ClientID, ScriptHelper.GetScript(trackingScript));
            }
        }
    }

    #endregion


    #region "Helper methods"

    /// <summary>
    /// Gets main domain part from current domain.
    /// </summary>
    /// <returns>Main domain part</returns>
    private string GetMainDomain()
    {
        string[] domainParts = URLHelper.GetCurrentDomain().Split('.');
        string mainDomain = String.Empty;

        // Check if the domain name consists of more than 3 domain levels
        int moreDomains = 0;
        if (domainParts.Length > 3)
        {
            moreDomains = domainParts.Length - 3;
        }

        for (int i = domainParts.Length - 1; i >= 0; i--)
        {
            // Main domain consist of generic and 2nd level domain
            if (i > (domainParts.Length - 3 - moreDomains))
            {
                mainDomain = "." + domainParts[i] + mainDomain;
            }
            else
            {
                break;
            }
        }
        return mainDomain;
    }

    #endregion
}

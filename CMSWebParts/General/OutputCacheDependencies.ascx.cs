using System;

using CMS.PortalControls;
using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.PortalEngine;

public partial class CMSWebParts_General_OutputCacheDependencies : CMSAbstractWebPart
{
    #region "Public properties"

    /// <summary>
    /// Search box width.
    /// </summary>
    public bool UseDefaultDependencies
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("UseDefaultDependencies"), true);
        }
        set
        {
            SetValue("UseDefaultDependencies", value);
        }
    }


    /// <summary>
    /// Search results box dimension.
    /// </summary>
    public override string CacheDependencies
    {
        get
        {
            return ValidationHelper.GetString(GetValue("CacheDependencies"), "");
        }
        set
        {
            SetValue("CacheDependencies", value);
        }
    }

    #endregion


    protected override void OnPreRender(EventArgs e)
    {
        if (StopProcessing)
        {
            // Do nothing
        }
        else
        {
            if ((CMSContext.ViewMode == ViewModeEnum.LiveSite) && (CacheDependencies != ""))
            {
                base.OnPreRender(e);

                // Use default cache dependencies
                if (UseDefaultDependencies)
                {
                    CMSContext.AddDefaultOutputCacheDependencies();
                }

                // More than one dependency is set
                string[] dependencies = CacheDependencies.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                CacheHelper.AddOutputCacheDependencies(dependencies);
            }
        }
    }
}
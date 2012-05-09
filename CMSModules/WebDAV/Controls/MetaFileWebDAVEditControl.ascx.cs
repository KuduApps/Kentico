using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using CMS.UIControls;
using CMS.SiteProvider;
using CMS.CMSHelper;

/// <summary>
/// Metafile WebDAV control.
/// </summary>
public partial class CMSModules_WebDAV_Controls_MetaFileWebDAVEditControl : WebDAVEditControl
{
     #region "Constructors"

    /// <summary>
    /// Creates instance.
    /// </summary>
    public CMSModules_WebDAV_Controls_MetaFileWebDAVEditControl()
    {
        mControlType = WebDAVControlTypeEnum.MetaFile;
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Gets the meta file URL.
    /// </summary>
    protected override string GetUrl()
    {
        return MetaFileURLProvider.GetWebDAVUrl(this.MetaFileGUID, this.FileName, SiteName);
    }

    #endregion
}

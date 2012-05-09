using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace CMS.CMSImportExport
{
    /// <summary>
    /// New site wizard enumeration.
    /// </summary>
    public enum NewSiteCreationEnum
    {
        BlankSite = 0,
        WebSiteTemplate = 1,
        UsingWizard = 2
    }


    /// <summary>
    /// Enumeration for the object import result.
    /// </summary>
    public enum ResultEnum
    {
        /// <summary>
        /// Success.
        /// </summary>
        Success = 0,
        /// <summary>
        /// Error.
        /// </summary>
        Error = 1,
        /// <summary>
        /// Warning.
        /// </summary>
        Warning = 2,
        /// <summary>
        /// Progress.
        /// </summary>
        Progress = 3
    }
}
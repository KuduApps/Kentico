using System;

using CMS.UIControls;

public partial class CMSModules_ImportExport_Controls_DefineSiteStructure : CMSUserControl
{
    /// <summary>
    /// Site name.
    /// </summary>
    public string SiteName
    {
        get
        {
            if (Request.QueryString.Count == 0)
            {
                return "?sitename=" + (string)ViewState["siteName"];
            }
            return "&sitename=" + (string)ViewState["siteName"];
        }
        set 
        { 
            ViewState["siteName"]= value; 
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
    }
}

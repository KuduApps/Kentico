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

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.CMSHelper;

public partial class CMSModules_PortalEngine_UI_WebParts_WebPartProperties_binding : CMSWebPartPropertiesPage
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public CMSModules_PortalEngine_UI_WebParts_WebPartProperties_binding()
    {
        this.PreInit += new EventHandler(CMSDesk_PortalEngine_WebpartProperties_PreInit);
    }


    /// <summary>
    /// PreInit event handler.
    /// </summary>
    void CMSDesk_PortalEngine_WebpartProperties_PreInit(object sender, EventArgs e)
    {
        // Initialize the control
        webPartBinding.AliasPath = ValidationHelper.GetString(Request.QueryString["aliaspath"], "");
        webPartBinding.WebpartId = ValidationHelper.GetString(Request.QueryString["webpartid"], "");
        webPartBinding.ZoneId = ValidationHelper.GetString(Request.QueryString["zoneid"], "");
        webPartBinding.InstanceGUID = ValidationHelper.GetGuid(Request.QueryString["instanceguid"], Guid.Empty);
        webPartBinding.PageTemplateId = ValidationHelper.GetInteger(Request.QueryString["templateid"], 0);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Check permissions for web part properties UI
        CurrentUserInfo currentUser = CMSContext.CurrentUser;
        if (!currentUser.IsAuthorizedPerUIElement("CMS.Content", "WebPartProperties.Bindings"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Content", "WebPartProperties.Bindings");
        }
    }
}

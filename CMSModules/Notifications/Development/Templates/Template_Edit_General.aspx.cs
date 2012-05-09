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
using CMS.CMSHelper;
using CMS.UIControls;

public partial class CMSModules_Notifications_Development_Templates_Template_Edit_General : CMSNotificationsPage
{
    #region "Variables"

    protected int templateId = 0;
    protected int siteId = 0;    

    #endregion


    #region "Events"

    protected void Page_Load(object sender, EventArgs e)
    {
              
        // get query strings
        templateId = QueryHelper.GetInteger("templateid", 0) ;
        siteId = QueryHelper.GetInteger("siteid", 0) ;

        templateEditElem.TemplateID = templateId;
        templateEditElem.SiteID = siteId;
    }

    #endregion
}

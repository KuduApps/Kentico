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
using CMS.SettingsProvider;
using CMS.FormEngine;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSModules_DocumentTypes_Pages_Development_DocumentType_Edit_Form : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        layoutElem.FormType = CMSModules_AdminControls_Controls_Class_Layout.FORMTYPE_DOCUMENT;
        layoutElem.ObjectID = QueryHelper.GetInteger("documenttypeid", 0);
    }
}
 

  

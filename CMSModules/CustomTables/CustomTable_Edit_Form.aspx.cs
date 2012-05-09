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

public partial class CMSModules_CustomTables_CustomTable_Edit_Form : CMSCustomTablesPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        layoutElem.FormType = CMSModules_AdminControls_Controls_Class_Layout.FORMTYPE_SYSTEMTABLE;
        layoutElem.ObjectID = QueryHelper.GetInteger("customtableid", 0);
    }
}
 

  

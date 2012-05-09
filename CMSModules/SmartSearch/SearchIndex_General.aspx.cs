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

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.SettingsProvider;

public partial class CMSModules_SmartSearch_SearchIndex_General : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {        
        IndexGeneral.ItemID = QueryHelper.GetInteger("indexid", 0);
    }
}

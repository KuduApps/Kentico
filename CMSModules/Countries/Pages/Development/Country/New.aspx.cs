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
using CMS.DataEngine;
using CMS.SiteProvider;
using CMS.UIControls;

// Breadcrumbs
[Breadcrumbs(2)]
[Breadcrumb(0, "Country_Edit.ItemListLink", "List.aspx", null)]
[Breadcrumb(1, "Country_Edit.NewItemCaption")]

// Title
[Title("Objects/CMS_Country/new.png", "Country_New.HeaderCaption", "new_country")]

public partial class CMSModules_Countries_Pages_Development_Country_New : SiteManagerPage
{
}

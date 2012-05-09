using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.UIControls;

// Edited object
[ParentObject(SiteObjectType.COUNTRY, "countryid")]

// Actions
[Actions(1)]
[Action(0, "Objects/CMS_Country/addstate.png", "Country_State_List.NewItemCaption", "Edit.aspx?countryid={%EditedObjectParent.ID%}")]

public partial class CMSModules_Countries_Pages_Development_State_List : SiteManagerPage
{
}

using System;

using CMS.SiteProvider;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.SettingsProvider;

// Check the license
[CheckLicence(FeatureEnum.Blogs)]

// Edited object
[EditedObject(SiteObjectType.COUNTRY, "countryid")]

// Title
[Title("Objects/CMS_Country/object.png", "Country_Edit.HeaderCaption", "new_country")]

// Breadcrumbs
[Breadcrumbs(2)]
[Breadcrumb(0, "Country_Edit.ItemListLink", "List.aspx", "_parent")]
[Breadcrumb(1, Text = "{%EditedObject.DisplayName%}")]

// Tabs
[Tabs(2, "content")]
[Tab(0, "general.general", "Tab_General.aspx?countryid={%EditedObject.ID%}", "SetHelpTopic('helpTopic', 'new_country');")]
[Tab(1, "Country_Edit.States", "../State/List.aspx?countryid={%EditedObject.ID%}", "SetHelpTopic('helpTopic', 'states_list');")]

public partial class CMSModules_Countries_Pages_Development_Country_Header : SiteManagerPage
{
}

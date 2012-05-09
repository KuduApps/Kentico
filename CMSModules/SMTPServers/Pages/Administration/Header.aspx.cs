using System;

using CMS.EmailEngine;
using CMS.UIControls;

// Edited object
[EditedObject(EmailObjectType.SMTPSERVER, "smtpserverid")]

// Title
[Title("Objects/CMS_SMTPServer/object.png", "SMTPServer_Edit.HeaderCaption", "smtpserver_general")]

// Breadcrumbs
[Breadcrumbs(2)]
[Breadcrumb(0, "SMTPServer_Edit.ItemListLink", "List.aspx", "_parent")]
[Breadcrumb(1, Text = "{%EditedObject.ServerName%}")]

// Tabs
[Tabs(2, "content")]
[Tab(0, "general.general", "Tab_General.aspx?smtpserverid={%EditedObject.ServerID%}", "SetHelpTopic('helpTopic', 'smtpserver_general');")]
[Tab(1, "smtpserver_edit.sites", "Tab_Sites.aspx?smtpserverid={%EditedObject.ServerID%}", "SetHelpTopic('helpTopic', 'smtpserver_sites');")]

public partial class CMSModules_SMTPServers_Pages_Administration_Header : CMSSMTPServersPage
{
}
using CMS.UIControls;

[Title("CMSModules/CMS_Integration/module.png", "integration.integration", "integration_integrationtask_outgoing_list")]

[Tabs(3, "integrationContent")]
[Tab(0, "integration.outcomingtasks", "OutcomingTasks/List.aspx", "SetHelpTopic('helpTopic', 'integration_integrationtask_outgoing_list'); return false;")]
[Tab(1, "integration.incomingtasks", "IncomingTasks/List.aspx", "SetHelpTopic('helpTopic', 'integration_integrationtask_incoming_list'); return false;")]
[Tab(2, "integration.connector.list", "Connectors/List.aspx", "SetHelpTopic('helpTopic', 'integration_integrationconnector_list'); return false;")]
public partial class CMSModules_Integration_Pages_Administration_Header : CMSIntegrationPage
{
}

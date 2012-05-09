using System;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.Synchronization;

// Edited object
[EditedObject(SynchronizationObjectType.INTEGRATIONCONNECTOR, "connectorId")]

// Breadcrumbs
[Breadcrumbs(2)]
[Breadcrumb(0, "integration.connector.list", "~/CMSModules/Integration/Pages/Administration/Connectors/List.aspx", null, Javascript = "parent.frames['integrationMenu'].SetHelpTopic('helpTopic', 'integration_integrationconnector_list');")]
[Breadcrumb(1, Text = "{%EditedObject.DisplayName%}", ExistingObject = true)]
[Breadcrumb(1, ResourceString = "integration.connector.new", NewObject = true)]

public partial class CMSModules_Integration_Pages_Administration_Connectors_Edit : CMSIntegrationPage
{
    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptHelper.RegisterStartupScript(this, typeof(string), "setHelpTopic", ScriptHelper.GetScript("parent.frames['integrationMenu'].SetHelpTopic('helpTopic', 'integration_integrationconnector_edit');"));
    }

    #endregion
}

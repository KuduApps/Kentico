using System;

using CMS.UIControls;
using CMS.GlobalHelper;

[Security(Resource = "CMS.Content", UIElements = "Validation.CSS")]
public partial class CMSModules_Content_CMSDesk_Validation_CSS : CMSValidationPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UIContext.ValidationTab = ValidationTabEnum.CSS;

        int nodeId = QueryHelper.GetInteger("nodeid", 0);
        string mode = QueryHelper.GetString("action", "");
        validator.Url = GetDocumentUrl(nodeId, mode);
        validator.NodeID = nodeId;
    }
}

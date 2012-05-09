using System;

using CMS.UIControls;
using CMS.GlobalHelper;

[Security(Resource = "CMS.Content", UIElements = "Validation.Links")]
public partial class CMSModules_Content_CMSDesk_Validation_Links : CMSValidationPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UIContext.ValidationTab = ValidationTabEnum.Links;

        int nodeId = QueryHelper.GetInteger("nodeid", 0);
        string mode = QueryHelper.GetString("action", "");
        validator.Url = GetDocumentUrl(nodeId, mode);
        validator.NodeID = nodeId;
    }
}

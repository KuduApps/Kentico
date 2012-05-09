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
using CMS.UIControls;

public partial class CMSModules_DocumentTypes_Pages_Development_DocumentType_Edit_Transformation_List : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int documentTypeId = QueryHelper.GetInteger("documenttypeid", 0);

        // New item link
        string[,] actions = new string[2, 6];
        actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
        actions[0, 1] = GetString("DocumentType_Edit_Transformation_List.btnNew");
        actions[0, 2] = null;
        actions[0, 3] = ResolveUrl("DocumentType_Edit_Transformation_Edit.aspx?documenttypeid=" + documentTypeId.ToString() + "&hash=" + QueryHelper.GetHash("?documenttypeid=" + documentTypeId));
        actions[0, 4] = null;
        actions[0, 5] = GetImageUrl("Objects/CMS_Transformation/add.png");

        actions[1, 0] = HeaderActions.TYPE_HYPERLINK;
        actions[1, 1] = GetString("DocumentType_Edit_Transformation_List.btnHierarchicalNew");
        actions[1, 2] = null;
        actions[1, 3] = ResolveUrl("HierarchicalTransformations_New.aspx?documenttypeid=" + documentTypeId.ToString());
        actions[1, 4] = null;
        actions[1, 5] = GetImageUrl("Objects/CMS_Transformation/hierarchicalTransformation.png");


        this.CurrentMaster.HeaderActions.Actions = actions;

        // Set the query editor control
        this.classTransformations.ClassID = documentTypeId;
        this.classTransformations.EditPageUrl = "DocumentType_Edit_Transformation_Frameset.aspx";
        this.classTransformations.IsSiteManager = true;
    }
}

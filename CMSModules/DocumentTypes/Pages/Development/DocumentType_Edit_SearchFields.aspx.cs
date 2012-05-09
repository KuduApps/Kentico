using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSModules_DocumentTypes_Pages_Development_DocumentType_Edit_SearchFields : SiteManagerPage
{
    int classId = QueryHelper.GetInteger("documenttypeid", 0);

    protected void Page_Load(object sender, EventArgs e)
    {
        DataClassInfo dci = DataClassInfoProvider.GetDataClass(classId);
        if ((dci == null) || (!dci.ClassIsCoupledClass))
        {
            lblError.Text = GetString("srch.doctype.ErrorIsNotCoupled");
            this.SearchFields.StopProcessing = true;
            this.SearchFields.Visible = false;
        }
        else
        {
            this.SearchFields.ItemID = classId;
        }
    }
}

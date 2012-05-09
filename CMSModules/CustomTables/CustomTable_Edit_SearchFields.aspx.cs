using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSModules_CustomTables_CustomTable_Edit_SearchFields : SiteManagerPage
{
    private int classId = QueryHelper.GetInteger("classid", 0);

    protected void Page_Load(object sender, EventArgs e)
    {
        DataClassInfo dci = DataClassInfoProvider.GetDataClass(classId);
        // Set edited object
        EditedObject = dci;

        // Class exists
        if ((dci == null) || (!dci.ClassIsCoupledClass))
        {
            lblError.Text = GetString("customtable.ErrorNoFieldsSearch");
            this.SearchFields.Visible = false;
            this.SearchFields.StopProcessing = true;
        }
        else
        {
            this.SearchFields.ItemID = QueryHelper.GetInteger("classid", 0);
        }
    }
}

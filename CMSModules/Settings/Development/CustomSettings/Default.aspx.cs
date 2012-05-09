using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.ExtendedControls;
using CMS.SiteProvider;
using CMS.SettingsProvider;

public partial class CMSModules_Settings_Development_CustomSettings_Default : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (CultureHelper.IsUICultureRTL())
        {
            ControlsHelper.ReverseFrames(colsFrameset);
        }

        int catId = QueryHelper.GetInteger("categoryid", -1);
        string treeRoot = QueryHelper.GetString("treeroot", "customsettings");

        if (catId <= 0 && treeRoot == "settings")
        {
            DataSet ds = SettingsCategoryInfoProvider.GetSettingsCategories("CategoryParentID = (SELECT CategoryId FROM CMS_SettingsCategory WHERE CategoryName = 'CMS.Settings')", "CategoryOrder", 1, "CategoryIDPath, CategoryName, CategoryID, CategoryParentID");
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                DataRow firstRow = ds.Tables[0].Rows[0];
                catId = SqlHelperClass.GetInteger(firstRow["CategoryID"], 0);
            }
        }


        this.frameTree.Attributes["src"] = "CustomSettings_Menu.aspx?treeroot=" + Request.QueryString["treeroot"] + "&categoryid=" + catId;
        this.frameMain.Attributes["src"] = "CustomSettingsCategory_Default.aspx?treeroot=" + Request.QueryString["treeroot"] + "&categoryid=" + catId + "&siteid=" + Request.QueryString["siteid"];
    }
}


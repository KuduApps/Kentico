using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.SettingsProvider;

public partial class CMSModules_AdminControls_Pages_ObjectRelationships : CMSModalSiteManagerPage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        // Setup the control
        this.relElem.ObjectID = QueryHelper.GetInteger("objectid", 0);
        this.relElem.ObjectType = QueryHelper.GetString("objecttype", "");

        GeneralizedInfo obj = this.relElem.Object;
        if (obj != null)
        {
            // Set the master page title
            this.Title = String.Format(GetString("ObjectRelationships.Title"), GetString("objecttype." + obj.ObjectType.Replace(".", "_").Replace("#", "_")), obj.ObjectDisplayName);

            CurrentMaster.Title.TitleText = this.Title;
            CurrentMaster.Title.TitleImage = GetObjectIconUrl(this.relElem.ObjectType, "/object.png");

            ((Panel)CurrentMaster.PanelBody.FindControl("pnlContent")).CssClass = "";        
        }
    }
}

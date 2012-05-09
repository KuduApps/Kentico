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

using CMS.SiteProvider;
using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSModules_Categories_Pages_Administration_Category_List : CMSCategoriesPage
{
    #region "Page events"

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        // Initialize the controls
        SetupControl();
    }


    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Initializes the controls on the page.
    /// </summary>
    private void SetupControl()
    {
        if (this.SiteID > 0)
        {
            CategoriesElem.DisplaySiteSelector = false;
        }

        CategoriesElem.StartInCreatingMode = QueryHelper.GetBoolean("createNew", false);

        titleElem.TitleText = GetString("Development.Categories");
        titleElem.TitleImage = GetImageUrl("Objects/CMS_Category/object.png");
        titleElem.HelpTopicName = "categories_list";
        titleElem.HelpName = "helpTopic";
    }

    #endregion
}

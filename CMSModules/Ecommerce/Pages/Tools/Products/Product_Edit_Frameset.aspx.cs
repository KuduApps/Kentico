using System;
using System.Text;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.Ecommerce;

public partial class CMSModules_Ecommerce_Pages_Tools_Products_Product_Edit_Frameset : CMSProductsPage
{
    #region "Page methods"

    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);

        int categoryId = QueryHelper.GetInteger("categoryid", 0);

        if (categoryId > 0)
        {
            this.IsProductOption = true;
        }

        // Set different height of the header in product edit and product categories > product edit
        rowsFrameset.Attributes["rows"] = (categoryId > 0) ? TabsBreadFrameHeight + ",*" : TabsBreadHeadFrameHeight + ",*";
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        // Register wopener script
        ScriptHelper.RegisterWOpenerScript(this.Page);
    }

    #endregion
}

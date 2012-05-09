using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.PortalControls;

public partial class CMSModules_OnlineMarketing_Controls_Content_MenuAddWidgetVariant : CMSAbstractPortalUserControl
{
    #region "Page methods"

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        // Use UI culture for strings
        string culture = CMSContext.CurrentUser.PreferredUICultureCode;

        // Add MVT variant
        this.iAddMVTVariant.ImageUrl = GetImageUrl("Design/Controls/VariantSlider/addWidget.png");
        this.iAddMVTVariant.Text = ResHelper.GetString("mvtvariant.new", culture);
        this.iAddMVTVariant.Attributes.Add("onclick", "ContextAddWebPartMVTVariant(GetContextMenuParameter('addWidgetVariantMenu'));");

        // Add Content personalization variant
        this.iAddCPVariant.ImageUrl = GetImageUrl("Design/Controls/VariantSlider/addWidget.png");
        this.iAddCPVariant.Text = ResHelper.GetString("contentpersonalizationvariant.new", culture);
        this.iAddCPVariant.Attributes.Add("onclick", "ContextAddWebPartCPVariant(GetContextMenuParameter('addWidgetVariantMenu'));");

        pnlWebPartMenu.Attributes.Add("onmouseover", "ActivateParentBorder();");
    }

    #endregion
}
